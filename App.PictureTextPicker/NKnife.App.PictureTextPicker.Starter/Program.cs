using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;

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

            DI.Initialize();

            ILog logger = LogManager.GetCurrentClassLogger();
            logger.Info("NLog日志组志启用成功.......");

            Application.Run(new MainForm());
        }
    }
}
