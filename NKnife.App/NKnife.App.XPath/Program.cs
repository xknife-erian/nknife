using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gean.Client.XPathTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();
            if (args.Length > 0)
            {
                form._XmlFileName = args[0];
            }
            Application.Run((Form)form);
        }

    }
}
