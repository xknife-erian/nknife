using System;
using NLog;
using NLog.Targets;

namespace NKnife.Logging.LogPanel
{
    /// <summary>
    /// 这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个ListView控件
    /// </summary>
    [Target("LogPanel")]
    public class ShowLogPanelTarget : TargetWithLayout
    {
        public ShowLogPanelTarget()
        {
           
            LogPanel = LogPanel.ME;
        }

        public LogPanel LogPanel { get; private set; }

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                if (null != LogPanel && !LogPanel.IsDisposed)
                    LogPanel.AddLog(logEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("向控件写日志发生异常.{0}{1}", e.Message, e.StackTrace));
            }
        }
    }
}