using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.TextFormatting;
using NKnife.Events;

namespace NKnife.Tunnel.Events
{
    public class SessionEventArgs<TSessionId, TData> : EventArgs<ITunnelSession<TSessionId, TData>>
    {
        public SessionEventArgs(ITunnelSession<TSessionId, TData> item)
            : base(item)
        {
        }
    }
}
