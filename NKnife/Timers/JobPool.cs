using System.Collections.Generic;
using NKnife.Interface;

namespace NKnife.Timers
{
    public class JobPool : List<IJobPoolItem>, IJobPoolItem
    {
        #region Implementation of IJobPoolItem

        public bool IsPool { get; } = true;

        #endregion
    }
}