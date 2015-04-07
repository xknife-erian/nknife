using NKnife.Protocol.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneProtocolCommandParser : BytesProtocolCommandParser
    {
        public override byte[] GetCommand(byte[] datagram)
        {
            if (datagram[3] == 0xA0)
                return new[] {datagram[3], datagram[4]};
            return new[] {datagram[3]};
        }
    }
}