using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using NKnife.Adapters;
using NKnife.Interface;
using SocketKnife.Common;
using SocketKnife.Events;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;
        private readonly Timer _BeatingTimer;

        public HeartbeatServerFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000 * 15;
            EnableStrictMode = false;
            EnableAggressiveMode = true;

            //初始化Timer
            _BeatingTimer = new Timer();
            _BeatingTimer.Elapsed += BeatingTimerElapsed;
        }

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
        /// 主动模式
        /// </summary>
        public bool EnableAggressiveMode { get; set; }

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected internal override void OnClientCome(SocketSessionEventArgs e)
        {
            base.OnClientCome(e);
            Start();
        }

        protected internal override void OnClientBroken(ConnectionBrokenEventArgs e)
        {
            base.OnClientBroken(e);
            //停止心跳timer
            _BeatingTimer.Stop();
            _IsTimerStarted = false;
        }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            IList<KnifeSocketProtocolHandler> handlers = _HandlersGetter.Invoke();
            KnifeSocketSessionMap map = SessionMapGetter.Invoke();

            var todoList = new List<EndPoint>(0);//待移除
            foreach (KeyValuePair<EndPoint, KnifeSocketSession> pair in map)
            {
                EndPoint endpoint = pair.Key;
                KnifeSocketSession session = pair.Value;
                if (!(session.WaitingForReply)) //两种情况：1.第一次检查时为非等待状态，2.心跳后收到回复后回写为非等待状态
                {
                    try
                    {
                        handlers[0].Write(session, Heartbeat.RequestOfHeartBeat);
                        session.WaitingForReply = true; //在PrcoessReceiveData方法里，当收到回复时会回写为false
#if DEBUG
                        _logger.Trace(() => string.Format("Server发出{0}心跳.", session.Source));
#endif
                    }
                    catch (Exception ex) //发送异常，发不出去则立即移出
                    {
                        _logger.Warn(string.Format("向客户端{0}发送心跳时异常:{1}", endpoint, ex.Message), ex);
                        RemoveEndPointFromSessionMap(endpoint);
                    }
                }
                else
                {
                    todoList.Add(endpoint);
                }
            }
            foreach (EndPoint endPoint in todoList)
            {
                RemoveEndPointFromSessionMap(endPoint);
            }
        }

        protected virtual void RemoveEndPointFromSessionMap(EndPoint endPoint)
        {
            KnifeSocketSessionMap map = SessionMapGetter.Invoke();
            KnifeSocketSession session;
            if (map.TryGetValue(endPoint, out session))
            {
                try
                {
                    if (session.Connector != null && session.Connector.Connected)
                        session.Connector.Close();
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("断开客户端{0}时异常:{1}", endPoint, ex.Message), ex);
                }
                map.Remove(endPoint);
                _logger.Info(string.Format("心跳检查客户端{0}无响应，从SessionMap中移除之。池中:{1}", endPoint, map.Count));
            }
        }

        protected internal virtual void Start()
        {
            if (EnableAggressiveMode && !_IsTimerStarted) //第一次监听到时启动
            {
                _IsTimerStarted = true;
                _BeatingTimer.Interval = Interval;
                _BeatingTimer.Start();
                _logger.Info(string.Format("服务器心跳启动。间隔:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Count > 0, "Handler未设置");
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!EnableStrictMode)
            {//非严格模式，收到任何数据，均认为心跳正常
                session.WaitingForReply = false;
#if DEBUG
                _logger.Trace(() => string.Format("Server收到{0}信息,关闭心跳等待（非严格模式）.", session.Source));
#endif
                return;
            }

            if (data.IndexOf(Heartbeat.RequestOfHeartBeat) == 0)
            {
                try
                {
                    _ContinueNextFilter = false;
                    _HandlersGetter.Invoke()[0].Write(session, Heartbeat.ReplyOfHeartBeat);
#if DEBUG
                    _logger.Trace(() => string.Format("Server收到{0}心跳.回复完成.", session.Source));
#endif
                }
                catch (SocketException ex)
                {
                    _logger.Trace(() => string.Format("Server收到{0}心跳.回复时socket异常.", session.Source));
                    RemoveEndPointFromSessionMap(session.Source);
                }
                return;
            }

            if (data.IndexOf(Heartbeat.ReplyOfHeartBeat) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
#if DEBUG
                _logger.Trace(() => string.Format("Server收到{0}心跳回复.", session.Source));
#endif
            }
            else
            {
                _ContinueNextFilter = true;
            }
        }

    }
}