using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSession : ISocketSession
    {
        public KnifeSocketSession()
        {
            Id = DateTime.Now.Ticks;
        }

        public long Id { get; private set; }
        public EndPoint Point { get; set; }
        public Socket Socket { get; set; }

    }
}
