using System.Collections.Generic;
using System.Net;
using NKnife.Base;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.Common
{
    class ServerList : Dictionary<Pair<IPAddress, int>, IKnifeSocketServer>
    {

    }
}