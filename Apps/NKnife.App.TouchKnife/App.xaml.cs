using System.Windows;
using NKnife.App.TouchInputKnife.Core;
using NKnife.IoC;

namespace NKnife.App.TouchInputKnife
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
