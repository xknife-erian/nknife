using System;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Server
{
    public class InitializeConnection : NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = {0x00, 0x00};
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes); 

        public InitializeConnection(byte[] serverAddress)
            : base(CommandBytes)
        {
            CommandParamList.AddRange(serverAddress);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "连接初始化指令";
        }
    }
}
