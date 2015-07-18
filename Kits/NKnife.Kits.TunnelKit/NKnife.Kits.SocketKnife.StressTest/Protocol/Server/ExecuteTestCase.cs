using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;

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
        public static byte[] CommandBytes = { 0x00, 0x02 };
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
    }
}
