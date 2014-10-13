using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Protocols
{
    public class ProtocolHead : IProtocolHead
    {
        public byte[] Head
        {
            get { return new byte[] { }; }
        }
    }
}
