using System;
using System.IO;
using System.Text;

namespace Gean.Wrapper.Logger
{
    /// <summary>Gean的一个非常轻量级的写入文本文件的日志记录模块
    /// </summary>
    public sealed class LogWriter : IGLoggerWriter
    {
        private static LogWriter _LogWriter = null;

        private readonly FileInfo _LogFile;
        private readonly StreamWriter _Stream;

        /// <summary>
        /// 构造函数。文本文件日志记录类。
        /// </summary>
        /// <param name="logfile">日志记录的文件</param>
        private LogWriter(string logfile)
        {
            if (!File.Exists(logfile)) //如果Log文件存在，将不再保留
            {
                var path = Path.GetDirectoryName(Path.GetFullPath(logfile));
                if (path != null)
                {
                    UtilityFile.CreateDirectory(path);
                    _Stream = FileCreator(logfile);
                }
                else
                {
                    throw new DirectoryNotFoundException("当创建目录时，传入的目录为空。");
                }
            }
            else
            {
                try
                {
                    File.SetAttributes(logfile, FileAttributes.Normal);
                    File.Delete(logfile);
                }
                catch //文件虽然存在，但文件操作发生异常，一般可能是被锁定
                {
                    logfile += ".log";
                }
                _Stream = FileCreator(logfile);
            }
            _LogFile = new FileInfo(logfile);
        }

        /// <summary>
        /// 写一条日志信息
        /// </summary>
        /// <param name="logLevel">当前信息的日志等级</param>
        /// <param name="message">信息主体，可是多个对象(当未是String时，将会调用object的Tostring()获取字符串)</param>
        public void Write(GLogLevel logLevel, params object[] message)
        {
            lock (_Stream)
            {
                var sb = new StringBuilder();
                //加入时间信息
                sb.Append(DateTime.Now.ToString("yyMMdd HH:mm:ss"))
                    .Append(" ")
                    .Append(DateTime.Now.Millisecond.ToString().PadLeft(3, '0'))
                    .Append(",\t")
                    .Append(logLevel.ToString())
                    .Append(", \t");
                //使用者附加的Log信息
                foreach (object item in message)
                {
                    if (item == null)
                        continue;
                    if (item is Exception)
                    {
                        sb.Append(((Exception) item).Message).Append("; ");
                        sb.Append(((Exception) item).StackTrace).Append("; ");
                    }
                    else
                        sb.Append(item.ToString()).Append("; ");
                }
                //写入文件
                string log = sb.ToString(0, sb.Length - 2);
                _Stream.WriteLine(log);
                _Stream.Flush();
                OnLogWrited(new LogWritedEventArgs(log));
            } //lock (_StreamWriterDic)
        }

        public void Trace(params object[] message)
        {
            Write(GLogLevel.Trace, message);
        }

        public void Debug(params object[] message)
        {
            Write(GLogLevel.Debug, message);
        }

        public void Info(params object[] message)
        {
            Write(GLogLevel.Info, message);
        }

        public void Warn(params object[] message)
        {
            Write(GLogLevel.Warn, message);
        }

        public void Error(params object[] message)
        {
            Write(GLogLevel.Error, message);
        }

        public void Fatal(params object[] message)
        {
            Write(GLogLevel.Fatal, message);
        }

        /// <summary>
        /// 文本文件日志记录类的初始化，主要初始化日志文件。
        /// </summary>
        /// <param name="logfile">log文件的完全路径名</param>
        /// <param name="isByData">是否按日期新建</param>
        /// <param name="isAppend">是否是追加模式</param>
        public static LogWriter InitializeComponent(string logfile, bool isByData, bool isAppend)
        {
            return _LogWriter ?? (_LogWriter = new LogWriter(logfile));
        }

        /// <summary>
        /// 创建一个日志文件，第一行将注明日志创建日期
        /// </summary>
        /// <param name="file">文件全名</param>
        /// <returns></returns>
        private static StreamWriter FileCreator(string file)
        {
            StreamWriter sw = File.AppendText(file);
            return sw;
        }

        /// <summary>
        /// 备份日志文件
        /// </summary>
        public void BakupLogFile()
        {
            string bakFile = _LogFile.FullName + DateTime.Now.ToString("-yy-MM-dd HH-mm-ss") + ".bak.log";
            try
            {
                _Stream.Flush();
            }
            catch
            {
            }
            _LogFile.CopyTo(bakFile);
            _LogFile.Delete();
        }

        /// <summary>
        /// 关闭日志文件的读写流(使用后，全局日志相关方法将全部失效)
        /// </summary>
        /// <param name="isBakup">是否备份</param>
        public void Close(bool isBakup)
        {
            lock (_Stream)
            {
                _Stream.Flush();
                _Stream.Close();
                _Stream.Dispose();
            }
            if (isBakup)
            {
                BakupLogFile();
            }
        }

        /// <summary>
        /// 当日志写入后发生的事件
        /// </summary>
        public event LogWritedEventHandler LogWritedEvent;

        private void OnLogWrited(LogWritedEventArgs e)
        {
            if (LogWritedEvent != null)
                LogWritedEvent(this, e);
        }

        public delegate void LogWritedEventHandler(Object sender, LogWritedEventArgs e);

        public class LogWritedEventArgs : EventArgs
        {
            public String LogString { get; private set; }

            public LogWritedEventArgs(String logString)
            {
                LogString = logString;
            }
        }
    }
}