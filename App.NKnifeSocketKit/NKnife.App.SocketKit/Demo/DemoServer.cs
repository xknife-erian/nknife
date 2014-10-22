using System.Diagnostics;
using System.Net;
using System.Windows.Threading;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Demo.Protocols;
using NKnife.Base;
using NKnife.Collections;
using NKnife.IoC;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Demo
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

        private IKnifeSocketServer _Server;

        public DemoServer()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public void Initialize(IPAddress ipAddress, int port, KnifeSocketServerConfig config, SocketTools socketTools)
        {
            Pair<IPAddress, int> key = Pair<IPAddress, int>.Build(ipAddress, port);
            _ServerListKey = key;

            var heartbeatServerFilter = DI.Get<HeartbeatServerFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            var keepAliveFilter = DI.Get<KeepAliveServerFilter>();
            var codec = DI.Get<KnifeSocketCodec>();
            codec.SocketDecoder = (KnifeSocketDatagramDecoder)DI.Get(socketTools.Decoder);
            codec.SocketEncoder = (KnifeSocketDatagramEncoder)DI.Get(socketTools.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(socketTools.CommandParser);

            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<IKnifeSocketServer>();
                _Server.Config = config;
                if (socketTools.NeedHeartBeat)
                    _Server.AddFilter(heartbeatServerFilter);
                _Server.AddFilter(keepAliveFilter);
                _Server.Configure(ipAddress, port);
                _Server.Bind(codec, protocolFamily, new DemoServerHandler(SocketMessages));
                _ServerList.Add(key, _Server);
            }
            else
            {
                Debug.Fail("不应出现相同IP与端口的服务器请求。");
            }
        }

        private StringProtocolFamily GetProtocolFamily()
        {
            var register = DI.Get<Register>();

            var family = DI.Get<StringProtocolFamily>();
            family.Family = "socket-kit";

            var custom = DI.Get<StringProtocol>("TestCustom");
            custom.Family = family.Family;
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