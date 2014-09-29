using System.Windows;
using NKnife.App.TouchIme.TouchInput.Common;
using NKnife.Ioc;

namespace NKnife.App.TouchIme.TouchInput
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
