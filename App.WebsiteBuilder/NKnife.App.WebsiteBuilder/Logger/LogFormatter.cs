using System;

namespace Jeelu.Logger
{
    /// <summary>Used to format log output.</summary>
    public class LogFormatter
    {
        private string format = null;

        /// <summary>Default constructor.</summary>
        public LogFormatter()
        {
        }

        /// <summary>Initializes with a log format pattern.</summary>
        /// <param name="pattern">String pattern.</param>
        public LogFormatter(string pattern)
        {
            this.format = pattern;
        }

        /// <summary>Gets or sets the format pattern.</summary>
        public string FormatString
        {
            get { return format; }
            set { format = value; }
        }

        /// <summary>Formats a string representation of an ILog instance with the pattern specified.  Patterns may include any of the following values, which will be replaced with the coresponding ILog values: "{app}", "{time}", "{status}", "{type}", "{id}", "{message}", "{details}", "{object}".</summary>
        /// <example>
        /// <code>
        /// formatter.Format(log, "{app} {time} Type:{type} Status:{status}"
        ///  + (log.Identifier != null ? " Identifier:{id}" : string.Empty)
        ///  + "\r\n{message}\r\n{details}"
        ///  + (log.Object != null ? "\r\n{object}" : string.Empty)
        ///  + "\r\n\r\n");
        /// </code>
        /// </example>
        /// <param name="log">ILog instance to format.</param>
        /// <returns>String representation of the ILog instance in the specified format.</returns>
        public string Format(ILog log)
        {
            if (format == null)
            {
                format = "{app} {time} Type:{type} Status:{status}" + (log.Identifier != null ? " Identifier:{id}" : string.Empty) + "\r\n{message}\r\n{details}" + (log.Object != null ? "\r\n{object}" : string.Empty) + "\r\n\r\n";
            }
            return Format(log, format);
        }

        /// <summary>Formats a string representation of an ILog instance with the pattern specified.  Patterns may include any of the following values, which will be replaced with the coresponding ILog values: "{app}", "{time}", "{status}", "{type}", "{id}", "{message}", "{details}", "{object}".</summary>
        /// <example>
        /// <code>
        /// formatter.Format(log, "Log from {app} at {time} with status {status}\r\n{message}\r\n{details}");
        /// </code>
        /// </example>
        /// <param name="log">ILog instance to format.</param>
        /// <param name="pattern">Log format string.</param>
        /// <returns>String representation of the ILog instance in the specified format.</returns>
        public string Format(ILog log, string pattern)
        {
            pattern = pattern.Replace("{app}", log.Application != null ? log.Application : string.Empty);
            pattern = pattern.Replace("{time}", log.LogTime.ToShortDateString() + " " + log.LogTime.ToShortTimeString());
            pattern = pattern.Replace("{status}", log.Status.ToString());
            pattern = pattern.Replace("{type}", log.LogType.ToString());
            pattern = pattern.Replace("{id}", log.Identifier != null ? log.Identifier : string.Empty);
            pattern = pattern.Replace("{message}", log.Message != null ? log.Message : string.Empty);
            pattern = pattern.Replace("{details}", log.Details != null ? log.Details : string.Empty);
            pattern = pattern.Replace("{object}", log.Object != null ? log.Object.ToString() : string.Empty);
            return pattern;
        }
    }
}