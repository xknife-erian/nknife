using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Dialogs;
using NKnife.App.SocketKit.Socket;
using NKnife.Base;
using NKnife.IoC;
using NKnife.Mvvm;
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
        public ObservableCollection<SocketMessage> SocketMessages { get; set; }
        private readonly ServerList _ServerList = DI.Get<ServerList>();
        private IKnifeSocketServer _Server;

        public TcpServerViewViewModel()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
        }

        public void Initialize(IPAddress ipAddress, int port)
        {
            var key = Pair<IPAddress, int>.Build(ipAddress, port);

            var keepAliveFilter = new KeepAliveServerFilter();

            var protocolFamily = GetProtocolFamily();

            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<IKnifeSocketServer>();
                _Server.Config.Initialize(1000, 1000, 1024*10, 32, 1024*10);
                _Server.AddFilter(keepAliveFilter);
                _Server.Configure(ipAddress, port);
                _Server.Bind(protocolFamily, new MyHandler(SocketMessages));
                _Server.Attach(new HeartbeatPlan());
                _ServerList.Add(key, _Server);
            }
            else
            {
                Debug.Fail("不应出现相同IP与端口的服务器请求。");
            }
        }

        private static IProtocolFamily GetProtocolFamily()
        {
            var getTicket = DI.Get<GetTicket>();
            var call = DI.Get<Call>();
            var dance = DI.Get<Dance>();
            var recall = DI.Get<ReCall>();
            var sing = DI.Get<Sing>();
            var register = DI.Get<Register>();


            var family = DI.Get<IProtocolFamily>();
            family.CommandParser = new FirstFieldCommandParser();
            family.Decoder = new FixedTailDecoder();
            family.Encoder = new DatagramEncoder();

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
            for (int i = 0; i < 100; i++)
            {
                SocketMessages.Add(new SocketMessage()
                {
                    Message = Guid.NewGuid().ToString(),
                    SocketDirection = SocketDirection.Send,
                    Time = DateTime.Now.ToLongTimeString()
                });
            }
        }

        public void PauseServer()
        {
            _Server.Stop();
        }

        public void StopServer()
        {
            _Server.Stop();
        }
    }

    internal class MyHandler : KnifeProtocolHandler
    {
        private readonly ObservableCollection<SocketMessage> _SocketMessages;

        public MyHandler(ObservableCollection<SocketMessage> socketMessages)
        {
            _SocketMessages = socketMessages;
        }

        public override void Recevied(ISocketSession session, IProtocol protocol)
        {
            var msg = new SocketMessage();
            msg.Command = protocol.Command;
            msg.SocketDirection = SocketDirection.Receive;
            msg.Message = protocol.Generate();
            msg.Time = DateTime.Now.ToString("HH:mm:ss.fff");
            _SocketMessages.Add(msg);
        }
    }
}
