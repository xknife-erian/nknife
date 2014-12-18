using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.TextFormatting;
using NKnife.Events;

namespace NKnife.Tunnel.Events
{
    public class SessionEventArgs<TData, TSessionId> : EventArgs<ITunnelSession<TData, TSessionId>>
    {
        public SessionEventArgs(ITunnelSession<TData, TSessionId> item)
            : base(item)
        {
        }
    }
}
