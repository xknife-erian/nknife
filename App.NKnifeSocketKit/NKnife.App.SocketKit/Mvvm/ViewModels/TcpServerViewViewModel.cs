using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Windows.Threading;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Dialogs;
using NKnife.App.SocketKit.IoC;
using NKnife.App.SocketKit.Socket;
using NKnife.Base;
using NKnife.Collections;
using NKnife.IoC;
using NKnife.Mvvm;
using NKnife.Protocol;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;
using SocketKnife.Generic.Filters;
using SocketKnife.Generic.Protocols;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Mvvm.ViewModels
{
    public class TcpServerViewViewModel : NotificationObject
    {
        internal Dispatcher Dispatcher { get; set; }
        public AsyncObservableCollection<SocketMessage> SocketMessages { get; set; }
        private readonly ServerList _ServerList = DI.Get<ServerList>();

        private IKnifeSocketServer _Server;

        public TcpServerViewViewModel()
        {
            SocketMessages = new AsyncObservableCollection<SocketMessage>();
        }

        public void Initialize(IPAddress ipAddress, int port)
        {
            var key = Pair<IPAddress, int>.Build(ipAddress, port);

            var heartbeatServerFilter = DI.Get<HeartbeatServerFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            var keepAliveFilter = DI.Get<KeepAliveServerFilter>();
            keepAliveFilter.ClientCome += args => true;

            var protocolFamily = GetProtocolFamily();

            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<IKnifeSocketServer>();
                _Server.Config.Initialize(1000, 1000, 1024*10, 32, 1024*10);
                _Server.AddFilter(heartbeatServerFilter);
                _Server.AddFilter(keepAliveFilter);
                _Server.Configure(ipAddress, port);
                _Server.Bind(protocolFamily, new DemoServerHandler(SocketMessages));
                _ServerList.Add(key, _Server);
            }
            else
            {
                Debug.Fail("不应出现相同IP与端口的服务器请求。");
            }
        }

        private IProtocolFamily GetProtocolFamily()
        {
            var getTicket = DI.Get<GetTicket>();
            var call = DI.Get<Call>();
            var dance = DI.Get<Dance>();
            var recall = DI.Get<ReCall>();
            var sing = DI.Get<Sing>();

            var register = DI.Get<Register>();
            register.Packager = new ProtocolXmlPackager();
            register.UnPackager = new ProtocolDataTableDeserializeUnPackager();

            var family = DI.Get<IProtocolFamily>();
            family.CommandParser = new FirstFieldCommandParser();
            family.Decoder = new FixedTailDecoder();
            family.Encoder = new FixedTailEncoder();

            family.Add(getTicket);
            family.Add(call);
            family.Add(dance);
            family.Add(recall);
            family.Add(sing);
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
