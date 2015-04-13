using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        private static string _hex;

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            if (!(protocol is CareSaying))
                _logger.Warn("ProtocolÀàÐÍÓÐÎó");
            var saying = (CareSaying) protocol;
            var hex = protocol.Command.ToHexString();
            if (_hex == hex)
            {
                _hex = hex;
                _logger.Fatal(string.Format("{0},Recevied:{1}", hex, saying.Content));
            }
            else
            {
                _hex = hex;
                Console.Write(">");
                //_logger.Info(string.Format("{0},Recevied:{1}", hex, saying.Content));
            }
        }
    }
}