using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private readonly TcpServerViewModel _viewModel;

        public SocketConfig Config { get; set; }
        internal SocketCustomSetting CustomSetting { get; set; }

        public TcpServerView()
        {
            InitializeComponent();
            _viewModel = new TcpServerViewModel();
            _MainGrid.DataContext = _viewModel;

            _SessionDataGrid.ItemsSource = _viewModel.Sessions;

            _OnlyOnceRadioButton.Checked += ReplayModeRadioButtonOnClick;
            _FixTimeRadioButton.Checked += ReplayModeRadioButtonOnClick;
            _RandomTimeRadioButton.Checked += ReplayModeRadioButtonOnClick;
        }

        private void ReplayModeRadioButtonOnClick(object sender, RoutedEventArgs routedEventArgs)//切换回复模式时界面的变化
        {
            if (_FixTimeRadioButton.IsChecked != null)
                _FixTimeTextBox.IsEnabled = (bool) _FixTimeRadioButton.IsChecked;
            if (_RandomTimeRadioButton.IsChecked != null)
                _RandomMinTimeTextBox.IsEnabled = (bool)_RandomTimeRadioButton.IsChecked;
            if (_RandomTimeRadioButton.IsChecked != null)
                _RandomMaxTimeTextBox.IsEnabled = (bool)_RandomTimeRadioButton.IsChecked;
        }

        private void View_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            _viewModel.StopServer();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = false;
            _StopButton.IsEnabled = true;
            _viewModel.StartServer(Config, CustomSetting);
            _StartReplayButton.IsEnabled = true;
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            _StartButton.IsEnabled = true;
            _StopButton.IsEnabled = false;
            _viewModel.StopServer();
            _StartReplayButton.IsEnabled = false;
            _StopReplayButton.IsEnabled = false;
        }

        private void _BuildProtocolButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ProtocolEditorDialog();
            dialog.Closed += ProtocolEditorDialog_Closed;
            dialog.ShowDialog();
        }

        void ProtocolEditorDialog_Closed(object sender, EventArgs e)
        {
            var dialog = sender as ProtocolEditorDialog;
            if (dialog != null && dialog.CurrentProtocol != null)
            {
                _viewModel.CurrentProtocol = dialog.CurrentProtocol;
            }
        }

        private void _SelectAllClientCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((CheckBox) sender).IsChecked;
            foreach (var session in _viewModel.Sessions)
            {
                if (isChecked != null) 
                    session.IsSelected = (bool) isChecked;
            }
        }

        private void _StopReplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            _StopReplayButton.IsEnabled = false;
            _StartReplayButton.IsEnabled = true;
            _viewModel.StopReplay();
        }

        private void _StartReplayButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsFixTime || _viewModel.IsRandomTime)
            {
                _StartReplayButton.IsEnabled = false;
                _StopReplayButton.IsEnabled = true;
            }
            _viewModel.Replay();
        }
    }
}
