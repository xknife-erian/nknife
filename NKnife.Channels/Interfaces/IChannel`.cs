using System;
using System.Collections.Generic;
using NKnife.Channels.Base;
using NKnife.Channels.EventParams;
using NKnife.Interface;
using NKnife.Timers;

namespace NKnife.Channels.Interfaces
{
    /// <summary>
    ///     描述一个数据通道。该来源可以是某个串口，某个TCP/IP端口，某类驱动调用等等。
    /// </summary>
    public interface IChannel<T>
    {
        /// <summary>
        ///     描述该数据通道的模式是否是同步获取，也称对话模式。即发送采集指令后直接等待返回。
        /// </summary>
        /// <returns>当true时，是同步的，反之为异步</returns>
        bool IsSynchronous { get; set; }

        /// <summary>
        ///     当前数据通道的所面向的实际目标，当数据通道的目标只有一个时，本属性可以忽略。
        ///     在很多情况下，虽然数据是通过一个数据通道出去，但是类似485，CAN的总结架构时，可能总线上有很多的设备（多达数百个），在这种情况下，这里是设备列表。
        ///     再比如，在GPIB总线下，同样一个总线下可能挂着多达几十台的仪器。
        /// </summary>
        List<IId> Targets { get; }

        /// <summary>
        ///     数据通道是否打开
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        ///     工作管理器
        /// </summary>
        JobManager JobManager { get; set; }

        /// <summary>
        ///     当发送完成时将要被执行的方法
        /// </summary>
        Action<ChannelJobBase<T>> Sent { get; set; }

        /// <summary>
        ///     当采集到数据(返回的数据)的处理方法。当返回true时，表示接收数据是完整的，返回false时，表示接收数据不完整，还需要继续接收
        /// </summary>
        Func<ChannelJobBase<T>, bool> Received { get; set; }

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
        event EventHandler<ChannelAnswerDataEventArgs<T>> DataArrived;

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