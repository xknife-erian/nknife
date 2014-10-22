using System.Windows;
using NKnife.App.SocketKit.Common;
using NKnife.App.SocketKit.Demo;
using SocketKnife.Generic;

namespace NKnife.App.SocketKit.Mvvm.Views
{

    /// <summary>
    /// TcpServerView.xaml 的交互逻辑
    /// </summary>
    public partial class TcpClientView
    {
        private readonly DemoClient _ViewModel;

        public KnifeSocketConfig ServerConfig { get; set; }
        public SocketTools SocketTools { get; set; }

        public TcpClientView()
        {
            InitializeComponent();
            Title = "SocketKnife客户端";
            _ViewModel = new DemoClient();
            _View.DataContext = _ViewModel;
            _ViewModel.Dispatcher = Dispatcher;
        }

        private void View_OnLoaded(object sender, RoutedEventArgs e)
        {
            _ViewModel.Initialize(ServerConfig, SocketTools);
        }

        private void DataGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            _ViewModel.Start();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _ViewModel.Stop();
        }
    }
}
