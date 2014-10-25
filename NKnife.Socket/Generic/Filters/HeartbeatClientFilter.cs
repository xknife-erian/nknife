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

        protected internal override void OnConnectioned(ConnectionedEventArgs e)
        {
            base.OnConnectioned(e);
            Start();
        }

        protected void BeatingTimerElapsed(object sender, EventArgs e)
        {
            KnifeSocketProtocolHandler[] handlers = _HandlersGetter.Invoke();
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
                    _logger.Warn(string.Format("��ͻ���{0}��������ʱ�쳣:{1}", session.Source, ex.Message), ex);
                }
            }
        }

        protected byte[] GetReplay()
        {
            return Heartbeat.ReplayOfServer;
        }

        protected internal void Start()
        {
            if (!_IsTimerStarted) //��һ�μ�����ʱ����
            {
                _IsTimerStarted = true;

                var beatingTimer = new Timer();
                beatingTimer.Elapsed += BeatingTimerElapsed;
                beatingTimer.Interval = Interval;
                beatingTimer.Start();
                _logger.Info(string.Format("�������������������:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Length > 0, "Handlerδ����");
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!IsStrictMode)
            {//���ϸ�ģʽ
                session.WaitingForReply = false;
                _logger.Trace(() => string.Format("�յ�{0}��Ϣ,�ر������ȴ�.", session.Source));
            }
            if (data.IndexOf(GetReplay()) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("�յ�{0}�����ظ�.", session.Source));
            }
            else
            {
                _ContinueNextFilter = true;
            }
        }

    }
}