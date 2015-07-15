using System;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Server
{
    public class InitializeTest : NangleProtocol
    {
        public InitializeTest(byte[] targetAddress,byte[] serverAddress)
            : base(targetAddress, new byte[] { 0x00, 0x00 })
        {
            CommandParamList.AddRange(serverAddress);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试系统初始化指令";
        }
    }
}
