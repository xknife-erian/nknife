using System;
using NKnife.Channels.EventParams;
using NKnife.Channels.Serials;

namespace NKnife.Kits.ChannelKit
{
    internal class Program
    {
        private static SerialChannel _serialChannel;

        private static void Main(string[] args)
        {
            SerialUtils.RefreshSerialPorts();

            Console.WriteLine();
            Console.WriteLine("=== Serial Channel Demo. =========================");
            var port = GetSerialPort();
            _serialChannel = new SerialChannel(new SerialConfig(port) {BaudRate = 115200});
            _serialChannel.IsSynchronous = GetSyncModel();
            _serialChannel.Open();
            _serialChannel.DataArrived += Serial_DataArrived;
            Console.WriteLine($"=== 已开启串口{port}");

            Send();
            Console.WriteLine("=== Press any key exit. =========================");
            Console.ReadKey();
        }

        private static bool GetSyncModel()
        {
            Console.WriteLine("-- 1.Sync; 2.Async");
            var line = Console.ReadLine();
            switch (line)
            {
                case "1":
                    Console.WriteLine("-- 选择同步模式");
                    return true;
                case "2":
                    Console.WriteLine("-- 选择异步模式");
                    return false;
            }
            Console.WriteLine("-- 选择同步模式");
            return true;
        }

        private static void Send()
        {
            var isSend = true;
            while (isSend)
            {
                Console.WriteLine("-- x.退出发送; +.循环发送的内容; =.仅一条发送的内容");
                var line = Console.ReadLine();
                if (line != null)
                {
                    switch (line.ToLower())
                    {
                        case "x": //退出发送
                            isSend = false;
                            break;
                        case "+": //循环发送的内容
                            AddAsk();
                            break;
                        case "=": //仅一条发送的内容
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
            var question = new SerialQuestion(null, false, 100, line.ToBytes());
            var group = new SerialQuestionGroup();
            group.Add(question);
            _serialChannel.UpdateQuestionGroup(group);
            _serialChannel.AutoSend(null);
        }

        private static void AddAsk()
        {
            Console.WriteLine("-- 请输入一条需要发送的内容:");
            var line = Console.ReadLine();
            var question = new SerialQuestion(null, true, 100, line.ToBytes());
            var group = new SerialQuestionGroup();
            group.Add(question);
            _serialChannel.UpdateQuestionGroup(group);
            _serialChannel.AutoSend(null);
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
                    isNum = true;
                else
                    Console.WriteLine("!!! 不是正确的串口号，请重新输入。");
            }
            return num;
        }
    }
}