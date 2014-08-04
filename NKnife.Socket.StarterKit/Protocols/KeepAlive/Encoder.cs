using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Sockets;

namespace NKnife.Socket.StarterKit.Protocols.KeepAlive
{
    class Encoder : IDatagramEncoder
    {
        public byte[] Execute(string replay, bool isCompress)
        {
            return Encoding.Default.GetBytes("KeepAliveTestFromClient");
        }
    }
}
