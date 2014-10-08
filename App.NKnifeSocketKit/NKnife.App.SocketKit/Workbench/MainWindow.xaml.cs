using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NKnife.Adapters;
using NKnife.App.SocketKit.Dialogs;
using NKnife.App.SocketKit.Views;
using NKnife.Interface;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Themes;

namespace NKnife.App.SocketKit.Workbench
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILogger _Logger = LogFactory.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            var pane = _DockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (pane != null)
            {
                _Documents = pane.Children;
            }
            else
            {
                Debug.Fail("未找到文档面板。");
            }
        }

        private readonly ObservableCollection<LayoutContent> _Documents; 

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ServerCreatorMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var win = new NewTcpServerCreatorDialog();
            var drs = win.ShowDialog(this);
            if (drs != null && (bool)drs)
            {
                var view = new TcpServerView(win.IpAddress, win.Port);
                _Documents.Add(view);
            }
        }

        private void ClientCreatorMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var view = new TcpClientView();
            _Documents.Add(view);
        }

        private void OptionMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("OptionMenuItem_Click");
        }

        private void ParamsViewMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("ParamsViewMenuItem_Click");
        }

        private void PropertiesViewMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("PropertiesViewMenuItem_Click");
        }

        private void LoggerMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var view = new LoggerView();
            _Documents.Add(view);
            for (int i = 0; i < 100; i++)
            {
                _Logger.Debug(i.ToString());
            }
        }

        private void AboutMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("AboutMenuItem_Click");
        }

        public static void ChangeTheme(DockingManager dockingManager, ThemeStyle themeStyle)
        {
            #region 设置控件背景的样式

            Xceed.Wpf.AvalonDock.Themes.Theme theme = null;
            switch (themeStyle)
            {
                case ThemeStyle.Aero:
                    theme = new AeroTheme();
                    break;
                case ThemeStyle.ExpressionDark:
                    theme = new ExpressionDarkTheme();
                    break;
                case ThemeStyle.ExpressionLight:
                    theme = new ExpressionLightTheme();
                    break;
                case ThemeStyle.Metro:
                    theme = new MetroTheme();
                    break;
                case ThemeStyle.Vs2010:
                default:
                    theme = new VS2010Theme();
                    break;
            }
            dockingManager.Theme = theme;

            #endregion
        }
    }

    public enum ThemeStyle
    {
        Aero,
        ExpressionDark,
        ExpressionLight,
        Metro,
        Vs2010
    }
}
