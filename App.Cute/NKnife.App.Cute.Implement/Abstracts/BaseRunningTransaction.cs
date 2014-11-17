namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>正在运作的预约事件的交易信息
    /// </summary>
    public abstract class BaseRunningTransaction : BaseTransaction
    {
        /// <summary>办理预约事件的员工ID
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>时间资源拥有者的ID。在银行排队中一般是柜台。
        /// </summary>
        public string TimeaxisId { get; set; }
    }
}