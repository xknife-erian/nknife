using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Default
{
    public class DefaultFilterChain : IFilterChain
    {
        private LinkedList<IFilter> _Filters = new LinkedList<IFilter>(); 

        public IEnumerator<IFilter> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IFilter item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IFilter item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IFilter[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IFilter item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        public void AddAfter(IFilter filter, IFilter newfilter)
        {
            throw new NotImplementedException();
        }

        public void AddBefore(IFilter filter, IFilter newfilter)
        {
            throw new NotImplementedException();
        }

        public void AddFirst(IFilter filter)
        {
            throw new NotImplementedException();
        }

        public void AddLast(IFilter filter)
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
