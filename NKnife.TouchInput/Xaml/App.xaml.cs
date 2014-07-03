using System.Windows;
using System.Windows.Navigation;
using NKnife.Ioc;

namespace NKnife.TouchInput.Xaml
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DI.Initialize();
        }
    }
}
