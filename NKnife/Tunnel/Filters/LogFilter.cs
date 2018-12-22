using System;
using System.Text;
using Common.Logging;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel.Filters
{
    public class LogFilter : BaseTunnelFilter
    {
        private static readonly ILog _Logger = LogManager.GetLogger<LogFilter>();

        public override bool PrcoessReceiveData(ITunnelSession session)
        {
            _Logger.Debug(string.Format("收到数据，来自{0}：{1}", session.Id, Encoding.Default.GetString(session.Data)));
            return true;
        }

        public override void ProcessSessionBroken(long id)
        {
            _Logger.Debug(string.Format("连接断开，来自{0}", id));
        }

        public override void ProcessSessionBuilt(long id)
        {
            _Logger.Debug(string.Format("连接建立,来自{0}", id));
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
            _Logger.Debug(string.Format("发送数据，目标{0}：{1}", session.Id, Encoding.Default.GetString(session.Data)));
        }

        public override void ProcessSendToAll(byte[] data)
        {
            _Logger.Debug(string.Format("发送数据，目标全体Session：{0}", Encoding.Default.GetString(data)));
        }
    }
}