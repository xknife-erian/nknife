using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Consoles.My;

namespace NKnife.Kits.SerialKnife.Consoles
{
    class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger<Program>();
        private readonly SerialClient _Tunnels = DI.Get<SerialClient>();

        static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("**** START ****************************");

            DI.Initialize();

            _logger.Info("DI初始化结束....");

            var server = new SerialClient();
            server.Start();

            Thread.Sleep(200);

            Console.WriteLine("--------------------");
            Console.ReadLine();
        }
    }
}
