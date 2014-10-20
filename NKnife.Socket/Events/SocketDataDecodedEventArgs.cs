using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NKnife.Tunnel.Events;

namespace SocketKnife.Events
{
    public class SocketDataDecodedEventArgs : DataDecodedEventArgs<EndPoint>
    {
        public SocketDataDecodedEventArgs(EndPoint source, string[] datas) : base(source, datas)
        {
        }
    }
}
