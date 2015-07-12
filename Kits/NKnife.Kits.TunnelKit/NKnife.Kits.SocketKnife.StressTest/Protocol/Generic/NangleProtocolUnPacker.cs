using System;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    public class NangleProtocolUnPacker:BytesProtocolUnPacker
    {
        /// <summary>
        /// datagram：目的地址（4字节）+命令字（2字节）+数据（不定长）
        /// 目的地址（4字节）+命令字（2字节）组成协议中的Command
        /// 目的地址（4字节）+数据组成协议中的CommandParam
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="data"></param>
        /// <param name="command"></param>
        public override void Execute(BytesProtocol protocol, byte[] data, byte[] command)
        {
            if (data == null)
                return;
            if (data.Length <6)
                return;
            protocol.CommandParam = new byte[data.Length-2];
            Array.Copy(data,0, protocol.CommandParam,0, 4); //先填充目的地址
            if (data.Length > 6)
            {
                Array.Copy(data,6,protocol.CommandParam,4,data.Length-6); //再填充数据
            }
        }
    }
}
