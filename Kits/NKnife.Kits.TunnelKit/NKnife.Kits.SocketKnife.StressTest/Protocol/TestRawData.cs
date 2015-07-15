using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol
{
    public class TestRawData:NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x00, 0x08 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes); 
        public TestRawData(byte[] targetAddress, byte index, byte[] data)
            : base(targetAddress, CommandBytes)
        {
            CommandParamList.Add(index);
            CommandParamList.AddRange(data);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试数据";
        }
    }
}
