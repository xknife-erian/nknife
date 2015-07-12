using System;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    public class NangleProtocolPacker:BytesProtocolPacker
    {
        /// <summary>
        /// datagram：目的地址（4字节）+命令字（2字节）+数据（不定长）
        /// 命令字（2字节）组成协议中的Command
        /// 目的地址（4字节）+数据组成协议中的CommandParam
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public override byte[] Combine(BytesProtocol content)
        {
            int len = content.Command.Length;
            if (content.CommandParam != null)
            {
                len += content.CommandParam.Length;
            }
            var result = new byte[len];
            if (content.CommandParam != null) //先填充目的地址（4字节）
            {
                Array.Copy(content.CommandParam, 0, result, 0, 4);
            }
            Array.Copy(content.Command, 0, result, 4, content.Command.Length); //再填充命令字（2字节）
            if (content.CommandParam != null && content.CommandParam.Length>4) //最后填充数据
            {
                Array.Copy(content.CommandParam, 4, result, content.Command.Length + 4, content.CommandParam.Length-4);
            }
            return result;
        }
    }
}
