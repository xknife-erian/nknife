using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Kits.ChannelKit
{
    class Program
    {
        private static SerialChannel _serialChannel;

        static void Main(string[] args)
        {
            SerialUtils.RefreshSerialPorts();

            Console.WriteLine();
            Console.WriteLine("=== Serial Channel Demo. =========================");
            var port = GetSerialPort();
            Console.WriteLine($"=== 准备开启串口{port}...");
            _serialChannel = new SerialChannel(new SerialConfig(port){BaudRate = 115200});
            _serialChannel.Open();
            _serialChannel.DataArrived += Serial_DataArrived;

            Send();
            Console.WriteLine("=== Press any key exit. =========================");
            Console.ReadKey();
        }

        private static void Send()
        {
            var isSend = true;
            while (isSend)
            {
                var line = Console.ReadLine();
                if (line != null)
                {
                    switch (line.ToLower())
                    {
                        case "x"://退出发送
                            isSend = false;
                            break;
                        case "+"://增加发送的内容
                            AddAsk();
                            break;
                        case "%"://仅一条发送的内容
                            SendAsk();
                            break;
                    }
                }
            }
        }

        private static void SendAsk()
        {
            Console.WriteLine("-- 请输入一条需要发送的内容:");
            var line = Console.ReadLine();
            var question = new SerialQuestion(_serialChannel,null,null,true,line.ToBytes());
            var group = new SerialQuestionGroup();
            group.Add(question);
            _serialChannel.UpdateQuestionGroup(group);
            _serialChannel.AutoSend(null);
        }

        private static void AddAsk()
        {
            Console.WriteLine("-- 请输入增加发送的内容:");
        }

        private static void Serial_DataArrived(object sender, ChannelAnswerDataEventArgs<byte[]> e)
        {
            Console.WriteLine($"<<< {DateTime.Now:mm:ss,fff} {e.Answer.Data.ToHexString()}");
        }

        private static ushort GetSerialPort()
        {
            var isNum = false;
            ushort num = 0;
            while (!isNum)
            {
                Console.WriteLine("-- 请输入串口号：");
                var line = Console.ReadLine();
                if (ushort.TryParse(line, out num))
                {
                    isNum = true;
                }
                else
                {
                    Console.WriteLine("!!! 不是正确的串口号，请重新输入。");
                }
            }
            return num;
        }
    }
}
