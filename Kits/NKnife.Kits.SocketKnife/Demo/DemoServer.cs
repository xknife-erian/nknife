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
        private KeepAliveServerFilter _KeepAliveFilter = DI.Get<KeepAliveServerFilter>();
        private KnifeSocketServer _Server;

        public KnifeSocketServer GetSocketServer()
        {
            return _Server;
        }

        public KnifeSocketServerFilter GetSocketServerFilter()
        {
            return _KeepAliveFilter;
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

            var family = DI.Get<StringProtocolFamily>();
            family.FamilyName = "socket-kit";

            var custom = DI.Get<StringProtocol>("TestCustom");
            custom.Family = family.FamilyName;
            custom.Command = "custom";

            family.Add(family.Build("call"));
            family.Add(family.Build("recall"));
            family.Add(family.Build("sing"));
            family.Add(family.Build("dance"));
            family.Add(register);
            family.Add(custom);

            return family;
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