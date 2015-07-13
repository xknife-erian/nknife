using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Protocol;
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
    public class TestKernel
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private int _ServerListenPort = Properties.Settings.Default.ServerPort;
        private readonly BytesProtocolFamily _Family = DI.Get<BytesProtocolFamily>();
        private KnifeSocketServer _Server;
        private List<KnifeLongSocketClient> _Clients = new List<KnifeLongSocketClient>();
        private List<MainTestClientHandler> _ClientHandlers = new List<MainTestClientHandler>();
        public EventHandler MockClientAmountChanged;

        private MainTestOption _TestOption;
        private TestServerMonitorFilter _TestMonitorFilter;

        #region server相关
        public bool BuildServer(TestServerMonitorFilter testMonitorFilter,MainTestServerHandler handler)
        {
            try
            {
                var serverConfig = (SocketServerConfig)DI.Get<SocketConfig>("Server");
                serverConfig.MaxConnectCount = 1000;
                serverConfig.MaxSessionTimeout = 0;
                _Server = BuildServer(serverConfig, handler, testMonitorFilter);
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
                    if (_Server != null)
                        _Server.Stop();
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
                if (_Server != null)
                    return _Server.Start();
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
                    if (_Server != null)
                        _Server.Stop();
                });
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("StopServer遇到异常：{0}", ex.Message));
                return false;
            }
        }

        private KnifeSocketServer BuildServer(SocketConfig config, BaseProtocolHandler<byte[]> handler, TestServerMonitorFilter testMonitorFilter)
        {
            var server = DI.Get<KnifeSocketServer>();
            var tunnel = DI.Get<ITunnel>("Server");
            var ipAddresses = UtilityNet.GetLocalIpv4();

            BytesProtocolFamily protocolFamily = GetProtocolFamily();
            var protocolFilter = DI.Get<SocketBytesProtocolFilter>();
            var codec = DI.Get<BytesCodec>();

            protocolFilter.Bind(codec, protocolFamily);
            protocolFilter.AddHandlers(handler);

            tunnel.AddFilters(protocolFilter);

            tunnel.AddFilters(testMonitorFilter);
            server.Config = config;
            server.Configure(ipAddresses[0], _ServerListenPort);
            _logger.Info(string.Format("Server: {0}:{1}", ipAddresses[0], 22011));

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

        public List<MainTestClientHandler> GetClientHandlers()
        {
            return _ClientHandlers;
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
                        var clientHandler = new MainTestClientHandler();
                        clientHandler.ProtocolReceived+= ProtocolReceived;
                        var client = BuildClient(clientConfig, clientHandler);
                        _Clients.Add(client);
                        _ClientHandlers.Add(clientHandler);
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
