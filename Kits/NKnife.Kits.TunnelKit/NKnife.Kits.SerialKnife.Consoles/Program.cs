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

            var server1 = new SerialClient(7);
            server1.Start();

            Thread.Sleep(100);

            var count = 50;
            Console.WriteLine("----------------");
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < count; i++)
            {
                if (i%2 == 0)
                {
                    server1.Send(new byte[] {0x09, 0x00, 0x05, 0xAA, 0x01, 0x31, 0x32, 0x33});
                    Console.Write(">");
                }
                else
                {
                    server1.Send(new byte[] {0x09, 0x00, 0x05, 0xAA, 0x01, 0x41, 0x42, 0x43});//, 0x09, 0x00, 0x05, 0xAA, 0x01, 0x51, 0x52, 0x53});
                    Console.Write(">");
                }
                Thread.Sleep(2);
            }
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("--{0}--{1}-----", sw.ElapsedMilliseconds, sw.ElapsedMilliseconds/count);
            Console.ReadLine();
        }
    }
}