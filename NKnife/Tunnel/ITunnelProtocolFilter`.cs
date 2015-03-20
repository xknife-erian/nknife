using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel
{
    public interface ITunnelProtocolFilter<TSessionId, TData> : ITunnelFilter<TSessionId, TData>
    {
        void AddHandlers(params KnifeProtocolHandlerBase<TData, TSessionId, TData>[] handlers);

        void RemoveHandler(KnifeProtocolHandlerBase<TData, TSessionId, TData> handler);
    }
}
