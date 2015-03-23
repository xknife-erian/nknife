using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface IDataConnector<TSessionId, in TDataIn>
    {
        bool Stop();
        bool Start();

        event EventHandler<SessionEventArgs<TSessionId>> SessionBuilt;
        event EventHandler<SessionEventArgs<TSessionId>> SessionBroken;
        event EventHandler<SessionEventArgs<TSessionId>> DataReceived;

        void Send(TSessionId id, TDataIn data);
        void SendAll(TDataIn data);
        void KillSession(TSessionId id);
    }
}
