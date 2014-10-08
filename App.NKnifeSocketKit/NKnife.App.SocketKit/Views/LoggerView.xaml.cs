using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Ioc;
using NKnife.NLog3.Logging.LoggerWPFControl;

namespace NKnife.App.SocketKit.Views
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class LoggerView
    {

        public LoggerView()
        {
            InitializeComponent();
            _LoggerGrid.ItemsSource = DI.Get<ObservableCollection<LogMessage>>();
        }

    }
}
