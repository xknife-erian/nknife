﻿using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Default
{
    public class ProtocolHead : IProtocolHead
    {
        public byte[] Head
        {
            get { return new byte[] { }; }
        }
    }
}
