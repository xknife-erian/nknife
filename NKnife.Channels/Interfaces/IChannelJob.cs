using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Events;
using NKnife.Interface;

namespace NKnife.Channels.Interfaces
{
    public interface IChannelJob<T> : IJob
    {
        /// <summary>
        ///     本次交换的数据
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// 当远端数据到达时，通常这是同步的回复数据
        /// </summary>
        event EventHandler<EventArgs<byte[]>> Answered;
    }
}
