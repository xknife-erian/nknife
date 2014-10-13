using System;
using System.Collections;
using System.Collections.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class DefaultSocketPolicy : ISocketPolicy
    {
        private readonly LinkedList<FilterBase> _Filters = new LinkedList<FilterBase>();

        public IEnumerator<FilterBase> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(FilterBase item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(FilterBase item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(FilterBase[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(FilterBase item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        public void AddAfter(FilterBase filter, FilterBase newfilter)
        {
            throw new NotImplementedException();
        }

        public void AddBefore(FilterBase filter, FilterBase newfilter)
        {
            throw new NotImplementedException();
        }

        public void AddFirst(FilterBase filter)
        {
            throw new NotImplementedException();
        }

        public void AddLast(FilterBase filter)
        {
            _Filters.AddLast(filter);
        }

        public void RemoveFirst()
        {
            throw new NotImplementedException();
        }

        public void RemoveLast()
        {
            throw new NotImplementedException();
        }
    }
}
