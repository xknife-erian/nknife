using System.Text;

namespace Gean.Logging
{
    /// <summary>
    /// Log formatter.
    /// </summary>
    public class LogFormatter
    {
        /// <summary>
        /// Quick formatter that toggles between delimited and xml.
        /// </summary>
        /// <param name="formatter"></param>
        /// <param name="logEvent"></param>
        public static string Format(string formatter, LogEvent logEvent)
        {
            if (string.IsNullOrEmpty(formatter))
                return Format(logEvent);

            if (formatter.ToLower().Trim() == "xml")
                return FormatXml(logEvent);

            return Format(logEvent);
        }

        /// <summary>
        /// Builds the log message using message and arguments.
        /// </summary>
        public static string Format(LogEvent logEvent)
        {
            string msg = ConvertToString(logEvent.Args);
            string message = logEvent.Message.ToString();
            msg = string.IsNullOrEmpty(msg) ? message : message + " - " + msg;

            // Build a delimited string
            // <time>:<thread>:<level>:<loggername>:<message>
            string line = logEvent.CreateTime.ToString();

            if (!string.IsNullOrEmpty(logEvent.ThreadName)) line += ":" + logEvent.ThreadName;
            line += ":" + logEvent.Level.ToString();
            line += ":" + logEvent.LogType.Name;
            line += ":" + msg;
            return line;
        }

        private static string ConvertToString(object[] args)
        {
            if (args == null || args.Length == 0)
                return string.Empty;

            var buffer = new StringBuilder();
            foreach (object arg in args)
            {
                if (arg != null)
                    buffer.Append(arg.ToString());
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Builds the log message using message and arguments.
        /// </summary>
        public static string FormatXml(LogEvent logEvent)
        {
            string msg = ConvertToString(logEvent.Args);
            string message = logEvent.Message.ToString();
            msg = string.IsNullOrEmpty(msg) ? message : message + " - " + msg;

            // Build a delimited string
            // <time>:<thread>:<level>:<loggername>:<message>
            string line = string.Format("<time>{0}</time>", logEvent.CreateTime.ToString());

            if (!string.IsNullOrEmpty(logEvent.ThreadName)) line += string.Format("<thread>{0}</thread>", logEvent.ThreadName);
            line += string.Format("<level>{0}</level>", logEvent.Level.ToString());
            line += string.Format("<type>{0}</type>", logEvent.LogType.Name);
            line += string.Format("<message>{0}</message>", msg);
            return line;
        }
    }
}