using System;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Represents a batch of ILoggers.</summary>
    [XmlType("CompositeLogger")]
    public class CompositeLogger : AbstractLogger
    {
        private List<AbstractLogger> loggers = new List<AbstractLogger>();

        /// <summary>Default initialization.</summary>
        public CompositeLogger()
        {
        }

        /// <summary>Initializes with a collection of ILoggers.</summary>
        /// <param name="loggers">ILogger collection.</param>
        public CompositeLogger(List<AbstractLogger> loggers)
        {
            this.loggers = loggers;
        }

        /// <summary>Initializes with a collection of ILoggers.</summary>
        /// <param name="loggers">ILogger collection.</param>
        /// <param name="loglevel">Minimum log level.</param>
        public CompositeLogger(List<AbstractLogger> loggers, LogType loglevel)
            : this(loggers)
        {
            this.level = loglevel;
        }

        /// <summary>Initializes with a collection of ILoggers.</summary>
        /// <param name="loggers">ILogger collection.</param>
        /// <param name="loglevel">Minimum log level.</param>
        /// <param name="strict">Determines if levels higher than the specified minimum will be logged.  True means only logs of the minimum level with be logged.</param>
        public CompositeLogger(List<AbstractLogger> loggers, LogType loglevel, bool strict)
            : this(loggers, loglevel)
        {
            this.strict = strict;
        }

        /// <summary>Internal collection of ILogger objects.</summary>
        [XmlArrayItem("Logger", typeof(AbstractLogger))]
        [XmlArray("Loggers")]
        public List<AbstractLogger> Loggers
        {
            get { return loggers; }
            set { loggers = value; }
        }

        /// <summary>List<Log> the supplied ILog object to internal ILoggers.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
            foreach (ILogger lgr in loggers)
            {
                try
                {
                    lgr.Log(log);
                }
                catch { }
            }
        }

        /// <summary>Retrieves logs.</summary>
        /// <returns>Collection of ILog objects.</returns>
        public override List<Log> GetLogs()
        {
            List<Log> lgs = new List<Log>();
            foreach (ILogger lgr in loggers)
            {
                List<Log> logs = lgr.GetLogs();
                if (logs != null)
                {
                    lgs.AddRange(logs);
                }
            }
            return lgs;
        }

        /// <summary>Cleans up internal resources.</summary>
        public override void Dispose()
        {
            foreach (ILogger lgr in loggers)
            {
                lgr.Dispose();
            }
        }
    }
}