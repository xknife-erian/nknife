using SocketKnife.Interfaces;

namespace NKnife.SocketClient.StarterKit.Base.ProtocolTools
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