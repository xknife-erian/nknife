using System.Text;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Default
{
    public class ProtocolTail : IProtocolTail
    {
        #region IProtocolTail Members

        public byte[] Tail
        {
            get { return Encoding.UTF8.GetBytes("ö"); }
        }

        #endregion
    }
}