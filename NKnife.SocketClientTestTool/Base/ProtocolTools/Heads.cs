using System;
using SocketKnife.Interfaces;

namespace NKnife.SocketClientTestTool.Base.ProtocolTools
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