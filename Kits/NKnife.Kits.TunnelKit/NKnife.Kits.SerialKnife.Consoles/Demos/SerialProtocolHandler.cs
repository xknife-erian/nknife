using System;
using System.Collections.Generic;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace NKnife.Kits.SerialKnife.Consoles.Demos
{
    public class SerialProtocolHandler : BaseProtocolHandler<byte[]>
    {
        public override List<byte[]> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            throw new NotImplementedException();
        }
    }
}