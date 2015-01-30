using System;
using System.Net;
using System.Text;
using Common.Logging;
using NKnife.Events;
using NKnife.Tunnel.Events;
using NKnife.Tunnel.Generic;

namespace NKnife.Tunnel.Filters
{
    public class LogFilter : TunnelFilterBase<byte[], EndPoint>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public override bool PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
        {
            _logger.Debug(string.Format("收到数据，来自{0}：{1}",session.Id,Encoding.Default.GetString(session.Data)));
            return true;
        }

        public override void ProcessSessionBroken(EndPoint id)
        {
            _logger.Debug(string.Format("连接断开，来自{0}", id));
        }

        public override void ProcessSessionBuilt(EndPoint id)
        {
            _logger.Debug(string.Format("连接建立,来自{0}", id));
        }

        public override void ProcessSendToSession(ITunnelSession<byte[], EndPoint> session)
        {
            _logger.Debug(string.Format("发送数据，目标{0}：{1}",session.Id,Encoding.Default.GetString(session.Data)));
        }

        public override void ProcessSendToAll(byte[] data)
        {
            _logger.Debug(string.Format("发送数据，目标全体Session：{0}", Encoding.Default.GetString(data)));
        }

        public override event EventHandler<SessionEventArgs<byte[], EndPoint>> OnSendToSession;
        public override event EventHandler<EventArgs<byte[]>> OnSendToAll;
        public override event EventHandler<EventArgs<EndPoint>> OnKillSession;
    }
}
