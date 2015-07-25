using System;
using System.Net.Configuration;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Kits.SocketKnife.StressTest.Kernel;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Server
{
    /// <summary>
    /// 0x00	0x02	用例编号	发送使能	发送目的地址	发送时间间隔（ms）	发送测试数据长度	发送帧数
    /// 1字节	1字节	2字节	1字节	4字节	    2字节	            2字节	        4字节
    /// </summary>
    public class ExecuteTestCase : NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x01, 0x00 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes); 
        public ExecuteTestCase(byte[] testCaseIndex,byte sendEnable,byte[] destAddress, byte[] sendInterval, byte[] dataLength, byte[] sendFrameCount)
            : base(CommandBytes)
        {
            CommandParamList.AddRange(testCaseIndex);
            CommandParamList.Add(sendEnable);
            CommandParamList.AddRange(destAddress);
            CommandParamList.AddRange(sendInterval);
            CommandParamList.AddRange(dataLength);
            CommandParamList.AddRange(sendFrameCount);
            CommandParam = CommandParamList.ToArray();
        }
        public override string ToString()
        {
            return "测试用例执行指令";
        }

        /// 0x00	0x02	用例编号	发送使能	发送目的地址	发送时间间隔（ms）	发送测试数据长度	发送帧数
        /// 1字节	1字节	2字节	1字节	4字节	    2字节	            2字节	        4字节
        public static bool Parse(ref ExecuteTestCaseParam param, BytesProtocol protocol)
        {
            var commandparam = protocol.CommandParam;
            //commandparam组成如下
            //初始化结果	本机地址
            //1字节	    4字节

            if (commandparam == null || commandparam.Length != 15)
            {
                return false;
            }

            var testCaseIndexBytes = new byte[]{0x00,0x00};
            Array.Copy(commandparam, 0, testCaseIndexBytes, 0, 2);
            param.TestCaseIndex = NangleCodecUtility.ConvertFromTwoBytesToInt(testCaseIndexBytes);

            param.SendEnable = commandparam[2] == 0x01;

            Array.Copy(commandparam, 3, param.TargetAddress, 0, 4);

            var sendIntervalBytes = new byte[] {0x00, 0x00};
            Array.Copy(commandparam,7,sendIntervalBytes,0,2);
            param.SendInterval = NangleCodecUtility.ConvertFromTwoBytesToInt(sendIntervalBytes);

            var frameDataLengthBytes = new byte[] {0x00, 0x00};
            Array.Copy(commandparam,9,frameDataLengthBytes,0,2);
            param.FrameDataLength = NangleCodecUtility.ConvertFromTwoBytesToInt(frameDataLengthBytes);

            var frameCountBytes = new byte[] {0x00, 0x00, 0x00, 0x00};
            Array.Copy(commandparam,11,frameCountBytes,0,4);
            param.FrameCount = NangleCodecUtility.ConvertFromFourBytesToInt(frameCountBytes);

            return true;
        }
    }
}
