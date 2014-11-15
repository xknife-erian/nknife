using System;

namespace NKnife.App.Cute.Base.Enums
{
    /// <summary>交易的状态
    /// </summary>
    [Flags]
    public enum TransactionStatus : byte
    {
        /// <summary>正在等待
        /// </summary>
        OnWait = 1,
        /// <summary>正在服务
        /// </summary>
        OnServe = 2,
        /// <summary>未服务，
        /// </summary>
        UnServe = 4,
        /// <summary>服务结束
        /// </summary>
        Finished = 8,
        /// <summary>弃票
        /// </summary>
        GivenUp = 16,
        /// <summary>预约关闭。(含三种状态：未服务；放弃；服务结束)
        /// </summary>
        Closed = UnServe | GivenUp | Finished,
        /// <summary>预约进行中。（含两种状态：正在等待；正在服务）
        /// </summary>
        Running = OnWait | OnServe
    }
}