using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using Ninject.Activation;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Consoles.Demos;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Consoles
{
    class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger<Program>();

        static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("**** START ****************************");

            DI.Initialize();

            _logger.Info("DI初始化结束....");

            var serverConfig = new SocketServerConfig();//DI.Get<SocketConfig>("Server");

            var server = new DemoServer();
            server.Initialize(serverConfig, new DemoServerHandler());
            var socket = server.GetSocket();
            socket.DataReceived += (s, e) => _logger.Info(e.Item.Data);
            socket.SessionBuilt += (s, e) => _logger.Info(e.Item.Data);
            socket.SessionBroken += (s, e) => _logger.Info(e.Item.Data);
            server.StartServer();

            Thread.Sleep(200);

            Console.WriteLine("--------------------");
            Console.ReadLine();
        }

    }
}
