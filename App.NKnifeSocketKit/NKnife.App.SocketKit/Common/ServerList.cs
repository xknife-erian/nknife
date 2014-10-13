using System.Collections.Generic;
using System.Net;
using NKnife.Base;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Common
{
    class ServerList : Dictionary<Pair<IPAddress, int>, IKnifeSocketServer>
    {

    }
}