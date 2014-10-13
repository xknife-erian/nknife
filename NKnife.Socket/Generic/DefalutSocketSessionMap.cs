using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class DefalutSocketSessionMap : Dictionary<EndPoint, ISocketSession>, ISocketSessionMap
    {
    }
}
