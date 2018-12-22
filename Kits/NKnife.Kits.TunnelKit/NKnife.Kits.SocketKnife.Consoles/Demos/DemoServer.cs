using System.Net;
using Common.Logging;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.Consoles.Demos
{
    public class DemoServer
    {
        private static readonly ILog _Logger = LogManager.GetLogger<DemoServer>();

        private readonly StringProtocolFamily _family = Di.Get<StringProtocolFamily>();
        private readonly SocketProtocolFilter _protocolFilter = Di.Get<SocketProtocolFilter>();
        private readonly KnifeSocketServer _server = Di.Get<KnifeSocketServer>();
        private readonly ITunnel _tunnel = Di.Get<ITunnel>();
        private bool _isInitialized;

        private IPAddress[] _ipAddresses;

        internal void Initialize(SocketConfig config, BaseProtocolHandler<string> handler)
        {
            _ipAddresses = UtilityNet.GetLocalIpv4();

            if (_isInitialized)
                return;

            var heartbeatServerFilter = Di.Get<HeartbeatFilter>();
            heartbeatServerFilter.Heartbeat = new Heartbeat("Server", "Client");
            heartbeatServerFilter.Heartbeat.Name = "Server";
            heartbeatServerFilter.Interval = 1000*2;
            heartbeatServerFilter.EnableStrictMode = true; //严格模式
            heartbeatServerFilter.HeartBeatMode = HeartBeatMode.Responsive;

            StringProtocolFamily protocolFamily = GetProtocolFamily();

            var codec = Di.Get<StringCodec>();

            _protocolFilter.Bind(codec, protocolFamily);
            _protocolFilter.AddHandlers(handler);

            _tunnel.AddFilters(Di.Get<LogFilter>());
            _tunnel.AddFilters(heartbeatServerFilter);
            _tunnel.AddFilters(_protocolFilter);

            _server.Config = config;
            _server.Configure(_ipAddresses[0], 22011);
            _Logger.Info(string.Format("Server: {0}:{1}", _ipAddresses[0], 22011));

            _tunnel.BindDataConnector(_server);

            _isInitialized = true;
        }

        public ISocketServer GetSocket()
        {
            return _server;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            _family.FamilyName = "socket-kit";
            return _family;
        }

        public void StartServer()
        {
            if (_server != null)
                _server.Start();
        }

        public void StopServer()
        {
            if (_server != null)
                _server.Stop();
        }
    }
}