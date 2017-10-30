using System.Collections.Generic;

namespace NKnife.Channels.Interfaces
{
    /// <summary>
    /// 描述PC采集端向仪器发出的询问的集合的接口
    /// </summary>
    public interface IQuestionGroup<T> : ICollection<IQuestion<T>>
    {
        int GetMaxTimeout();
    }
}