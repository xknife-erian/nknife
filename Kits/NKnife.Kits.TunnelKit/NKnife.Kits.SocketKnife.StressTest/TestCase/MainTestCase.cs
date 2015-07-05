using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Common.Logging;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.StressTest.TestCase
{
    public class MainTestCase
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly StringProtocolFamily _Family = DI.Get<StringProtocolFamily>();
        private KnifeSocketServer _Server;
        private List<KnifeLongSocketClient> _Clients = new List<KnifeLongSocketClient>();

        private List<MainTestClientHandler> _ClientHandlers = new List<MainTestClientHandler>();
        public void Start(MainTestOption testOption)
        {
            var serverConfig = DI.Get<SocketConfig>("Server");
            var clientConfig = DI.Get<SocketConfig>("Client");
            _Server = BuildServer(serverConfig, new MainTestServerHandler());

            for (int i = 0; i < testOption.ClientCount; i++)
            {
                var clientHandler = new MainTestClientHandler();
                var client = BuildClient(clientConfig, clientHandler);
                _Clients.Add(client);
                clientHandler.StartSendingTimer(testOption.SendInterval);
                _ClientHandlers.Add(clientHandler);
            }
        }

        public void Stop()
        {
            foreach (var handler in _ClientHandlers)
            {
                handler.StopSendingTimer();
            }
            foreach (var client in _Clients)
            {
                client.Stop();
            }
            if(_Server !=null)
                _Server.Stop();
        }

        private KnifeSocketServer BuildServer(SocketConfig config, BaseProtocolHandler<string> handler)
        {
            var server = DI.Get<KnifeSocketServer>();
            var tunnel = DI.Get<ITunnel>("Server");
            var ipAddresses = UtilityNet.GetLocalIpv4();

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            var protocolFilter = DI.Get<SocketProtocolFilter>();
            var codec = DI.Get<StringCodec>();

            protocolFilter.Bind(codec, protocolFamily);
            protocolFilter.AddHandlers(handler);

            tunnel.AddFilters(protocolFilter);

            server.Config = config;
            server.Configure(ipAddresses[0], 22011);
            _logger.Info(string.Format("Server: {0}:{1}", ipAddresses[0], 22011));

            tunnel.BindDataConnector(server);
            server.Start();
            return server;
        }

        private KnifeLongSocketClient BuildClient(SocketConfig config, BaseProtocolHandler<string> handler)
        {
            var client = DI.Get<KnifeLongSocketClient>();
            var tunnelClient = DI.Get<ITunnel>("Client");
            var ipAddresses = UtilityNet.GetLocalIpv4();

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            var protocolFilter = DI.Get<SocketProtocolFilter>();
            var codec = DI.Get<StringCodec>();

            protocolFilter.Bind(codec, protocolFamily);
            protocolFilter.AddHandlers(handler);

            tunnelClient.AddFilters(protocolFilter);

            client.Config = config;
            client.Configure(ipAddresses[0], 22011);
            _logger.Info(string.Format("Client: {0}:{1}", ipAddresses[0], 22011));

            tunnelClient.BindDataConnector(client);
            client.Start();
            return client;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            _Family.FamilyName = "socket-test";
            return _Family;
        }
    }
}
