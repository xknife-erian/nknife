using System;

namespace Didaku.Engine.Timeaxis.Implement.Abstracts
{
    /// <summary>请求评价
    /// </summary>
    public abstract class BaseCommentingTransaction : BaseTransaction
    {
        /// <summary>办理预约事件的员工ID
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>时间资源拥有者的ID。在银行排队中一般是柜台。
        /// </summary>
        public string TimeaxisId { get; set; }

        /// <summary>对一次预约或预约产生的交易进行了评论
        /// </summary>
        public BaseComment Comment { get; set; }
    }
}