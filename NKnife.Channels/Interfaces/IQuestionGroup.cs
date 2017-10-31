using System.Collections.Generic;
using NKnife.Base;

namespace NKnife.Channels.Interfaces
{
    /// <summary>
    /// 描述PC采集端向仪器发出的询问的集合的接口
    /// </summary>
    public interface IQuestionGroup<T> : ICollection<IQuestion<T>>
    {
        /// <summary>
        /// 返回本组询问中最长的超时
        /// </summary>
        int GetMaxTimeout();

        /// <summary>
        /// 返回本组询问中第一个询问的超时与循环周期
        /// </summary>
        IQuestion<T> First { get; }
    }
}