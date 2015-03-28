using System.Net;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Consoles.My.Protocols;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;

namespace NKnife.Kits.SocketKnife.Consoles.My
{
    public class DemoServer
    {
        private static readonly ILog _logger = LogManager.GetLogger<DemoServer>();

        private readonly StringProtocolFamily _Family = DI.Get<StringProtocolFamily>();
        private readonly SocketProtocolFilter _ProtocolFilter = DI.Get<SocketProtocolFilter>();
        private readonly KnifeSocketServer _Server = DI.Get<KnifeSocketServer>();
        private readonly ITunnel _Tunnel = DI.Get<ITunnel>();
        private bool _IsInitialized;

        internal void Initialize(KnifeSocketConfig config, BaseProtocolHandler<string> handler)
        {
            if (_IsInitialized)
                return;

            var heartbeatServerFilter = DI.Get<HeartbeatFilter>();
            heartbeatServerFilter.Heartbeat = new Heartbeat("Server", "Client");
            heartbeatServerFilter.Heartbeat.Name = "Server";
            heartbeatServerFilter.Interval = 1000*2;
            heartbeatServerFilter.EnableStrictMode = true; //严格模式
            heartbeatServerFilter.HeartBeatMode = HeartBeatMode.Responsive;

            StringProtocolFamily protocolFamily = GetProtocolFamily();

            var codec = DI.Get<KnifeStringCodec>();

            _ProtocolFilter.Bind(codec, protocolFamily);
            _ProtocolFilter.AddHandlers(handler);

            _Tunnel.AddFilters(DI.Get<LogFilter>());
            _Tunnel.AddFilters(heartbeatServerFilter);
            _Tunnel.AddFilters(_ProtocolFilter);

            _Server.Config = config;
            _Server.Configure(IPAddress.Parse("127.0.0.1"), 22011);
            _logger.Info(string.Format("Server: {0}:{1}", "127.0.0.1", 22011));

            _Tunnel.BindDataConnector(_Server);

            _IsInitialized = true;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            _Family.FamilyName = "socket-kit";
            return _Family;
        }

        public void StartServer()
        {
            if (_Server != null)
                _Server.Start();
        }

        public void StopServer()
        {
            if (_Server != null)
                _Server.Stop();
        }
    }
}