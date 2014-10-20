using System;
using System.Collections.Generic;
using System.Windows.Documents;
using NKnife.Tunnel;

namespace NKnife.Protocol
{
    public interface IProtocolHandler<TSource, TConnector, T>
    {
        ITunnelSessionMap<TSource, TConnector> SessionMap { get; set; }

        List<T> Commands { get; set; }

        void Recevied(ITunnelSession<TSource, TConnector> session, IProtocol<T> protocol);

        void Write(ITunnelSession<TSource, TConnector> session, byte[] data);

        void Write(ITunnelSession<TSource, TConnector> session, IProtocol<T> data);
    }
}