using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class MainTestServerHandler : BaseProtocolHandler<string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<MainTestServerHandler>();

        public override List<string> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<string> protocol)
        {
            string command = protocol.Command;
            string message = _Family.Generate(protocol);
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            _logger.Debug(string.Format("server[收到{0}] <== {1},{2},{3}", sessionId, time, command, message));

            time = DateTime.Now.ToString("HH:mm:ss.fff");
            WriteToSession(sessionId,protocol);
            _logger.Debug(string.Format("server[发出{0}] <== {1},{2},{3}", sessionId, time, command, message));
        }
    }
}
