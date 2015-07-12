using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol
{
    public class StopExecuteTestCase:NangleProtocol
    {
        public StopExecuteTestCase(byte[] targetAddress, byte[] testCaseIndex)
            : base(targetAddress, new byte[] { 0x00, 0x04 })
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
