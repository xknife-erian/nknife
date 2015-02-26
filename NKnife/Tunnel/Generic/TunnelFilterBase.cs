using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Events;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel.Generic
{
    public abstract class TunnelFilterBase<TData, TSessionId> : ITunnelFilter<TData, TSessionId>
    {
        public abstract bool PrcoessReceiveData(ITunnelSession<TData, TSessionId> session);

        public abstract void ProcessSessionBroken(TSessionId id);

        public abstract void ProcessSessionBuilt(TSessionId id);

        public virtual void ProcessSendToSession(ITunnelSession<TData, TSessionId> session)
        {
            //默认啥也不干
        }

        public virtual void ProcessSendToAll(TData data)
        {
            //默认啥也不干
        }

        public abstract event EventHandler<SessionEventArgs<TData, TSessionId>> OnSendToSession;
        public abstract event EventHandler<EventArgs<TData>> OnSendToAll;
        public abstract event EventHandler<EventArgs<TSessionId>> OnKillSession;
    }
}
