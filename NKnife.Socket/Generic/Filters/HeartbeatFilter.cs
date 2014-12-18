using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using Common.Logging;
using NKnife.Interface;
using NKnife.Tunnel;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatFilter : ITunnelFilter<byte[], EndPoint>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        IFilterListener<byte[], EndPoint> ITunnelFilter<byte[], EndPoint>.Listener
        {
            get { return Listener; }
            set { Listener = (HeartBeatListener)value; }
        }

        public HeartBeatListener Listener { get; set; }


        public bool ContinueNextFilter { get; private set; }

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }

        /// <summary>
        /// 严格模式开关
        /// </summary>
        /// <returns>
        /// true  心跳返回内容一定要和HeartBeat类中定义的ReplayOfClient一致才算有心跳响应
        /// false 心跳返回任何内容均算有心跳相应
        /// </returns>
        public bool EnableStrictMode { get; set; }

        /// <summary>
        /// true 主动模式，false 被动模式
        /// 主动模式：主动定时向远端发出心跳请求，如果发送失败（收到来自DataConnector的SessionBroken消息）
        /// 或者发送后经过指定时间没有收到心跳响应，则发出HeartBroken消息，Listener会发出KillSession消息，
        /// DataConnector会杀死对应的Session后，发出SessionBroken消息
        /// 
        /// 被动模式：不主动发出心跳请求，但连接建立后，经过指定时间没有收到来自远端的心跳请求，
        /// 则发出HeartBroken消息，Listener会发出KillSession消息，DataConnector会杀死对应的Session后，
        /// 发出SessionBroken消息
        /// 
        /// 不论是主动模式还是被动模式，如果收到心跳请求，都会做出心跳回复
        /// </summary>
        public bool EnableAggressiveMode { get; set; }

        private bool _IsTimerStarted;
        private readonly Timer _BeatingTimer;

        private readonly ConcurrentDictionary<EndPoint, HeartBeatSession> _HeartBeartSessionMap = new ConcurrentDictionary<EndPoint, HeartBeatSession>();

        public HeartbeatFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000 * 15;
            EnableStrictMode = false;
            EnableAggressiveMode = true;

            //初始化Timer
            _BeatingTimer = new Timer();
            _BeatingTimer.Elapsed += BeatingTimerElapsed;

            Listener = new HeartBeatListener();
        }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            var todoList = new List<EndPoint>(0);//待移除
            foreach (KeyValuePair<EndPoint, HeartBeatSession> pair in _HeartBeartSessionMap)
            {
                EndPoint endpoint = pair.Key;
                HeartBeatSession session = pair.Value;
                if (!session.WaitingForReply) 
                {
                    if (EnableAggressiveMode) //主动模式需要发出心跳请求，并进入等待状态
                    {
                        Listener.ProcessHeartReply(session.SessionId, Heartbeat.RequestOfHeartBeat);
                        session.WaitingForReply = true; //在PrcoessReceiveData方法里，当收到回复时会回写为false
#if DEBUG
                        _logger.Trace(string.Format("Server发出{0}心跳.", session.SessionId));
#endif
                    }
                    else //被动模式说明当前周期内收到了心跳请求，进入等待状态，再等一个周期
                    {
                        session.WaitingForReply = true;
                    }
                }
                else
                {
                    _logger.Info(string.Format("心跳检查客户端{0}无响应，从SessionMap中移除之。池中:{1}", endpoint, _HeartBeartSessionMap.Count));
                    todoList.Add(endpoint);
                }
            }

            foreach (EndPoint endPoint in todoList)
            {
                RemoveHeartBeatSessionFromMap(endPoint);
                Listener.ProcessHeartBroke(endPoint); //发出心跳中断消息
            }
        }

        private void RemoveHeartBeatSessionFromMap(EndPoint endPoint)
        {
            HeartBeatSession session;
            _HeartBeartSessionMap.TryRemove(endPoint,out session);
        }

        private HeartBeatSession GetHeartBeatSessionFromMap(EndPoint endPoint)
        {
            HeartBeatSession result = null;
            _HeartBeartSessionMap.TryGetValue(endPoint, out result);
            return result;
        }

        protected internal virtual void Start()
        {
            if (!_IsTimerStarted) 
            {
                _IsTimerStarted = true;
                _BeatingTimer.Interval = Interval;
                _BeatingTimer.Start();
                _logger.Info(string.Format("服务器心跳启动。间隔:{0}", Interval));
            }
        }

        public void PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
        {
            var data = session.Data;
            var heartSession = GetHeartBeatSessionFromMap(session.Id);

            if (heartSession == null)
            {
                //session字典中不存在
                _logger.Warn(string.Format("Session：{0}在心跳字典中不存在，直接跳过", session.Id));
                ContinueNextFilter = true; 
                return;
            }

            if (!EnableStrictMode)
            {
                //非严格模式，收到任何数据，均认为心跳正常
                heartSession.WaitingForReply = false;
#if DEBUG
                _logger.TraceFormat("Server收到{0}信息,关闭心跳等待（非严格模式）.", session.Id);
#endif
            }

            if (Compare(ref data, Heartbeat.RequestOfHeartBeat)) //收到心跳请求
            {

                ContinueNextFilter = false;
                //被动模式下，收到了心跳请求，则标记WaitingForReply = false
                //主动模式下，收到心跳请求，则只是回复心跳，是否标记WaitingForReply = false取决于是否严格模式
                if (!EnableAggressiveMode) //被动模式
                {
                    heartSession.WaitingForReply = false;
                }
                Listener.ProcessHeartReply(session.Id, Heartbeat.ReplyOfHeartBeat);
#if DEBUG
                _logger.TraceFormat("Server收到{0}心跳请求.回复完成.", session.Id);
#endif
 
                return;
            }

            if (Compare(ref data, Heartbeat.ReplyOfHeartBeat)) //收到心跳应答
            {
                ContinueNextFilter = false;
                //主动模式下，收到了心跳应答，则标记WaitingForReply = false
                //被动模式下，收到了心跳应答（按理不会收到，因为被动模式根本不会发出心跳请求），是否标记WaitingForReply = false取决于是否严格模式
                if (EnableAggressiveMode) //主动模式
                {
                    heartSession.WaitingForReply = false;
                }
#if DEBUG
                _logger.TraceFormat("Server收到{0}心跳回复.", session.Id);
#endif
                return;
            }

            //收到的内容既不是心跳请求也不是心跳应答，则是否标记WaitingForReply = false取决于是否严格模式，处理交给后续的filter
            ContinueNextFilter = true;
        }

        public void ProcessSessionBroken(EndPoint id)
        {
            if (_HeartBeartSessionMap.ContainsKey(id))
            {
                HeartBeatSession session;
                _HeartBeartSessionMap.TryRemove(id, out session);
            }

            if (_HeartBeartSessionMap.Count == 0)
            {
                //停止心跳timer
                _BeatingTimer.Stop();
                _IsTimerStarted = false;
            }
            Listener.OnSessionBroken(id);
        }

        public void ProcessSessionBuilt(EndPoint id)
        {
            if (!_HeartBeartSessionMap.ContainsKey(id))
            {
                var session = new HeartBeatSession();
                session.SessionId = id;
                //被动模式则WaitingForReply=true,从session建立起，就开始等着收到心跳请求了，主动模式不用
                session.WaitingForReply = !EnableAggressiveMode; 

                _HeartBeartSessionMap.TryAdd(id, session);
            }

            //第一次监听到Session建立时启动
            Start();
            Listener.OnSessionBuilt(id);
        }

        /// <summary>
        /// 比较收到的数据中是否有待比较的数据(一般是心跳数据)。如果收到的数据中不光是心跳协议时（粘包时）,会将心跳协议进行剔除。
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="toCompare">待比较的数据(一般是心跳数据)</param>
        /// <returns>当True时,收到的数据中有待比较的数据,反之Flase</returns>
        private bool Compare(ref byte[] data, byte[] toCompare)
        {
            var srcLength = data.Length;
            var index = data.Find(toCompare);
            if (index < 0)
                return false;
            if (toCompare.Length < data.Length) //当源数据中包含待比较数据以外的数据时，将待比较数据移除
            {
                var tmpData = data.ToArray();
                data = new byte[data.Length - toCompare.Length];
                Buffer.BlockCopy(tmpData, 0, data, 0, index);
                Buffer.BlockCopy(tmpData, index + toCompare.Length, data, index, srcLength - index - toCompare.Length);
            }
            return true;
        }


        public class HeartBeatListener : IFilterListener<byte[], EndPoint>
        {
            private ISessionProvider<byte[], EndPoint> _SessionProvider;
            public void OnSessionBroken(EndPoint id)
            {
                //不用做什么
            }

            public void OnSessionBuilt(EndPoint id)
            {
                //不用做什么
            }

            public void BindSessionHandler(ISessionProvider<byte[], EndPoint> sessionProvider)
            {
                _SessionProvider = sessionProvider;
            }

            /// <summary>
            /// 心跳中断处理
            /// </summary>
            /// <returns></returns>
            public void ProcessHeartBroke(EndPoint id)
            {
                //发出杀死Session的指令，session杀死后，
                //filter会收到SessionBroken消息，收到消息后将对应的session移出map
                _SessionProvider.KillSession(id);
            }

            public void ProcessHeartReply(EndPoint sessionId, byte[] requestOfHeartBeat)
            {
                _SessionProvider.Send(sessionId,requestOfHeartBeat);
            }
        }

        internal class HeartBeatSession
        {
            public EndPoint SessionId { get; set; }
            /// <summary>
            /// 心跳时等待回复
            /// </summary>
            public bool WaitingForReply { get; set; }

            protected bool Equals(HeartBeatSession other)
            {
                return SessionId == other.SessionId && WaitingForReply.Equals(other.WaitingForReply);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = SessionId.GetHashCode();
                    hashCode = (hashCode * 397) ^ WaitingForReply.GetHashCode();
                    return hashCode;
                }
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((KnifeSocketSession)obj);
            }
        }
    }
}