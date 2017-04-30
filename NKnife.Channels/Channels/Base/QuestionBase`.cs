﻿using NKnife.Channels.Interfaces;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Channels.Channels.Base
{
    /// <summary>
    /// 描述仪器向PC采集端返回的交换数据的基类
    /// </summary>
    public abstract class QuestionBase<T> : IQuestion<T>
    {
        protected QuestionBase(IChannel<T> channel, IDevice device, IExhibit exhibit, bool isLoop, T data)
        {
            Channel = channel;
            Device = device;
            Exhibit = exhibit;
            IsLoop = isLoop;
            Data = data;
        }

        #region Implementation of IDeviceSwap

        /// <summary>
        /// 该询问是否需要循环。true时需要,false时仅问询一次。
        /// </summary>
        public bool IsLoop { get; set; }

        /// <summary>
        /// 本次交换的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 数据采集通道
        /// </summary>
        public IChannel<T> Channel { get; }

        /// <summary>
        /// 数据采集的仪器
        /// </summary>
        public IDevice Device { get; }

        /// <summary>
        /// 被采集、观察的对象，如电阻源，电压源等。
        /// </summary>
        public IExhibit Exhibit { get; }

        #endregion
    }
}