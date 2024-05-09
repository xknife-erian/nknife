using System;
using System.Windows.Forms;
using Autofac;
using NKnife.Win.Forms.Kit.IoC;

namespace NKnife.Win.Forms.Kit
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterModule<Modules>();

            using (var container = builder.Build())
            {
                var startup = container.Resolve<LibraryDemoWorkbench>();
                //开启当前程序作用域下的 ApplicationContext 实例
                Application.Run(startup);
            }
        }
    }
}
