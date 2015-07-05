using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NKnife.IoC;

namespace NKnife.Kits.SocketKnife.StressTest
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

            Global.Culture = "zh-CN";
            DI.Initialize();

            Application.Run(new MainForm());
        }
    }
}
