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

        public LinkedListNode<ITunnelFilter<byte[], TSessionId>> Previous(LinkedListNode<ITunnelFilter<byte[], TSessionId>> currentNode)
        {
            return currentNode.Previous;
        }

        public LinkedListNode<ITunnelFilter<byte[], TSessionId>> Next(LinkedListNode<ITunnelFilter<byte[], TSessionId>> currentNode)
        {
            return currentNode.Next;
        }

        public IEnumerator<ITunnelFilter<byte[], TSessionId>> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        public void Add(ITunnelFilter<byte[], TSessionId> item)
        {
            _Filters.AddLast(new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(item));
        }

        public bool Contains(ITunnelFilter<byte[], TSessionId> item)
        {
            return _Filters.Contains(item);
        }

        public LinkedListNode<ITunnelFilter<byte[], TSessionId>> Find(ITunnelFilter<byte[], TSessionId> filter)
        {
            return _Filters.Find(filter);
        }

        public void CopyTo(ITunnelFilter<byte[], TSessionId>[] array, int arrayIndex)
        {
            _Filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITunnelFilter<byte[], TSessionId> item)
        {
            return _Filters.Remove(item);
        }

        public void AddAfter(LinkedListNode<ITunnelFilter<byte[], TSessionId>> node, ITunnelFilter<byte[], TSessionId> newfilter)
        {
            _Filters.AddAfter(node, new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(newfilter));
        }

        public void AddBefore(LinkedListNode<ITunnelFilter<byte[], TSessionId>> node, ITunnelFilter<byte[], TSessionId> newfilter)
        {
            _Filters.AddBefore(node, new LinkedListNode<ITunnelFilter<byte[], TSessionId>>(newfilter));
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