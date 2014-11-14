using System;

namespace Didaku.Engine.Timeaxis.Implement.Abstracts
{
    /// <summary>结束的交易。记录了整个预约的事件发生的各个时间节点，和结束的时间。
    /// </summary>
    public abstract class BaseFinishedTransaction : BaseTransaction
    {
        /// <summary>时间资源拥有者的ID。在银行排队中一般是柜台。
        /// </summary>
        public string TimeaxisId { get; set; }
    }
}