using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Timers;
using Common.Logging;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Generic;

namespace NKnife.Tunnel.Filters
{
    public class HeartbeatFilter : TunnelFilterBase<byte[], EndPoint>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private ISessionProvider<byte[], EndPoint> _SessionProvider;

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }

        /// <summary>
        /// �ϸ�ģʽ����
        /// </summary>
        /// <returns>
        /// true  ������������һ��Ҫ��HeartBeat���ж����ReplayOfClientһ�²�����������Ӧ
        /// false ���������κ����ݾ�����������Ӧ
        /// </returns>
        public bool EnableStrictMode { get; set; }

        /// <summary>
        /// true ����ģʽ��false ����ģʽ
        /// ����ģʽ��������ʱ��Զ�˷������������������ʧ�ܣ��յ�����DataConnector��SessionBroken��Ϣ��
        /// ���߷��ͺ󾭹�ָ��ʱ��û���յ�������Ӧ���򷢳�HeartBroken��Ϣ��Listener�ᷢ��KillSession��Ϣ��
        /// DataConnector��ɱ����Ӧ��Session�󣬷���SessionBroken��Ϣ
        /// 
        /// ����ģʽ�������������������󣬵����ӽ����󣬾���ָ��ʱ��û���յ�����Զ�˵���������
        /// �򷢳�HeartBroken��Ϣ��Listener�ᷢ��KillSession��Ϣ��DataConnector��ɱ����Ӧ��Session��
        /// ����SessionBroken��Ϣ
        /// 
        /// ����������ģʽ���Ǳ���ģʽ������յ��������󣬶������������ظ�
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

            //��ʼ��Timer
            _BeatingTimer = new Timer();
            _BeatingTimer.Elapsed += BeatingTimerElapsed;
        }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            var todoList = new List<EndPoint>(0);//���Ƴ�
            foreach (KeyValuePair<EndPoint, HeartBeatSession> pair in _HeartBeartSessionMap)
            {
                EndPoint endpoint = pair.Key;
                HeartBeatSession session = pair.Value;
                if (!session.WaitingForReply) 
                {
                    if (EnableAggressiveMode) //����ģʽ��Ҫ�����������󣬲�����ȴ�״̬
                    {
                        ProcessHeartBeatRequestOrReply(session.SessionId, Heartbeat.RequestOfHeartBeat);
                        session.WaitingForReply = true; //��PrcoessReceiveData��������յ��ظ�ʱ���дΪfalse
#if DEBUG
                        _logger.Trace(string.Format("Server����{0}����.", session.SessionId));
#endif
                    }
                    else //����ģʽ˵����ǰ�������յ����������󣬽���ȴ�״̬���ٵ�һ������
                    {
                        session.WaitingForReply = true;
                    }
                }
                else
                {
                    
                    todoList.Add(endpoint);
                }
            }

            foreach (EndPoint endPoint in todoList)
            {
                _logger.Info(string.Format("�������ͻ���{0}����Ӧ����SessionMap���Ƴ�֮������:{1}", endPoint, _HeartBeartSessionMap.Count));
                RemoveHeartBeatSessionFromMap(endPoint);
                ProcessHeartBroke(endPoint); //���������ж���Ϣ
            }
        }

        private void RemoveHeartBeatSessionFromMap(EndPoint endPoint)
        {
            HeartBeatSession session;
            _HeartBeartSessionMap.TryRemove(endPoint,out session);
        }

        private HeartBeatSession GetHeartBeatSessionFromMap(EndPoint endPoint)
        {
            HeartBeatSession result;
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
                _logger.Info(string.Format("�������������������:{0}", Interval));
            }
        }

        public override void BindSessionProvider(ISessionProvider<byte[], EndPoint> sessionProvider)
        {
            _SessionProvider = sessionProvider;
        }

        public override void PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
        {
            var data = session.Data;
            var heartSession = GetHeartBeatSessionFromMap(session.Id);

            if (heartSession == null)
            {
                //session�ֵ��в�����
                _logger.Warn(string.Format("Session��{0}�������ֵ��в����ڣ�ֱ������", session.Id));
                ContinueNextFilter = true; 
                return;
            }

            if (!EnableStrictMode)
            {
                //���ϸ�ģʽ���յ��κ����ݣ�����Ϊ��������
                heartSession.WaitingForReply = false;
#if DEBUG
                _logger.TraceFormat("Server�յ�{0}��Ϣ,�ر������ȴ������ϸ�ģʽ��.", session.Id);
#endif
            }

            if (Compare(ref data, Heartbeat.RequestOfHeartBeat)) //�յ���������
            {

                ContinueNextFilter = false;
                //����ģʽ�£��յ���������������WaitingForReply = false
                //����ģʽ�£��յ�����������ֻ�ǻظ��������Ƿ���WaitingForReply = falseȡ�����Ƿ��ϸ�ģʽ
                if (!EnableAggressiveMode) //����ģʽ
                {
                    heartSession.WaitingForReply = false;
                }
                ProcessHeartBeatRequestOrReply(session.Id, Heartbeat.ReplyOfHeartBeat);
#if DEBUG
                _logger.TraceFormat("Server�յ�{0}��������.�ظ����.", session.Id);
#endif
 
                return;
            }

            if (Compare(ref data, Heartbeat.ReplyOfHeartBeat)) //�յ�����Ӧ��
            {
                ContinueNextFilter = false;
                //����ģʽ�£��յ�������Ӧ������WaitingForReply = false
                //����ģʽ�£��յ�������Ӧ�𣨰��������յ�����Ϊ����ģʽ�������ᷢ���������󣩣��Ƿ���WaitingForReply = falseȡ�����Ƿ��ϸ�ģʽ
                if (EnableAggressiveMode) //����ģʽ
                {
                    heartSession.WaitingForReply = false;
                }
#if DEBUG
                _logger.TraceFormat("Server�յ�{0}�����ظ�.", session.Id);
#endif
                return;
            }

            //�յ������ݼȲ�����������Ҳ��������Ӧ�����Ƿ���WaitingForReply = falseȡ�����Ƿ��ϸ�ģʽ����������������filter
            ContinueNextFilter = true;
        }

        public override void ProcessSessionBroken(EndPoint id)
        {
            if (_HeartBeartSessionMap.ContainsKey(id))
            {
                HeartBeatSession session;
                _HeartBeartSessionMap.TryRemove(id, out session);
            }

            if (_HeartBeartSessionMap.Count == 0)
            {
                //ֹͣ����timer
                _BeatingTimer.Stop();
                _IsTimerStarted = false;
            }
        }

        public override void ProcessSessionBuilt(EndPoint id)
        {
            if (!_HeartBeartSessionMap.ContainsKey(id))
            {
                var session = new HeartBeatSession
                {
                    SessionId = id, 
                    WaitingForReply = !EnableAggressiveMode
                };
                //����ģʽ��WaitingForReply=true,��session�����𣬾Ϳ�ʼ�����յ����������ˣ�����ģʽ����

                _HeartBeartSessionMap.TryAdd(id, session);
            }

            //��һ�μ�����Session����ʱ����
            Start();
        }

        /// <summary>
        /// �����жϴ���
        /// </summary>
        /// <returns></returns>
        public void ProcessHeartBroke(EndPoint id)
        {
            //����ɱ��Session��ָ�sessionɱ����
            //filter���յ�SessionBroken��Ϣ���յ���Ϣ�󽫶�Ӧ��session�Ƴ�map
            _SessionProvider.KillSession(id);
        }

        public void ProcessHeartBeatRequestOrReply(EndPoint sessionId, byte[] requestOrReply)
        {
            _SessionProvider.Send(sessionId, requestOrReply);
        }

        /// <summary>
        /// �Ƚ��յ����������Ƿ��д��Ƚϵ�����(һ������������)������յ��������в���������Э��ʱ��ճ��ʱ��,�Ὣ����Э������޳���
        /// </summary>
        /// <param name="data">Դ����</param>
        /// <param name="toCompare">���Ƚϵ�����(һ������������)</param>
        /// <returns>��Trueʱ,�յ����������д��Ƚϵ�����,��֮Flase</returns>
        private bool Compare(ref byte[] data, byte[] toCompare)
        {
            var srcLength = data.Length;
            var index = data.Find(toCompare);
            if (index < 0)
                return false;
            if (toCompare.Length < data.Length) //��Դ�����а������Ƚ��������������ʱ�������Ƚ������Ƴ�
            {
                var tmpData = data.ToArray();
                data = new byte[data.Length - toCompare.Length];
                Buffer.BlockCopy(tmpData, 0, data, 0, index);
                Buffer.BlockCopy(tmpData, index + toCompare.Length, data, index, srcLength - index - toCompare.Length);
            }
            return true;
        }

        internal class HeartBeatSession
        {
            public EndPoint SessionId { get; set; }
            /// <summary>
            /// ����ʱ�ȴ��ظ�
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
                return Equals((HeartBeatSession)obj);
            }
        }
    }
}