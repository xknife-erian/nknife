using System.Diagnostics;
using System.Net;
using System.Windows.Threading;
using NKnife.Base;
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
    public class DemoServer : NotificationObject
    {
        #region App

        internal Dispatcher Dispatcher { get; set; }

        private readonly ServerList _ServerList = DI.Get<ServerList>();
        public AsyncObservableCollection<SocketMessage> SocketMessages { get; set; }

        private Pair<IPAddress, int> _ServerListKey;

        public void RemoveServer()
        {
            _ServerList.Remove(_ServerListKey);
        }

        #endregion

        private bool _IsInitialized = false;
        private IKnifeSocketServer _Server;

        public DemoServer()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public void Initialize(KnifeSocketServerConfig config, SocketTools socketTools)
        {
            if (_IsInitialized) return;

            Pair<IPAddress, int> key = Pair<IPAddress, int>.Build(socketTools.IpAddress, socketTools.Port);
            _ServerListKey = key;

            var heartbeatServerFilter = DI.Get<HeartbeatServerFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            var keepAliveFilter = DI.Get<KeepAliveServerFilter>();
            var codec = DI.Get<KnifeSocketCodec>();
            if (codec.SocketDecoder.GetType() != socketTools.Decoder)
                codec.SocketDecoder = (KnifeSocketDatagramDecoder) DI.Get(socketTools.Decoder);
            if (codec.SocketEncoder.GetType() != socketTools.Encoder)
                codec.SocketEncoder = (KnifeSocketDatagramEncoder) DI.Get(socketTools.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            if (protocolFamily.CommandParser.GetType() != socketTools.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(socketTools.CommandParser);

            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<IKnifeSocketServer>();
                _Server.Config = config;
                if (socketTools.NeedHeartBeat)
                    _Server.AddFilter(heartbeatServerFilter);
                _Server.AddFilter(keepAliveFilter);
                _Server.Configure(socketTools.IpAddress, socketTools.Port);
                _Server.Bind(codec, protocolFamily, new DemoServerHandler(SocketMessages));
                _ServerList.Add(key, _Server);
            }
            else
            {
                Debug.Fail(string.Format("不应出现相同IP与端口的服务器请求。{0}", key));
            }
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
            _Server.Start();
        }

        public void StopServer()
        {
            _Server.Stop();
        }

    }
}