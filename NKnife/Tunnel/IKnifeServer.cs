using System;
using System.Net;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface IKnifeServer<TSource, TConnector, TCommand> : IDisposable
    {
        void Bind(ITunnelCodec<TCommand> codec, IProtocolFamily protocolFamily, IProtocolHandler<TSource, TConnector> handler);
        ITunnelConfig Config { get; }
        void AddFilter(ITunnelFilter<TSource, TConnector, TCommand> filter);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}