using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Timers;
using NKnife.Adapters;
using NKnife.Interface;
using SocketKnife.Common;
using SocketKnife.Events;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatClientFilter : KnifeSocketClientFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected Timer _BeatingTimer;
        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;

        public HeartbeatClientFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000 * 15;
            IsStrictMode = false;
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
        public bool IsStrictMode { get; set; }

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected internal override void OnConnected(ConnectedEventArgs e)
        {
            base.OnConnected(e);
            Start();
        }

        protected virtual void BeatingTimerElapsed(object sender, EventArgs e)
        {
            IList<KnifeSocketProtocolHandler> handlers = _HandlersGetter.Invoke();
            KnifeSocketSession session = SessionGetter.Invoke();

            if (!(session.WaitingForReply)) //���������1.��һ�μ��ʱΪ�ǵȴ�״̬��2.�������յ��ظ����дΪ�ǵȴ�״̬
            {
                try
                {
                    handlers[0].Write(session, Heartbeat.BeatingOfClientHeart);
                    session.WaitingForReply = true; //��PrcoessReceiveData��������յ��ظ�ʱ���дΪfalse
                }
                catch (Exception ex) //�����쳣��������ȥ�������Ƴ�
                {
                    _logger.Warn(string.Format("�������{0}��������ʱ�쳣:{1}", session.Source, ex.Message), ex);
                    _logger.Info("׼������������......");
                    Reconnects(session);
                }
            }
            else
            {
                _IsTimerStarted = false;
                _BeatingTimer.Stop();//�ر��������ȴ����������Ϻ��ٴ�������

                _logger.Warn("��һ��ʱ���ڣ�������δ��Ӧ��׼������������......");
                Reconnects(session);
            }
        }

        protected virtual void Reconnects(KnifeSocketSession session)
        {
            OnConnectionBroken(new ConnectionBrokenEventArgs(session.Source, BrokenCause.LoseHeartbeat));
        }

        protected internal virtual void Start()
        {
            if (!_IsTimerStarted) //��һ�μ�����ʱ����
            {
                _IsTimerStarted = true;

                _BeatingTimer = new Timer();
                _BeatingTimer.Elapsed += BeatingTimerElapsed;
                _BeatingTimer.Interval = Interval;
                _BeatingTimer.Start();
                _logger.Info(string.Format("�ͻ����������������:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Count > 0, "Handlerδ����");
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!IsStrictMode)
            {   //���ϸ�ģʽ
                session.WaitingForReply = false;
                _logger.Trace(() => string.Format("�ͻ����յ�{0}��Ϣ,�ر������ȴ�.", session.Source));
            }
            if (data.IndexOf(Heartbeat.BeatingOfServerHeart) == 0)
            {
                _HandlersGetter.Invoke()[0].Write(session, Heartbeat.ReplayOfClient);
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("�ͻ����յ�{0}����.�ظ����.", session.Source));
                return;
            } 
            if (data.IndexOf(Heartbeat.ReplayOfServer) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("�ͻ����յ�{0}�����ظ�.", session.Source));
            }
            else
            {
                _ContinueNextFilter = true;
            }
        }

    }
}