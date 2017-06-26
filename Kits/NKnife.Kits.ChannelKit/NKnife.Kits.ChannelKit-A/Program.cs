using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Channels.Serials;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Kits.ChannelKit
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialUtils.RefreshSerialPorts();

            Console.WriteLine();
            Console.WriteLine("=== Serial Channel Demo. =========================");
            var port = GetSerialPort();
            Console.WriteLine($"=== 准备开启串口{port}...");
            var serial = new SerialChannel(new SerialConfig(port){BaudRate = 115200});
            serial.Open();
            serial.DataArrived += Serial_DataArrived;

            Console.WriteLine("=== Press any key exit. =========================");
            Console.ReadKey();
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
                Console.WriteLine("=== 请输入串口号：");
                var line = Console.ReadLine();
                if (ushort.TryParse(line, out num))
                {
                    isNum = true;
                }
                else
                {
                    Console.WriteLine("=== 不是正确的串口号，请重新输入。");
                }
            }
            return num;
        }
    }
}
