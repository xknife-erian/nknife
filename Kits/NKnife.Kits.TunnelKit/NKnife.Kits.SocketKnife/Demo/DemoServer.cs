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
        private bool _IsInitialized;
        private readonly ITunnel _Tunnel = DI.Get<ITunnel>();
        private KnifeSocketServer _Server = DI.Get<KnifeSocketServer>();
        private SocketProtocolFilter _ProtocolFilter = DI.Get<SocketProtocolFilter>();
        private readonly StringProtocolFamily _Family = DI.Get<StringProtocolFamily>();

        public KnifeSocketServer GetSocketServer()
        {
            return _Server;
        }

        public ISocketServer GetSocket()
        {
            return _Server;
        }

        public StringProtocolFamily GetFamily()
        {
            return _Family;
        }

        internal void Initialize(SocketConfig config, SocketCustomSetting socketTools, BaseProtocolHandler<string> handler)
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

            var codec = DI.Get<StringCodec>();
            if (codec.StringDecoder.GetType()!=socketTools.Decoder)
            {
                var decoder = DI.Get(socketTools.Decoder) as FixedTailDecoder;
                codec.StringDecoder = decoder;
            }
            if (codec.StringEncoder.GetType()!=socketTools.Encoder)
            {
                var encoder = (FixedTailEncoder)DI.Get(socketTools.Encoder);
                codec.StringEncoder = encoder;
            }
            if (protocolFamily.CommandParser == null || protocolFamily.CommandParser.GetType() != socketTools.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(socketTools.CommandParser);

            _ProtocolFilter.Bind(codec, protocolFamily);
            _ProtocolFilter.AddHandlers(handler);

            _Tunnel.AddFilters(DI.Get<LogFilter>());
            if (socketTools.NeedHeartBeat)
                _Tunnel.AddFilters(heartbeatServerFilter);
            _Tunnel.AddFilters(_ProtocolFilter);

            _Server.Config = config;
            _Server.Configure(socketTools.IpAddress, socketTools.Port);
            _Tunnel.BindDataConnector(_Server);

            _IsInitialized = true;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            var register = DI.Get<Register>();

            _Family.FamilyName = "socket-kit";

            var custom = DI.Get<StringProtocol>("TestCustom");
            custom.Family = _Family.FamilyName;
            custom.Command = "custom";

            _Family.Build("command");
           
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