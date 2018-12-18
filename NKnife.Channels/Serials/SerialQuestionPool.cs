using NKnife.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Channels.Serials
{
    public class SerialQuestionPool : List<IJobPoolItem>, IJobPool
    {
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsPool => throw new NotImplementedException();

        public void Add(IJobPoolItem item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<IJobPoolItem> jobs)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IJobPoolItem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IJobPoolItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IJobPoolItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(IJobPoolItem item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
