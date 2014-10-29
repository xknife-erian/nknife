using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Families;

namespace SocketKnife.Interfaces
{
    public interface ISocketFilter : ITunnelFilter<EndPoint, Socket>
    {
        void BindGetter(
            Func<KnifeSocketCodec> codecFunc,
            Func<IList<KnifeSocketProtocolHandler>> handlersGetter, 
            Func<StringProtocolFamily> familyGetter);
    }
}