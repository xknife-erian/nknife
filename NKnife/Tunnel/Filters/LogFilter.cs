using System.Net;
using System.Text;
using Common.Logging;

namespace NKnife.Tunnel.Filters
{
    public class LogFilter : ITunnelFilter<byte[], EndPoint>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        public IFilterListener<byte[], EndPoint> Listener { get; set; }
        public bool ContinueNextFilter { get { return true; } }
        public void PrcoessReceiveData(ITunnelSession<byte[], EndPoint> session)
        {
            _logger.Debug(string.Format("收到数据，来自{0}：{1}",session.Id,Encoding.Default.GetString(session.Data)));
        }

        public void ProcessSessionBroken(EndPoint id)
        {
            _logger.Debug(string.Format("连接断开，来自{0}", id));
        }

        public void ProcessSessionBuilt(EndPoint id)
        {
            _logger.Debug(string.Format("连接建立,来自{0}", id));
        }
    }
}
