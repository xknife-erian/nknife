using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Dialogs;
using NKnife.Base;
using NKnife.IoC;
using NKnife.Mvvm;
using SocketKnife.Interfaces;

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
            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<ISocketServerKnife>();
                _Server.Config.Initialize(1000, 1000, 1024*10, 32, 1024*10);
                _Server.Bind(ipAddress, port);
                _ServerList.Add(key, _Server);
            }
            else
            {
                Debug.Fail("不应出现相同IP与端口的服务器请求。");
            }
        }

    }
}
