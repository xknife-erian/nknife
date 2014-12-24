using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel.Generic
{
    public abstract class TunnelFilterBase<TData, TSessionId> : ITunnelFilter<TData, TSessionId>
    {
        public IFilterListener<TData, TSessionId> Listener { get; set; }

        public bool ContinueNextFilter { get; protected set; }

        public abstract void PrcoessReceiveData(ITunnelSession<TData, TSessionId> session);

        public abstract void ProcessSessionBroken(TSessionId id);

        public abstract void ProcessSessionBuilt(TSessionId id);

        public virtual void PrcoessSendData(ITunnelSession<TData, TSessionId> session)
        {
            //默认啥也不干
        }
    }
}
