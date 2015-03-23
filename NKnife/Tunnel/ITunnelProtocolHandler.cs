using System;
using System.Collections.Generic;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelProtocolHandler<TData>
    {
        List<TData> Commands { get; set; }
        ITunnelCodec<TData> Codec { get; set; }
        void Recevied(long session, IProtocol<TData> protocol);

        event EventHandler<SessionEventArgs> OnSendToSession;

        event EventHandler<EventArgs> OnSendToAll;
    }
}