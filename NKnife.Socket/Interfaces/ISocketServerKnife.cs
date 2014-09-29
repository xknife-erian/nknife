using System.Net;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerKnife
    {
        void Bind(IPAddress ipAddress, int port);
        ISocketConfig GetConfig { get; }
        IFilterChain GetFilterChain();
        void Start();
        void ReStart();
        void Stop();
    }
}