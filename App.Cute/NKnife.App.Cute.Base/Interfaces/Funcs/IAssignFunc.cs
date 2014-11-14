using System.Collections.Generic;
using Didaku.Wrapper;
using Didaku.Engine.Timeaxis.Base.Entities;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>描述一个从队列中分配排队交易的方法接口
    /// </summary>
    /// <typeparam name="T">呼叫时的参数</typeparam>
    /// <remarks></remarks>
    public interface IAssignFunc<T>
    {
        /// <summary>发起呼叫(提取交易)的柜台
        /// </summary>
        /// <value>The counter.</value>
        /// <remarks></remarks>
        IdName Counter { get; set; }

        /// <summary>发起呼叫(提取交易)的员工
        /// </summary>
        /// <value>
        /// The staff.
        /// </value>
        IdName Staff { get; set; }

        /// <summary>呼叫时的条件参数
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>从一个柜台的服务逻辑中获取交易的方法
        /// </summary>
        /// <returns></returns>
        bool Execute(out Transaction transaction, ServiceQueueLogic serviceLogic, ICollection<ServiceQueue> queues);
    }
}