using System;
using NKnife.Tunnel;

namespace NKnife.Protocol
{
    public interface IProtocolHandler<TSource, TConnector>
    {
        ITunnelSessionMap<TSource, TConnector> SessionMap { get; set; }

        void Recevied(ITunnelSession<TSource, TConnector> session, IProtocol protocol);

        void Write(ITunnelSession<TSource, TConnector> session, byte[] data);

        void Write(ITunnelSession<TSource, TConnector> session, IProtocol data);
    }
}