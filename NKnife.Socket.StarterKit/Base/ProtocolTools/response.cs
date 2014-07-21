using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Protocol;

namespace NKnife.Socket.StarterKit.Base.ProtocolTools
{
    class response : AbstractProtocol
    {
        public response()
            : base("Socket-Client-StarterKit", "response")
        {
        }
    }
}