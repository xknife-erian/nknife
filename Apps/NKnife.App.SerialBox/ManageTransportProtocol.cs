using System;
using System.Collections.Generic;
using System.Text;

namespace NKnife.SerialBox
{
    public class ManageTransportProtocol
    {
        public const int SEND_TASK_SUCCESS = 1;
        public const int SEND_TASK_SERIAL_PORT_ERROR = 2;
        public const int SEND_TASK_RETRY_OVER_ERROR = 4;
        public const int SEND_TASK_ABOERT_ERROR = 8;
        public const int SINGLE_SEND = 1;
        public const int AUTO_SEND = 2;
        public const int FILE_SEND = 3;

        public TransportProtocol SendTransportProtocol { get; set; }

        public TransportProtocol RevTransportProtocol { get; set; }

        public byte[] SendTransportProtocolBuf { get; set; } = new byte[0x200];

        public byte[] RevTransportProtocolBuf { get; set; } = new byte[0x200];

        public byte OriginalFrameSequence { get; set; }

        public int SendFileOneFrameDataLength { get; set; }

        public bool IsReadSendFileOneFrameData { get; set; }

        public bool IsStopSendFile { get; set; }

        public string SendFileName { get; set; }

        public int SendTaskResult { get; set; }

        public int CurrentSendTaskType { get; set; }

        public bool IsHasSendTask { get; set; }

        public bool IsEnableTransportProtocol { get; set; }

        public byte RecieveSequence { get; set; }

        public byte SendSequence { get; set; }

        public bool IsAbortSend { get; set; }

        public bool IsCompleteRetry { get; set; }

        public int RetryCount { get; set; }

        public bool IsRevTransportProtocolSuccess { get; set; }

        public bool IsSendTransportProtocolSuccess { get; set; }

        public bool IsAutoNewLine { get; set; }

        public bool IsDispOriginalData { get; set; }

