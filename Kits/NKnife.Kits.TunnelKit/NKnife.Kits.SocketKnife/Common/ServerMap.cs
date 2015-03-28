using System.Collections.Generic;
using System.Net;
using NKnife.Base;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.Common
{
    public class ServerMap : Dictionary<IPEndPoint, IKnifeSocketServer>
    {

    }
}