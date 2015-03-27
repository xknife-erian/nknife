using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Common;

namespace SocketKnife
{
    public class SocketTunnel : BaseTunnel
    {
        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        protected override void SetFilterChain()
        {
            _FilterChain = new TunnelFilterChain();
        }
    }
}
