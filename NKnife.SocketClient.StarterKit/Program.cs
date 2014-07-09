using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NKnife.Configuring.CoderSetting;
using NKnife.Ioc;
using NKnife.SocketClient.StarterKit;
using NLog;


namespace NKnife.SocketClientTestTool
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

            CoderSettingXmlFile[] fileList = CoderSettingService.GetOptionFiles();
            CoderSettingService.ME.Initializes(fileList);

            Application.Run(new MainForm());

        }
    }
}
