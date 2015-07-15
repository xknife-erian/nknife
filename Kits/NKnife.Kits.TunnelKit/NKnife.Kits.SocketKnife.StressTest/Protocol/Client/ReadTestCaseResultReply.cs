using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Client
{
    public class ReadTestCaseResultReply:NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x00, 0x07 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes); 
        public ReadTestCaseResultReply(byte[] targetAddress, byte[] testCaseIndex,byte[] sendFrameCount,byte[] receiveFrameCount,byte[] receiveFrameLostCount,byte[] receiveFrameErrorCount)
            : base(targetAddress, CommandBytes)
        {
            CommandParamList.AddRange(testCaseIndex);
            CommandParamList.AddRange(sendFrameCount);
            CommandParamList.AddRange(receiveFrameCount);
            CommandParamList.AddRange(receiveFrameLostCount);
            CommandParamList.AddRange(receiveFrameErrorCount);

            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试用例执行结果汇报";
        }
    }
}
