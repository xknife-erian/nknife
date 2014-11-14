using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using Common.Logging;
using NKnife.Base;
using NKnife.Events;
using NKnife.Interface;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatClientFilter : KnifeSocketClientFilter, IHeartbeatFilter
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        protected Timer _BeatingTimer;
        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;

        public HeartbeatClientFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000 * 15;
            EnableStrictMode = false;
            EnableAggressiveMode = true;

            _BeatingTimer = new Timer();
            _BeatingTimer.Elapsed += BeatingTimerElapsed;
        }

        public Heartbeat Heartbeat { get; set; }//����Э��
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
                    handlers[0].Write(session, Heartbeat.RequestOfHeartBeat);
                    session.WaitingForReply = true; //��PrcoessReceiveData��������յ��ظ�ʱ���дΪfalse
#if DEBUG
                    _logger.TraceFormat("Client����{0}����.", session.Source);
#endif
                }
                catch (SocketException ex) //�����쳣��������ȥ�������Ƴ���ͬʱ׼������
                {
                    _logger.Warn(string.Format("��Server{0}��������ʱ�쳣:{1}", session.Source, ex.Message), ex);
                    _logger.Info("׼������Server......");
                    Reconnects(session);
                }
            }
            else
            {
                _logger.Warn(string.Format("Filter�ڼ��ʱ��({0})�ڣ�Server����δ��Ӧ�����¼�OnConnectionBroken֪ͨClientʵ��......", Interval));
                _logger.Info("׼������Server......");
                Reconnects(session);
            }
        }

        protected virtual void Reconnects(KnifeSocketSession session)
        {
            OnConnectionBroken(new ConnectionBrokenEventArgs(session.Source, BrokenCause.LoseHeartbeat));
        }

        protected internal override void OnConnectionBroken(ConnectionBrokenEventArgs e)
        {
            base.OnConnectionBroken(e);
            _IsTimerStarted = false;
            if (_BeatingTimer != null)
                _BeatingTimer.Stop(); //�ر��������ȴ����������Ϻ��ٴ�������
        }

        protected internal virtual void Start()
        {
            if (EnableAggressiveMode && !_IsTimerStarted) //��һ�μ�����ʱ����
            {
                _IsTimerStarted = true;
                _BeatingTimer.Interval = Interval;
                _BeatingTimer.Start();
                _logger.Info(string.Format("Client�������������:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Count > 0, "Handlerδ����");
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, ref byte[] data)
        {
            int sourceLength = data.Length;
            if (!EnableStrictMode)
            {
                //���ϸ�ģʽʱ���յ���Ϣ����Ϊ������Ϊ�״̬�����ر������ȴ�
                session.WaitingForReply = false;
#if DEBUG
                _logger.TraceFormat("Client�յ�{0}��Ϣ,�ر������ȴ�.", session.Source);
#endif
            }
            if (Compare(ref data, Heartbeat.RequestOfHeartBeat)) //�յ����Է�������������Ϣ
            {
                try
                {
                    _ContinueNextFilter = data.Length < sourceLength;
                    _HandlersGetter.Invoke()[0].Write(session, Heartbeat.ReplyOfHeartBeat);
#if DEBUG
                    _logger.TraceFormat("Client�յ�{0}��������.�ظ����.", session.Source);
#endif
                }
                catch (SocketException ex)
                {
                    _logger.WarnFormat("Client�յ�{0}��������.�ظ�ʱ����Socket�쳣��{1}.", session.Source, ex.Message);
                    _logger.Info("׼������Server......");
                    Reconnects(session);
                }
                return;
            }
            if (Compare(ref data, Heartbeat.ReplyOfHeartBeat)) //�յ����Է������������ظ���Ϣ
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = data.Length < sourceLength;
#if DEBUG
                _logger.TraceFormat("Client�յ�{0}�����ظ�.", session.Source);
#endif
                return;
            }
            _ContinueNextFilter = true;
        }



    }
}