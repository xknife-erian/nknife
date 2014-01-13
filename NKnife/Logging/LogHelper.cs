using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Gean.Logging
{
    /// <summary>
    /// Helper class for logging.
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// Logs to the console.
        /// </summary>
        /// <typeparam name="T">The datatype of the caller that is logging the event.</typeparam>
        /// <param name="level">The log level</param>
        /// <param name="message">Message to log</param>
        /// <param name="ex">Exception to log</param>
        /// <param name="args">Additional arguments.</param>
        public void LogToConsole<T>(LogLevel level, string message, Exception ex, object[] args)
        {
            LogEvent logevent = BuildLogEvent(typeof (T), level, message, ex, null);
            Console.WriteLine(logevent.FinalMessage);
        }

        /// <summary>
        /// Construct the logevent using the values supplied.
        /// Fills in other data values in the log event.
        /// </summary>
        /// <param name="level">The log level</param>
        /// <param name="message">Message to log</param>
        /// <param name="ex">Exception to log</param>
        /// <param name="args">Additional args.</param>
        public static LogEvent BuildLogEvent(Type logType, LogLevel level, object message, Exception ex, object[] args)
        {
            var logevent = new LogEvent();
            logevent.Level = level;
            logevent.Message = message;
            logevent.Error = ex;
            logevent.Args = args;
            logevent.Computer = Environment.MachineName;
            logevent.CreateTime = DateTime.Now;
            logevent.ThreadName = Thread.CurrentThread.Name;
            logevent.LogType = logType;
            logevent.FinalMessage = LogFormatter.Format(null, logevent);
            return logevent;
        }

        /// <summary>
        /// Build the log file name.
        /// </summary>
        /// <param name="loglevel">Log level : "critical | error | warning | info | debug"</param>
        /// Name of logfile containing substituions. </param>
        /// <returns></returns>
        public static LogLevel GetLogLevel(string loglevel)
        {
            var level = (LogLevel) Enum.Parse(typeof (LogLevel), loglevel, true);
            return level;
        }

        /// <summary>
        /// Build the log file name.
        /// </summary>
        /// <param name="appName">E.g. "StockMarketApplication".</param>
        /// <param name="date">E.g. Date to put in the name.</param>
        /// <param name="env">Environment name. E.g. "DEV", "PROD".</param>
        /// <param name="logFileName">E.g. "%name%-%yyyy%-%MM%-%dd%-%env%.log".
        /// Name of logfile containing substituions. </param>
        /// <returns></returns>
        public static string BuildLogFileName(string logFileName, string appName, DateTime date, string env)
        {
            if (string.IsNullOrEmpty(env)) env = string.Empty;

            // Log file name = <app>-<date>-<env>.log
            // e.g.  StockMarketApp-2009-10-30-PROD.log
            IDictionary<string, string> subs = new Dictionary<string, string>();
            subs["%datetime%"] = date.ToString("yyyy-MM-dd-HH-mm-ss");
            subs["%date%"] = date.ToString("yyyy-MM-dd");
            subs["%yyyy%"] = date.ToString("yyyy");
            subs["%MM%"] = date.ToString("MM");
            subs["%dd%"] = date.ToString("dd");
            subs["%MMM%"] = date.ToString("MMM");
            subs["%hh%"] = date.ToString("hh");
            subs["%HH%"] = date.ToString("HH");
            subs["%mm%"] = date.ToString("mm");
            subs["%ss%"] = date.ToString("ss");
            subs["%name%"] = appName;
            subs["%env%"] = env.ToUpper();
            subs.ForEach(pair => { logFileName = logFileName.Replace(pair.Key, pair.Value); });
            if (!logFileName.Contains(".log") && !logFileName.Contains(".txt"))
                logFileName += ".log";

            // Replace any left over % with underscore "_".
            logFileName = logFileName.Replace("%", "_");
            logFileName = logFileName.Replace("--", "-");
            logFileName = logFileName.Replace("__", "_");
            if (logFileName.StartsWith("-")) logFileName = "Log" + logFileName;
            if (logFileName.StartsWith("_")) logFileName = "Log" + logFileName;

            return logFileName;
        }
    }
}