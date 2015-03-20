using System;
using System.Collections.Generic;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelProtocolHandler<TSessionId, TData, TSource>
    {
        List<TData> Commands { get; set; }
        ITunnelCodec<TData, TSource> Codec { get; set; }
        void Recevied(TSessionId session, IProtocol<TData> protocol);

        event EventHandler<SessionEventArgs<TSessionId, TData>> OnSendToSession;

        event EventHandler<EventArgs<TData>> OnSendToAll;
    }
}