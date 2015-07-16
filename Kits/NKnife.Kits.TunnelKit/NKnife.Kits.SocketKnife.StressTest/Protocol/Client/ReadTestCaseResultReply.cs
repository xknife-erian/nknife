using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.Protocol.Generic;

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

        public static bool Parse(ref TestCaseResult result,BytesProtocol protocol)
        {
            var commandparam = protocol.CommandParam;
            //commandparam组成如下
            //目标地址 用例编号	发送帧数	接收帧数	接收丢失帧数	接收错误帧数
            //4字节    2字节	    4字节	4字节	4字节	    4字节
            if (commandparam == null || commandparam.Length != 22)
            {
                return false;
            }
            result.TestCaseIndex =
                NangleCodecUtility.ConvertFromTwoBytesToInt(new[] {commandparam[4], commandparam[5]});
            result.FrameSent =
                NangleCodecUtility.ConvertFromFourBytesToInt(new[]
                {commandparam[6], commandparam[7], commandparam[8], commandparam[9]});
            result.FrameReceived = 
                NangleCodecUtility.ConvertFromFourBytesToInt(new[] 
                { commandparam[10], commandparam[11], commandparam[12], commandparam[13] });
            result.FrameLost =
                NangleCodecUtility.ConvertFromFourBytesToInt(new[] 
                { commandparam[14], commandparam[15], commandparam[16], commandparam[17] });
            result.FrameError =
                NangleCodecUtility.ConvertFromFourBytesToInt(new[] 
                { commandparam[18], commandparam[19], commandparam[20], commandparam[21] });
            return true;
        }
    }
}
