﻿using System;
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

            DI.Initialize();

            _logger.Info("DI初始化结束....");

            var server = new SerialClient(4);
            server.Start();

            Thread.Sleep(100);

            const int COUNT = 50;
            Console.WriteLine("--{0}--------------", COUNT);
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < COUNT; i++)
            {
                for (int j = 209; j < 221; j++)
                {
                    if (j == 216)
                        continue;
                    var command = GetA0(UtilityConvert.ConvertTo<byte>(j));
                    server.Send(command);
                }
            }
            sw.Stop();
            for (int i = 0; i < 500; i++)
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