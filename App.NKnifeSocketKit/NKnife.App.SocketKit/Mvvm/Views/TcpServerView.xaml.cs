using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using NKnife.App.SocketKit.Dialogs;
using NKnife.App.SocketKit.Mvvm.ViewModels;
using NKnife.Base;
using NKnife.IoC;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Mvvm.Views
{
    /// <summary>
    /// TcpServerView.xaml 的交互逻辑
    /// </summary>
    public partial class TcpServerView
    {
        private readonly ServerList _ServerList = DI.Get<ServerList>();
        private readonly ISocketServerKnife _Server;

        public TcpServerView(IPAddress ipAddress, int port)
        {
            InitializeComponent();
            _DataGrid.DataContext = new TcpServerViewViewModel();
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

        private void Start(object sender, RoutedEventArgs e)
        {
            var c = (Button) sender;
            c.IsEnabled = !(c.IsEnabled);
            _Server.Start();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            _Server.Start();
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            _Server.ReStart();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _Server.Stop();
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            _Server.Stop();
            base.OnClosing(args);
        }
    }
}
