using System;
using NKnife.Channels.EventParams;
using NKnife.Channels.Interfaces;
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
            Console.WriteLine($"=== 串口测试. ===========================================================");
            var port = GetSerialPort();
            _serialChannel = new SerialChannel(new SerialConfig(port) {BaudRate = 115200});
            _serialChannel.IsSynchronous = GetSyncModel();
            _serialChannel.Open();
            _serialChannel.DataArrived += Serial_DataAsyncArrived;
            Console.WriteLine($"=== 已开启串口{port}.  ==================================================");

            Send();

            _serialChannel.Close();
            Console.WriteLine($"=== Press any key exit. ================================================");
            Console.ReadKey();
        }

        private static bool GetSyncModel()
        {
            Console.WriteLine("--> 1.Sync; 2.Async");
            var line = Console.ReadLine();
            switch (line)
            {
                case "2":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("--< 选择异步模式");
                    Console.ResetColor();
                    return false;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("--< 选择同步模式");
                    Console.ResetColor();
                    return true;
            }
        }

        private static void Send()
        {
            var isSend = true;
            while (isSend)
            {
                Console.WriteLine("\n-- (x).退出发送; (=).循环发送的内容; (+).仅一条发送的内容");
                var line = Console.ReadLine();
                if (line != null)
                {
                    switch (line.ToLower())
                    {
                        case "x": //退出发送
                            isSend = false;
                            break;
                        case "=": //循环发送的内容
                            AddQuestion();
                            break;
                        case "+": //仅一条发送的内容
                            SendQuestion();
                            break;
                    }
                }
            }
        }

        private static void SendQuestion()
        {
            Console.WriteLine("-- 请输入一条需要发送的内容:");
            var line = Console.ReadLine();
            var question = new SerialQuestion(null, false, 100, line.ToBytes());
            var group = new SerialQuestionGroup();
            group.Add(question);
            _serialChannel.UpdateQuestionGroup(group);
            if (_serialChannel.IsSynchronous)
                _serialChannel.SendReceiver(Serial_DataSend, Serial_DataSyncArrived);
            else
                _serialChannel.AutoSend(Serial_DataSend);
        }

        private static void AddQuestion()
        {
            Console.WriteLine("-- 请输入一条需要循环发送的内容:");
            var line = Console.ReadLine();
            var question = new SerialQuestion(null, true, 3000, line.ToBytes());
            var group = new SerialQuestionGroup();
            group.Add(question);
            _serialChannel.UpdateQuestionGroup(group);
            if (_serialChannel.IsSynchronous)
                _serialChannel.SendReceiver(Serial_DataSend, Serial_DataSyncArrived);
            else
                _serialChannel.AutoSend(Serial_DataSend);
        }

        private static void Serial_DataSend(IQuestion<byte[]> question)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($">>> {DateTime.Now:mm:ss,fff} \t {question.Data.ToHexString()}");
            Console.ResetColor();
        }

        private static bool Serial_DataSyncArrived(IAnswer<byte[]> e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"<<< {DateTime.Now:mm:ss,fff} \t {e.Data.ToHexString()}");
            Console.ResetColor();
            return true;
        }

        private static void Serial_DataAsyncArrived(object sender, ChannelAnswerDataEventArgs<byte[]> e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"<<< {DateTime.Now:mm:ss,fff} \t {e.Answer.Data.ToHexString()}");
            Console.ResetColor();
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