using System.Collections.Generic;

namespace NKnife.Interface
{
    public interface IJobPool : ICollection<IJobPoolItem>, IJobPoolItem
    {
        void AddRange(IEnumerable<IJobPoolItem> jobs);
    }
}