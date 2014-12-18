using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NKnife.IoC;
using NKnife.Tunnel.Generic;

namespace NKnife.Tunnel
{
    public class KnifeTunnel<TData, TSessionId> : TunnelBase<TData, TSessionId>
    {
        public override void Dispose()
        {
            
        }


        protected override void SetFilterChain()
        {
            _FilterChain = DI.Get<ITunnelFilterChain<TData, TSessionId>>();
        }
    }
}
