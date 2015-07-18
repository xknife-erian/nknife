using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Server
{
    public class SetSpeechMode : NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x02, 0x00 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes);

        /// <summary>
        /// 语音模式：
        /// 值	意义
        /// 0x00	IDLE模式，喇叭麦克关闭
        /// 0x01	对讲模式，喇叭麦克开启
        /// 0x02	播放模式，喇叭开启麦克关闭
        /// 0x03	采集模式，喇叭关闭麦克打开

        /// </summary>
        /// <param name="speechMode"></param>
        /// <param name="targetAddress"></param>
        public SetSpeechMode(byte speechMode, byte[] targetAddress)
            : base(CommandBytes)
        {
            CommandParamList.Add(speechMode);
            CommandParamList.AddRange(targetAddress);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "设置语音模式";
        }
    }
}
