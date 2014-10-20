using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NKnife.Tunnel.Events;

namespace SocketKnife.Events
{
    public class SocketConnectionBreakEventArgs : ConnectionBreakEventArgs<EndPoint>
    {
        public SocketConnectionBreakEventArgs(EndPoint endPoint, string message) : base(endPoint, message)
        {
        }
    }
}
