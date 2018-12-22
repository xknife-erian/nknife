using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NKnife.IoC;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    /// <summary>
    /// ProtocolsView.xaml 的交互逻辑
    /// </summary>
    public partial class ProtocolsView
    {
        private readonly ProtocolViewModel _viewModel = Di.Get<ProtocolViewModel>();

        public ProtocolsView()
        {
            InitializeComponent();
            _MainGrid.DataContext = _viewModel;
            _ProtocolsGrid.ItemsSource = _viewModel.Protocols;
        }

        private void ProtocolsGrid_OnSelected(object sender, RoutedEventArgs e)
        {
            var protocol = _ProtocolsGrid.SelectedItem as ProtocolViewModel.SimpleProtocol;
            if (protocol != null)
            {
                _viewModel.SelectedProtocol = protocol.Protocol;
            }
        }
    }
}
