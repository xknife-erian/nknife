using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Server
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

            ///初始化
            Manager.Initialize();
            Application.Run(new Form1());
        }
    }
}
