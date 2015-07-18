using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Client
{
    public class SetSpeechModeReply : NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x02, 0x01 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes);

        /// <summary>
        ///
        /// </summary>
        /// <param name="setResult"></param>
        public SetSpeechModeReply(byte setResult)
            : base(CommandBytes)
        {
            CommandParamList.Add(setResult);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "设置语音模式回应";
        }
    }
}
