using System;
using System.Windows.Forms;
using NKnife.IoC;

namespace NKnife.Kits.WinFormUIKit
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

            Application.Run(new LibraryDemoWorkbench());
        }
    }
}
