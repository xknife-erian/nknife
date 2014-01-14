using System.Text;
using NKnife.NetWork.Interfaces;

namespace Gean.Network.Protocol.Implementation
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