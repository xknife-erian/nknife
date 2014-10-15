using System;
using System.Collections;
using System.Collections.Generic;
using SocketKnife.Common;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketPolicy : ISocketPolicy
    {
        private readonly LinkedList<KnifeSocketServerFilter> _Filters = new LinkedList<KnifeSocketServerFilter>();

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

        public void Clear()
        {
            _Filters.Clear();
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
