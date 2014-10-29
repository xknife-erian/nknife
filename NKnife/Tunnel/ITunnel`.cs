using System;
using System.Net;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface ITunnel<TSource, TConnector, TCommand> : IDisposable
    {
        void Bind(ITunnelCodec<TCommand> codec, IProtocolFamily<TCommand> protocolFamily);
        void AddHandlers(params IProtocolHandler<TSource, TConnector, TCommand>[] handlers);
        void RemoveHandler(IProtocolHandler<TSource, TConnector, TCommand> handler);
        ITunnelConfig Config { get; set; }
        void AddFilters(params ITunnelFilter<TSource, TConnector>[] filter);
        void RemoveFilter(ITunnelFilter<TSource, TConnector> filter);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}