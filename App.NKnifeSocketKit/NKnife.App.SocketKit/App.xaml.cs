using System.Windows;
using NKnife.Ioc;
using NKnife.NLog3.Ioc;

namespace NKnife.App.SocketKit
{
    /// <summary>
    ///     App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NLogModules.Style = NLogModules.AppStyle.WPF;
            DI.Initialize();
        }
    }
}