using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Logging;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using Timer = System.Timers.Timer;

namespace NKnife.Tunnel.Filters
{
    public class HeartbeatFilter : BaseTunnelFilter
    {
        private static readonly ILog _logger = LogManager.GetLogger<HeartbeatFilter>();
        private readonly Dictionary<long, HeartBeatSession> _HeartBeartSessionMap = new Dictionary<long, HeartBeatSession>();
        private readonly ManualResetEvent _ReceiveProcessingResetEvent = new ManualResetEvent(false);
        private Timer _BeatingTimer;
        private bool _IsTimerStarted;
        private bool _OnReceiveProcessing;

        public HeartbeatFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000*15;
            EnableStrictMode = false;
            HeartBeatMode = HeartBeatMode.Responsive;
        }

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }

        /// <summary>
        ///     �ϸ�ģʽ����
        /// </summary>
        /// <returns>
        ///     true  ������������һ��Ҫ��HeartBeat���ж����ReplayOfClientһ�²�����������Ӧ
        ///     false ���������κ����ݾ�����������Ӧ
        /// </returns>
        public bool EnableStrictMode { get; set; }

        /// <summary>
        ///     Active����ģʽ��
        ///     ����ģʽ��������ʱ��Զ�˷������������������ʧ�ܣ��յ�����DataConnector��SessionBroken��Ϣ��
        ///     ���߷��ͺ󾭹�ָ��ʱ��û���յ�������Ӧ���򷢳�HeartBroken��Ϣ��Listener�ᷢ��KillSession��Ϣ��
        ///     DataConnector��ɱ����Ӧ��Session�󣬷���SessionBroken��Ϣ
        ///     Passive����ģʽ�������������������󣬵����ӽ����󣬾���ָ��ʱ��û���յ�����Զ�˵���������
        ///     �򷢳�HeartBroken��Ϣ��Listener�ᷢ��KillSession��Ϣ��DataConnector��ɱ����Ӧ��Session��
        ///     ����SessionBroken��Ϣ
        ///     ����������ģʽ���Ǳ���ģʽ������յ��������󣬶������������ظ�
        ///     ResponsiveӦ��ģʽ���������������������κ�ʱ����յ���������󣬽�������Ӧ��
        /// </summary>
        public HeartBeatMode HeartBeatMode { get; set; }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            if (_OnReceiveProcessing)
            {
                _ReceiveProcessingResetEvent.Reset();
                _ReceiveProcessingResetEvent.WaitOne();
            }

            var todoList = new List<long>(0); //���Ƴ�
            foreach (var pair in _HeartBeartSessionMap)
            {
                var endpoint = pair.Key;
                var session = pair.Value;
                if (!session.GetWaitForReply())
                {
                    if (HeartBeatMode == HeartBeatMode.Active) //����ģʽ��Ҫ�����������󣬲�����ȴ�״̬
                    {
#if DEBUG
                        _logger.Trace(string.Format("{0}��{1}������������.", Heartbeat.LocalHeartDescription, session.Id));
#endif
                        session.SetWaitForReply(true); //��PrcoessReceiveData��������յ��ظ�ʱ���дΪfalse
                        ProcessHeartBeatRequestOrReply(session.Id, Heartbeat.RequestToRemote);
                    }
                    else //����ģʽ˵����ǰ�������յ����������󣬽���ȴ�״̬���ٵ�һ������
                    {
                        session.SetWaitForReply(true);
                    }
                }
                else
                {
                    todoList.Add(endpoint);
                }
            }

            foreach (var endPoint in todoList)
            {
                _logger.Info(string.Format("{0}�������{1}����Ӧ����SessionMap���Ƴ�֮������:{2}", Heartbeat.LocalHeartDescription, endPoint, _HeartBeartSessionMap.Count));
                RemoveHeartBeatSessionFromMap(endPoint);
                ProcessHeartBroke(endPoint); //���������ж���Ϣ
            }
        }

        private void RemoveHeartBeatSessionFromMap(long endPoint)
        {
            _HeartBeartSessionMap.Remove(endPoint);
        }

        private HeartBeatSession GetHeartBeatSessionFromMap(long endPoint)
        {
            HeartBeatSession result;
            _HeartBeartSessionMap.TryGetValue(endPoint, out result);
            return result;
        }

        private void StartBeatingTimer()
        {
            if (HeartBeatMode != HeartBeatMode.Responsive)
            {
                if (_BeatingTimer == null)
                {
                    _BeatingTimer = new Timer();
                    _BeatingTimer.Elapsed += BeatingTimerElapsed;
                }

                if (!_IsTimerStarted)
                {
                    _IsTimerStarted = true;
                    _BeatingTimer.Interval = Interval;
                    _BeatingTimer.Start();
                    _logger.Info(string.Format("{0}�������������:{1}", Heartbeat.LocalHeartDescription, Interval));
                }
            }
        }

        private void StopBeatingTimer()
        {
            if (HeartBeatMode != HeartBeatMode.Responsive)
            {
                _BeatingTimer.Stop();
                _IsTimerStarted = false;
            }
        }

        public override bool PrcoessReceiveData(ITunnelSession session)
        {
            _OnReceiveProcessing = true;

            var data = session.Data;
            var heartSession = GetHeartBeatSessionFromMap(session.Id);

            if (heartSession == null)
            {
                //session�ֵ��в�����
                _logger.Warn(string.Format("{0}���Session��{1}�������ֵ��в����ڣ��˳�", Heartbeat.LocalHeartDescription, session.Id));
                _OnReceiveProcessing = false;
                _ReceiveProcessingResetEvent.Set();
                return false;
            }

            if (!EnableStrictMode)
            {
                //���ϸ�ģʽ���յ��κ����ݣ�����Ϊ��������
                heartSession.SetWaitForReply(false);
#if DEBUG
                _logger.TraceFormat("{0}�յ�{1}��Ϣ,�ر������ȴ������ϸ�ģʽ��.", Heartbeat.LocalHeartDescription, session.Id);
#endif
            }

            if (Compare(ref data, Heartbeat.RequestFromRemote)) //�ж��Ƿ��յ���������
            {
                //����ģʽ�£��յ���������������WaitingForReply = false
                //����ģʽ�£��յ�����������ֻ�ǻظ��������Ƿ���WaitingForReply = falseȡ�����Ƿ��ϸ�ģʽ
                if (HeartBeatMode == HeartBeatMode.Passive) //����ģʽ
                {
                    heartSession.SetWaitForReply(false);
                }
                ProcessHeartBeatRequestOrReply(session.Id, Heartbeat.ReplyToRemote);
#if DEBUG
                _logger.TraceFormat("{0}�յ�{1}��������.�ظ����.", Heartbeat.LocalHeartDescription, session.Id);
#endif
                _OnReceiveProcessing = false;
                _ReceiveProcessingResetEvent.Set();
                return false;
            }

            if (Compare(ref data, Heartbeat.ReplyFromRemote)) //�ж��Ƿ��յ�����Ӧ��
            {
                //����ģʽ�£��յ�������Ӧ������WaitingForReply = false
                //����ģʽ�£��յ�������Ӧ�𣨰������յ�����Ϊ����ģʽ�������ᷢ���������󣩣��Ƿ���WaitingForReply = falseȡ�����Ƿ��ϸ�ģʽ
                if (HeartBeatMode == HeartBeatMode.Active) //����ģʽ
                {
                    heartSession.SetWaitForReply(false);
                }
#if DEBUG
                _logger.TraceFormat("{0}�յ�{1}�����ظ�.", Heartbeat.LocalHeartDescription, session.Id);
#endif
                _OnReceiveProcessing = false;
                _ReceiveProcessingResetEvent.Set();
                return false;
            }

            _OnReceiveProcessing = false;
            _ReceiveProcessingResetEvent.Set();
            //�յ������ݼȲ�����������Ҳ��������Ӧ�����Ƿ���WaitingForReply = falseȡ�����Ƿ��ϸ�ģʽ��������������filter
            return true;
        }

        public override void ProcessSessionBroken(long id)
        {
            if (_HeartBeartSessionMap.ContainsKey(id))
            {
                _HeartBeartSessionMap.Remove(id);
            }

            if (_HeartBeartSessionMap.Count == 0)
            {
                //ֹͣ����timer
                StopBeatingTimer();
            }
        }

        public override void ProcessSessionBuilt(long id)
        {
            if (!_HeartBeartSessionMap.ContainsKey(id))
            {
                var session = new HeartBeatSession
                {
                    Id = id
                };
                session.SetWaitForReply(HeartBeatMode == HeartBeatMode.Passive);

                //����ģʽ��WaitingForReply=true,��session�����𣬾Ϳ�ʼ�����յ����������ˣ�����ģʽ����
                _HeartBeartSessionMap.Add(id, session);
            }
            //��һ�μ�����Session����ʱ����
            StartBeatingTimer();
        }

        /// <summary>
        ///     �����жϴ���
        /// </summary>
        /// <returns></returns>
        public void ProcessHeartBroke(long id)
        {
            //����ɱ��Session��ָ�sessionɱ����
            //filter���յ�SessionBroken��Ϣ���յ���Ϣ�󽫶�Ӧ��session�Ƴ�map
            OnKillSession(this, new SessionEventArgs(_HeartBeartSessionMap[id]));
        }

        public void ProcessHeartBeatRequestOrReply(long sessionId, byte[] requestOrReply)
        {
            OnSendToSession(this, new SessionEventArgs(new TunnelSession
            {
                Id = sessionId,
                Data = requestOrReply
            }));
        }

        /// <summary>
        ///     �Ƚ��յ����������Ƿ��д��Ƚϵ�����(һ������������)������յ��������в���������Э��ʱ��ճ��ʱ��,�Ὣ����Э������޳���
        /// </summary>
        /// <param name="data">Դ����</param>
        /// <param name="toCompare">���Ƚϵ�����(һ������������)</param>
        /// <returns>��Trueʱ,�յ����������д��Ƚϵ�����,��֮Flase</returns>
        protected bool Compare(ref byte[] data, byte[] toCompare)
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

        internal class HeartBeatSession : TunnelSession
        {
            /// <summary>
            ///     ����ʱ�ȴ��ظ�
            /// </summary>
            private bool _WaitingForReply;

            public bool GetWaitForReply()
            {
                lock (this)
                {
                    return _WaitingForReply;
                }
            }

            public void SetWaitForReply(bool value)
            {
                _logger.Trace(string.Format("SetWaitForReply = {0}", value));
                lock (this)
                {
                    _WaitingForReply = value;
                }
            }

            protected bool Equals(HeartBeatSession other)
            {
                return Id == other.Id && _WaitingForReply.Equals(other._WaitingForReply);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Id.GetHashCode();
                    hashCode = (hashCode*397);
                    return hashCode;
                }
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((HeartBeatSession) obj);
            }
        }
    }

    public enum HeartBeatMode
    {
        Active, //����ģʽ������������������������������δ�յ�Ӧ�����ж������жϣ��Ƴ�����
        Passive, //����ģʽ�������������������󣬽��յ���������󣬽�������Ӧ��������������δ�յ��������ж������жϣ��Ƴ�����
        Responsive //Ӧ��ģʽ���������������������κ�ʱ����յ���������󣬽�������Ӧ��
    }
}