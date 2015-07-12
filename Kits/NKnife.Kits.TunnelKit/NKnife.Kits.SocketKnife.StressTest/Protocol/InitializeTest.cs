using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol
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
