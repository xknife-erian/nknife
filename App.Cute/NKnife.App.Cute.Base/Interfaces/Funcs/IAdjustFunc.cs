using System;
using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Implement;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Funcs
{
    /// <summary>描述调整交易与队列方法的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAdjustFunc<T>
    {
        /// <summary>为调整交易与队列的动作准备的条件
        /// </summary>
        /// <value>The params.</value>
        /// <remarks></remarks>
        T Condition { get; set; }

        /// <summary>执行调整交易与队列的动作
        /// </summary>
        /// <returns></returns>
        bool Execute(IDictionary<string, ServiceQueueLogic> logicMap);
    }
}