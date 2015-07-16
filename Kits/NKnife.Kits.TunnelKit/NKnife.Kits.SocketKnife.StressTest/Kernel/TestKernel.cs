using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using SerialKnife.Generic.Filters;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;

namespace NKnife.Kits.SocketKnife.StressTest.Kernel
{
    public class TestKernel : IKernel
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly int _ServerListenPort = Properties.Settings.Default.ServerPort;
        private readonly BytesProtocolFamily _Family = DI.Get<BytesProtocolFamily>();
        public KnifeSocketServer Server { get; private set; }

        public NangleServerFilter ServerProtocolFilter { get; private set; }
        public ServerHandler ServerHandler { get; private set; }

        public List<KnifeLongSocketClient> Clients { get; private set; }
        public List<MockClientHandler> ClientHandlers { get; private set; }
        public EventHandler MockClientAmountChanged;

        private MainTestOption _TestOption;

        public TestKernel()
        {
            ServerHandler = new ServerHandler();
            Clients = new List<KnifeLongSocketClient>();
            ClientHandlers = new List<MockClientHandler>();
            ServerProtocolFilter = DI.Get<NangleServerFilter>();
        }

        #region server相关
        public bool BuildServer()
        {
            try
            {
                var serverConfig = (SocketServerConfig)DI.Get<SocketConfig>("Server");
                serverConfig.MaxConnectCount = 1000;
                serverConfig.MaxSessionTimeout = 0;
                Server = BuildServer(serverConfig);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveServer()
        {
            try
            {
                int count = ClientHandlers.Count;
                foreach (var handler in ClientHandlers)
                {
                    handler.StopSendingTimer();
                }
                foreach (var client in Clients)
                {
                    client.Stop();
                }

                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(count * 100);
                    if (Server != null)
                        Server.Stop();
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool StartServer()
        {
            try
            {
                if (Server != null)
                    return Server.Start();
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("StartServer遇到异常：{0}",ex.Message));
                return false;
            }
        }

        public bool StopServer()
        {
            try
            {
                int count = ClientHandlers.Count;
                foreach (var handler in ClientHandlers)
                {
                    handler.StopSendingTimer();
                }
                foreach (var client in Clients)
                {
                    client.Stop();
                }

                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(count * 100);
                    if (Server != null)
                        Server.Stop();
                });
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("StopServer遇到异常：{0}", ex.Message));
                return false;
            }
        }

        private KnifeSocketServer BuildServer(SocketConfig config)
        {
            var server = DI.Get<KnifeSocketServer>();
            var tunnel = DI.Get<ITunnel>("Server");
            var ipAddresses = UtilityNet.GetLocalIpv4();

            BytesProtocolFamily protocolFamily = GetProtocolFamily();
            var codec = DI.Get<BytesCodec>();

            ServerProtocolFilter.Bind(codec, protocolFamily);
            ServerProtocolFilter.AddHandlers(ServerHandler);

            var logFilter = DI.Get<SerialLogFilter>();
            tunnel.AddFilters(logFilter);
            tunnel.AddFilters(ServerProtocolFilter);

            server.Config = config;
            server.Configure(ipAddresses[0], _ServerListenPort);
            _logger.Info(string.Format("Server: {0}:{1}", ipAddresses[0], _ServerListenPort));

            tunnel.BindDataConnector(server);
            return server;
        }
        #endregion

        #region client相关
        public EventHandler<NangleProtocolEventArgs> MockClientProtocolReceived;

        private void OnMockClientProtocolReceived(object sender, NangleProtocolEventArgs eventArgs)
        {
            var handler = MockClientProtocolReceived;
            if (handler != null)
            {
                handler.Invoke(sender, eventArgs);
            }
        }
 
        public bool BuildCient(MainTestOption testOption)
        {
            try
            {
                _TestOption = testOption;
                var clientConfig = DI.Get<SocketConfig>("Client");

                Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < testOption.ClientCount; i++)
                    {
                        var clientHandler = new MockClientHandler();
                        clientHandler.ProtocolReceived+= ProtocolReceived;
                        var client = BuildClient(clientConfig, clientHandler);
                        Clients.Add(client);
                        ClientHandlers.Add(clientHandler);
                        OnMockClientAmountChanged();
                        Thread.Sleep(100);
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void ProtocolReceived(object sender, NangleProtocolEventArgs nangleProtocolEventArgs)
        {
            OnMockClientProtocolReceived(sender, nangleProtocolEventArgs);
        }

        public bool RemoveClient()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool StartClientConnection()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool StopClientConnection()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void AddTalk()
        {
            if (ServerProtocolFilter.TalkCount + 1 < ClientHandlers.Count)
            {
                ServerProtocolFilter.TalkCount += 1;
                var clientHandler = ClientHandlers[ServerProtocolFilter.TalkCount];
                clientHandler.StartSendingTimer(50); //发送间隔50毫秒

                ServerProtocolFilter.InvokeServerStateChanged();
            }
        }

        public void RemoveTalk()
        {
            if (ServerProtocolFilter.TalkCount < ClientHandlers.Count)
            {
                var clientHandler = ClientHandlers[ServerProtocolFilter.TalkCount];
                clientHandler.StopSendingTimer();
                ServerProtocolFilter.TalkCount -= 1;

                ServerProtocolFilter.InvokeServerStateChanged();
            }

        }

        private KnifeLongSocketClient BuildClient(SocketConfig config, BaseProtocolHandler<byte[]> handler)
        {
            var client = DI.Get<KnifeLongSocketClient>();
            var tunnelClient = DI.Get<ITunnel>("Client");
            var ipAddresses = UtilityNet.GetLocalIpv4();

            BytesProtocolFamily protocolFamily = GetProtocolFamily();
            var protocolFilter = DI.Get<SocketBytesProtocolFilter>();
            var codec = DI.Get<BytesCodec>();

            protocolFilter.Bind(codec, protocolFamily);
            protocolFilter.AddHandlers(handler);

            tunnelClient.AddFilters(protocolFilter);

            client.Config = config;
            client.Configure(ipAddresses[0], _ServerListenPort);
            _logger.Info(string.Format("Client: {0}:{1}", ipAddresses[0], 22011));

            tunnelClient.BindDataConnector(client);
            client.Start();
            return client;
        }

        private void OnMockClientAmountChanged()
        {
            var handler = MockClientAmountChanged;
            if(handler !=null)
                handler.Invoke(this,EventArgs.Empty);
        }
        #endregion

        public BytesProtocolFamily GetProtocolFamily()
        {
            _Family.FamilyName = "nangle-socket";
            return _Family;
        }
    }
}
