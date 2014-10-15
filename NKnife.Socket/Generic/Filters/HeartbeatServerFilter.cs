using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Utility;
using SocketKnife.Common;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatServerFilter : KnifeSocketServerFilter
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();
        private bool _IsTimerStarted = false;

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
            var handler = _HandlerGetter.Invoke();
            var map = _SessionMapGetter.Invoke();

            var list = new List<EndPoint>(0);
            foreach (var pair in map)
            {
                var endpoint = pair.Key;
                var session = pair.Value;
                if (!session.WaitHeartBeatingReplay)//第一次检查时，和心跳后收到回复后回写为非等待状态
                {
                    handler.Write(session, Heartbeat.BeatingOfServerHeart);
                    session.WaitHeartBeatingReplay = true;
                }
                else
                {
                    list.Add(endpoint);
                }
            }
            foreach (var endPoint in list)
            {
                map.Remove(endPoint);
                _logger.Info(string.Format("心跳检查客户端{0}无响应，移除之。池中:{1}", endPoint, map.Count));
            }
        }

        public override void PrcoessReceiveData(Socket socket, byte[] data)
        {
        }
    }
}