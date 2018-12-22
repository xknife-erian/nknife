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
        private static readonly ILog _Logger = LogManager.GetLogger<Program>();

        private static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("**** START ****************************");

            Di.Initialize();

            _Logger.Info("DI初始化结束....");

            var server = new SerialClient(10);
            server.Start();

            Thread.Sleep(100);

            const int count = 50;
            Console.WriteLine("--{0}--------------", count);
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
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
            Console.WriteLine("--{0}--{1}-----", sw.ElapsedMilliseconds, sw.ElapsedMilliseconds/(count*(12)));
            Console.ReadLine();
        }

        private static byte[] GetA0(byte subCommand)
        {
            return new byte[] { 0x08, 0x00, 0x02, 0xA0, subCommand };
        }
    }
}