using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using SocketKnife.Common;

namespace SocketKnife.Interfaces
{
    public interface IKnifeSocketClient : ITunnel<EndPoint, Socket, string>
    {
    }
}