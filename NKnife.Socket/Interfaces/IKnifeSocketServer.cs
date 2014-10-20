using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Protocol;
using NKnife.Tunnel;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;

namespace SocketKnife.Interfaces
{
    public interface IKnifeSocketServer : IKnifeServer<EndPoint, Socket, string>
    {
        void Configure(IPAddress ipAddress, int port);
    }
}