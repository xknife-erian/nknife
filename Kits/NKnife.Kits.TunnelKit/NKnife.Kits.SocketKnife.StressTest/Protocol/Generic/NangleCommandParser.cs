using System.Linq;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    /// <summary>
    /// datagram：命令字（2字节）+数据（不定长）
    /// 命令字（2字节）组成协议中的Command
    /// </summary>
    public class NangleCommandParser : BytesProtocolCommandParser
    {
        public override byte[] GetCommand(byte[] datagram)
        {
            if (datagram != null && datagram.Count() >= 2)
            {
                return new[] { datagram[0],datagram[1] };
            }
            return new byte[] { };
        }
    }
}
