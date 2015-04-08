using System;
using System.Collections.Generic;
using Common.Logging;
using MonitorKnife.Tunnels.Common;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SerialKnife.Consoles.Demos
{
    public class SerialProtocolHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialProtocolHandler>();
        public override List<byte[]> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            if (!(protocol is CareSaying))
                _logger.Warn("Protocol¿‡–Õ”–ŒÛ");
            var saying = (CareSaying) protocol;
            _logger.Info("Recevied:" + saying.Content);
        }
    }
}