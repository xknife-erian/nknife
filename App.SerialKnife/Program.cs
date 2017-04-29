using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using NKnife;
using NKnife.IoC;
using SocketKnife.Views;

namespace SocketKnife
{
    class Program
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
            LogManager.GetLogger<Program>();

            Kernel.Initialize();
            Application.Run(new Workbench());
        }
    }
}
