using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusSoftwareUpdate
{
    public static class LogService
    {
        static private FileStream _fileStream;
        static private StreamWriter _writer;

        static public void Initialize()
        {
            string log = Path.Combine(Application.StartupPath,"update.log");
            _fileStream = new FileStream(log, FileMode.Append, FileAccess.Write);
            _writer = new StreamWriter(_fileStream, Encoding.UTF8);
        }

        [Conditional("DEBUG")]
        static public void WriteInfoLog(string msg)
        {
            WriteLogCore(msg);
        }

        static public void WriteErrorLog(Exception exception,string msg)
        {
            if (exception == null)
            {
                return;
            }
            string text = "异常信息:{0}\r\n异常类型:{1}\r\n异常堆栈:\r\n{2}\r\n\r\n";
            if (!string.IsNullOrEmpty(msg))
            {
                text = "辅助信息:" + msg + "\r\n" + text;
            }
            text = string.Format(text, exception.Message, exception.GetType().FullName, exception.StackTrace);
            WriteLogCore(text);
        }

        static private void WriteLogCore(string msg)
        {
            _writer.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n" + msg + "\r\n\r\n");
            _writer.Flush();
        }

        static public void Close()
        {
            _writer.Flush();
            _writer.Close();
            _fileStream.Close();
        }
    }
}
