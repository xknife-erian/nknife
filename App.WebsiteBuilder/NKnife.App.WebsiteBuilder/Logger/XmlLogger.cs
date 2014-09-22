using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Logging utility that appends to an Xml file.</summary>
    [XmlType("XmlLogger")]
    public class XmlLogger : AbstractLogger
    {
        private string filename;

        /// <summary>Default constructor.</summary>
        public XmlLogger()
        {
        }

        /// <summary>Initializes with a file name.</summary>
        /// <param name="filename">String file name.</param>
        public XmlLogger(string filename)
        {
            this.filename = filename;
        }

        /// <summary>Initializes with a file name.</summary>
        /// <param name="filename">String file name.</param>
        /// <param name="loglevel">Minimum log level.</param>
        public XmlLogger(string filename, LogType loglevel)
            : this(filename)
        {
            this.level = loglevel;
        }

        /// <summary>Initializes with a file name.</summary>
        /// <param name="filename">String file name.</param>
        /// <param name="loglevel">Minimum log level.</param>
        /// <param name="strict">Determines if levels higher than the specified minimum will be logged.  True means only logs of the minimum level with be logged.</param>
        public XmlLogger(string filename, LogType loglevel, bool strict)
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
            XmlLogFile xlf = null;
            if (File.Exists(filename))
            {
                xlf = XmlLogFileSerializer.ReadFile(filename);
            }
            if (xlf == null) { xlf = new XmlLogFile(); }
            if (xlf.Logs == null) { xlf.Logs = new List<Log>(); }
            xlf.Logs.Add(log as Log);
            XmlLogFileSerializer.WriteFile(filename, xlf);
        }

        /// <summary>Resets the log file to an empty file.</summary>
        public void ResetFile()
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
            catch { }
        }

        /// <summary>Gets all logs.</summary>
        /// <returns>Log collection.</returns>
        public override List<Log> GetLogs()
        {
            try
            {
                XmlLogFile xlf = null;
                if (File.Exists(filename))
                {
                    xlf = XmlLogFileSerializer.ReadFile(filename);
                    return xlf.Logs;
                }
            }
            catch { }
            return new List<Log>();
        }

        /// <summary>Cleans up internal resources.</summary>
        public override void Dispose()
        {
        }
    }
}