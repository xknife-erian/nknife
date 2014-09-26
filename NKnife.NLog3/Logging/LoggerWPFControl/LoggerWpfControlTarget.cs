using System;
using System.Windows;
using NLog;
using NLog.Targets;

namespace NKnife.NLog3.Logging.LoggerWPFControl
{
    /// <summary>
    /// 这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个WPF控件可绑定的ObservableCollection
    /// </summary>
    [Target("ShowLogControl4WPF")]
    public class LoggerWpfControlTarget : TargetWithLayout
    {
        private readonly LogMessageCollection _LogList = LogMessageCollection.ME;
        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                if (Application.Current == null || Application.Current.Dispatcher == null)
                    return;
                if (Application.Current.Dispatcher.CheckAccess())
                {
                    AddLogMessage(logEvent);
                }
                else
                {
                    var logDelegate = new LogMessageWriter(AddLogMessage);
                    Application.Current.Dispatcher.BeginInvoke(logDelegate, new object[] {logEvent});
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("向控件写日志发生异常.{0}{1}", e.Message, e.StackTrace));
            }
        }

        protected void AddLogMessage(LogEventInfo logEvent)
        {
            TrimLogMessageCollection();
            _LogList.Insert(0, LogMessage.Build(logEvent));
        }

        private void TrimLogMessageCollection()
        {
            if (_LogList.Count >= 200)
            {
                while (_LogList.Count >= 200)
                    _LogList.RemoveAt(_LogList.Count - 1);
            }
        }

        private delegate void LogMessageWriter(LogEventInfo logEvent);
    }
}