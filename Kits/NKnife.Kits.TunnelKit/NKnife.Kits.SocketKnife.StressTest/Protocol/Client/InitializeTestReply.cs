using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Client
{
    public class InitializeTestReply:NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x00, 0x01 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes); 
        public InitializeTestReply(byte result,byte[] clientAddress)
            : base(CommandBytes)
        {
            CommandParamList.Add(result);
            CommandParamList.AddRange(clientAddress);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试系统初始化回复";
        }

        public static bool Parse(ref byte[] currentInitializeRepliedSessionAddress, BytesProtocol protocol)
        {
            var commandparam = protocol.CommandParam;
            //commandparam组成如下
            //初始化结果	本机地址
            //1字节	    4字节

            if (commandparam == null || commandparam.Length != 5)
            {
                return false;
            }
            Array.Copy(commandparam,1,currentInitializeRepliedSessionAddress,0,4);
            return true;
        }
    }
}
