﻿using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class ProtocolHead : IProtocolHead
    {
        public byte[] Head
        {
            get { return new byte[] { }; }
        }
    }
}
