using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Jeelu.SimplusPagePreviewer
{
    static class Program
    {
        /// <summary>
        /// 传入的绝对路径
        /// </summary>
        public static string AbsolutePath;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            /// 绝对路径
#if DEBUG
            string absolutePath = "";
#else
            string absolutePath = null;
#endif
            if (args.Length > 0)
            {
                absolutePath = Encoding.UTF8.GetString(Convert.FromBase64String(args[0]));
                bool isCreate;
                EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, args[0], out isCreate);
                waitHandle.Set();
            }
            else
            {
                if (absolutePath == null)
                {
                    return;
                }
            }

            AbsolutePath = absolutePath;

            PerviewViewForm pv = new PerviewViewForm(absolutePath);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            pv.Run();
        }

        //处理异常写入日志文件
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionService.CreateException((Exception)e.ExceptionObject);
        }
    }
}
