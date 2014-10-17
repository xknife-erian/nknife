using System;
using System.Net;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface IKnifeServer<TSource, TConnector> : IDisposable
    {
        void Bind(ITunnelCodec codec, IProtocolFamily protocolFamily, IProtocolHandler<TSource, TConnector> handler);
        ITunnelConfig Config { get; }
        void AddFilter(ITunnelFilter<TSource, TConnector> filter);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}