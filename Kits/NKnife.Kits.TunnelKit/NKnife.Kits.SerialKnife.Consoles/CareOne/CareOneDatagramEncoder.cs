using NKnife.Tunnel.Generic;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneDatagramEncoder : BytesDatagramEncoder
    {
        public override byte[] Execute(byte[] saying)
        {
            return saying;
        }
    }
}