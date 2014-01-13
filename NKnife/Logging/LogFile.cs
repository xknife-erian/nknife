using System;
using System.IO;

namespace Gean.Logging
{
    /// <summary>
    /// File based logger.
    /// </summary>
    public class LogFile : LogBase, ILog, IDisposable
    {
        private readonly string _filepath;
        private readonly object _lockerFlush = new object();
        private readonly int _maxFileSizeInMegs = 10;
        private readonly bool _rollFile = true;
        private string _filepathUnique;
        private int _iterativeFlushCount;
        private int _iterativeFlushPeriod = 4;
        private StreamWriter _writer;


        /// <summary>
        /// Initialize with path of the log file.
        /// </summary>
        /// <param name="filepath"></param>
        public LogFile(string name, string filepath) :
            this(name, filepath, DateTime.Now, "")
        {
        }


        /// <summary>
        /// Initialize log file.
        /// </summary>
        /// <param name="name">Name of application.</param>
        /// <param name="filepath">File path, can contain substitutions. e.g. "%yyyy%.</param>
        /// <param name="date">Date to use in the name of the log file.</param>
        /// <param name="env">Environment name to put into the name of the log file.</param>
        public LogFile(string name, string filepath, DateTime date, string env)
            : this(name, filepath, date, env, true, 20)
        {
        }


        /// <summary>
        /// Initialize log file.
        /// </summary>
        /// <param name="name">Name of application.</param>
        /// <param name="filepath">File path, can contain substitutions. e.g. "%yyyy%.</param>
        /// <param name="date">Date to use in the name of the log file.</param>
        /// <param name="env">Environment name to put into the name of the log file.</param>
        public LogFile(string name, string filepath, DateTime date, string env, bool rollFile, int maxSizeInMegs)
            : base(name)
        {
            _rollFile = rollFile;
            _maxFileSizeInMegs = maxSizeInMegs;
            _filepath = LogHelper.BuildLogFileName(filepath, name, date, env);
            _filepathUnique = _filepath;
            _writer = new StreamWriter(_filepathUnique, true);
        }


        /// <summary>
        /// The full path to the log file.
        /// </summary>
        public string FilePath
        {
            get { return _filepathUnique; }
        }

        #region IDisposable Members

        /// <summary>
        /// Flushes the data to the file.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_writer != null)
                {
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region ILog Members

        /// <summary>
        /// Log the event to file.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void Log(LogEvent logEvent)
        {
            string finalMessage = logEvent.FinalMessage;
            if (string.IsNullOrEmpty(finalMessage))
                finalMessage = BuildMessage(logEvent);

            _writer.WriteLine(finalMessage);
            FlushCheck();
        }


        /// <summary>
        /// Flush the output.
        /// </summary>
        public override void Flush()
        {
            _writer.Flush();
        }


        /// <summary>
        /// Shutsdown the logger.
        /// </summary>
        public override void ShutDown()
        {
            Dispose();
        }

        #endregion

        /// <summary>
        /// Destructor to close the writer
        /// </summary>
        ~LogFile()
        {
            Dispose();
        }


        /// <summary>
        /// Flush the data and check file size for rolling.
        /// </summary>
        private void FlushCheck()
        {
            lock (_lockerFlush)
            {
                if (_iterativeFlushCount%_iterativeFlushPeriod == 0)
                {
                    _writer.Flush();
                    _iterativeFlushCount = 1;
                }
                else
                    _iterativeFlushCount++;

                // Now check file size.
                if (_rollFile && GetSizeInMegs(_filepath) > _maxFileSizeInMegs)
                {
                    ExecuteHelper.TryCatch(() =>
                                               {
                                                   string searchPath = _filepath.Substring(0, _filepath.LastIndexOf('.'));
                                                   var file = new FileInfo(_filepath);
                                                   string[] files = Directory.GetFiles(file.DirectoryName, searchPath + "*", SearchOption.TopDirectoryOnly);

                                                   _filepathUnique = string.Format("{0}-part{1}{2}", searchPath, files.Length.ToString(), file.Extension);
                                                   _writer.Flush();
                                                   _writer.Close();
                                                   _writer = new StreamWriter(_filepathUnique);
                                               });
                }
            }
        }

        private static int GetSizeInMegs(string filePath)
        {
            var f = new FileInfo(filePath);
            var size = f.Length / 1000000;
            return (int)size;
        }
    }
}