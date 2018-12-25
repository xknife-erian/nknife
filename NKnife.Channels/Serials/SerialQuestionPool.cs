using System;
using System.Collections;
using System.Collections.Generic;
using NKnife.Interface;

namespace NKnife.Channels.Serials
{
    public class SerialQuestionPool : List<IJobPoolItem>, IJobPool
    {
        bool ICollection<IJobPoolItem>.IsReadOnly => ((ICollection<IJobPoolItem>) this).IsReadOnly;

        public bool IsPool => true;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event EventHandler Answered;

        protected virtual void OnAnswered()
        {
            Answered?.Invoke(this, EventArgs.Empty);
        }
    }
}