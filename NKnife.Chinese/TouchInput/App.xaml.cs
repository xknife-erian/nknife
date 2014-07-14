using System.Windows;
using NKnife.Ioc;

namespace NKnife.Chinese.TouchInput
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

            var notify = DI.Get<Notify>();
            notify.Show();
        }
    }
}
