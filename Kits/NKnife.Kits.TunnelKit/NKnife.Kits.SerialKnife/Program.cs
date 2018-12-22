using System;
using System.Windows.Forms;
using NKnife.IoC;

namespace NKnife.Kits.SerialKnife
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
            Di.Initialize();

            Application.Run(new WorkBenchForm());
        }
    }
}
