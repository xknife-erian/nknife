using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NKnife.Tunnel.Events;

namespace SocketKnife.Events
{
    public class SocketDataFetchedEventArgs : DataFetchedEventArgs<EndPoint>
    {
        public SocketDataFetchedEventArgs(EndPoint source, byte[] data) 
            : base(source, data)
        {
        }
    }
}
