namespace Gean.Logging
{
    /// <summary>
    /// Level for the logging.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug level
        /// </summary>
        Debug,

        /// <summary>
        /// Info level
        /// </summary>
        Info,

        /// <summary>
        /// Warn level
        /// </summary>
        Warn,

        /// <summary>
        /// Error level
        /// </summary>
        Error,

        /// <summary>
        /// Fatal level
        /// </summary>
        Fatal,

        /// <summary>
        /// Used to always log a message regardless of loglevel
        /// </summary>
        Message
    };
}