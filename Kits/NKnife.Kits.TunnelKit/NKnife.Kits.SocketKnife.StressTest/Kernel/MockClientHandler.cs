using System;
using System.Collections.Generic;
using System.Threading;
using Common.Logging;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Kernel
{
    public class MockClientHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<MockClientHandler>();
        public override List<byte[]> Commands { get; set; }

        public EventHandler<NangleProtocolEventArgs> ProtocolReceived;

        private void OnProtocolReceived(IProtocol<byte[]> protocol)
        {
            var handler = ProtocolReceived;
            if (handler != null)
            {
                handler.Invoke(this,new NangleProtocolEventArgs
                {
                    Protocol = (BytesProtocol)protocol,
                });
            }
        }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            byte[] command = protocol.Command;
            byte[] message = _Family.Generate(protocol);
            //_logger.Debug(string.Format("client[收到]<==[{0}], {1}", command.ToHexString(), message.ToHexString()));
            OnProtocolReceived(protocol);
        }
    }
}
