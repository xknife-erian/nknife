using System.Windows;
using NKnife.IoC;
using NKnife.NLog.IoC;

namespace NKnife.Kits.SocketKnife
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NLogModules.Style = NLogModules.AppStyle.WPF;
            DI.Initialize();

//            var properties = new NameValueCollection();
//            properties["configType"] = "FILE";
//            properties["configFile"] = "~/NLog.config";//string.Format("~/{0}", CONFIG_FILE_NAME);
//            LogManager.Adapter = new NLogLoggerFactoryAdapter(properties);
        }
    }
}
