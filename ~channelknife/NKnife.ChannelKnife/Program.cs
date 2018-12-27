using System;
using System.Windows.Forms;
using Common.Logging;
using NKnife.ChannelKnife.Views;
using NKnife.IoC;
using NKnife.Wrapper;

namespace NKnife.ChannelKnife
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
            DI.Initialize();
            LogManager.GetLogger<Workbench>().Info("DI.Initialize complete.");
            Application.Run(DI.Get<Workbench>());
        }
    }

    public class AboutMe : About
    {
    }
}
