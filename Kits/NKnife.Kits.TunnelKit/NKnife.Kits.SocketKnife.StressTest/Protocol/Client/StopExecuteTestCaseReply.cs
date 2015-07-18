using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Client
{
    public class StopExecuteTestCaseReply:NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x00, 0x05 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes); 
        public StopExecuteTestCaseReply(byte result)
            : base(CommandBytes)
        {
            CommandParamList.Add(result);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试用例停止回复";
        }
    }
}
