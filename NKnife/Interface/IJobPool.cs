using System.Collections.Generic;

namespace NKnife.Interface
{
    public interface IJobPool : ICollection<IJobPoolItem>, IJobPoolItem
    {
        /// <summary>
        /// 工作池中的子工作轮循模式，当True时，会循环执行整个池中的所有子工作；当False时，对每项子工作都会执行完毕，才执行下一个工作。
        /// </summary>
        bool IsOverall { get; set; }

        /// <summary>
        /// 向集合中添加子工作集合
        /// </summary>
        void AddRange(IEnumerable<IJobPoolItem> jobs);
    }
}