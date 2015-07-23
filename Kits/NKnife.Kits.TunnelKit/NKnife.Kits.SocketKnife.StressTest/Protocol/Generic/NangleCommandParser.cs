using System.Linq;
using Common.Logging;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    /// <summary>
    /// datagram：命令字（2字节）+数据（不定长）
    /// 命令字（2字节）组成协议中的Command
    /// </summary>
    public class NangleCommandParser : BytesProtocolCommandParser
    {
        private static readonly ILog _logger = LogManager.GetLogger<NangleCommandParser>();

        public override byte[] GetCommand(byte[] datagram)
        {
            if (datagram != null && datagram.Length >= 2)
            {
                return new[] { datagram[0],datagram[1] };
            }
            return new byte[] { };
        }
    }
}
