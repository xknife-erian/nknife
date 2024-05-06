using System;
using NKnife.Events;

namespace NKnife.Channel.Interfaces
{
    public interface IChannelJob<T> : IJob
    {
        /// <summary>
        ///     本次交换的数据
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// 当远端应答后，通常这是同步的应答数据已收到
        /// </summary>
        event EventHandler<EventArgs<T>> Answered;

        /// <summary>
        /// 应答数据
        /// </summary>
        T Answer { get; set; }
    }
}
