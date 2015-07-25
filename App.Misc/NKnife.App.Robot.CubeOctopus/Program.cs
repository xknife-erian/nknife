using System;
using System.Windows.Forms;
using NKnife.Tools.CubeOctopus;

namespace NKnife.Tools.Robot.CubeOctopus
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
            Application.Run(new CubeExplorerForm());
            //Application.Run(new DistinguishCubeSurfaceForm());
        }
    }
}
