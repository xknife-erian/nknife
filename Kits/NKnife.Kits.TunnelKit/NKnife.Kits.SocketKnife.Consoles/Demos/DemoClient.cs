using NKnife.IoC;
using NKnife.Kits.SocketKnife.Consoles.Common;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Filters;
using NKnife.Tunnel.Generic;
using SocketKnife;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.Consoles.Demos
{
    class DemoClient
    {
        private bool _isInitialized = false;
        private readonly ITunnel _tunnel = Di.Get<ITunnel>();
        private readonly ISocketClient _client = Di.Get<KnifeLongSocketClient>();
        private readonly StringProtocolFamily _family = Di.Get<StringProtocolFamily>();

        public StringProtocolFamily GetFamily()
        {
            return _family;
        }

        public void Initialize(SocketConfig config, SocketCustomSetting customSetting, BaseProtocolHandler<string> handler)
        {
            if (_isInitialized) return;

            var heartbeatServerFilter = Di.Get<HeartbeatFilter>();
            heartbeatServerFilter.Heartbeat = new Heartbeat("Client","Server");
            heartbeatServerFilter.Heartbeat.Name = "Client";

            heartbeatServerFilter.Interval = 1000 * 2;
            heartbeatServerFilter.EnableStrictMode = true; //严格模式
            heartbeatServerFilter.HeartBeatMode = HeartBeatMode.Active; 

            var codec = Di.Get<StringCodec>();
            if (codec.StringDecoder.GetType() != customSetting.Decoder)
                codec.StringDecoder = (StringDatagramDecoder)Di.Get(customSetting.Decoder);
            if (codec.StringEncoder.GetType() != customSetting.Encoder)
                codec.StringEncoder = (StringDatagramEncoder)Di.Get(customSetting.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            if (protocolFamily.CommandParser.GetType() != customSetting.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) Di.Get(customSetting.CommandParser);

            var protocolFilter = Di.Get<SocketProtocolFilter>();
            protocolFilter.Bind(codec, protocolFamily);
            protocolFilter.AddHandlers(handler);

            _tunnel.AddFilters(Di.Get<LogFilter>());
            if (customSetting.NeedHeartBeat)
                _tunnel.AddFilters(heartbeatServerFilter);
            _tunnel.AddFilters(protocolFilter);

            _client.Config = config;
            _client.Configure(customSetting.IpAddress, customSetting.Port);
            _tunnel.BindDataConnector(_client);
            _isInitialized = true;
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            _family.FamilyName = "socket-kit";

            var custom = Di.Get<StringProtocol>("TestCustom");
            custom.Family = _family.FamilyName;
            custom.Command = "custom";

            return _family;
        }

        public void Start()
        {
            if (_client != null)
                _client.Start();
        }

        public void Stop()
        {
            if (_client != null)
                _client.Stop();
        }

    }
}