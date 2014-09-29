using System.Net;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerKnife
    {
        void Bind(IPAddress ipAddress, int port);
        ISocketServerConfig Config { get; }
        IFilterChain GetFilterChain();
        bool Start();
        bool ReStart();
        bool Stop();
    }
}