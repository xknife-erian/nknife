using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Protocols;

namespace NKnife.Socket.StarterKit.Protocols.KeepAlive
{
    class Package : IProtocolPackage
    {
        public short Version {
            get { return 1; } 
        }
        public string Combine(IProtocolContent pc)
        {
            return "KeepAliveTestFromClient";
        }
    }
}
