using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    public class NangleProtocolUtility
    {
        public static string GetProtocolDescription(BytesProtocol protocol)
        {
            string result = "";
            var command = protocol.Command;
            if (command.Length == 2 && command[0] == 0x00) //测试相关协议
            {
                switch (command[1])
                {
                    case 0x00: //测试系统初始化指令 InitializeTest
                        result = "测试系统初始化指令";
                        break;
                    case 0x01:
                        result = "测试系统初始化回复";
                        break;
                    case 0x02:
                        result = "测试用例执行指令";
                        break;
                    case 0x03:
                        result = "测试用例执行回复";
                        break;
                    case 0x04:
                        result = "测试用例停止指令";
                        break;
                    case 0x05:
                        result = "测试用例停止回复";
                        break;
                    case 0x06:
                        result = "测试用例执行结果读取指令";
                        break;
                    case 0x07:
                        result = "测试用例执行结果汇报";
                        break;
                    case 0x08:
                        result = "测试数据";
                        break;
                }
            }
            else
            {
                result = "未知";
            }
            return result;
        }
    }
}
