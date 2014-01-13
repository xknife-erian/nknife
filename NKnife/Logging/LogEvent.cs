using System;

namespace Gean.Logging
{
    /// <summary>
    /// A record in the log.
    /// </summary>
    public class LogEvent
    {
        /// <summary>
        /// Additional arguments passed by caller.
        /// </summary>
        public object[] Args { get; set; }

        /// <summary>
        /// Name of the computer.
        /// </summary>
        public string Computer { get; set; }

        /// <summary>
        /// Create time.
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Exception passed.
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// The exception.
        /// </summary>
        public Exception Ex { get; set; }

        /// <summary>
        /// This is the final message that is printed.
        /// </summary>
        public string FinalMessage { get; set; }

        /// <summary>
        /// The log level.
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// The data type of the caller that is logging the event.
        /// </summary>
        public Type LogType { get; set; }

        /// <summary>
        /// Message that is logged.
        /// </summary>
        public object Message { get; set; }

        /// <summary>
        /// The name of the currently executing thread that created this log entry.
        /// </summary>
        public string ThreadName { get; set; }

        /// <summary>
        /// Enable default constructor.
        /// </summary>
        public LogEvent()
        {
        }

        public LogEvent(LogLevel level, string message, Exception ex)
        {
            Level = level;
            Message = message;
            Ex = ex;
        }
    }
}