using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketServerFilterChain : ITunnelFilterChain<EndPoint, Socket>
    {
        private readonly LinkedList<KnifeSocketServerFilter> _Filters = new LinkedList<KnifeSocketServerFilter>();

        IEnumerator<ITunnelFilter<EndPoint, Socket>> IEnumerable<ITunnelFilter<EndPoint, Socket>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<ITunnelFilter<EndPoint, Socket>>.Add(ITunnelFilter<EndPoint, Socket> item)
        {
            Add((KnifeSocketServerFilter)item);
        }

        public void Clear()
        {
            _Filters.Clear();
        }

        bool ICollection<ITunnelFilter<EndPoint, Socket>>.Contains(ITunnelFilter<EndPoint, Socket> item)
        {
            return Contains((KnifeSocketServerFilter)item);
        }

        void ICollection<ITunnelFilter<EndPoint, Socket>>.CopyTo(ITunnelFilter<EndPoint, Socket>[] array, int arrayIndex)
        {
            CopyTo((KnifeSocketServerFilter[])array, arrayIndex);
        }

        bool ICollection<ITunnelFilter<EndPoint, Socket>>.Remove(ITunnelFilter<EndPoint, Socket> item)
        {
            return Remove((KnifeSocketServerFilter)item);
        }

        public int Count
        {
            get { return _Filters.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<KnifeSocketServerFilter>)_Filters).IsReadOnly; }
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddAfter(ITunnelFilter<EndPoint, Socket> filter, ITunnelFilter<EndPoint, Socket> newfilter)
        {
            AddAfter((KnifeSocketServerFilter)filter, (KnifeSocketServerFilter)newfilter);
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddBefore(ITunnelFilter<EndPoint, Socket> filter, ITunnelFilter<EndPoint, Socket> newfilter)
        {
            AddBefore((KnifeSocketServerFilter)filter, (KnifeSocketServerFilter)newfilter);
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddFirst(ITunnelFilter<EndPoint, Socket> filter)
        {
            AddFirst((KnifeSocketServerFilter)filter);
        }

        void ITunnelFilterChain<EndPoint, Socket>.AddLast(ITunnelFilter<EndPoint, Socket> filter)
        {
            AddLast((KnifeSocketServerFilter)filter);
        }

        public void RemoveFirst()
        {
            _Filters.RemoveFirst();
        }

        public void RemoveLast()
        {
            _Filters.RemoveLast();
        }

        public IEnumerator<KnifeSocketServerFilter> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        public void Add(KnifeSocketServerFilter item)
        {
            _Filters.AddLast(item);
        }

        public bool Contains(KnifeSocketServerFilter item)
        {
            return _Filters.Contains(item);
        }

        public void CopyTo(KnifeSocketServerFilter[] array, int arrayIndex)
        {
            _Filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(KnifeSocketServerFilter item)
        {
            return _Filters.Remove(item);
        }

        public void AddAfter(KnifeSocketServerFilter filter, KnifeSocketServerFilter newfilter)
        {
            _Filters.AddAfter(new LinkedListNode<KnifeSocketServerFilter>(filter), new LinkedListNode<KnifeSocketServerFilter>(newfilter));
        }

        public void AddBefore(KnifeSocketServerFilter filter, KnifeSocketServerFilter newfilter)
        {
            _Filters.AddBefore(new LinkedListNode<KnifeSocketServerFilter>(filter), new LinkedListNode<KnifeSocketServerFilter>(newfilter));
        }

        public void AddFirst(KnifeSocketServerFilter filter)
        {
            _Filters.AddFirst(new LinkedListNode<KnifeSocketServerFilter>(filter));
        }

        public void AddLast(KnifeSocketServerFilter filter)
        {
            _Filters.AddLast(new LinkedListNode<KnifeSocketServerFilter>(filter));
        }
    }
}