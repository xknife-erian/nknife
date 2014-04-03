using NKnife.NetWork.Interfaces;

namespace NKnife.NetWork.Protocol.Implementation
{
    public class ProtocolHead : IProtocolHead
    {
        public byte[] Head
        {
            get { return new byte[] { }; }
        }
    }
}
