using System;
using NLog.Common;
using NLog.Targets;

namespace NKnife.NLog.WPF
{
    [Target("NLogViewer")]
    public class NLogViewerTarget : Target
    {
        public event Action<AsyncLogEventInfo> LogReceived;

        protected override void Write(global::NLog.Common.AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);

            if (LogReceived != null)
                LogReceived(logEvent);
        }
    }
}
