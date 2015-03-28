using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Consoles.My;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Consoles
{
    class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger<Program>();

        static void Main(string[] args)
        {
            Console.WriteLine("**** START ****************************");

            DI.Initialize();

            _logger.Info("DI初始化结束....");

            var serverConfig = DI.Get<KnifeSocketConfig>("Server");

            var server = new DemoServer();
            server.Initialize(serverConfig, new DemoServerHandler());
            server.StartServer();

            Console.WriteLine("**** END ****************************");
            Console.ReadKey();
        }
    }
}
