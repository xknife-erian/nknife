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
        internal Dispatcher Dispatcher { get; set; }

        public AsyncObservableCollection<SocketMessage> SocketMessages { get; set; }

        private bool _IsInitialized = false;
        private IKnifeSocketClient _Client;

        public DemoClient()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public void Initialize(KnifeSocketConfig config, SocketTools socketTools)
        {
            if (_IsInitialized) return;

            var heartbeatServerFilter = DI.Get<HeartbeatServerFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            var keepAliveFilter = DI.Get<KeepAliveClientFilter>();
            var codec = DI.Get<KnifeSocketCodec>();
            if (codec.SocketDecoder.GetType() != socketTools.Decoder)
                codec.SocketDecoder = (KnifeSocketDatagramDecoder) DI.Get(socketTools.Decoder);
            if (codec.SocketEncoder.GetType() != socketTools.Encoder)
                codec.SocketEncoder = (KnifeSocketDatagramEncoder) DI.Get(socketTools.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            if (protocolFamily.CommandParser.GetType() != socketTools.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(socketTools.CommandParser);

            _Client = DI.Get<IKnifeSocketClient>();
            _Client.Config = config;
//            if (socketTools.NeedHeartBeat)
//                _Client.AddFilter(heartbeatServerFilter);
            _Client.AddFilter(keepAliveFilter);
            _Client.Configure(socketTools.IpAddress, socketTools.Port);
            _Client.Bind(codec, protocolFamily, new DemoClientHandler(SocketMessages));

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
            _Client.Start();
        }

        public void Stop()
        {
            _Client.Stop();
        }

    }
}