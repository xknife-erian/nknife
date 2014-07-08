using SocketKnife.Interfaces;

namespace NKnife.SocketClientTestTool.Base.ProtocolTools
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