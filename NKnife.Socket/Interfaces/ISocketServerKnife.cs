using System.Net;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerKnife
    {
        void Bind(IPAddress ipAddress, int port);
        ISocketServerConfig Config { get; }
        IFilterChain FilterChain { get; }
        void Attach(IProtocolTools protocolTools);
        void Attach(ISocketPlan socketPlan);
        bool Start();
        bool ReStart();
        bool Stop();
    }
}