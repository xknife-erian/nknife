using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel
{
    public interface ITunnelSessionMap<TSource, TConnector> : IDictionary<TSource, ITunnelSessionMap<TSource, TConnector>>
    {
    }
}
