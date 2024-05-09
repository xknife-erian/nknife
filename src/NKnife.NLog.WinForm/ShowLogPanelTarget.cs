using System;
using System.Threading;
using System.Windows.Forms;
using NKnife.NLog.WinForm.Util;
using NLog;
using NLog.Targets;

namespace NKnife.NLog.WinForm
{
    /// <summary>
    ///     这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个自定义显示的组合控件。
    ///     lukan@xknfe.net
    ///     2016.08.21
    /// </summary>
    [Target("LoggerListView")]
    public class ShowLogPanelTarget : TargetWithLayout
    {
        private readonly LoggerListViewViewModel _loggerListViewViewModel = LoggerListViewViewModel.Instance;

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                _loggerListViewViewModel.AddLogInfo(logEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"向控件写日志发生异常.{e.Message}{e.StackTrace}");
            }
        }
    }
}