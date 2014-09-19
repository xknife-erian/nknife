using System;

namespace Jeelu.Logger
{
    /// <summary>Builds a logger hierarchy from an XML config file.</summary>
    public class LoggerFactory
    {
        private string config;
        private string app;

        /// <summary>Initializes with a config file path and an application name.</summary>
        /// <param name="config">XML config file path.</param>
        /// <param name="app">Application name.</param>
        public LoggerFactory(string config, string app)
        {
            this.config = config;
            this.app = app;
        }

        /// <summary>Gets an instance from a config path and app name.</summary>
        /// <param name="config">XML config file path.</param>
        /// <param name="app">Application name.</param>
        /// <returns>LoggerFactory instance.</returns>
        public static LoggerFactory GetInstance(string config, string app)
        {
            return new LoggerFactory(config, app);
        }

        private ILogger GetLogger()
        {
            ILogger logger = null;
            try
            {
                logger = LoggerConfigSerializer.ReadFile(config).Logger;
            }
            catch { }
            return logger;
        }

        #region Logging
        /// <summary>List[Log] an entry.</summary>
        /// <param name="msg">Log message.</param>
        /// <param name="type">Log type.</param>
        /// <param name="status">Log status.</param>
        public void Log(string msg, LogType type, LogStatus status)
        {
            using (ILogger logger = GetLogger())
            {
                if (logger == null) { return; }
                ILog log = new Log(app, type, status, msg, null);
                logger.Log(log);
            }
        }

        /// <summary>List[Log] an entry.</summary>
        /// <param name="msg">Log message.</param>
        /// <param name="type">Log type.</param>
        /// <param name="status">Log status.</param>
        /// <param name="details">Log details.</param>
        public void Log(string msg, LogType type, LogStatus status, string details)
        {
            using (ILogger logger = GetLogger())
            {
                if (logger == null) { return; }
                ILog log = new Log(app, type, status, msg, details);
                logger.Log(log);
            }
        }

        /// <summary>List[Log] an exception.</summary>
        /// <param name="ex">Exception instance.</param>
        public void Log(Exception ex)
        {
            using (ILogger logger = GetLogger())
            {
                if (logger == null) { return; }
                ILog log = new Log(ex);
                log.Application = app;
                logger.Log(log);
            }
        }

        /// <summary>List[Log] an exception.</summary>
        /// <param name="ex">Exception instance.</param>
        /// <param name="info">Additional information.  Most loggers use the object's ToString() method for output.</param>
        public void Log(Exception ex, object info)
        {
            using (ILogger logger = GetLogger())
            {
                if (logger == null) { return; }
                ILog log = new Log(ex);
                log.Application = app;
                log.Object = info;
                logger.Log(log);
            }
        }

        /// <summary>List[Log] a message with type Error and status Failure.</summary>
        /// <param name="msg">Log message.</param>
        public void LogError(string msg)
        {
            Log(msg, LogType.Error, LogStatus.Failure);
        }

        /// <summary>List[Log] a message with type Warning and status Other.</summary>
        /// <param name="msg">Log message.</param>
        public void LogWarning(string msg)
        {
            Log(msg, LogType.Warning, LogStatus.Other);
        }

        /// <summary>List[Log] a message with type Debug and status Other.</summary>
        /// <param name="msg">Log message.</param>
        public void LogDedbug(string msg)
        {
            Log(msg, LogType.Debug, LogStatus.Other);
        }

        /// <summary>List[Log] a message with type Information and status Other.</summary>
        /// <param name="msg">Log message.</param>
        public void LogInfo(string msg)
        {
            Log(msg, LogType.Information, LogStatus.Other);
        }
        #endregion
    }
}