using System;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Logging utility that writes to the console.</summary>
    public class ConsoleLogger : AbstractLogger
    {
        /// <summary>Default constructor.</summary>
        public ConsoleLogger()
        {
        }

        /// <summary>Initializes with the minimum log level.</summary>
        /// <param name="loglevel">Minimum log level.</param>
        public ConsoleLogger(LogType loglevel)
        {
            this.level = loglevel;
        }

        /// <summary>Initializes with the minimum log level and a strict flag.</summary>
        /// <param name="loglevel">Minimum log level.</param>
        /// <param name="strict">Determines if levels higher than the specified minimum will be logged.  True means only logs of the minimum level with be logged.</param>
        public ConsoleLogger(LogType loglevel, bool strict)
            : this(loglevel)
        {
            this.strict = strict;
        }

        /// <summary>List<Log> an entry.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
            Console.WriteLine(log.ToString());
        }

        /// <summary>Gets all logs.</summary>
        /// <returns>Log collection.</returns>
        public override List<Log> GetLogs()
        {
            return new List<Log>();
        }

        /// <summary>Cleans up internal resources.</summary>
        public override void Dispose()
        {
        }
    }
}