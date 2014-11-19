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
        public AsyncObservableCollection<SocketMessage> SocketMessages { get; set; }

        private bool _IsInitialized = false;
        private IKnifeSocketClient _Client;

        public DemoClient()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public void Initialize(KnifeSocketConfig config, SocketCustomSetting customSetting)
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
            _Client.AddHandlers(new DemoClientHandler(SocketMessages));

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