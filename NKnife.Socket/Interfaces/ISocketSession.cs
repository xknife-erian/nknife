using System.Dynamic;
using System.Net;
using System.Net.Sockets;

namespace SocketKnife.Interfaces
{
    public interface ISocketSession
    {
        long Id { get; }

        EndPoint EndPoint { get; set; }

        Socket Socket { get; set; }

        bool WaitHeartBeatingReplay { get; set; }
    }
}