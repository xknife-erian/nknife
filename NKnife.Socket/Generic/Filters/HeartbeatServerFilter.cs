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
    public class HeartbeatServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;

        public HeartbeatServerFilter()
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

        protected internal override void OnClientCome(SocketSessionEventArgs e)
        {
            base.OnClientCome(e);
            Start();
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
                        handlers[0].Write(session, Heartbeat.BeatingOfServerHeart);
                        session.WaitingForReply = true; //��PrcoessReceiveData��������յ��ظ�ʱ���дΪfalse
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
            if (!_IsTimerStarted) //��һ�μ�����ʱ����
            {
                _IsTimerStarted = true;

                var beatingTimer = new Timer();
                beatingTimer.Elapsed += BeatingTimerElapsed;
                beatingTimer.Interval = Interval;
                beatingTimer.Start();
                _logger.Info(string.Format("�������������������:{0}", Interval));
                var handlers = _HandlersGetter.Invoke();
                Debug.Assert(handlers != null && handlers.Count > 0, "Handlerδ����");
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!IsStrictMode)
            {//���ϸ�ģʽ
                session.WaitingForReply = false;
                _logger.Trace(() => string.Format("Server�յ�{0}��Ϣ,�ر������ȴ�.", session.Source));
            }
            if (data.IndexOf(Heartbeat.BeatingOfClientHeart) == 0)
            {
                _HandlersGetter.Invoke()[0].Write(session, Heartbeat.ReplayOfServer);
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("Server�յ�{0}����.�ظ����.", session.Source));
                return;
            }
            if (data.IndexOf(Heartbeat.ReplayOfClient) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("Server�յ�{0}�����ظ�.", session.Source));
            }
            else
            {
                _ContinueNextFilter = true;
            }
        }

    }
}