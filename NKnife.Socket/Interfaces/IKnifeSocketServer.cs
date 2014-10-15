using System;
using System.Net;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;

namespace SocketKnife.Interfaces
{
    public interface IKnifeSocketServer : IDisposable
    {
        void Configure(IPAddress ipAddress, int port);
        void Bind(IProtocolFamily protocolFamily, KnifeProtocolHandler handler);
        ISocketServerConfig Config { get; }
        void AddFilter(KnifeSocketServerFilter filter);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}