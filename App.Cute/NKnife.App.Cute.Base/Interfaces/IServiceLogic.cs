using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>服务的逻辑。一个逻辑中会包括多个队列:<see cref="IServiceQueue"/>。
    /// </summary>
    public interface IServiceLogic : IList<IServiceQueue>, IXmlSerializable, IEquatable<IServiceLogic>
    {
        /// <summary>尝试从队列组中分配一个交易
        /// </summary>
        /// <param name="transaction">输出:一个排队的交易</param>
        /// <returns>如果有可用的交易，返回真。反之，返回否。</returns>
        bool TryAssign(out ITransaction transaction);

        /// <summary>判断队列组是否激活。(即俗称的备用队列的激活条件)
        /// </summary>
        bool IsActive();
    }
}