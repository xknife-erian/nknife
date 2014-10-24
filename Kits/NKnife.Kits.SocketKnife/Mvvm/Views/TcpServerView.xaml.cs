using System.ComponentModel;
using System.Windows;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    /// <summary>
    /// TcpServerView.xaml 的交互逻辑
    /// </summary>
    public partial class TcpServerView
    {
        private readonly DemoServer _ViewModel;

        public KnifeSocketServerConfig ServerConfig { get; set; }
        public SocketTools SocketTools { get; set; }

        public TcpServerView()
        {
            InitializeComponent();
            _ViewModel = new DemoServer();
            _View.DataContext = _ViewModel;
            _ViewModel.Dispatcher = Dispatcher;
        }

        private void View_OnLoaded(object sender, RoutedEventArgs e)
        {
            _ViewModel.Initialize(ServerConfig, SocketTools);
        }
        
        private void TcpServerView_OnClosing(object sender, CancelEventArgs e)
        {
            _ViewModel.RemoveServer();
        }

        private void DataGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            //TODO:想在这里尝试做一下最后一列的自适应宽度,未成功
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = false;
            _StopButton.IsEnabled = true;

            _ViewModel.StartServer();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = true;
            _StopButton.IsEnabled = false;

            _ViewModel.StopServer();
        }


    }
}
