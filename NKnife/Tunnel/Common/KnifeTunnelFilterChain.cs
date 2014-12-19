using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace NKnife.Tunnel.Common
{
    public class KnifeTunnelFilterChain : ITunnelFilterChain<byte[], EndPoint>
    {
        private readonly LinkedList<ITunnelFilter<byte[], EndPoint>> _Filters = new LinkedList<ITunnelFilter<byte[], EndPoint>>();

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
            get { return ((ICollection<ITunnelFilter<byte[], EndPoint>>)_Filters).IsReadOnly; }
        }

        public void RemoveFirst()
        {
            _Filters.RemoveFirst();
        }

        public void RemoveLast()
        {
            _Filters.RemoveLast();
        }

        public IEnumerator<ITunnelFilter<byte[], EndPoint>> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        public void Add(ITunnelFilter<byte[], EndPoint> item)
        {
            _Filters.AddLast(item);
        }

        public bool Contains(ITunnelFilter<byte[], EndPoint> item)
        {
            return _Filters.Contains(item);
        }

        public void CopyTo(ITunnelFilter<byte[], EndPoint>[] array, int arrayIndex)
        {
            _Filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITunnelFilter<byte[], EndPoint> item)
        {
            return _Filters.Remove(item);
        }

        public void AddAfter(ITunnelFilter<byte[], EndPoint> filter, ITunnelFilter<byte[], EndPoint> newfilter)
        {
            _Filters.AddAfter(new LinkedListNode<ITunnelFilter<byte[], EndPoint>>(filter), new LinkedListNode<ITunnelFilter<byte[], EndPoint>>(newfilter));
        }

        public void AddBefore(ITunnelFilter<byte[], EndPoint> filter, ITunnelFilter<byte[], EndPoint> newfilter)
        {
            _Filters.AddBefore(new LinkedListNode<ITunnelFilter<byte[], EndPoint>>(filter), new LinkedListNode<ITunnelFilter<byte[], EndPoint>>(newfilter));
        }

        public void AddFirst(ITunnelFilter<byte[], EndPoint> filter)
        {
            _Filters.AddFirst(new LinkedListNode<ITunnelFilter<byte[], EndPoint>>(filter));
        }

        public void AddLast(ITunnelFilter<byte[], EndPoint> filter)
        {
            _Filters.AddLast(new LinkedListNode<ITunnelFilter<byte[], EndPoint>>(filter));
        }
    }
}