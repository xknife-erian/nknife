using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol
{
    /// <summary>
    /// 0x00	0x02	用例编号	发送使能	发送目的地址	发送时间间隔（ms）	发送测试数据长度	发送帧数
    /// 1字节	1字节	2字节	1字节	4字节	    2字节	            2字节	        4字节
    /// </summary>
    public class ExecuteTestCase : NangleProtocol
    {
        public ExecuteTestCase(byte[] targetAddress,byte[] testCaseIndex,byte sendEnable,byte[] destAddress, byte[] sendInterval, byte[] dataLength, byte[] data, byte[] sendFrameCount)
            : base(targetAddress,new byte[] {0x00,0x02 })
        {
            CommandParamList.AddRange(testCaseIndex);
            CommandParamList.Add(sendEnable);
            CommandParamList.AddRange(destAddress);
            CommandParamList.AddRange(sendInterval);
            CommandParamList.AddRange(dataLength);
            CommandParamList.AddRange(data);
            CommandParamList.AddRange(sendFrameCount);
            CommandParam = CommandParamList.ToArray();
        }
        public override string ToString()
        {
            return "测试用例执行指令";
        }
    }
}
