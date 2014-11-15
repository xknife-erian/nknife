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
        private readonly ProtocolViewModel _ViewModel = DI.Get<ProtocolViewModel>();

        public ProtocolsView()
        {
            InitializeComponent();
            _MainGrid.DataContext = _ViewModel;
            _ProtocolsGrid.ItemsSource = _ViewModel.Protocols;
        }
    }
}
