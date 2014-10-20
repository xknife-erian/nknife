using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using SocketKnife.Common;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketFilterChain : ITunnelFilterChain<EndPoint, Socket, string>
    {
        private readonly LinkedList<KnifeSocketServerFilter> _Filters = new LinkedList<KnifeSocketServerFilter>();

        IEnumerator<ITunnelFilter<EndPoint, Socket, string>> IEnumerable<ITunnelFilter<EndPoint, Socket, string>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KnifeSocketServerFilter> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KnifeSocketServerFilter item)
        {
            _Filters.AddLast(item);
        }

        void ICollection<ITunnelFilter<EndPoint, Socket, string>>.Add(ITunnelFilter<EndPoint, Socket, string> item)
        {
            Add((KnifeSocketServerFilter)item);
        }

        public void Clear()
        {
            _Filters.Clear();
        }

        bool ICollection<ITunnelFilter<EndPoint, Socket, string>>.Contains(ITunnelFilter<EndPoint, Socket, string> item)
        {
            return Contains((KnifeSocketServerFilter) item);
        }

        void ICollection<ITunnelFilter<EndPoint, Socket, string>>.CopyTo(ITunnelFilter<EndPoint, Socket, string>[] array, int arrayIndex)
        {
            CopyTo((KnifeSocketServerFilter[])array, arrayIndex);
        }

        bool ICollection<ITunnelFilter<EndPoint, Socket, string>>.Remove(ITunnelFilter<EndPoint, Socket, string> item)
        {
            return Remove((KnifeSocketServerFilter) item);
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

        public int Count
        {
            get { return _Filters.Count; }
            
        }
        public bool IsReadOnly { get { return ((ICollection<KnifeSocketServerFilter>)_Filters).IsReadOnly; } }

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

        void ITunnelFilterChain<EndPoint, Socket, string>.AddAfter(ITunnelFilter<EndPoint, Socket, string> filter, ITunnelFilter<EndPoint, Socket, string> newfilter)
        {
            AddAfter((KnifeSocketServerFilter)filter, (KnifeSocketServerFilter)newfilter);
        }

        void ITunnelFilterChain<EndPoint, Socket, string>.AddBefore(ITunnelFilter<EndPoint, Socket, string> filter, ITunnelFilter<EndPoint, Socket, string> newfilter)
        {
            AddBefore((KnifeSocketServerFilter)filter, (KnifeSocketServerFilter)newfilter);
        }

        void ITunnelFilterChain<EndPoint, Socket, string>.AddFirst(ITunnelFilter<EndPoint, Socket, string> filter)
        {
            AddFirst((KnifeSocketServerFilter)filter);
        }

        void ITunnelFilterChain<EndPoint, Socket, string>.AddLast(ITunnelFilter<EndPoint, Socket, string> filter)
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
    }
}
