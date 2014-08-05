using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Protocol;

namespace NKnife.Socket.StarterKit.Protocols
{
    class KeepAliveTestFromServer : AbstractProtocol
    {
        public KeepAliveTestFromServer()
            : base("Socket-Client-StarterKit", "KeepAliveTestFromServer")
        {
        }
    }
}
