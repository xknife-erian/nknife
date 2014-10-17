using System.Collections.Generic;

namespace NKnife.Tunnel
{
    public interface ITunnelFilterChain<TSource, TConnector> : ICollection<ITunnelFilter<TSource, TConnector>>
    {
        void AddAfter(ITunnelFilter<TSource, TConnector> filter, ITunnelFilter<TSource, TConnector> newfilter);
        void AddBefore(ITunnelFilter<TSource, TConnector> filter, ITunnelFilter<TSource, TConnector> newfilter);
        void AddFirst(ITunnelFilter<TSource, TConnector> filter);
        void AddLast(ITunnelFilter<TSource, TConnector> filter);
        void RemoveFirst();
        void RemoveLast();
    }
}