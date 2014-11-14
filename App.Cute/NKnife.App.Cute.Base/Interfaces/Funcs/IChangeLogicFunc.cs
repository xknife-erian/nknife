using System;
using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>描述逻辑调整时的方法接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks></remarks>
    public interface IChangeLogicFunc<T>
    {
        /// <summary>逻辑调整时的条件参数
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>执行逻辑调整的动作
        /// </summary>
        /// <param name="sourceLogics">供参考的逻辑库的源</param>
        /// <param name="targetLogics">需调整的逻辑库</param>
        /// <returns></returns>
        bool Execute(IDictionary<string, ServiceQueueLogic> sourceLogics, IDictionary<string, ServiceQueueLogic> targetLogics);
    }
}