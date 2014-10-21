﻿using System;
using System.Net;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface ITunnel<TSource, TConnector, TCommand> : IDisposable
    {
        void Bind(ITunnelCodec<TCommand> codec, IProtocolFamily<TCommand> protocolFamily, params IProtocolHandler<TSource, TConnector, TCommand>[] handlers);
        ITunnelConfig Config { get; set; }
        void AddFilter(ITunnelFilter<TSource, TConnector> filter);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}