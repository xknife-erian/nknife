using System;

namespace Jeelu.Logger
{
    /// <summary>Log Exception.</summary>
    public class LogException : Exception
    {
        private ILog log;

        /// <summary>Initializes with a message.</summary>
        /// <param name="message">String message.</param>
        public LogException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes with a message and the ILog instance that failed to log.</summary>
        /// <param name="message">String message.</param>
        /// <param name="log">ILog instance.</param>
        public LogException(string message, ILog log)
            : base(message)
        {
            this.log = log;
        }
    }
}