using System;
using System.Net;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerKnife
    {
        void Bind(IPAddress ipAddress, int port);
        void Bind(ProtocolHandlerBase handler);
        ISocketServerConfig Config { get; }
        void AddFilter(KeepAliveFilter filter);
        void Attach(IProtocolFamily protocolFamily);
        void Attach(ISocketPlan socketPlan);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}