using System;
using System.Windows.Forms;

namespace NKnife.NLog.WinForm.Example
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
            NLogConfigFileSimpleCreate.Load();
            Application.Run(new Workbench());
        }
    }
}
