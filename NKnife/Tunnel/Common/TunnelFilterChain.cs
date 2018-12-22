using System.Collections;
using System.Collections.Generic;

namespace NKnife.Tunnel.Common
{
    public class TunnelFilterChain : ITunnelFilterChain
    {
        private readonly LinkedList<ITunnelFilter> _filters = new LinkedList<ITunnelFilter>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _filters.Clear();
        }

        public int Count
        {
            get { return _filters.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<ITunnelFilter>) _filters).IsReadOnly; }
        }

        public void RemoveFirst()
        {
            _filters.RemoveFirst();
        }

        public void RemoveLast()
        {
            _filters.RemoveLast();
        }

        public LinkedListNode<ITunnelFilter> Previous(LinkedListNode<ITunnelFilter> currentNode)
        {
            return currentNode.Previous;
        }

        public LinkedListNode<ITunnelFilter> Next(LinkedListNode<ITunnelFilter> currentNode)
        {
            return currentNode.Next;
        }

        public IEnumerator<ITunnelFilter> GetEnumerator()
        {
            return _filters.GetEnumerator();
        }

        public void Add(ITunnelFilter item)
        {
            _filters.AddLast(new LinkedListNode<ITunnelFilter>(item));
        }

        public bool Contains(ITunnelFilter item)
        {
            return _filters.Contains(item);
        }

        public LinkedListNode<ITunnelFilter> Find(ITunnelFilter filter)
        {
            return _filters.Find(filter);
        }

        public void CopyTo(ITunnelFilter[] array, int arrayIndex)
        {
            _filters.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITunnelFilter item)
        {
            return _filters.Remove(item);
        }

        public void AddAfter(LinkedListNode<ITunnelFilter> node, ITunnelFilter newfilter)
        {
            _filters.AddAfter(node, new LinkedListNode<ITunnelFilter>(newfilter));
        }

        public void AddBefore(LinkedListNode<ITunnelFilter> node, ITunnelFilter newfilter)
        {
            _filters.AddBefore(node, new LinkedListNode<ITunnelFilter>(newfilter));
        }

        public void AddFirst(ITunnelFilter filter)
        {
            _filters.AddFirst(new LinkedListNode<ITunnelFilter>(filter));
        }

        public void AddLast(ITunnelFilter filter)
        {
            _filters.AddLast(new LinkedListNode<ITunnelFilter>(filter));
        }
    }
}