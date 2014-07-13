using System;
using SocketKnife.Interfaces;

namespace NKnife.Socket.StarterKit.Base.ProtocolTools
{
    public class Heads : IProtocolHead
    {
        #region IProtocolHead Members

        public byte[] Head
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}