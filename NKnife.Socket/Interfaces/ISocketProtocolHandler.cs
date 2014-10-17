using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NKnife.Protocol;
using NKnife.Tunnel;

namespace SocketKnife.Interfaces
{
    public interface ISocketProtocolHandler : IProtocolHandler<EndPoint, Socket>
    {
    }
}
