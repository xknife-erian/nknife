using System;

namespace Gean.Logging
{
    /// <summary>
    /// This is used scoping a logger with a particular class.
    /// e.g. Have a logger only for class "BlogPostService".
    /// This is analogous to LogManger.Get(typeof(BlogPostService)) in Log4Net.
    /// </summary>
    /// <example>
    /// ILog logger = Logger.Get&lt;BlogPostService&gt;("default");
    /// logger.Info("testing");
    /// </example>
    public class LogInstance : LogBase
    {
        private readonly string _LoggerName;
        private readonly Type _LoggerType;


        /// <summary>
        /// Initialize with reference to the actually logger that does the logging
        /// and the calling type of the logger.
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="callingType"></param>
        public LogInstance(string loggerName, Type callingType) 
            : base(callingType.FullName)
        {
            _LoggerName = loggerName;
            _LoggerType = callingType;
        }

        #region ILog Members

        /// <summary>
        /// Log the event to file.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void Log(LogEvent logEvent)
        {
            logEvent.LogType = _LoggerType;
            logEvent.FinalMessage = BuildMessage(logEvent);
            Logger.Get(_LoggerName).Log(logEvent);
        }

        #endregion
    }
}