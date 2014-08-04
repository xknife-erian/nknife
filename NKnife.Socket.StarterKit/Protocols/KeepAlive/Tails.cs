using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Protocols;

namespace NKnife.Socket.StarterKit.Protocols.KeepAlive
{
    class Tails : IProtocolTail
    {
        public byte[] Tail {
            get { return new byte[0];}
        }
    }
}
