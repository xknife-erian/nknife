using System.Dynamic;
using System.Net;
using System.Net.Sockets;

namespace SocketKnife.Interfaces
{
    public interface ISocketSession
    {
        long Id { get; }

        EndPoint Point { get; set; }

        Socket Socket { get; set; }

        void Wirte(byte[] data);
    }
}