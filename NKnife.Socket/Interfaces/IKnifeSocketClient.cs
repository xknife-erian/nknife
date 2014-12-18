using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using SocketKnife.Common;
using SocketKnife.Generic;

namespace SocketKnife.Interfaces
{
    public interface IKnifeSocketClient : IDataConnector<byte[], EndPoint>
    {
        KnifeSocketConfig Config { get; set; }
        void Configure(IPAddress ipAddress, int port);

    }
}