using System;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel.Base
{
    public abstract class BaseTunnelFilter : ITunnelFilter
    {
        public abstract bool PrcoessReceiveData(ITunnelSession session);

        public abstract void ProcessSessionBroken(long id);

        public abstract void ProcessSessionBuilt(long id);

        public virtual void ProcessSendToSession(ITunnelSession session)
        {
            //默认啥也不干
        }

        public virtual void ProcessSendToAll(byte[] data)
        {
            //默认啥也不干
        }

        public abstract event EventHandler<SessionEventArgs> SendToSession;
        public abstract event EventHandler<SessionEventArgs> SendToAll;
        public abstract event EventHandler<SessionEventArgs> KillSession;
    }
}
