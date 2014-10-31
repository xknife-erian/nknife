using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Dialogs;
using NKnife.Kits.SocketKnife.Mvvm.Views;
using SocketKnife.Generic;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using Xceed.Wpf.AvalonDock.Themes;

namespace NKnife.Kits.SocketKnife.Mvvm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Workbench : Window
    {
        private readonly ILogger _Logger = LogFactory.GetCurrentClassLogger();

        public Workbench()
        {
            InitializeComponent();

            DI.Get<DockUtil>().Init(_DockingManager);
            DI.Get<DockUtil>().Buttom.Children.Add(new LoggerView());
            _RightPane.Children.Add(new ProtocolsView());

            _Logger.Info("主窗体构造完成");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var docs = DI.Get<DockUtil>().Documents;
            while (docs.Count > 0)
            {
                docs[0].Close();
            }

            base.OnClosing(e);

            var serializer = new XmlLayoutSerializer(_DockingManager);
            using (var stream = new StreamWriter(string.Format(@".\AvalonDock.config")))
                serializer.Serialize(stream);
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ServerCreatorMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var win = new NewTunnelCreatorDialog();
            win.Config = DI.Get<KnifeSocketConfig>("Server");
            win.Title = "新建Socket服务器";
            win.IpAddressLabel = "本地IpAddress:";
            var drs = win.ShowDialog(this);
            if (drs != null && (bool)drs)
            {
                var view = new TcpServerView();
                view.Config = win.Config;
                view.SocketTools = win.SocketTools;
                view.Title = string.Format("Server: {0}", win.SocketTools.Port);
                _Logger.Info(string.Format("用户交互创建Server:{0},{1}", win.SocketTools.IpAddress, win.SocketTools.Port));
                DI.Get<DockUtil>().Documents.Add(view);
            }
        }

        private void ClientCreatorMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var win = new NewTunnelCreatorDialog();
            win.Config = DI.Get<KnifeSocketConfig>("Client");
            win.Title = "新建Socket客户端";
            win.IpAddressLabel = "远程IpAddress:";
            var drs = win.ShowDialog(this);
            if (drs != null && (bool)drs)
            {
                var view = new TcpClientView();
                view.Config = win.Config;
                view.SocketTools = win.SocketTools;
                view.Title = string.Format("Client: {0}", new IPEndPoint(win.SocketTools.IpAddress, win.SocketTools.Port));
                _Logger.Info(string.Format("用户交互创建Client:{0},{1}", win.SocketTools.IpAddress, win.SocketTools.Port));
                DI.Get<DockUtil>().Documents.Add(view);
            }
        }

        private void OptionMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("OptionMenuItem_Click");
        }

        private void AboutMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new ProtocolEditorDialog();
            dialog.ShowDialog(this);
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
