using System.Collections.Generic;

namespace NKnife.Tunnel
{
    public interface ITunnelFilterChain<TData, TSessionId> : ICollection<ITunnelFilter<TData, TSessionId>>
    {
        void AddAfter(ITunnelFilter<TData, TSessionId> filter, ITunnelFilter<TData, TSessionId> newfilter);
        void AddBefore(ITunnelFilter<TData, TSessionId> filter, ITunnelFilter<TData, TSessionId> newfilter);
        void AddFirst(ITunnelFilter<TData, TSessionId> filter);
        void AddLast(ITunnelFilter<TData, TSessionId> filter);
        void RemoveFirst();
        void RemoveLast();
    }
}