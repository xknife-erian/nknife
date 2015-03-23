﻿using System;
using System.Collections.Generic;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    /// <summary>
    /// Tunnel与Protocol的交互。
    /// 一般来讲，本接口的实现将会被ITunnelFilter调用。并且一个Filter可以有多个Handler,这时候不同Handler可以拥有它自有的Command集合的处理能力。
    /// </summary>
    /// <typeparam name="TData">Tunnel传递给Protocol的数据类型</typeparam>
    public interface ITunnelProtocolHandler<TData>
    {
        List<TData> Commands { get; set; }
        ITunnelCodec<TData> Codec { get; set; }
        void Recevied(long session, IProtocol<TData> protocol);

        event EventHandler<SessionEventArgs> OnSendToSession;

        event EventHandler<SessionEventArgs> OnSendToAll;
    }
}