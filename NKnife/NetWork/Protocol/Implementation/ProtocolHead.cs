using Gean.Network.Interfaces;

namespace Gean.Network.Protocol.Implementation
{
    public class ProtocolHead : IProtocolHead
    {
        public byte[] Head
        {
            get { return new byte[] { }; }
        }
    }
}
