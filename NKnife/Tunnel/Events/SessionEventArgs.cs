using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.TextFormatting;
using NKnife.Events;
using NKnife.Tunnel.Common;

namespace NKnife.Tunnel.Events
{
    public class SessionEventArgs : EventArgs<TunnelSession>
    {
        public SessionEventArgs(TunnelSession session)
            : base(session)
        {
        }
    }
}
