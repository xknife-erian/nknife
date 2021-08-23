using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Util.Serial;

namespace NKnife.SerialBox
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

            Task.Factory.StartNew(SerialHelper.RefreshSerialPorts);

            Application.Run(new Workbench());
        }
    }
}
