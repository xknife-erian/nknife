using System;
using System.Windows.Forms;
using NKnife.Configuring.CoderSetting;
using NKnife.Ioc;
using NLog;

namespace NKnife.Socket.StarterKit
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
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("初始化...");

            Application.Run(new MainForm());

        }
    }
}
