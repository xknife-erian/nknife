using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Server.Manager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                ServerStart serverStart = new ServerStart();
                serverStart.Run();
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            }
            catch(Exception ex)
            {
                ShowException(ex);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowException(e.Exception);
        }

        static void ShowException(Exception ex)
        {
            string msg = "出现未知异常:" + ex.Message +
                "\r\n\r\n堆栈信息:\r\n" + ex.StackTrace + "\r\n";
            MessageBox.Show(msg, "未处理异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
