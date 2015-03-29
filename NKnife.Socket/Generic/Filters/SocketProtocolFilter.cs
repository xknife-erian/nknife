using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using NKnife.Tunnel.Filters;
using NKnife.Utility;

namespace SocketKnife.Generic.Filters
{
    public class SocketProtocolFilter : ProtocolFilter<string>
    {
    }
}
