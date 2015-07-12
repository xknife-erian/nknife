using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol
{
    public class ReadTestCaseResult:NangleProtocol
    {
        public ReadTestCaseResult(byte[] targetAddress, byte[] testCaseIndex)
            : base(targetAddress, new byte[] { 0x00, 0x06 })
        {
            CommandParamList.AddRange(testCaseIndex);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试用例执行结果读取指令";
        }
    }
}
