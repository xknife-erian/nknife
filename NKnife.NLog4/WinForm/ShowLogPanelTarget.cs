using System;
using System.Threading;
using System.Windows.Forms;
using NKnife.Collections;
using NKnife.Extensions;
using NLog;
using NLog.Targets;

namespace NKnife.NLog.WinForm
{
    /// <summary>
    ///     这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个DataGridView以及一些交互的组合控件。
    ///     lukan@xknfe.net
    ///     2016.08.21
    /// </summary>
    [Target("LoggerListView")]
    public class ShowLogPanelTarget : TargetWithLayout
    {
        private readonly LoggerListView _loggerListView;

        private readonly SyncQueue<LogEventInfo> _logQueue = new SyncQueue<LogEventInfo>();
        private bool _writeEnable = true;

        public ShowLogPanelTarget()
        {
            _loggerListView = LoggerListView.Instance;
            var thread = new Thread(() =>
            {
                while (_writeEnable)
                {
                    if (_logQueue.Count <= 0)
                        _logQueue.AddEvent.WaitOne();
                    LogEventInfo logEvent;
                    bool has = _logQueue.TryDequeue(out logEvent);
                    if (!has)
                        continue;
                    _loggerListView.ThreadSafeInvoke(() =>
                    {
                        if (logEvent != null)
                            _loggerListView.ViewModel.AddLogInfo(logEvent);
                    });
                }
            }) {Name = "NKnife-NLog4ListView-Thread", IsBackground = true};
            Application.ApplicationExit += (s, e) =>
            {
                _writeEnable = false;
                _logQueue.AddEvent.Set();
                Thread.Sleep(10);
                thread.Abort();
            };
            thread.Start();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                if (null != _loggerListView && !_loggerListView.IsDisposed)
                {
                    _logQueue.Enqueue(logEvent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"向控件写日志发生异常.{e.Message}{e.StackTrace}");
            }
        }
    }
}