using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using SocketKnife.Common;
using SocketKnife.Generic.Filters;

namespace SocketKnife.Interfaces
{
    public interface ISocketFilterChain : ITunnelFilterChain<EndPoint, Socket, string>
    {
    }
}