using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol.Generic;

namespace NKnife.Tunnel
{
    public interface ITunnelProtocolFilter<TData, TSessionId> : ITunnelFilter<TData, TSessionId>
    {
        void AddHandlers(params KnifeProtocolHandlerBase<TData, TSessionId, TData>[] handlers);

        void RemoveHandler(KnifeProtocolHandlerBase<TData, TSessionId, TData> handler);
    }
}
