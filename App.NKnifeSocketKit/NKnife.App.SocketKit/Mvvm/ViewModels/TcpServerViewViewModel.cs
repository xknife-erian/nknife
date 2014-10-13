using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Dialogs;
using NKnife.Base;
using NKnife.IoC;
using NKnife.Mvvm;
using SocketKnife.Generic;
using SocketKnife.Interfaces;
using SocketKnife.Protocol;
using SocketKnife.Protocol.Interfaces;

namespace NKnife.App.SocketKit.Mvvm.ViewModels
{
    public class TcpServerViewViewModel : NotificationObject
    {
        public ObservableCollection<SocketMessage> SocketMessages { get; set; }
        private readonly ServerList _ServerList = DI.Get<ServerList>();
        private ISocketServerKnife _Server;

        public TcpServerViewViewModel()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
        }

        public void Initialize(IPAddress ipAddress, int port)
        {
            var key = Pair<IPAddress, int>.Build(ipAddress, port);

            var filter = new DefaultFilter();

            var protocolFamily = DI.Get<IProtocolFamily>();
            protocolFamily.Add("", typeof(IProtocol));

            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<ISocketServerKnife>();
                _Server.Config.Initialize(1000, 1000, 1024*10, 32, 1024*10);
                _Server.Policy.AddLast(filter);
                _Server.Bind(ipAddress, port);
                _Server.Attach(protocolFamily);
                _Server.Attach(new HeartbeatPlan());
                _ServerList.Add(key, _Server);
            }
            else
            {
                Debug.Fail("不应出现相同IP与端口的服务器请求。");
            }
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
}
