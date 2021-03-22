using System;
using System.Windows.Forms;
using Autofac;
using NKnife.Channels.SerialKnife.IoC;
using NKnife.Channels.SerialKnife.Views;
using NLog;

namespace NKnife.Channels.SerialKnife
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NKnife.NLog.WinForm.NLogConfigFileSimpleCreate.Load();
            LogManager.GetCurrentClassLogger();

            Kernel.Initialize();

            var builder = new ContainerBuilder();
            builder.RegisterModule<Modules>();
            builder.RegisterModule<ViewModelModules>();

            using (var container = builder.Build())
            {
                var startup = container.Resolve<Workbench>();
                //开启当前程序作用域下的 ApplicationContext 实例
                Application.Run(startup);
                container.Dispose();
            }
        }
    }
}
