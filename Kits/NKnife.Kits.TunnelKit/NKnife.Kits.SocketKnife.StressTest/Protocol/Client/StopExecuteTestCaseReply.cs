using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Client
{
    public class StopExecuteTestCaseReply:NangleProtocol
    {
        public StopExecuteTestCaseReply(byte[] targetAddress, byte result)
            : base(targetAddress, new byte[] { 0x00, 0x05 })
        {
            CommandParamList.Add(result);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试用例停止回复";
        }
    }
}
