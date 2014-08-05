using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Protocols;

namespace NKnife.Socket.StarterKit.Protocols.KeepAlive
{
    class Parser : IProtocolParser
    {
        public short Version {
            get { return 1; }
        }

        public void Execute(ref IProtocolContent content, string data, string family, string command)
        {
            content.Command = command;
        }
    }
}
