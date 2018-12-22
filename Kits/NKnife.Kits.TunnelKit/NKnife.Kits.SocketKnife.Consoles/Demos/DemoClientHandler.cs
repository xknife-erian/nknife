using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SocketKnife.Consoles.Demos
{
    public class Demo1ClientHandler : BaseProtocolHandler<string>
    {
        private static readonly ILog _Logger = LogManager.GetLogger<Demo1ClientHandler>();

        public override List<string> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<string> protocol)
        {
            string command = protocol.Command;
            string message = _Family.Generate(protocol);
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            _Logger.Info(string.Format("<== {0},{1},{2}", time, command, message));
        }
    }
}