using System;
using System.Collections.Generic;
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

            _Server = new Server();
            _Server.Bind(ipAddress, port);
//            _Server.GetConfig.SetReadBufferSize(2048);
//            _Server.Start();
//            _Server.ReStart();
//            _Server.Stop();
            _ServerList.Add(Pair<IPAddress, int>.Build(ipAddress, port), _Server);
        }


    }
}
