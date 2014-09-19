using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Logging utility that appends to a log file.</summary>
    [XmlType("FileLogger")]
    public class FileLogger : AbstractLogger
    {
        private string filename;

        /// <summary>Default constructor.</summary>
        public FileLogger()
        {
        }

        /// <summary>Initializes with a file name.</summary>
        /// <param name="filename">String file name.</param>
        public FileLogger(string filename)
        {
            this.filename = filename;
        }

        /// <summary>Initializes with a file name.</summary>
        /// <param name="filename">String file name.</param>
        /// <param name="loglevel"></param>
        public FileLogger(string filename, LogType loglevel)
            : this(filename)
        {
            this.level = loglevel;
        }

        /// <summary>Initializes with a file name.</summary>
        /// <param name="filename">String file name.</param>
        /// <param name="loglevel"></param>
        /// <param name="strict"></param>
        public FileLogger(string filename, LogType loglevel, bool strict)
            : this(filename, loglevel)
        {
            this.strict = strict;
        }

        /// <summary>Gets or sets the file name of the log file used.</summary>
        [XmlElement("logfile")]
        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }

        /// <summary>List<Log> an entry.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
            using (FileStream fs = new FileStream(@filename, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(log.ToString());
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>Resets the log file to an empty file.</summary>
        public void ResetFile()
        {
            using (FileStream fs = new FileStream(@filename, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(string.Empty);
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>Gets all logs.</summary>
        /// <returns>Log collection.</returns>
        public override List<Log> GetLogs()
        {
            return new List<Log>();
        }

        /// <summary>Cleans up internal resources.</summary>
        public override void Dispose()
        {
        }
    }
}