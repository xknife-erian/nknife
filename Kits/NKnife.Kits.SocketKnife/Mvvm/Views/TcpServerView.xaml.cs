using System.ComponentModel;
using System.Windows;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo;
using NKnife.Kits.SocketKnife.Dialogs;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    /// <summary>
    /// TcpServerView.xaml 的交互逻辑
    /// </summary>
    public partial class TcpServerView
    {
        private readonly DemoServer _ViewModel;

        public KnifeSocketConfig Config { get; set; }
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
            _ViewModel.Initialize(Config, SocketTools);
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            _ViewModel.RemoveServer();
            _ViewModel.StopServer();
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


        private void _BuildProtocolButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ProtocolEditorDialog();
            dialog.Show();
        }
    }
}
