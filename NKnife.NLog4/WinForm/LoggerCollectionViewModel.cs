using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using NLog;

namespace NKnife.NLog.WinForm
{
    public class LoggerCollectionViewModel
    {
        internal Level CurrentLevel { get; set; } 
            = Level.Trace | Level.Debug | Level.Info | Level.Warn | Level.Error | Level.Fatal;

        public int MaxViewCount { get; set; } = 300;

        public ObservableCollection<CustomLogInfo> LogInfos { get; } = new ObservableCollection<CustomLogInfo>();

        public void AddLogInfo(LogEventInfo logEvent)
        {
            if (LogInfos.Count >= MaxViewCount)
                LogInfos.RemoveAt(LogInfos.Count - 1);
            if (CurrentLevel.HasFlag(GetTopLevel(logEvent.Level)))
                LogInfos.Insert(0, new CustomLogInfo(logEvent));
        }

        private static Level GetTopLevel(LogLevel logLevel)
        {
            Level result;
            if (!Enum.TryParse(logLevel.Name, out result))
            {
                result = Level.None;
            }
            return result;
        }
    }

    [Flags]
    internal enum Level : byte
    {
        None = 0,
        Trace = 1,
        Debug = 2,
        Info = 4,
        Warn = 8,
        Error = 16,
        Fatal = 32
    }
}