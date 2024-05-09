using System;
using NLog;
using NLog.Common;
using NLog.Targets;

namespace NKnife.NLog.WPF
{
    [Target("NLogViewer")]
    public class NLogViewerTarget : Target
    {
        private readonly LogStack _logStack = LogStack.Instance;

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);
            try
            {
                _logStack.AddLog(logEvent.LogEvent);
            }
            catch (Exception e)
            {
#if DEBUG
                throw new InvalidOperationException($"Writing logs into the stack occurs normally: {logEvent.LogEvent}");
#endif
            }
        }
    }
}
