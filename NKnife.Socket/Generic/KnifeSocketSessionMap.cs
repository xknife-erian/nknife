using System.Collections.Concurrent;
using System.Net;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSessionMap : ConcurrentDictionary<EndPoint, ISocketSession>, ISocketSessionMap
    {
    }
}