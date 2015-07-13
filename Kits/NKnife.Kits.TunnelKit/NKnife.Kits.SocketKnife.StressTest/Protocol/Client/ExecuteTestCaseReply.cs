using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Client
{
    public class ExecuteTestCaseReply:NangleProtocol
    {
        public ExecuteTestCaseReply(byte[] targetAddress, byte result)
            : base(targetAddress, new byte[] { 0x00, 0x03 })
        {
            CommandParamList.Add(result);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试用例执行回复";
        }
    }
}
