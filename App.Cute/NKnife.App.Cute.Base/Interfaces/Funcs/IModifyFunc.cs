using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>描述调整交易方法的接口
    /// </summary>
    public interface IModifyFunc<T>
    {
        /// <summary>为调整交易动作准备的条件
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>执行调整交易动作方法
        /// </summary>
        /// <param name="queues"> </param>
        /// <returns></returns>
        bool Execute(ICollection<ServiceQueue> queues);
    }
}