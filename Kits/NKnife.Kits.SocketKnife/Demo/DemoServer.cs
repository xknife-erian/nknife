using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using NKnife.Base;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo.Protocols;
using NKnife.Protocol.Generic;
using SocketKnife;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServer
    {
        private bool _IsInitialized;
        private KnifeSocketServer _Server;
        private KeepAliveServerFilter _KeepAliveFilter = DI.Get<KeepAliveServerFilter>();
        private readonly StringProtocolFamily _Family = DI.Get<StringProtocolFamily>();

        public KnifeSocketServer GetSocketServer()
        {
            return _Server;
        }

        public KnifeSocketServerFilter GetSocketServerFilter()
        {
            return _KeepAliveFilter;
        }

        public StringProtocolFamily GetFamily()
        {
            return _Family;
        }

        public void Initialize(KnifeSocketConfig config, SocketTools socketTools, KnifeSocketServerProtocolHandler handler)
        {
            if (_IsInitialized) 
                return;

            var heartbeatServerFilter = DI.Get<HeartbeatServerFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            _KeepAliveFilter = DI.Get<KeepAliveServerFilter>();
            var codec = DI.Get<KnifeSocketCodec>();
            if (codec.SocketDecoder.GetType() != socketTools.Decoder)
                codec.SocketDecoder = (KnifeSocketDatagramDecoder) DI.Get(socketTools.Decoder);
            if (codec.SocketEncoder.GetType() != socketTools.Encoder)
                codec.SocketEncoder = (KnifeSocketDatagramEncoder) DI.Get(socketTools.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            if (protocolFamily.CommandParser.GetType() != socketTools.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(socketTools.CommandParser);

            _Server = DI.Get<KnifeSocketServer>();
            _Server.Config = config;
            if (socketTools.NeedHeartBeat)
                _Server.AddFilters(heartbeatServerFilter);
            _Server.AddFilters(_KeepAliveFilter);
            _Server.Configure(socketTools.IpAddress, socketTools.Port);
            _Server.Bind(codec, protocolFamily);
            _Server.AddHandlers(handler);

            _IsInitialized = true;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            var register = DI.Get<Register>();

            _Family.FamilyName = "socket-kit";

            var custom = DI.Get<StringProtocol>("TestCustom");
            custom.Family = _Family.FamilyName;
            custom.Command = "custom";

            _Family.Add(_Family.Build("call"));
            _Family.Add(_Family.Build("recall"));
            _Family.Add(_Family.Build("sing"));
            _Family.Add(_Family.Build("dance"));
            _Family.Add(register);
            _Family.Add(custom);

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