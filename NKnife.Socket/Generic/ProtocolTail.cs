using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
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