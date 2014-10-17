using System.Dynamic;
using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;

namespace SocketKnife.Interfaces
{
    public interface ISocketSession: ITunnelSession<EndPoint, Socket>
    {
    }
}