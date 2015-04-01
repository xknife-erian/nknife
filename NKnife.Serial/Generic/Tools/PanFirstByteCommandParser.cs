using System.Linq;
using NKnife.Protocol.Generic;

namespace SerialKnife.Generic.Tools
{
    public class PanFirstByteCommandParser : BytesProtocolCommandParser
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
