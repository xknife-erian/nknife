using System;
using System.Threading;
using System.Windows.Forms;
using NKnife.Collections;
using NLog;
using NLog.Targets;

namespace NKnife.NLog.WinForm
{
    /// <summary>
    ///     这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个ListView以及一些交互的组合控件。
    ///     lukan@xknfe.net
    ///     2016.08.21
    /// </summary>
    [Target("LoggerListView")]
    public class ShowLogPanelTarget : TargetWithLayout
    {
        private readonly LoggerListView _LoggerListView;

        private readonly SyncQueue<LogEventInfo> _LogQueue = new SyncQueue<LogEventInfo>();
        private bool _WriteEnable = true;

        public ShowLogPanelTarget()
        {
            _LoggerListView = LoggerListView.Instance;
            var thread = new Thread(() =>
            {
                while (_WriteEnable)
                {
                    if (_LogQueue.Count <= 0)
                        _LogQueue.AutoResetEvent.WaitOne();
                    var logEvent = _LogQueue.Dequeue();
                    _LoggerListView.ThreadSafeInvoke(() =>
                    {
                        if (logEvent != null)
                            _LoggerListView.ViewModel.AddLogInfo(logEvent);
                    });
                }
            }) {Name = "NKnife-NLog4ListView-Thread", IsBackground = true};
            Application.ApplicationExit += (s, e) =>
            {
                _WriteEnable = false;
                _LogQueue.AutoResetEvent.Set();
                Thread.Sleep(10);
                thread.Abort();
            };
            thread.Start();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                if (null != _LoggerListView && !_LoggerListView.IsDisposed)
                {
                    _LogQueue.Enqueue(logEvent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"向控件写日志发生异常.{e.Message}{e.StackTrace}");
            }
        }
    }
}