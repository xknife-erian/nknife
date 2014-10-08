using System.Collections.ObjectModel;
using NKnife.IoC;
using NKnife.NLog3.Logging.LoggerWPFControl;

namespace NKnife.App.SocketKit.Mvvm.Views
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
