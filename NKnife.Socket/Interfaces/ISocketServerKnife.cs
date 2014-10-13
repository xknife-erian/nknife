using System;
using System.Net;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerKnife
    {
        void Bind(IPAddress ipAddress, int port);
        void Bind(IProtocolHandler handler);
        ISocketServerConfig Config { get; }
        ISocketPolicy Policy { get; }
        void Attach(IProtocolFamily protocolFamily);
        void Attach(ISocketPlan socketPlan);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}