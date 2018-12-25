using System;
using System.Collections.Generic;
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

        protected ChannelJobBase(T data, bool isLoop, int interval)
        {
            IsLoop = isLoop;
            Interval = interval;
            Data = data;
        }

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
        public Func<IJob, bool> Run { get; set; }

        /// <inheritdoc />
        public Func<IJob, bool> Verify { get; set; }

        #endregion

        #region Implementation of IChannelJob

        /// <inheritdoc />
        public T Data { get; set; }

        /// <inheritdoc />
        public abstract T Answer { get; set; }

        /// <inheritdoc />
        public event EventHandler<EventArgs<T>> Answered;

        #endregion

        protected internal virtual void OnAnswered(EventArgs<T> e)
        {
            Answered?.Invoke(this, e);
        }
    }
}