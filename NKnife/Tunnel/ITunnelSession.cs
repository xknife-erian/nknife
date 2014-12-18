using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NKnife.Tunnel
{
    public interface ITunnelSession<TData, TSessionId>
    {
        TSessionId Id { get; }
        TData Data { get; }
    }
}
