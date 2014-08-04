using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Protocols;

namespace NKnife.Socket.StarterKit.Protocols.KeepAlive
{
    class Heads : IProtocolHead
    {
        public byte[] Head {
            get { return new byte[0];}
        }
    }
}
