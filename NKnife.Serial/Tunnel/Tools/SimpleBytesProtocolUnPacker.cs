using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol.Generic;

namespace SerialKnife.Tunnel.Tools
{
    public class SimpleBytesProtocolUnPacker : BytesProtocolUnPacker
    {
        public override void Execute(BytesProtocol protocol, byte[] data, byte[] command)
        {
            if (data == null)
                return;
            if (data.Length == 0)
                return;
            protocol.CommandParam = new byte[data.Length];
            Array.Copy(data, protocol.CommandParam, data.Length);
        }
    }
}
