using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using NKnife.Adapters;
using NKnife.Interface;
using SocketKnife.Common;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();
        private bool _IsTimerStarted;

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }

        protected internal override void OnListenToClient(SocketAsyncEventArgs e)
        {
            base.OnListenToClient(e);
            if (!_IsTimerStarted)
            {
                _IsTimerStarted = true;

                var beatingTimer = new Timer();
                beatingTimer.Elapsed += BeatingTimer_Elapsed;
                beatingTimer.Interval = Interval;
                beatingTimer.Start();
                _logger.Info(string.Format("服务器心跳启动。{0}", Interval));
            }
        }

        private void BeatingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IProtocolHandler handler = _HandlerGetter.Invoke();
            ISocketSessionMap map = _SessionMapGetter.Invoke();

            var list = new List<EndPoint>(0);
            foreach (var pair in map)
            {
                EndPoint endpoint = pair.Key;
                ISocketSession session = pair.Value;
                if (!session.WaitHeartBeatingReplay) //两种情况：1.第一次检查时为非等待状态，2.心跳后收到回复后回写为非等待状态
                {
                    handler.Write(session, Heartbeat.BeatingOfServerHeart);
                    session.WaitHeartBeatingReplay = true; //在PrcoessReceiveData方法里，当收到回复时会回写为false
                }
                else
                {
                    list.Add(endpoint);
                }
            }
            foreach (EndPoint endPoint in list)
            {
                ISocketSession session;
                if (map.TryGetValue(endPoint, out session))
                {
                    try
                    {
                        session.Socket.Close();
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

        public override void PrcoessReceiveData(ISocketSession socket, byte[] data)
        {
        }
    }
}