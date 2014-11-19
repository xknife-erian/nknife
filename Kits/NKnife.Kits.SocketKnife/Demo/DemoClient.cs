using System.Windows.Threading;
using NKnife.Collections;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo.Protocols;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.Demo
{
    class DemoClient : NotificationObject
    {
        private bool _IsInitialized = false;
        private IKnifeSocketClient _Client;
        private StringProtocolFamily _Family = DI.Get<StringProtocolFamily>();

        public StringProtocolFamily GetFamily()
        {
            return _Family;
        }

        public void Initialize(KnifeSocketConfig config, SocketCustomSetting customSetting, KnifeSocketClientProtocolHandler handler)
        {
            if (_IsInitialized) return;

            var heartbeatServerFilter = DI.Get<HeartbeatClientFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            var keepAliveFilter = DI.Get<KeepAliveClientFilter>();
            var codec = DI.Get<KnifeSocketCodec>();
            if (codec.SocketDecoder.GetType() != customSetting.Decoder)
                codec.SocketDecoder = (KnifeSocketDatagramDecoder) DI.Get(customSetting.Decoder);
            if (codec.SocketEncoder.GetType() != customSetting.Encoder)
                codec.SocketEncoder = (KnifeSocketDatagramEncoder) DI.Get(customSetting.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            if (protocolFamily.CommandParser.GetType() != customSetting.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(customSetting.CommandParser);

            _Client = DI.Get<IKnifeSocketClient>();
            _Client.Config = config;
            if (customSetting.NeedHeartBeat)
                _Client.AddFilters(heartbeatServerFilter);
            _Client.AddFilters(keepAliveFilter);
            _Client.Configure(customSetting.IpAddress, customSetting.Port);
            _Client.Bind(codec, protocolFamily);
            _Client.AddHandlers(handler);

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