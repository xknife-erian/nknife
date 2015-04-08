using System;
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

            Thread.Sleep(200);
            for (int i = 0; i < 5; i++)
            {
                server1.Send(new byte[] { 0x09, 0x00, 0x05, 0xAA, 0x01, 0x31, 0x32, 0x33 });
                Thread.Sleep(600);
            }

            Thread.Sleep(200);

            Console.ReadLine();
        }
    }
}