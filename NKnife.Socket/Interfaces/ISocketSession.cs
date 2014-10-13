using System.Dynamic;
using System.Net;
using System.Net.Sockets;

namespace SocketKnife.Interfaces
{
    public interface ISocketSession
    {
        long Id { get; }

        EndPoint Point { get; }

        Socket Socket { get; }

        void Wirte(byte[] data);
    }
}