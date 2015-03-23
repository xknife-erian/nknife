using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel.Common
{
    public class TunnelSession : ITunnelSession
    {
        public long Id { get; set; }
        public byte[] Data { get; set; }
        public object Tag { get; set; }
    }
}
