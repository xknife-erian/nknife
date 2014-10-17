using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NKnife.Tunnel
{
    public interface ITunnelSession<TSource, TConnector>
    {
        long Id { get; }

        TSource Source { get; set; }

        TConnector Connector { get; set; }

        Object TryGetAttribute(Object key, Object defaultValue);

        void SetAttribute(Object key, Object value);

        Object GetAttribute(Object key);

        bool RemoveAttribute(Object key);

        bool ContainsAttribute(Object key);
    }
}
