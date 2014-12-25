using System;
using Common.Logging;
using NKnife.Tunnel;
using NKnife.Tunnel.Generic;

namespace SerialKnife.Tunnel.Filters
{
    public class SerialLogFilter:TunnelFilterBase<byte[],int>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        public SerialLogFilter()
        {
            ContinueNextFilter = true;
        }

        public override void BindSessionHandler(ISessionProvider<byte[], int> sessionProvider)
        {
            //什么都不做，也不需要
        }

        public override void PrcoessReceiveData(ITunnelSession<byte[], int> session)
        {
            _logger.Debug(string.Format("收到数据，来自{0}：{1}", session.Id, session.Data.ToHexString()));
        }

        public override void ProcessSessionBroken(int id)
        {
            _logger.Debug(string.Format("连接断开，来自{0}", id));
        }

        public override void ProcessSessionBuilt(int id)
        {
            _logger.Debug(string.Format("连接建立,来自{0}", id));
        }

        public override void PrcoessSendData(ITunnelSession<byte[], int> session)
        {
            _logger.Debug(string.Format("发送数据，目标{0}：{1}", session.Id, session.Data.ToHexString()));
        }
    }
}
