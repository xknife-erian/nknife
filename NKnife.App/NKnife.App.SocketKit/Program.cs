using System;
using System.Windows.Forms;
using NKnife.Configuring;
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

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("初始化...");
            DI.Initialize();
            logger.Info("Ioc完成...");

            Application.Run(new DoorForm());
        }
    }
}
