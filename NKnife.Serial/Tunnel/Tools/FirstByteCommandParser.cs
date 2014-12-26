using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol.Generic;

namespace SerialKnife.Tunnel.Tools
{
    public class FirstByteCommandParser : BytesProtocolCommandParser
    {
        public override byte[] GetCommand(byte[] datagram)
        {
            if (datagram != null && datagram.Count() > 0)
            {
                return new[] {datagram[0]};
            }
            return new byte[] {};
        }
    }
}
