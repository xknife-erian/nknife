using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Protocols;

namespace SocketKnife.Protocol.Implementation
{
    public class ProtocolHead : IProtocolHead
    {
        public byte[] Head
        {
            get { return new byte[] {}; }
        }
    }
}