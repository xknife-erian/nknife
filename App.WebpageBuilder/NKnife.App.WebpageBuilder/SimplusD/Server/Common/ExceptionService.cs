using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Server
{
    public class ExceptionService
    {
        /// <summary>
        /// 将异常记录到日志
        /// </summary>
        static public void WriteExceptionLog(Exception e)
        {
            string strExFile = Path.Combine(Application.StartupPath,@"Log\Exception.txt");
            if (!File.Exists(strExFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(strExFile));
                File.Create(strExFile).Close();
            }
            File.AppendAllText(strExFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + e.Message + "\r\n" + e.StackTrace + "\r\n\r\n");
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}
