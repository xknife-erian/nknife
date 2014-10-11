﻿using System.ComponentModel;
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
        private void DataGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            //TODO:想在这里尝试做一下最后一列的自适应宽度
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = false;
            _PauseButton.IsEnabled = true;
            _StopButton.IsEnabled = true;

            _ViewModel.StartServer();
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = true;
            _PauseButton.IsEnabled = false;
            _StopButton.IsEnabled = true;

            _ViewModel.PauseServer();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = true;
            _PauseButton.IsEnabled = false;
            _StopButton.IsEnabled = false;

            _ViewModel.StopServer();
        }

        private void View_OnLoaded(object sender, RoutedEventArgs e)
        {
            _ViewModel.Initialize(IpAddress, Port);
        }

    }
}
