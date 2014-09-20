using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NKnife.Ioc;
using NLog;

namespace NKnife.App.PictureTextPicker.Starter
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

            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("NLog日志组志启用成功.");

            DI.Initialize();
            logger.Info("Ninject初始化完成.");

            Application.Run(new Form1());
        }
    }
}
