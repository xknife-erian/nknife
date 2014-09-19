using System;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Specification for a logging utility.</summary>
    public interface ILogger : IDisposable
    {
        /// <summary>Event that fires when the logger logs an entry.</summary>
        event OnLogHandler OnLog;
        /// <summary>Determines the level at which to log.  ie: Setting LoggingLevel equal to LoggingLevel.Warning will log all Errors and Warnings.</summary>
        LogType LoggingLevel { get; set; }
        /// <summary>If true, will only log the type specified by LoggingLevel, otherwise will log severity greater than or equal to LoggingLevel.</summary>
        bool StrictLevel { get; set; }
        /// <summary>List[Log] an entry.</summary>
        /// <param name="log">ILog instance.</param>
        void Log(ILog log);
        /// <summary>Gets all logs.</summary>
        /// <returns>Log collection.</returns>
        List<Log> GetLogs();
    }
}