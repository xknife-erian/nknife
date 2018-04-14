using System;
using NKnife.Interface;

namespace NKnife.Timers
{
    public class Job : IJobPoolItem
    {
        /// <summary>
        /// 当工作异常时，需要等待的时长。当设置该时长时，一定要比间隔时长要长；否则无论是否发生工作异常，本工作都将在间隔时长达到时结束工作。
        /// </summary>
        public int Timeout { get; set; } = 0;

        /// <summary>
        /// 是否需要循环
        /// </summary>
        public bool IsLoop { get; set; } = false;

        /// <summary>
        /// 间隔时长
        /// </summary>
        public int Interval { get; set; } = 0;

        /// <summary>
        /// 循环执行的资数。当本值小于等于0时，如果需要循环，将无限循环下去。
        /// </summary>
        public int LoopNumber { get; set; } = 0;

        /// <summary>
        /// 注入本工作的执行方法。返回true表明执行完成，并实现了期望的效果；false反之，可能出现了执行异常，例如，发送对话请求后，未得到远端回应。
        /// </summary>
        public Func<Job, bool> Func { get; set; }

        #region Implementation of IJobPoolItem

        public bool IsPool { get; } = false;

        #endregion
    }
}