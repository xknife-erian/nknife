using System;
using System.Windows.Forms;

namespace NKnife.App.XPath
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new MainForm();
            if (args.Length > 0)
            {
                form._XmlFileName = args[0];
            }
            Application.Run(form);
        }
    }
}