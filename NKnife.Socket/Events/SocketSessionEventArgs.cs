using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Events
{
    public class SocketSessionEventArgs : SessionEventArgs<EndPoint, Socket>
    {
        public SocketSessionEventArgs(ISocketSession item) 
            : base(item)
        {
        }
    }
}
