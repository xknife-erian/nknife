﻿using System;
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

//            var server2 = new SerialClient(1);
//            server2.Start();

            Thread.Sleep(2000);
            server1.Send(new byte[] { 0xA0, 0x01, 0x02, 0x03, 0xFF });

            //var timer1 = new Timer(Timer1Call, server1, 1000, 300);
            //var timer2 = new Timer(Timer2Call, server2, 1000, 260);

            Thread.Sleep(200);

            Console.ReadLine();
        }

        private static void Timer2Call(object state)
        {
            _logger.Info("2--->");
            var server = (SerialClient) state;
            server.Send(new byte[] {0xA0, 0x01, 0x02, 0x03, 0xFF});
        }

        private static void Timer1Call(object state)
        {
            _logger.Info("1--->");
            var server = (SerialClient) state;
            server.Send(new byte[] { 0xA0, 0xA1, 0xA2, 0xA3, 0xFF });
        }
    }
}