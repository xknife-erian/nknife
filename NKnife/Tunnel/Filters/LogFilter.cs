using System.Net;
using System.Text;
using Common.Logging;
using NKnife.Tunnel.Generic;

namespace NKnife.Tunnel.Filters
{
    public class LogFilter : TunnelFilterBase<byte[], EndPoint>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public LogFilter()
        {
            ContinueNextFilter = true;
        }

        public override void BindSessionHandler(ISessionProvider<byte[], EndPoint> sessionProvider)
        {
            //什么都不做，也不需要
        }

        public override void PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
        {
            _logger.Debug(string.Format("收到数据，来自{0}：{1}",session.Id,Encoding.Default.GetString(session.Data)));
        }

        public override void ProcessSessionBroken(EndPoint id)
        {
            _logger.Debug(string.Format("连接断开，来自{0}", id));
        }

        public override void ProcessSessionBuilt(EndPoint id)
        {
            _logger.Debug(string.Format("连接建立,来自{0}", id));
        }

        public override void PrcoessSendData(ITunnelSession<byte[], EndPoint> session)
        {
            _logger.Debug(string.Format("发送数据，目标{0}：{1}",session.Id,Encoding.Default.GetString(session.Data)));
        }
    }
}
