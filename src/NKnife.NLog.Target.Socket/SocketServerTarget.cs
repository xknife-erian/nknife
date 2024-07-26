using NLog;
using NLog.Targets;

namespace NKnife.NLog.Target.Socket
{
    [Target("SocketServerTarget")]
    public class SocketServerTarget : global::NLog.Targets.Target
    {
        protected override void Write(LogEventInfo logEvent)
        {
            // Do something
        }
    }
}
