using System;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    public class NangleProtocolPacker:BytesProtocolPacker
    {
        /// <summary>
        /// datagram：命令字（2字节）+数据（不定长）
        /// 命令字（2字节）组成协议中的Command
        /// 数据组成协议中的CommandParam
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
            Array.Copy(content.Command, 0, result, 0, content.Command.Length); //先命令字（2字节）
            if (content.CommandParam != null && content.CommandParam.Length>0) //再填充数据
            {
                Array.Copy(content.CommandParam, 0, result, content.Command.Length, content.CommandParam.Length);
            }
            return result;
        }
    }
}
