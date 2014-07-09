using System;
using SocketKnife.Interfaces;

namespace NKnife.SocketClient.StarterKit.Base.ProtocolTools
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