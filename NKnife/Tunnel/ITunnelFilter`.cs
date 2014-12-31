using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelFilter<TData, TSessionId>
    {
        void BindSessionProvider(ISessionProvider<TData, TSessionId> sessionProvider);
        bool ContinueNextFilter { get; }

        void PrcoessReceiveData(ITunnelSession<TData, TSessionId> session);

        void ProcessSessionBroken(TSessionId id);
        void ProcessSessionBuilt(TSessionId id);
        void PrcoessSendData(ITunnelSession<TData, TSessionId> session);
    }
}
