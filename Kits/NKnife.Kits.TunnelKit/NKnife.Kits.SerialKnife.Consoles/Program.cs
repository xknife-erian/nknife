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

            var count = 3;
            Console.WriteLine("----------------");
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < count; i++)
            {
                //09 17 09 AA 00 52 45 41 44 3F 0D 0A
                server1.Send(new byte[] {0x08, 0x17, 0x09, 0xAA, 0x00, 0x52, 0x45, 0x41, 0x44, 0x3F, 0x0D, 0x0A});
                Console.Write("<<< ");
                Thread.Sleep(50);
            }
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("--{0}--{1}-----", sw.ElapsedMilliseconds, sw.ElapsedMilliseconds/count);
            Console.ReadLine();
        }
    }
}