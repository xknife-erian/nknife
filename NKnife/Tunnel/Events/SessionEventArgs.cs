using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.TextFormatting;
using NKnife.Events;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel.Events
{
    public class SessionEventArgs<TSessionId> : EventArgs<TunnelSession<TSessionId>>
    {
        public SessionEventArgs(TunnelSession<TSessionId> item)
            : base(item)
        {
        }
    }
}
