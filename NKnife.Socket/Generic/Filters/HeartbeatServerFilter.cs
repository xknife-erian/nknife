using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using NKnife.Adapters;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Generic.Families;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        private const string WAIT = "WAIT";
        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected internal override void OnClientCome(SocketSessionEventArgs e)
        {
            base.OnClientCome(e);
            if (!_IsTimerStarted) //第一次监听到时启动
            {
                _IsTimerStarted = true;

                var beatingTimer = new Timer();
                beatingTimer.Elapsed += BeatingTimer_Elapsed;
                beatingTimer.Interval = Interval;
                beatingTimer.Start();
                _logger.Info(string.Format("服务器心跳启动。间隔:{0}", Interval));
            }
        }

        private void BeatingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            KnifeSocketProtocolHandler handler = _HandlerGetter.Invoke();
            KnifeSocketSessionMap map = _SessionMapGetter.Invoke();

            var list = new List<EndPoint>(0);
            foreach (KeyValuePair<EndPoint, ISocketSession> pair in map)
            {
                EndPoint endpoint = pair.Key;
                ISocketSession session = pair.Value;
                if (!((bool)session.GetAttribute(WAIT))) //两种情况：1.第一次检查时为非等待状态，2.心跳后收到回复后回写为非等待状态
                {
                    handler.Write(session, Heartbeat.BeatingOfServerHeart);
                    session.SetAttribute(WAIT, true); //在PrcoessReceiveData方法里，当收到回复时会回写为false
                }
                else
                {
                    list.Add(endpoint);
                }
            }
            foreach (EndPoint endPoint in list)
            {
                KnifeSocketSession session;
                if (map.TryGetValue(endPoint, out session))
                {
                    try
                    {
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
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (data.IndexOf(Heartbeat.ReplayOfClient) == 0)
            {
                session.SetAttribute(WAIT, false);
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("收到{0}心跳回复.", session.Source));
            }
        }

    }
}