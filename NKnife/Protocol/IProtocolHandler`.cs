using System;
using System.Collections.Generic;
using System.Windows.Documents;
using NKnife.Events;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;

namespace NKnife.Protocol
{
    public interface IProtocolHandler<TData, TSessionId, T>
    {
        List<T> Commands { get; set; }
        void Recevied(TSessionId session, IProtocol<T> protocol);
        event EventHandler<SessionEventArgs<TData, TSessionId>> OnSendToSession;
        event EventHandler<EventArgs<TData>> OnSendToAll;
    }
}