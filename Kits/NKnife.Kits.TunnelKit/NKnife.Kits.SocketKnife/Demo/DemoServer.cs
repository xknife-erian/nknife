using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using NKnife.Base;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo.Protocols;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using SocketKnife;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServer
    {
        private bool _isInitialized;
        private readonly ITunnel _tunnel = Di.Get<ITunnel>();
        private KnifeSocketServer _server = Di.Get<KnifeSocketServer>();
        private SocketProtocolFilter _protocolFilter = Di.Get<SocketProtocolFilter>();
        private readonly StringProtocolFamily _family = Di.Get<StringProtocolFamily>();

        public KnifeSocketServer GetSocketServer()
        {
            return _server;
        }

        public ISocketServer GetSocket()
        {
            return _server;
        }

        public StringProtocolFamily GetFamily()
        {
            return _family;
        }

        internal void Initialize(SocketConfig config, SocketCustomSetting socketTools, BaseProtocolHandler<string> handler)
        {
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
            if (codec.StringDecoder.GetType()!=socketTools.Decoder)
            {
                var decoder = Di.Get(socketTools.Decoder) as FixedTailDecoder;
                codec.StringDecoder = decoder;
            }
            if (codec.StringEncoder.GetType()!=socketTools.Encoder)
            {
                var encoder = (FixedTailEncoder)Di.Get(socketTools.Encoder);
                codec.StringEncoder = encoder;
            }
            if (protocolFamily.CommandParser == null || protocolFamily.CommandParser.GetType() != socketTools.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) Di.Get(socketTools.CommandParser);

            _protocolFilter.Bind(codec, protocolFamily);
            _protocolFilter.AddHandlers(handler);

            _tunnel.AddFilters(Di.Get<LogFilter>());
            if (socketTools.NeedHeartBeat)
                _tunnel.AddFilters(heartbeatServerFilter);
            _tunnel.AddFilters(_protocolFilter);

            _server.Config = config;
            _server.Configure(socketTools.IpAddress, socketTools.Port);
            _tunnel.BindDataConnector(_server);

            _isInitialized = true;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            var register = Di.Get<Register>();

            _family.FamilyName = "socket-kit";

            var custom = Di.Get<StringProtocol>("TestCustom");
            custom.Family = _family.FamilyName;
            custom.Command = "custom";

            _family.Build("command");
           
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