using System.Collections.Generic;
using NKnife.App.Cute.Base.Common;
using NKnife.App.Cute.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Base.Interfaces.Services
{
    public interface ICountService
    {
        /// <summary>该引擎对应的柜台等待人数的集合
        /// </summary>
        CountMap Counters { get; }

        /// <summary>该引擎对应的所有队列等待人数的集合
        /// </summary>
        CountMap Queues { get; }

        /// <summary>该引擎对应的所有队列元素的集合
        /// </summary>
        CountMap Elements { get; }

        /// <summary>启动服务
        /// </summary>
        void Start(IEnumerable<IServiceQueue> queues);

        /// <summary>关闭服务
        /// </summary>
        void Close();
    }
}