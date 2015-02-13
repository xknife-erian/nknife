using System.Net;
using System.Text;
using System.Windows.Threading;
using NKnife.Collections;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo.Protocols;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
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
    class DemoClient : NotificationObject
    {
        private bool _IsInitialized = false;
        private readonly ITunnel<byte[], EndPoint> _Tunnel = DI.Get<ITunnel<byte[], EndPoint>>();
        private IKnifeSocketClient _Client = DI.Get<KnifeLongSocketClient>();
        private StringProtocolFamily _Family = DI.Get<StringProtocolFamily>();

        public StringProtocolFamily GetFamily()
        {
            return _Family;
        }

        public void Initialize(KnifeSocketConfig config, SocketCustomSetting customSetting, KnifeProtocolHandlerBase<byte[], EndPoint, string> handler)
        {
            if (_IsInitialized) return;

            var heartbeatServerFilter = DI.Get<HeartbeatFilter>();
            heartbeatServerFilter.Heartbeat = new Heartbeat("Client","Server");
            heartbeatServerFilter.Heartbeat.Name = "Client";

            heartbeatServerFilter.Interval = 1000 * 2;
            heartbeatServerFilter.EnableStrictMode = true; //严格模式
            heartbeatServerFilter.EnableAggressiveMode = true; //主动模式

            var codec = DI.Get<KnifeStringCodec>();
            if (codec.StringDecoder.GetType() != customSetting.Decoder)
                codec.StringDecoder = (KnifeStringDatagramDecoder)DI.Get(customSetting.Decoder);
            if (codec.StringEncoder.GetType() != customSetting.Encoder)
                codec.StringEncoder = (KnifeStringDatagramEncoder)DI.Get(customSetting.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            if (protocolFamily.CommandParser.GetType() != customSetting.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(customSetting.CommandParser);

            var protocolFilter = DI.Get<SocketKnifeProtocolFilter>();
            protocolFilter.Bind(codec, protocolFamily);
            protocolFilter.AddHandlers(handler);

            _Tunnel.AddFilters(DI.Get<LogFilter>());
            if (customSetting.NeedHeartBeat)
                _Tunnel.AddFilters(heartbeatServerFilter);
            _Tunnel.AddFilters(protocolFilter);

            _Client.Config = config;
            _Client.Configure(customSetting.IpAddress, customSetting.Port);
            _Tunnel.BindDataConnector(_Client);
            _IsInitialized = true;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            _Family.FamilyName = "socket-kit";

            var custom = DI.Get<StringProtocol>("TestCustom");
            custom.Family = _Family.FamilyName;
            custom.Command = "custom";

            return _Family;
        }

        public void Start()
        {
            if (_Client != null)
                _Client.Start();
        }

        public void Stop()
        {
            if (_Client != null)
                _Client.Stop();
        }

    }
}