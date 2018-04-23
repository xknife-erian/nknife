using System.Collections.Generic;

namespace NKnife.Interface
{
    public interface IJobPool : IList<IJobPoolItem>, IJobPoolItem
    {
        void AddRange(IEnumerable<IJobPoolItem> jobs);
    }
}