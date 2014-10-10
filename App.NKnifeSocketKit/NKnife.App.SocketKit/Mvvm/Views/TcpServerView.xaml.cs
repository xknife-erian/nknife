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
        private readonly TcpServerViewViewModel _ViewModel = new TcpServerViewViewModel();

        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }


        public TcpServerView()
        {
            InitializeComponent();
            _View.DataContext = _ViewModel;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var c = (Button) sender;
            c.IsEnabled = !(c.IsEnabled);
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
        }

        private void View_OnLoaded(object sender, RoutedEventArgs e)
        {
            _ViewModel.Initialize(IpAddress, Port);
        }
    }
}
