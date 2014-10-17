using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.TextFormatting;
using NKnife.Events;

namespace NKnife.Tunnel.Events
{
    public class SessionEventArgs<TSource, TConnector> : EventArgs<ITunnelSession<TSource, TConnector>>
    {
        public SessionEventArgs(ITunnelSession<TSource, TConnector> item) : base(item)
        {
        }
    }
}
