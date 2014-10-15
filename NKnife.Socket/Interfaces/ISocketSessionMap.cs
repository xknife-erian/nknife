using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using NKnife.Events;

namespace SocketKnife.Interfaces
{
    public interface ISocketSessionMap : IDictionary<EndPoint, ISocketSession>
    {
        event EventHandler<EventArgs<EndPoint>> Removed;
    }
}