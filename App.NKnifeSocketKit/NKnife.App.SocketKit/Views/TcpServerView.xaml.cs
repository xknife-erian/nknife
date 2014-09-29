using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NKnife.App.SocketKit.Dialogs;
using NKnife.Base;
using NKnife.Ioc;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Views
{
    /// <summary>
    /// TcpServerView.xaml 的交互逻辑
    /// </summary>
    public partial class TcpServerView
    {
        private ServerList _ServerList = DI.Get<ServerList>();
        private Server _Server;

        public TcpServerView(IPAddress ipAddress, int port)
        {
            InitializeComponent();
            Title = "SocketKnife服务端";

            var key = Pair<IPAddress, int>.Build(ipAddress, port);
            if (!_ServerList.ContainsKey(key))
            {
                _Server = new Server();
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
