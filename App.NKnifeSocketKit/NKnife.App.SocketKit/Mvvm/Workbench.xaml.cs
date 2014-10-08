using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NKnife.Adapters;
using NKnife.App.SocketKit.Dialogs;
using NKnife.App.SocketKit.Mvvm.Views;
using NKnife.Interface;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Themes;

namespace NKnife.App.SocketKit.Mvvm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Workbench : Window
    {
        private readonly ILogger _Logger = LogFactory.GetCurrentClassLogger();
        private readonly ObservableCollection<LayoutContent> _Documents; 

        public Workbench()
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
            _Logger.Info("主窗体构造完成");
        }

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
                _Logger.Info(string.Format("用户交互创建Server:{0},{1}", win.IpAddress, win.Port));
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
            //_Documents.Add(view);
            var bottomAnchorGroup = _DockingManager.Layout.BottomSide.Children.FirstOrDefault();
            if (bottomAnchorGroup == null)
            {
                bottomAnchorGroup = new LayoutAnchorGroup();
                _DockingManager.Layout.BottomSide.Children.Add(bottomAnchorGroup);
            }

            bottomAnchorGroup.Children.Add(view);
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

        private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
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
