using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace SocketKnife.Interfaces
{
    public interface ISocketSessionMap : IDictionary<EndPoint, ISocketSession>
    {
    }
}