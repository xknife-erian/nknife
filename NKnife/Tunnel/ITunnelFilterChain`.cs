using System.Collections.Generic;

namespace NKnife.Tunnel
{
    public interface ITunnelFilterChain<TData, TSessionId> : ICollection<ITunnelFilter<TData, TSessionId>>
    {
        LinkedListNode<ITunnelFilter<TData, TSessionId>> Find(ITunnelFilter<TData, TSessionId> filter);
        void AddAfter(LinkedListNode<ITunnelFilter<TData, TSessionId>> node, ITunnelFilter<TData, TSessionId> newfilter);
        void AddBefore(LinkedListNode<ITunnelFilter<TData, TSessionId>> node, ITunnelFilter<TData, TSessionId> newfilter);
        void AddFirst(ITunnelFilter<TData, TSessionId> filter);
        void AddLast(ITunnelFilter<TData, TSessionId> filter);
        void RemoveFirst();
        void RemoveLast();
        LinkedListNode<ITunnelFilter<TData, TSessionId>> Previous(LinkedListNode<ITunnelFilter<TData, TSessionId>> currentNode);

        LinkedListNode<ITunnelFilter<TData, TSessionId>> Next(LinkedListNode<ITunnelFilter<TData, TSessionId>> currentNode);
    }
}