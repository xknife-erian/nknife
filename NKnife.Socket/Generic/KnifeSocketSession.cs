using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSession : ITunnelSession<EndPoint, Socket>
    {
        public KnifeSocketSession()
        {
            Id = DateTime.Now.Ticks;
        }

        public long Id { get; private set; }
        public EndPoint Source { get; set; }
        public Socket Connector { get; set; }

        public bool WaitingForReply { get; set; }

    }
}
