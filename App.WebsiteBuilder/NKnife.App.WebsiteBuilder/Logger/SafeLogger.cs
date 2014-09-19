using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>A safe ILogger wrapper that insures that no exceptions are thrown.</summary>
    public class SafeLogger : AbstractLogger
    {
        private AbstractLogger logger;

        /// <summary>Default constructor.</summary>
        public SafeLogger()
        {
        }

        /// <summary>Initializes with a logger to wrap.</summary>
        /// <param name="logger">AbstractLogger instance.</param>
        public SafeLogger(AbstractLogger logger)
        {
            this.Logger = logger;
        }

        /// <summary>Gets or sets the wrapped logger.</summary>
        [XmlElement("Logger")]
        public AbstractLogger Logger
        {
            get { return this.logger; }
            set { this.logger = value; }
        }

        /// <summary>List<Log> the supplied ILog object to internal ILoggers.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
            try
            {
                logger.Log(log);
            }
            catch { }
        }

        /// <summary>Retrieves logs.</summary>
        /// <returns>Collection of ILog objects.</returns>
        public override List<Log> GetLogs()
        {
            if (logger == null) { return new List<Log>(); }
            return logger.GetLogs();
        }

        /// <summary>Cleans up internal resources.</summary>
        public override void Dispose()
        {
            try
            {
                logger.Dispose();
            }
            catch { }
        }
    }
}