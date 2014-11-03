using System;
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

            _SessionDataGrid.ItemsSource = _ViewModel.Sessions;

            _OnlyOnceRadioButton.Checked += ReplayModeRadioButtonOnClick;
            _FixTimeRadioButton.Checked += ReplayModeRadioButtonOnClick;
            _RandomRadioButton.Checked += ReplayModeRadioButtonOnClick;
        }

        private void ReplayModeRadioButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_FixTimeRadioButton.IsChecked != null)
                _FixTimeTextBox.IsEnabled = (bool) _FixTimeRadioButton.IsChecked;
            if (_RandomRadioButton.IsChecked != null)
                _RandomMinTimeTextBox.IsEnabled = (bool) _RandomRadioButton.IsChecked;
            if (_RandomRadioButton.IsChecked != null)
                _RandomMaxTimeTextBox.IsEnabled = (bool) _RandomRadioButton.IsChecked;
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
