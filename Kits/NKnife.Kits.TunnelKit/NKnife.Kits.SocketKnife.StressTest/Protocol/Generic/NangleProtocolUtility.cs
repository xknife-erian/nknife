using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Protocol.Generic
{
    public class NangleProtocolUtility
    {
        static NangleProtocolUtility()
        {
            EmptyBytes4 = new byte[]{0x00,0x00,0x00,0x00};
            EmptyBytes2 = new byte[] { 0x00, 0x00 };
            ServerAddress = new byte[] { 0x00, 0x00, 0x00, 0x00 };
        }

        public static byte[] EmptyBytes4 { get; private set; }
        public static byte[] EmptyBytes2 { get; private set; }
        public static byte[] ServerAddress { get; private set; } //主机地址

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

        /// <summary>
        /// 返回用例编号2字节
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static byte[] GetTestCaseIndex(int i)
        {
            return NangleCodecUtility.ConvertFromIntToTwoBytes(i);
        }

        /// <summary>
        /// 返回发送时间间隔（毫秒）2字节
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static byte[] GetSendInterval(int interval)
        {
            return NangleCodecUtility.ConvertFromIntToTwoBytes(interval);
        }

        public enum SendEnable : byte
        {
            Enable = 0x01,
            Disable = 0x00
        }

        /// <summary>
        /// 返回发送测试数据长度，2字节
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetTestDataLength(int length)
        {
            return NangleCodecUtility.ConvertFromIntToTwoBytes(length);
        }

        /// <summary>
        /// 返回发送帧数，4字节
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static byte[] GetFrameCount(int count)
        {
            return NangleCodecUtility.ConvertFromIntToFourBytes(count);
        }
    }
}