        public TransportProtocol ConvertByteToTransportProtocol()
        {
            if (RevTransportProtocolBuf.Length < 5 || RevTransportProtocolBuf.Length > 0x105)
            {
                RevTransportProtocol.Result = "帧格式错误，解析失败！";
                RevTransportProtocol.IsValid = false;
                if (!IsDispOriginalData)
                {
                    RevTransportProtocol.SbData = "";
                }
                else
                {
                    var builder = new StringBuilder();
                    var index = 0;
                    while (true)
                    {
                        if (index >= RevTransportProtocolBuf.Length)
                        {
                            RevTransportProtocol.SbData = builder.ToString();
                            break;
                        }

                        builder.Append(Convert.ToString(RevTransportProtocolBuf[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                        index++;
                    }
                }

                return RevTransportProtocol;
            }

            var num2 = RevTransportProtocolBuf[2];
            if (Convert.ToByte(SendTransportProtocol.Sequence) != num2)
            {
                RevTransportProtocol.Result = "帧序列错误，解析失败！";
                RevTransportProtocol.IsValid = false;
                if (!IsDispOriginalData)
                {
                    RevTransportProtocol.SbData = "";
                }
                else
                {
                    var builder2 = new StringBuilder();
                    var index = 0;
                    while (true)
                    {
                        if (index >= RevTransportProtocolBuf.Length)
                        {
                            RevTransportProtocol.SbData = builder2.ToString();
                            break;
                        }

                        builder2.Append(Convert.ToString(RevTransportProtocolBuf[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                        index++;
                    }
                }

                return RevTransportProtocol;
            }

            int num4 = RevTransportProtocolBuf[3];
            var count = RevTransportProtocolBuf.Length - 4 - num4;
            if (count < 1 || count > 2)
            {
                RevTransportProtocol.Result = "校验值长度错误，解析失败！";
                RevTransportProtocol.IsValid = false;
                if (!IsDispOriginalData)
                {
                    RevTransportProtocol.SbData = "";
                }
                else
                {
                    var builder3 = new StringBuilder();
                    var index = 0;
                    while (true)
                    {
                        if (index >= RevTransportProtocolBuf.Length)
                        {
                            RevTransportProtocol.SbData = builder3.ToString();
                            break;
                        }

                        builder3.Append(Convert.ToString(RevTransportProtocolBuf[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                        index++;
                    }
                }

                return RevTransportProtocol;
            }

            var dst = new byte[RevTransportProtocolBuf.Length - count];
            Buffer.BlockCopy(RevTransportProtocolBuf, 0, dst, 0, RevTransportProtocolBuf.Length - count);
            var list = new List<byte>();
            list.AddRange(dst);
            var num7 = new CheckTransportProtocol {ByteArray = list}.CalculateChecksum(RevTransportProtocol.ChecksumMethod);
            var buffer2 = new byte[count];
            Buffer.BlockCopy(RevTransportProtocolBuf, RevTransportProtocolBuf.Length - count, buffer2, 0, count);
            uint num8 = 0;
            if (count == 1)
                num8 = buffer2[0];
            else if (count == 2) num8 = (uint) ((buffer2[0] << 8) + buffer2[1]);
            if (num8 != num7)
            {
                RevTransportProtocol.Result = "校验错误，解析失败！";
                RevTransportProtocol.IsValid = false;
                if (!IsDispOriginalData)
                {
                    RevTransportProtocol.SbData = "";
                }
                else
                {
                    var builder4 = new StringBuilder();
                    var index = 0;
                    while (true)
                    {
                        if (index >= RevTransportProtocolBuf.Length)
                        {
                            RevTransportProtocol.SbData = builder4.ToString();
                            break;
                        }

                        builder4.Append(Convert.ToString(RevTransportProtocolBuf[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                        index++;
                    }
                }

                return RevTransportProtocol;
            }

            RevTransportProtocol.Result = "解析成功！";
            RevTransportProtocol.IsValid = true;
            RevTransportProtocol.DeviceAddr = Convert.ToString(RevTransportProtocolBuf[0], 0x10).ToUpper().PadLeft(2, '0');
            RevTransportProtocol.FunctionType = Convert.ToString(RevTransportProtocolBuf[1], 0x10).ToUpper().PadLeft(2, '0');
            RevTransportProtocol.Sequence = RevTransportProtocolBuf[2].ToString();
            RecieveSequence = RevTransportProtocolBuf[2];
            RevTransportProtocol.DataLength = RevTransportProtocolBuf[3].ToString();
            if (RevTransportProtocolBuf[3] == 0)
            {
                if (!IsDispOriginalData)
                {
                    RevTransportProtocol.SbData = "";
                }
                else
                {
                    var builder5 = new StringBuilder();
                    var index = 0;
                    while (true)
                    {
                        if (index >= RevTransportProtocolBuf.Length)
                        {
                            RevTransportProtocol.SbData = builder5.ToString();
                            break;
                        }

                        builder5.Append(Convert.ToString(RevTransportProtocolBuf[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                        index++;
                    }
                }
            }
            else if (IsDispOriginalData)
            {
                var builder7 = new StringBuilder();
                var index = 0;
                while (true)
                {
                    if (index >= RevTransportProtocolBuf.Length)
                    {
                        RevTransportProtocol.SbData = builder7.ToString();
                        break;
                    }

                    builder7.Append(Convert.ToString(RevTransportProtocolBuf[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                    index++;
                }
            }
            else
            {
                var buffer3 = new byte[RevTransportProtocolBuf[3]];
                Buffer.BlockCopy(RevTransportProtocolBuf, 4, buffer3, 0, RevTransportProtocolBuf[3]);
                var builder6 = new StringBuilder();
                var index = 0;
                while (true)
                {
                    if (index >= buffer3.Length)
                    {
                        RevTransportProtocol.SbData = builder6.ToString();
                        break;
                    }

                    builder6.Append(Convert.ToString(buffer3[index], 0x10).ToUpper().PadLeft(2, '0') + " ");
                    index++;
                }
            }

            if (IsAutoNewLine) RevTransportProtocol.SbData = RevTransportProtocol.SbData + "\r\n";
            RevTransportProtocol.Checksum = num8.ToString();
            IsRevTransportProtocolSuccess = true;
            return RevTransportProtocol;
        }

        public byte[] ConvertTransportProtocolToByte()
        {
            var list = new List<byte>
            {
                Utils.StrHexToByte(SendTransportProtocol.DeviceAddr),
                Utils.StrHexToByte(SendTransportProtocol.FunctionType),
                SendSequence
            };
            byte[] collection = null;
            collection = CurrentSendTaskType == 3 ? SendTransportProtocolBuf : Utils.StrHexArrToByte(SendTransportProtocol.SbData);
            list.Add(Convert.ToByte(SendTransportProtocol.DataLength));
            if (Convert.ToByte(SendTransportProtocol.DataLength) != 0) list.AddRange(collection);
            var protocol = new CheckTransportProtocol
            {
                ByteArray = list
            };
            var bytes = BitConverter.GetBytes(protocol.CalculateChecksum(SendTransportProtocol.ChecksumMethod));
            if (SendTransportProtocol.ChecksumMethod != 5)
            {
                list.Add(bytes[0]);
            }
            else
            {
                list.Add(bytes[1]);
                list.Add(bytes[0]);
            }

            return list.ToArray();
        }
    }
}