using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace NKnife.Tunnel.Common
{
    public class KnifeTunnelFilterChain<TSessionId> : ITunnelFilterChain<byte[], TSessionId>
    {
        private readonly LinkedList<ITunnelFilter<byte[], TSessionId>> _Filters = new LinkedList<ITunnelFilter<byte[], TSessionId>>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _Filters.Clear();
        }

        public int Count
        {
            get { return _Filters.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<ITunnelFilter<byte[], TSessionId>>)_Filters).IsReadOnly; }
        }

        public void RemoveFirst()
        {
            _Filters.RemoveFirst();
        }

        public void RemoveLast()
        {
            _Filters.RemoveLast();
        }

        public IEnumerator<ITunnelFilter<byte[], TSessionId>> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        public void Add(ITunnelFilter<byte[], TSessionId> item)
        {
            _Filters.AddLast(item);
        }

        public bool Contains(ITunnelFilter<byte[], TSessionId> item)
        {
            return _Filters.Contains(item);
        }

        public void CopyTo(ITunnelFilter<byte[], TSessionId>[] array, int arrayIndex)
        {
            _Filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITunnelFilter<byte[], TSessionId> item)
        {
            return _Filters.Remove(item);
        }

        public void AddAfter(ITunnelFilter<byte[], TSessionId> filter, ITunnelFilter<byte[], TSessionId> newfilter)
        {
            _Filters.AddAfter(new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(filter), new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(newfilter));
        }

        public void AddBefore(ITunnelFilter<byte[], TSessionId> filter, ITunnelFilter<byte[], TSessionId> newfilter)
        {
            _Filters.AddBefore(new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(filter), new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(newfilter));
        }

        public void AddFirst(ITunnelFilter<byte[], TSessionId> filter)
        {
            _Filters.AddFirst(new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(filter));
        }

        public void AddLast(ITunnelFilter<byte[], TSessionId> filter)
        {
            _Filters.AddLast(new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(filter));
        }
    }
}