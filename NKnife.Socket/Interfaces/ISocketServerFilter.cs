using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerFilter : ITunnelFilter<EndPoint, Socket, string>
    {
        void Bind(
            Func<KnifeSocketProtocolFamily> familyGetter,
            Func<KnifeSocketProtocolHandler[]> handlerGetter,
            Func<KnifeSocketSessionMap> sessionMapGetter,
            Func<KnifeSocketCodec> codecFunc 
            );
    }
}