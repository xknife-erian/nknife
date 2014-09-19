using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        /// <summary>
        /// 未知异常的处理
        /// </summary>
        public static class Exception
        {
            public static void ShowException(System.Exception exception)
            {
#if DEBUG
                System.Diagnostics.Debug.Assert(exception != null);

                ShowExceptionForm form = new ShowExceptionForm(exception);
                form.ShowDialog();
#else
                try
                {
                    string errorMsg = "异常时间:{0}\r\n异常类型:{1}\r\n异常信息:{2}\r\n堆栈信息:\r\n{3}\r\n\r\n";
                    errorMsg = string.Format(errorMsg, DateTime.Now, exception.GetType().FullName,
                        exception.Message, exception.StackTrace);
                    string fileName = Path.Combine(Application.StartupPath, "error.log");
                    File.AppendAllText(fileName, errorMsg,Encoding.UTF8);
                }
                catch { }
                MessageBox.Show("出现异常，程序将终止！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();

#endif
                //DialogResult result = MessageBox.Show("出现未知异常:\r\n" + exception.Message + "\r\n\r\n堆栈信息:\r\n" + exception.StackTrace,
                //    "异常", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);

                //switch (result)
                //{
                //    case DialogResult.Abort:
                //        Application.Exit();
                //        break;
                //    case DialogResult.Cancel:
                //    case DialogResult.Ignore:
                //    case DialogResult.Retry:
                //        break;
                //    default:
                //        Debug.Assert(false);
                //        break;
                //}
            }

            public static void ShowDefaultException(System.Exception exception)
            {
                ShowException(exception);
            }
        }
    }
}
