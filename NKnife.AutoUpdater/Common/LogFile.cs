using System;
using System.IO;
using System.Text;
using NKnife.Utility;

namespace NKnife.AutoUpdater.Common
{
    internal class LogFile : IDisposable
    {
        private readonly string _Filepath;
        private readonly StreamWriter _AllWriter;
        private readonly StreamWriter _CoreInfoWriter;

        /// <summary>
        /// Initialize with path of the log file.
        /// </summary>
        public LogFile(string filepath, string appName)
        {
            _Filepath = BuildLogFileName(filepath, appName, DateTime.Now);
            _AllWriter = new StreamWriter(_Filepath, true, Encoding.UTF8);
            _CoreInfoWriter = new StreamWriter(GetUpdateHistoryFile(), true, Encoding.UTF8);
        }

        /// <summary>
        /// The full path to the log file.
        /// </summary>
        public string FilePath
        {
            get { return _Filepath; }
        }

        #region IDisposable Members

        /// <summary>
        /// Flushes the data to the file.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_AllWriter != null)
                {
                    _AllWriter.Flush();
                    _AllWriter.Close();
                }
                if (_CoreInfoWriter != null)
                {
                    _CoreInfoWriter.Flush();
                    _CoreInfoWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        private static string GetUpdateHistoryFile()
        {
            string name = String.Format("UpdateHistory.Log");
            return Path.Combine(HistoryPath, name);
        }

        /// <summary>在Data目录下存储历史数据的目录（存储Runtime数据）
        /// </summary>
        /// <value>The history data path.</value>
        private static string HistoryPath
        {
            get
            {
                string subpath = string.Format("HistoryData\\");
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\", subpath);
                if (!Directory.Exists(path))
                    UtilityFile.CreateDirectory(path);
                return path;
            }
        }

        private static string BuildLogFileName(string logPath, string appName, DateTime date)
        {
            var sb = new StringBuilder(date.ToString("yyyy-MM-dd"));
            sb.Append(".log");
            sb.Insert(0, appName);
            logPath = Path.Combine(logPath, date.ToString("yyyyMM"));
            if (!Directory.Exists(logPath))
                UtilityFile.CreateDirectory(logPath);
            string logfile = Path.Combine(logPath, sb.ToString());
            return logfile;
        }

        /// <summary>
        /// Flush the output.
        /// </summary>
        public void Flush()
        {
            _AllWriter.Flush();
            _CoreInfoWriter.Flush();
        }

        /// <summary>
        /// Shutsdown the logger.
        /// </summary>
        public void ShutDown()
        {
            Dispose();
        }

        /// <summary>
        /// Destructor to close the writer
        /// </summary>
        ~LogFile()
        {
            Dispose();
        }

        public void Write(string info, bool isCoreInfo = false)
        {
            _AllWriter.WriteLine(info);
            if (isCoreInfo)
                _CoreInfoWriter.WriteLine(info);
            Flush();
        }
    }
}