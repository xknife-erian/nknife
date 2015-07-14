using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class MainTestServerHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<MainTestServerHandler>();

        public override List<byte[]> Commands { get; set; }

        public EventHandler<NangleProtocolEventArgs> ProtocolReceived;

        private void OnProtocolReceived(long sessionId, IProtocol<byte[]> protocol)
        {
            var handler = ProtocolReceived;
            if (handler != null)
            {
                handler.Invoke(this, new NangleProtocolEventArgs
                {
                    SessionId = sessionId,
                    Protocol = (BytesProtocol)protocol,
                });
            }
        }
        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            //byte[] command = protocol.Command;
            //byte[] message = _Family.Generate(protocol);
            //_logger.Debug(string.Format("server[收到{0}]<==[{1}], {2}", sessionId, command.ToHexString(), message.ToHexString()));

            //针对不同的协议有不同的处理逻辑

            OnProtocolReceived(sessionId,protocol);
        }

        
    }
}
