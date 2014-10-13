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
        private readonly LinkedList<KnifeSocketFilter> _Filters = new LinkedList<KnifeSocketFilter>();

        public IEnumerator<KnifeSocketFilter> GetEnumerator()
        {
            return _Filters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KnifeSocketFilter item)
        {
            _Filters.AddLast(item);
        }

        public void Clear()
        {
            _Filters.Clear();
        }

        public bool Contains(KnifeSocketFilter item)
        {
            return _Filters.Contains(item);
        }

        public void CopyTo(KnifeSocketFilter[] array, int arrayIndex)
        {
            _Filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(KnifeSocketFilter item)
        {
            return _Filters.Remove(item);
        }

        public int Count
        {
            get { return _Filters.Count; }
            
        }
        public bool IsReadOnly { get { return ((ICollection<KnifeSocketFilter>)_Filters).IsReadOnly; } }

        public void AddAfter(KnifeSocketFilter filter, KnifeSocketFilter newfilter)
        {
            _Filters.AddAfter(new LinkedListNode<KnifeSocketFilter>(filter), new LinkedListNode<KnifeSocketFilter>(newfilter));
        }

        public void AddBefore(KnifeSocketFilter filter, KnifeSocketFilter newfilter)
        {
            _Filters.AddBefore(new LinkedListNode<KnifeSocketFilter>(filter), new LinkedListNode<KnifeSocketFilter>(newfilter));
        }

        public void AddFirst(KnifeSocketFilter filter)
        {
            _Filters.AddFirst(new LinkedListNode<KnifeSocketFilter>(filter));
        }

        public void AddLast(KnifeSocketFilter filter)
        {
            _Filters.AddLast(new LinkedListNode<KnifeSocketFilter>(filter));
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
