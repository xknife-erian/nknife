using System.Collections.Generic;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>描述一个时间轴。这是本系统领域中最关键的概念。他代表任意一个需要进行时间管理的事实存在，如：柜台，人，餐桌，诊台等等。
    /// </summary>
    public abstract class BaseTimeaxis : ITimeaxis
    {
        #region Implementation of ITimeaxis

        /// <summary>有序的队列逻辑<see cref="IServiceLogic"/>存储器。
        /// [<see cref="IServiceLogic"/>]的优先级由[<see cref="IServiceLogic"/>]在List中的顺序决定。
        /// </summary>
        public IList<IServiceLogic> Logics { get; set; }

        #endregion

        #region Implementation of ICloneable

        public object Clone()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}