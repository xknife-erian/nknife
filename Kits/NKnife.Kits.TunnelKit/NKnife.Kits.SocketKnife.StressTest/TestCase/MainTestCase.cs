using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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


        private MainTestOption _TestOption;
        private TestServerMonitorFilter _TestMonitorFilter;
        public void Start(MainTestOption testOption, TestServerMonitorFilter testMonitorFilter)
        {
            _TestOption = testOption;
            _TestMonitorFilter = testMonitorFilter;
            var serverConfig = DI.Get<SocketConfig>("Server");
            serverConfig.MaxConnectCount = 1000;
            var clientConfig = DI.Get<SocketConfig>("Client");
            _Server = BuildServer(serverConfig, new MainTestServerHandler(), testMonitorFilter);

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < testOption.ClientCount; i++)
                {
                    var clientHandler = new MainTestClientHandler();
                    var client = BuildClient(clientConfig, clientHandler);
                    _Clients.Add(client);
                    _ClientHandlers.Add(clientHandler);
                    Thread.Sleep(100);
                }
            });
        }

        public void AddTalk()
        {
            if (_TestMonitorFilter.TalkCount + 1 < _ClientHandlers.Count)
            {
                _TestMonitorFilter.TalkCount += 1;
                var clientHandler = _ClientHandlers[_TestMonitorFilter.TalkCount];
                clientHandler.StartSendingTimer(_TestOption.SendInterval);
                
                _TestMonitorFilter.InvokeServerStateChanged();
            }
        }

        public void RemoveTalk()
        {
            if (_TestMonitorFilter.TalkCount < _ClientHandlers.Count)
            {
                var clientHandler = _ClientHandlers[_TestMonitorFilter.TalkCount];
                clientHandler.StopSendingTimer();
                _TestMonitorFilter.TalkCount -= 1;

                _TestMonitorFilter.InvokeServerStateChanged();
            }

        }

        public void Stop()
        {
            int count = _ClientHandlers.Count;
            foreach (var handler in _ClientHandlers)
            {
                handler.StopSendingTimer();
            }
            foreach (var client in _Clients)
            {
                client.Stop();
            }

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(count * 100);
                if(_Server !=null)
                    _Server.Stop();
            });

        }

        private KnifeSocketServer BuildServer(SocketConfig config, BaseProtocolHandler<string> handler, TestServerMonitorFilter testMonitorFilter)
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

            tunnel.AddFilters(testMonitorFilter);
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
