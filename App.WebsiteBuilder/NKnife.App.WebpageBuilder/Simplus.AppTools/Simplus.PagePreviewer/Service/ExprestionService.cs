using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusD;

namespace Jeelu.SimplusPagePreviewer
{
    static public class ExceptionService
    {
        static FileStream _file;
        static ExceptionService()
        {
            _file = new FileStream(LogFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            Application.ThreadExit += new EventHandler(Application_ThreadExit);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CreateException((Exception)e.ExceptionObject);
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            _file.Close();
        }
        static public string LogFileName
        {
            get
            {
                return Path.Combine(Application.StartupPath,
                    Path.GetFileName(Application.ExecutablePath) + "_error.log");
            }
        }
        static public void CreateException(Exception e)
        {
            //TODO:lukan: 因第一期暂不考虑日志功能，故撤下了Log4net组件，操作日志自行建一简单类完成
            //LogService.Info(e.Message + "\r\n" + e.GetType() + "\r\n" + e.StackTrace + "\r\n\r\n");

            string str = string.Format("捕获时间:{3}\r\n当前项目:{4}\r\n异常类型:{0}\r\n异常信息:{1}\r\n堆栈信息:\r\n{2}\r\n\r\n",
                e.GetType().FullName, e.Message, e.StackTrace,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),Program.AbsolutePath);

            byte[] bytes = Encoding.Default.GetBytes(str);

            //byte[] sdsites = Encoding.Default.GetBytes("sdsite:" + SdsiteXmlDocument.CurrentDocument.OuterXml+"\r\n");

            _file.Write(bytes, 0, bytes.Length);
            //_file.Write(sdsites, 0, sdsites.Length);

            _file.Flush();
        }
    }
}
