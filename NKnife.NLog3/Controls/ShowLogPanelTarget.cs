using System;
using NKnife.IoC;
using NLog;
using NLog.Targets;

namespace NKnife.NLog3.Controls
{
    /// <summary>
    /// 这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个ListView控件
    /// </summary>
    [Target("LogPanel")]
    public class ShowLogPanelTarget : TargetWithLayout
    {
        public ShowLogPanelTarget()
        {
            _LogPanel = LogPanel.Instance;
        }

        private readonly LogPanel _LogPanel;

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                if (null != _LogPanel && !_LogPanel.IsDisposed)
                    _LogPanel.AddLog(logEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine($"向控件写日志发生异常.{e.Message}{e.StackTrace}");
            }
        }
    }
}