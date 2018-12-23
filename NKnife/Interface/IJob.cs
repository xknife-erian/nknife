﻿using System;
using NKnife.Events;

namespace NKnife.Interface
{
    public interface IJob : IJobPoolItem
    {
        /// <summary>
        /// 间隔时长
        /// </summary>
        int Interval { get; set; }

        /// <summary>
        /// 当工作异常时，需要等待的时长。当设置该时长时，一定要比间隔时长要长；否则无论是否发生工作异常，本工作都将在间隔时长达到时结束工作。
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// 是否需要循环
        /// </summary>
        bool IsLoop { get; set; }

        /// <summary>
        /// 循环执行的资数。当本值小于等于0时，如果需要循环，将无限循环下去。
        /// </summary>
        int LoopNumber { get; set; } 

        /// <summary>
        /// 注入本工作的执行方法。返回true表明执行完成，并实现了期望的效果；false反之，可能出现了执行异常，例如，发送对话请求后，未得到远端回应。
        /// </summary>
        Func<IJob, bool> Func { get; set; }

        /// <summary>
        /// 当工作即将执行时
        /// </summary>
        event EventHandler<JobRunEventArgs> Running;

        /// <summary>
        /// 当工作执行结束后
        /// </summary>
        event EventHandler<JobRunEventArgs> Ran;
    }
}