using System.Collections.Generic;

namespace NKnife.Tunnel
{
    public interface ITunnelFilterChain<TSource, TConnector, TCommand> : ICollection<ITunnelFilter<TSource, TConnector, TCommand>>
    {
        void AddAfter(ITunnelFilter<TSource, TConnector, TCommand> filter, ITunnelFilter<TSource, TConnector, TCommand> newfilter);
        void AddBefore(ITunnelFilter<TSource, TConnector, TCommand> filter, ITunnelFilter<TSource, TConnector, TCommand> newfilter);
        void AddFirst(ITunnelFilter<TSource, TConnector, TCommand> filter);
        void AddLast(ITunnelFilter<TSource, TConnector, TCommand> filter);
        void RemoveFirst();
        void RemoveLast();
    }
}