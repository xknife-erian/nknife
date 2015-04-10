using System;
using System.Diagnostics;
using System.Threading;
using Common.Logging;
using NKnife.Converts;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Consoles.Demos;

namespace NKnife.Kits.SerialKnife.Consoles
{
    internal class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger<Program>();

        private static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("**** START ****************************");

//            Serial serial = new Serial();
//            serial.PortNum = "COM1";
//            serial.Open();
//            serial.Write(new byte[]{0x31,0x32});
//            var a = serial.Read(2);
//
//            Console.WriteLine(a.ToHexString());
//
//            Console.ReadLine();


            DI.Initialize();

            _logger.Info("DI初始化结束....");

            var server1 = new SerialClient(4);
            server1.Start();

            Thread.Sleep(100);

            const int COUNT = 100;
            Console.WriteLine("--{0}--------------", COUNT);
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < COUNT; i++)
            {
                for (int j = 209; j < 221; j++)
                {
                    var command = GetA0(UtilityConvert.ConvertTo<byte>(j));
                    server1.Send(command);
                }
            }
            sw.Stop();
            for (int i = 0; i < 4000; i++)
            {
                Thread.Sleep(1);
            }
            Console.WriteLine();
            Console.WriteLine("--{0}--{1}-----", sw.ElapsedMilliseconds, sw.ElapsedMilliseconds/(COUNT*(12)));
            Console.ReadLine();
        }

        private static byte[] GetA0(byte subCommand)
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xA0, subCommand };
        }
    }
}