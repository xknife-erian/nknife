﻿using System.Text;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Protocols;

namespace SocketKnife.Protocol.Implementation
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