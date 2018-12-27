using System;
using System.Collections.Generic;
using NKnife.Channels.Base;
using NKnife.Channels.EventParams;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Jobs;

namespace NKnife.Channels.Interfaces
{
    /// <summary>
    ///     描述一个数据通道。该来源可以是某个串口，某个TCP/IP端口，某类驱动调用等等。
    /// </summary>
    public interface IChannel<T>
    {
        /// <summary>
        ///     描述该数据通道的模式是否是同步获取，也称对话(Talk)模式，反之异步获取，也称广播(Broadcast)模式。
        /// </summary>
        /// <returns>当true时，是同步的，反之为异步</returns>
        bool IsSynchronous { get; set; }

        /// <summary>
        ///     数据通道是否打开
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        ///     工作管理器
        /// </summary>
        JobManager JobManager { get; set; }

        /// <summary>
        ///     打开数据通道
        /// </summary>
        /// <returns>打开动作是否完成</returns>
        bool Open();

        event EventHandler Opening;
        event EventHandler Opened;

        /// <summary>
        ///     关闭数据通道
        /// </summary>
        /// <returns>关闭动作是否完成</returns>
        bool Close();

        event EventHandler Closing;
        event EventHandler Closed;

        /// <summary>
        ///     当数据采集模式发生变化时，即同步向异步，或者异步向同步发生变化时。
        /// </summary>
        event EventHandler<ChannelModeChangedEventArgs> ChannelModeChanged;

        /// <summary>
        ///     当有数据到达时。
        /// </summary>
        event EventHandler<EventArgs<T>> DataArrived;

        /// <summary>
        ///     中断正在进行的工作流的执行方法，无论是异步与同步。
        /// </summary>
        void Break();

        /// <summary>
        /// 启动同步模式的通道侦听
        /// </summary>
        void SyncListen();
        /// <summary>
        /// 启动异步模式的通道侦听
        /// </summary>
        void AsyncListen();
    }
}