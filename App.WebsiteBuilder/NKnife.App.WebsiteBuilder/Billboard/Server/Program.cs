using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Jeelu.Billboard.Server
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            InitializeApplication.InitializeBeforeRun();
            Application.Run(new MainManagerForm());
        }
    }
}
