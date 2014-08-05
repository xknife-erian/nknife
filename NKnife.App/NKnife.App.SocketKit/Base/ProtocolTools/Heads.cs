using System;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Protocols;

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