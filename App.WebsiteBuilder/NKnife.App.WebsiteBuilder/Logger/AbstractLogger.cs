using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Defines the base behavior for loggers.</summary>
    [XmlInclude(typeof(MailLogger))]
    [XmlInclude(typeof(FileLogger))]
    [XmlInclude(typeof(XmlLogger))]
    [XmlInclude(typeof(EventLogger))]
    [XmlInclude(typeof(EmptyLogger))]
    [XmlInclude(typeof(CompositeLogger))]
    [XmlInclude(typeof(ChainLogger))]
    [XmlInclude(typeof(SafeLogger))]
    public abstract class AbstractLogger : ILogger
    {
        /// <summary>
        /// Protected internal value for LoggingLevel.
        /// </summary>
        protected internal LogType level = LogType.Information;

        /// <summary>
        /// Protected internal value for strict logging.
        /// </summary>
        protected internal bool strict = false;

        /// <summary>
        /// Determines the level at which to log.  ie: Setting LoggingLevel equal to 
        /// LoggingLevel.Warning will log all Errors and Warnings.
        /// </summary>
        [XmlAttribute("logType")]
        public virtual LogType LoggingLevel
        {
            get { return level; }
            set { level = value; }
        }

        /// <summary>
        /// If true, will only log the type specified by LoggingLevel, 
        /// otherwise will log severity greater than or equal to LoggingLevel.
        /// </summary>
        [XmlAttribute("strict")]
        public virtual bool StrictLevel
        {
            get { return strict; }
            set { strict = value; }
        }

        /// <summary>
        /// Determines if the supplied log level is within the bounds of the current log level.
        /// </summary>
        /// <param name="log">LogType level.</param>
        /// <returns>Boolean should be logged.</returns>
        internal bool IsLoggable(LogType log)
        {
            if (!strict)
            {
                return ((int)log) <= ((int)level);
            }
            else
            {
                return log == level;
            }
        }

        /// <summary>List[Log] an entry.</summary>
        /// <param name="log">ILog instance.</param>
        public void Log(ILog log)
        {
            if (IsLoggable(log.LogType))
            {
                DoLog(log);
                if (OnLog != null)
                {
                    OnLog(this, log);
                }
            }
        }

        /// <summary>
        /// List[Log] an entry.  
        /// Log() uses this method after checking it's type, 
        /// and fires an event after DoLog().
        /// </summary>
        /// <param name="log">ILog instance.</param>
        protected abstract void DoLog(ILog log);

        /// <summary>Gets all logs.</summary>
        /// <returns>Log collection.</returns>
        public abstract List<Log> GetLogs();

        /// <summary>Cleans up internal resources.</summary>
        public abstract void Dispose();

        /// <summary>Event fired when the logger instance logs an entry.</summary>
        public event OnLogHandler OnLog;
    }
}