using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Protocols;

namespace NKnife.Socket.StarterKit.Base.ProtocolTools
{
    public class Tails : IProtocolTail
    {
        #region IProtocolTail Members

        public byte[] Tail
        {
            get { return new byte[] {}; }
        }

        #endregion
    }
}