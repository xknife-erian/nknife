using System;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    public class NangleProtocolUnPacker:BytesProtocolUnPacker
    {
        /// <summary>
        /// datagram：命令字（2字节）+数据（不定长）
        /// 命令字（2字节）组成协议中的Command
        /// 数据组成协议中的CommandParam
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="data"></param>
        /// <param name="command"></param>
        public override void Execute(BytesProtocol protocol, byte[] data, byte[] command)
        {
            if (data == null)
                return;
            if (data.Length <2)
                return;
            protocol.CommandParam = new byte[data.Length-2];
            if (data.Length > 2)
            {
                Array.Copy(data,2,protocol.CommandParam,0,data.Length-2); //再填充数据
            }
        }
    }
}
