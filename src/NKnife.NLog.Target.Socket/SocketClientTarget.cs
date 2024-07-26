using NLog;
using NLog.Targets;

namespace NKnife.NLog.Target.Socket
{
    [Target("SocketClient")]
    public class SocketClientTarget : global::NLog.Targets.Target
    {
        protected override void Write(LogEventInfo logEvent)
        {
            // Do something
        }
    }
}