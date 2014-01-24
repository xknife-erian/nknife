using NLog;

namespace NKnife.Logging.LoggerWPFControl
{
    public struct LogMessage
    {
        public static LogMessage Build(LogEventInfo logEvent)
        {
            var lm = new LogMessage();
            lm.Time = logEvent.TimeStamp.ToString("HH:mm:ss fff");
            if (logEvent.LoggerName.LastIndexOf('.') > 0)
                lm.Source = logEvent.LoggerName.Substring(logEvent.LoggerName.LastIndexOf('.') + 1);
            else
                lm.Source = logEvent.LoggerName;
            lm.Message = logEvent.FormattedMessage;
            lm.Level = logEvent.Level;
            if (logEvent.HasStackTrace)
                lm.Message += logEvent.StackTrace.ToString();
            return lm;
        }
        public string Time { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public LogLevel Level { get; set; }
    }
}