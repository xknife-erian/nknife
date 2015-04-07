using System;
using System.Diagnostics;
using System.Text;
using NKnife.Converts;
using NKnife.Protocol;

namespace MonitorKnife.Tunnels.Common
{
    public class CareOneProtocolPacker : IProtocolPacker<byte[]>
    {
        byte[] IProtocolPacker<byte[]>.Combine(IProtocol<byte[]> content)
        {
            var careSaying = content as CareSaying;
            if (careSaying == null)
            {
                Debug.Assert(careSaying == null, "协议不应为Null");
                return new byte[0];
            }
            return Combine(careSaying);
        }

        public virtual byte[] Combine(CareSaying content)
        {
            var p = Encoding.ASCII.GetBytes(content.Content);
            var bs = new byte[5 + p.Length];
            bs[0] = 0x80;
            bs[1] = UtilityConvert.ConvertTo<byte>(content.GpibAddress);
            bs[2] = UtilityConvert.ConvertTo<byte>(2 + p.Length);
            Buffer.BlockCopy(p, 0, bs, 3, p.Length);
            return bs;
        }
    }
}