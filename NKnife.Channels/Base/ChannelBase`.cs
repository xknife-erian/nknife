﻿using System;
using System.Collections.Generic;
using NKnife.Channels.EventParams;
using NKnife.Channels.Interfaces;
using NKnife.Interface;

namespace NKnife.Channels.Base
{
    public abstract class ChannelBase<T> : IChannel<T>
    {
        #region Implementation of IChannel

        private bool _IsSynchronous = false;

        /// <summary>
        ///     描述该数据采集通道的模式是否是同步获取。即发送指令后是否等待对应该指令的回复。
        /// </summary>
        /// <returns>当true时，是同步的，反之为异步</returns>
        public bool IsSynchronous
        {
            get { return _IsSynchronous; }
            set
            {
                if (_IsSynchronous != value)
                {
                    _IsSynchronous = value;
                    OnChannelModeChanged(new ChannelModeChangedEventArgs(value));
                }
            }
        }

        public bool IsOpen { get; protected set; } = false;

        /// <summary>
        ///     当前数据采集通道的所面向的实际采集源。
        /// </summary>
        public List<IId> Targets { get; } = new List<IId>(1);

        /// <summary>
        ///     打开采集通道
        /// </summary>
        /// <returns></returns>
        public abstract bool Open();

        public event EventHandler Opening;
        public event EventHandler Opened;

        /// <summary>
        ///     关闭采集通道
        /// </summary>
        /// <returns></returns>
        public abstract bool Close();

        public event EventHandler Closeing;
        public event EventHandler Closed;

        /// <summary>
        ///     更新即将发送的数据
        /// </summary>
        /// <param name="questionPool">即将发送的数据</param>
        public abstract void UpdateQuestionPool(IQuestionPool questionPool);

        /// <summary>
        ///     发送数据并同步等待数据返回
        /// </summary>
        /// <param name="sendAction">当发送完成时</param>
        /// <param name="receivedFunc">当采集到数据(返回的数据)的处理方法,当返回true时，表示接收数据是完整的，返回flase时，表示接收数据不完整，还需要继续接收</param>
        public abstract void SendReceiver(Action<IQuestion<T>> sendAction, Func<IAnswer<T>, bool> receivedFunc);

        /// <summary>
        ///     自动发送数据
        /// </summary>
        public abstract void AutoSend(Action<IQuestion<T>> sendAction);

        /// <summary>
        ///     当自动发送模式时，中断正在不断进行的自动模式
        /// </summary>
        public abstract void Break();

        public event EventHandler<ChannelModeChangedEventArgs> ChannelModeChanged;
        public event EventHandler<ChannelAnswerDataEventArgs<T>> DataArrived;

        protected virtual void OnChannelModeChanged(ChannelModeChangedEventArgs e)
        {
            ChannelModeChanged?.Invoke(this, e);
        }

        protected virtual void OnDataArrived(ChannelAnswerDataEventArgs<T> e)
        {
            DataArrived?.Invoke(this, e);
        }

        #endregion

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

        protected virtual void OnCloseing()
        {
            Closeing?.Invoke(this, EventArgs.Empty);
        }
    }
}