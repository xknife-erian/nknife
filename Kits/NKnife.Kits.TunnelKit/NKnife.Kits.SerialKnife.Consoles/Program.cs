using System;
using System.Diagnostics;
using System.Threading;
using Common.Logging;
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

            DI.Initialize();

            _logger.Info("DI初始化结束....");

            var server1 = new SerialClient(4);
            server1.Start();

            Thread.Sleep(100);

            const int COUNT = 9;
            Console.WriteLine("--{0}--------------", COUNT);
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < COUNT; i++)
            {
                server1.Send(GetA0D1());
            }
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("--{0}--{1}-----", sw.ElapsedMilliseconds, sw.ElapsedMilliseconds/COUNT);
            Console.ReadLine();
        }

        private static byte[] GetA0D1()
        {
            return new byte[] {0x08, 0x00, 0x02, 0xA0, 0xD1};
        }
    }
}