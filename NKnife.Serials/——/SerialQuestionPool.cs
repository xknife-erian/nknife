using System;
using System.Collections;
using System.Collections.Generic;
using NKnife.Interface;

namespace NKnife.Channels.Serial
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

        #region Implementation of IJobPool

        /// <summary>
        /// 工作池中的子工作轮循模式，当True时，会循环执行整个池中的所有子工作；当False时，对每项子工作都会执行完毕，才执行下一个工作。
        /// </summary>
        public bool IsOverall { get; set; }

        #endregion
    }
}