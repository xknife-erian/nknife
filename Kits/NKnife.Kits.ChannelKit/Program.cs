using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Channels.Serials;
using NKnife.IoC;
using NKnife.Kits.ChannelKit.View;
using NKnife.Wrapper;

namespace NKnife.Kits.ChannelKit
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
            LogManager.GetLogger<Workbench>().Info("DI.Initialize complete.");
            Application.Run(DI.Get<Workbench>());
        }
    }

    public class AboutMe : About
    {
    }
}
