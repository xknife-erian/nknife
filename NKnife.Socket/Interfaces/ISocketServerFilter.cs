using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Common;
using SocketKnife.Events;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerFilter : ITunnelFilter<EndPoint, Socket>
    {
        void Bind(
            Func<IProtocolFamily> familyGetter, 
            Func<IProtocolHandler<EndPoint, Socket>> handlerGetter, 
            Func<ISocketSessionMap> sessionMapGetter,
            Func<ISocketCodec> codecFunc 
            );
    }
}