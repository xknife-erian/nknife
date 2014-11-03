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

            //��ʼ��Timer
            _BeatingTimer = new Timer();
            _BeatingTimer.Elapsed += BeatingTimerElapsed;
        }

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
        /// ����ģʽ
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
            //ֹͣ����timer
            _BeatingTimer.Stop();
            _IsTimerStarted = false;
        }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            IList<KnifeSocketProtocolHandler> handlers = _HandlersGetter.Invoke();
            KnifeSocketSessionMap map = SessionMapGetter.Invoke();

            var todoList = new List<EndPoint>(0);//���Ƴ�
            foreach (KeyValuePair<EndPoint, KnifeSocketSession> pair in map)
            {
                EndPoint endpoint = pair.Key;
                KnifeSocketSession session = pair.Value;
                if (!(session.WaitingForReply)) //���������1.��һ�μ��ʱΪ�ǵȴ�״̬��2.�������յ��ظ����дΪ�ǵȴ�״̬
                {
                    try
                    {
                        handlers[0].Write(session, Heartbeat.RequestOfHeartBeat);
                        session.WaitingForReply = true; //��PrcoessReceiveData��������յ��ظ�ʱ���дΪfalse
#if DEBUG
                        _logger.Trace(() => string.Format("Server����{0}����.", session.Source));
#endif
                    }
                    catch (Exception ex) //�����쳣��������ȥ�������Ƴ�
                    {
                        _logger.Warn(string.Format("��ͻ���{0}��������ʱ�쳣:{1}", endpoint, ex.Message), ex);
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
                    _logger.Warn(string.Format("�Ͽ��ͻ���{0}ʱ�쳣:{1}", endPoint, ex.Message), ex);
                }
                map.Remove(endPoint);
                _logger.Info(string.Format("�������ͻ���{0}����Ӧ����SessionMap���Ƴ�֮������:{1}", endPoint, map.Count));
            }
        }

        protected internal virtual void Start()
        {
            if (EnableAggressiveMode && !_IsTimerStarted) //��һ�μ�����ʱ����
            {
                _IsTimerStarted = true;
                _BeatingTimer.Interval = Interval;
                _BeatingTimer.Start();
                _logger.Info(string.Format("�������������������:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Count > 0, "Handlerδ����");
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!EnableStrictMode)
            {//���ϸ�ģʽ���յ��κ����ݣ�����Ϊ��������
                session.WaitingForReply = false;
#if DEBUG
                _logger.Trace(() => string.Format("Server�յ�{0}��Ϣ,�ر������ȴ������ϸ�ģʽ��.", session.Source));
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
                    _logger.Trace(() => string.Format("Server�յ�{0}����.�ظ����.", session.Source));
#endif
                }
                catch (SocketException ex)
                {
                    _logger.Trace(() => string.Format("Server�յ�{0}����.�ظ�ʱsocket�쳣.", session.Source));
                    RemoveEndPointFromSessionMap(session.Source);
                }
                return;
            }

            if (data.IndexOf(Heartbeat.ReplyOfHeartBeat) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
#if DEBUG
                _logger.Trace(() => string.Format("Server�յ�{0}�����ظ�.", session.Source));
#endif
            }
            else
            {
                _ContinueNextFilter = true;
            }
        }

    }
}