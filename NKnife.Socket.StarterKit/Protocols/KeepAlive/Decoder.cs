using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Sockets;

namespace NKnife.Socket.StarterKit.Protocols.KeepAlive
{
    class Decoder : IDatagramDecoder
    {
        public bool HasLengthOnHead { get { return false; } }
        public string[] Execute(byte[] data, out int done)
        {
            done = data.Length;
            return new string[] { "KeepAliveTestFromServer" };
        }
    }
}
