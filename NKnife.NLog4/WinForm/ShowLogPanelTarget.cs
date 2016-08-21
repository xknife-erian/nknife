using System;
using NLog;
using NLog.Targets;

namespace NKnife.NLog.WinForm
{
    /// <summary>
    ///     这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个DataGridView以及一些交互的组合控件。
    ///     lukan@xknfe.net
    ///     2016.08.21
    /// </summary>
    [Target("LoggerGridView")]
    public class ShowLogPanelTarget : TargetWithLayout
    {
        private readonly LoggerGridView _LoggerGridView;

        public ShowLogPanelTarget()
        {
            _LoggerGridView = LoggerGridView.Instance;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                if (null != _LoggerGridView && !_LoggerGridView.IsDisposed)
                {
                    _LoggerGridView.ThreadSafeInvoke(
                        () => _LoggerGridView.ViewModel.AddLogInfo(logEvent));//.LogInfos.Insert(0, new CustomLogInfo(logEvent)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"向控件写日志发生异常.{e.Message}{e.StackTrace}");
            }
        }
    }
}