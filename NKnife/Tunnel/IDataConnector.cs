using System;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface IDataConnector<in TData>
    {
        bool Stop();
        bool Start();

        event EventHandler<SessionEventArgs> SessionBuilt;
        event EventHandler<SessionEventArgs> SessionBroken;
        event EventHandler<SessionEventArgs> DataReceived;
        void Send(long id, TData data);
        void SendAll(TData data);
        void KillSession(long id);
    }
}