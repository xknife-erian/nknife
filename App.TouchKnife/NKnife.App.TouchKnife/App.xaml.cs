using System.Windows;
using NKnife.App.TouchKnife.Core;
using NKnife.IoC;

namespace NKnife.App.TouchKnife
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DI.Initialize();

            base.OnStartup(e);

            var notify = DI.Get<Notify>();
            notify.Show();
        }
    }
}
