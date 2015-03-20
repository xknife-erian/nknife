using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel.Common
{
    public class TunnelSession<TSessionId> : ITunnelSession<TSessionId, byte[]>
    {
        public TSessionId Id { get; set; }
        public byte[] Data { get; set; }
        public object Tag { get; set; }
    }
}
