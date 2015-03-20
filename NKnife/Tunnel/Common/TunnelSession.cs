using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel.Common
{
    public class TunnelSession<TSessionId, TData> : ITunnelSession<TSessionId, TData>
    {
        public TSessionId Id { get; set; }
        public TData Data { get; set; }
        public object Tag { get; set; }
    }
}
