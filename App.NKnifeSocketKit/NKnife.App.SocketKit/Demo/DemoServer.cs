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

        private readonly ServerList _ServerList = DI.Get<ServerList>();
        internal Dispatcher Dispatcher { get; set; }
        public AsyncObservableCollection<SocketMessage> SocketMessages { get; set; }

        #endregion

        private IKnifeSocketServer _Server;

        public DemoServer()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public void Initialize(IPAddress ipAddress, int port)
        {
            Pair<IPAddress, int> key = Pair<IPAddress, int>.Build(ipAddress, port);

            var config = DI.Get<KnifeSocketServerConfig>();
            config.Initialize(1000, 1000, 1024 * 10, 32, 1024 * 10);

            var heartbeatServerFilter = DI.Get<HeartbeatServerFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            var keepAliveFilter = DI.Get<KeepAliveServerFilter>();
            var codec = DI.Get<KnifeSocketCodec>();
            StringProtocolFamily protocolFamily = GetProtocolFamily();

            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<IKnifeSocketServer>();
                _Server.Config = config;
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

            family.Add(StringProtocol.Build("socket-kit", "call"));
            family.Add(StringProtocol.Build("socket-kit", "recall"));
            family.Add(StringProtocol.Build("socket-kit", "sing"));
            family.Add(StringProtocol.Build("socket-kit", "dance"));
            family.Add(register);

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