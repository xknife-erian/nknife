using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol
{
    public class TestRawData:NangleProtocol
    {
        public TestRawData(byte[] targetAddress, byte index, byte[] data)
            : base(targetAddress, new byte[] { 0x00, 0x08 })
        {
            CommandParamList.Add(index);
            CommandParamList.AddRange(data);
            CommandParam = CommandParamList.ToArray();
        }

        public override string ToString()
        {
            return "测试数据";
        }
    }
}
