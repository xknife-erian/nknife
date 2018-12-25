using System;
using System.Collections.Generic;
using NKnife.Channels.EventParams;
using NKnife.Channels.Interfaces;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Jobs;

namespace NKnife.Channels.Base
{
    public abstract class ChannelBase<T> : IChannel<T>
    {
        #region Implementation of IChannel

        private bool _isSynchronous;

        /// <inheritdoc />
        public bool IsSynchronous
        {
            get => _isSynchronous;
            set
            {
                if (_isSynchronous != value)
                {
                    _isSynchronous = value;
                    OnChannelModeChanged(new ChannelModeChangedEventArgs(value));
                }
            }
        }

        public bool IsOpen { get; protected set; } = false;

        /// <inheritdoc />
        public abstract bool Open();

        /// <inheritdoc />
        public event EventHandler Opening;

        /// <inheritdoc />
        public event EventHandler Opened;

        /// <inheritdoc />
        public abstract bool Close();

        /// <inheritdoc />
        public event EventHandler Closing;

        /// <inheritdoc />
        public event EventHandler Closed;

        /// <inheritdoc />
        public JobManager JobManager { get; set; }

        /// <inheritdoc />
        public abstract void Break();

        /// <inheritdoc />
        public abstract void SyncListen();

        /// <inheritdoc />
        public abstract void AsyncListen();

        /// <inheritdoc />
        public event EventHandler<ChannelModeChangedEventArgs> ChannelModeChanged;

        /// <inheritdoc />
        public event EventHandler<EventArgs<T>> DataArrived;

        #endregion

        /// <summary>
        /// 更新Job的执行动作
        /// </summary>
        public abstract void UpdateJobFunc();

        protected virtual void OnChannelModeChanged(ChannelModeChangedEventArgs e)
        {
            ChannelModeChanged?.Invoke(this, e);
        }

        protected virtual void OnDataArrived(EventArgs<T> e)
        {
            DataArrived?.Invoke(this, e);
        }

        protected virtual void OnOpening()
        {
            Opening?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnOpened()
        {
            Opened?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClosing()
        {
            Closing?.Invoke(this, EventArgs.Empty);
        }
    }
}