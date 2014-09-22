using System;
using System.IO;
using System.Collections.Generic;

namespace Jeelu.Logger
{
    /// <summary>Logging utility that writes to a stream.</summary>
    public class StreamLogger : AbstractLogger
    {
        private TextWriter writer;

        /// <summary>Initializes with a stream writer to log to.</summary>
        /// <param name="writer">StreamWriter instance.</param>
        public StreamLogger(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>Initializes with a stream writer to log to.</summary>
        /// <param name="writer">StreamWriter instance.</param>
        /// <param name="loglevel"></param>
        public StreamLogger(TextWriter writer, LogType loglevel)
            : this(writer)
        {
            this.level = loglevel;
        }

        /// <summary>Initializes with a stream writer to log to.</summary>
        /// <param name="writer">StreamWriter instance.</param>
        /// <param name="loglevel"></param>
        /// <param name="strict"></param>
        public StreamLogger(TextWriter writer, LogType loglevel, bool strict)
            : this(writer, loglevel)
        {
            this.strict = strict;
        }

        /// <summary>Gets or sets the stream that this logger writes to.</summary>
        public TextWriter Writer
        {
            get { return writer; }
            set { writer = value; }
        }

        /// <summary>List<Log> an entry.</summary>
        /// <param name="log">ILog instance.</param>
        protected override void DoLog(ILog log)
        {
            writer.WriteLine(log.ToString());
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
            if (writer != null)
            {
                writer.Flush();
                writer.Close();
                writer = null;
            }
        }
    }
}