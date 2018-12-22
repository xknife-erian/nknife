using System;
using NKnife.Channels.Interfaces;
using NKnife.Events;
using NKnife.Interface;

namespace NKnife.Channels.Base
{
    public abstract class ChannelJobBase<T> : IChannelJob<T>
    {
        protected ChannelJobBase(bool isPool)
        {
            IsPool = isPool;
        }

        protected ChannelJobBase(bool isLoop, T data, int interval, IId target)
        {
            IsLoop = isLoop;
            Interval = interval;
            Data = data;
            Target = target;
        }

        /// <summary>
        ///     本次交换的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     工作指定的对象
        /// </summary>
        public IId Target { get; set; }

        #region Implementation of IJobPoolItem

        /// <inheritdoc />
        public bool IsPool { get; }

        #endregion

        #region Implementation of IJob

        /// <inheritdoc />
        public int Interval { get; set; }

        /// <inheritdoc />
        public int Timeout { get; set; }

        /// <inheritdoc />
        public bool IsLoop { get; set; } = false;

        /// <inheritdoc />
        public int LoopNumber { get; set; }

        /// <inheritdoc />
        public Func<IJob, bool> Func { get; set; }

        /// <inheritdoc />
        public event EventHandler<JobRunEventArgs> Running;

        /// <inheritdoc />
        public event EventHandler<JobRunEventArgs> Ran;

        #endregion

        protected virtual void OnRunning(JobRunEventArgs e)
        {
            Running?.Invoke(this, e);
        }

        protected virtual void OnRan(JobRunEventArgs e)
        {
            Ran?.Invoke(this, e);
        }
    }
}