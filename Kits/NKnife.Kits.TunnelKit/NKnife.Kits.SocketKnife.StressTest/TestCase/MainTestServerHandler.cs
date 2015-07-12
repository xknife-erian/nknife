using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class MainTestServerHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<MainTestServerHandler>();

        public override List<byte[]> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            byte[] command = protocol.Command;
            byte[] message = _Family.Generate(protocol);
            _logger.Debug(string.Format("server[收到{0}] <== {1},{2}", sessionId, command.ToHexString(), message.ToHexString()));

            WriteToSession(sessionId,protocol);
            _logger.Debug(string.Format("server[发出{0}] <== {1},{2}", sessionId, command.ToHexString(), message.ToHexString()));
        }

        
    }
}
