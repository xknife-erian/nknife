using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface IDataConnector<TData, TSessionId> : ISessionProvider<TData, TSessionId>
    {
        bool Stop();
        bool Start();

        event EventHandler<SessionEventArgs<TData, TSessionId>> SessionBuilt;
        event EventHandler<SessionEventArgs<TData, TSessionId>> SessionBroken;
        event EventHandler<SessionEventArgs<TData, TSessionId>> DataReceived;
        event EventHandler<SessionEventArgs<TData, TSessionId>> DataSent;
    }
}
