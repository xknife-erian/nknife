using System;
using Common.Logging;
using NKnife.Events;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using NKnife.Tunnel.Generic;

namespace SerialKnife.Tunnel.Filters
{
    public class SerialLogFilter : TunnelFilterBase<byte[], int>
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialLogFilter>();

        public override bool PrcoessReceiveData(ITunnelSession<byte[], int> session)
        {
            _logger.Debug(string.Format("收到数据，来自{0}：{1}", session.Id, session.Data.ToHexString()));
            return true;
        }

        public override void ProcessSessionBroken(int id)
        {
            _logger.Debug(string.Format("连接断开，来自{0}", id));
        }

        public override void ProcessSessionBuilt(int id)
        {
            _logger.Debug(string.Format("连接建立,来自{0}", id));
        }

        public override void ProcessSendToSession(ITunnelSession<byte[], int> session)
        {
            _logger.Debug(string.Format("发送数据，目标{0}：{1}", session.Id, session.Data.ToHexString()));
        }

        public override event EventHandler<SessionEventArgs<byte[], int>> OnSendToSession;
        public override event EventHandler<EventArgs<byte[]>> OnSendToAll;
        public override event EventHandler<EventArgs<int>> OnKillSession;
    }
}