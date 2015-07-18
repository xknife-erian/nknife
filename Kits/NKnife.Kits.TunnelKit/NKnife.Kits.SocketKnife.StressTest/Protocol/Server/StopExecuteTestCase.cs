using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Server
{
    public class StopExecuteTestCase:NangleProtocol
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public static byte[] CommandBytes = { 0x00, 0x04 };
        /// <summary>
        /// 根据2字节的command命令字计算出的整数，用于switch条件判断等流程
        /// </summary>
        public static int CommandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(CommandBytes); 

        public StopExecuteTestCase(byte[] testCaseIndex)
            : base(CommandBytes)
        {
            CommandParamList.AddRange(testCaseIndex);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试用例停止指令";
        }
    }
}
