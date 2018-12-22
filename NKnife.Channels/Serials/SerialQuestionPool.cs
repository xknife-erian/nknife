using NKnife.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace NKnife.Channels.Serials
{
    public class SerialQuestionPool : List<IJobPoolItem>, IJobPool
    {
        bool ICollection<IJobPoolItem>.IsReadOnly => ((ICollection<IJobPoolItem>)this).IsReadOnly;

        public bool IsPool => true;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
