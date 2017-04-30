using System;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Channels.SerialKnife.Views;
using NKnife.IoC;

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

            DI.Initialize();
            LogManager.GetLogger<Program>();

            Kernel.Initialize();
            Application.Run(new Workbench());
        }
    }
}
