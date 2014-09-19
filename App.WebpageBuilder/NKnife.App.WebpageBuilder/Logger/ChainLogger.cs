using System;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Represents a Chain or Responsability Logger.  This class is actually a chain of responsability wrapper for existing ILoggers.  Must have a primary logger, and can have a backup.  If the primary fails, it will attempt to use the backup, either of which can also be a ChainLogger.</summary>
    [XmlType("ChainLogger")]
    public class ChainLogger : AbstractLogger
    {
        private List<AbstractLogger> loggers = new List<AbstractLogger>();

        /// <summary>Default initialization.</summary>
        public ChainLogger()
        {
        }

        /// <summary>Initializes with a minimum log level and strict flag.</summary>
        /// <param name="loglevel">Minimum log level.</param>
        /// <param name="strict">Determines if levels higher than the specified minimum will be logged.  True means only logs of the minimum level with be logged.</param>
        public ChainLogger(LogType loglevel, bool strict)
        {
            base.LoggingLevel = loglevel;
            base.StrictLevel = strict;
        }

        /// <summary>Initializes with a collection of ILoggers.</summary>
        /// <param name="loggers">AbstractLogger array.</param>
        public ChainLogger(params AbstractLogger[] loggers)
        {
            this.loggers.AddRange(loggers);
        }

        /// <summary>Initializes with a minimum log level and strict flag.</summary>
        /// <param name="loglevel">Minimum log level.</param>
        /// <param name="strict">Determines if levels higher than the specified minimum will be logged.  True means only logs of the minimum level with be logged.</param>
        /// <param name="loggers">AbstractLogger array.</param>
        public ChainLogger(LogType loglevel, bool strict, params AbstractLogger[] loggers)
            : this(loggers)
        {
            base.LoggingLevel = loglevel;
            base.StrictLevel = strict;
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
            bool handled = true;
            foreach (ILogger lgr in loggers)
            {
                if (lgr is AbstractLogger)
                {
                    if (!((AbstractLogger)lgr).IsLoggable(log.LogType))
                    {
                        continue;
                    }
                }
                try
                {
                    lgr.Log(log);
                    break;
                }
                catch
                {
                    handled = false;
                }
            }
            if (!handled)
            {
                throw new LogException("Log not handled.", log);
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