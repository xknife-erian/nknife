﻿using System.ComponentModel;
using System.Windows;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{

    /// <summary>
    /// TcpServerView.xaml 的交互逻辑
    /// </summary>
    public partial class TcpClientView
    {
        private readonly TcpClientViewModel _viewModel;

        public SocketConfig Config { get; set; }
        internal SocketCustomSetting CustomSetting { get; set; }

        public TcpClientView()
        {
            InitializeComponent();
            Title = "SocketKnife客户端";
            _viewModel = new TcpClientViewModel();
            _View.DataContext = _viewModel;
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            _viewModel.StopClient();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = false;
            _StopButton.IsEnabled = true;
            _viewModel.StartClient(Config, CustomSetting);
            _StartReplayButton.IsEnabled = true;
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = true;
            _StopButton.IsEnabled = false;

            _viewModel.StopClient();
        }

        private void _BuildProtocolButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _SelectAllClientCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _StopReplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void _StartReplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
