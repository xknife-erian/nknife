using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Client
{
    public class InitializeTestReply:NangleProtocol
    {
        public InitializeTestReply(byte[] targetAddress, byte result)
            : base(targetAddress, new byte[] { 0x00, 0x01 })
        {
            CommandParamList.Add(result);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试系统初始化回复";
        }
    }
}
