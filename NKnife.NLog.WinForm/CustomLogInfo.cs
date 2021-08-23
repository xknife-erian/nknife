using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace NKnife.NLog.WinForm
{
    class CustomLogInfo
    {
        public CustomLogInfo()
        {
        }

        public CustomLogInfo(LogEventInfo logInfo)
        {
            LogInfo = logInfo;
        }

        public DateTime DateTime => LogInfo.TimeStamp;
        public LogEventInfo LogInfo { get; set; }
        public string Source => LogInfo.LoggerName;
        public LogLevel LogLevel => LogInfo.Level;
    }
}
