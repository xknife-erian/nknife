using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeTunnelFilterChain : ITunnelFilterChain<byte[], EndPoint>
    {
        private readonly LinkedList<ITunnelFilter<byte[], EndPoint>> _Filters = new LinkedList<ITunnelFilter<byte[], EndPoint>>();

        IEnumerator<ITunnelFilter<byte[], EndPoint>> IEnumerable<ITunnelFilter<byte[], EndPoint>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<ITunnelFilter<byte[], EndPoint>>.Add(ITunnelFilter<byte[], EndPoint> item)
        {
            Add((ITunnelFilter<byte[], EndPoint>)item);
        }

        public void Clear()
        {
            _Filters.Clear();
        }

        bool ICollection<ITunnelFilter<byte[], EndPoint>>.Contains(ITunnelFilter<byte[], EndPoint> item)
        {
            return Contains((ITunnelFilter<byte[], EndPoint>)item);
        }

        void ICollection<ITunnelFilter<byte[], EndPoint>>.CopyTo(ITunnelFilter<byte[], EndPoint>[] array, int arrayIndex)
        {
            CopyTo((ITunnelFilter<byte[], EndPoint>[])array, arrayIndex);
        }

        bool ICollection<ITunnelFilter<byte[], EndPoint>>.Remove(ITunnelFilter<byte[], EndPoint> item)
        {
            return Remove((ITunnelFilter<byte[], EndPoint>)item);
        }

        public int Count
        {
            get { return _Filters.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<ITunnelFilter<byte[], EndPoint>>)_Filters).IsReadOnly; }
        }

        void ITunnelFilterChain<byte[], EndPoint>.AddAfter(ITunnelFilter<byte[], EndPoint> filter, ITunnelFilter<byte[], EndPoint> newfilter)
        {
            AddAfter((ITunnelFilter<byte[], EndPoint>)filter, (ITunnelFilter<byte[], EndPoint>)newfilter);
        }

        void ITunnelFilterChain<byte[], EndPoint>.AddBefore(ITunnelFilter<byte[], EndPoint> filter, ITunnelFilter<byte[], EndPoint> newfilter)
        {
            AddBefore((ITunnelFilter<byte[], EndPoint>)filter, (ITunnelFilter<byte[], EndPoint>)newfilter);
        }

        void ITunnelFilterChain<byte[], EndPoint>.AddFirst(ITunnelFilter<byte[], EndPoint> filter)
        {
            AddFirst((ITunnelFilter<byte[], EndPoint>)filter);
        }

        void ITunnelFilterChain<byte[], EndPoint>.AddLast(ITunnelFilter<byte[], EndPoint> filter)
        {
            AddLast((ITunnelFilter<byte[], EndPoint>)filter);
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