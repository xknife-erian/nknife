using System;
using System.Collections.Generic;
using NKnife.Channels.EventParams;
using NKnife.Interface;

namespace NKnife.Channels.Interfaces
{
    /// <summary>
    ///     描述一个数据采集通道。该来源可以是某个串口，某个TCPIP端口，某类驱动调用等等。
    /// </summary>
    public interface IChannel<T>
    {
        /// <summary>
        ///     描述该数据采集通道的模式是否是同步获取。即发送采集指令后直接等待返回。
        /// </summary>
        /// <returns>当true时，是同步的，反之为异步</returns>
        bool IsSynchronous { get; set; }

        /// <summary>
        ///     当前数据采集通道的所面向的实际采集源。
        /// </summary>
        List<IId> Targets { get; }

        /// <summary>
        ///     通道是否打开
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        ///     打开采集通道
        /// </summary>
        /// <returns></returns>
        bool Open();

        event EventHandler Opening;
        event EventHandler Opened;

        /// <summary>
        ///     关闭采集通道
        /// </summary>
        /// <returns></returns>
        bool Close();

        event EventHandler Closeing;
        event EventHandler Closed;

        /// <summary>
        ///     更新即将发送的数据
        /// </summary>
        /// <param name="questionGroup">即将发送的数据</param>
        void UpdateQuestionGroup(IQuestionGroup<T> questionGroup);

        /// <summary>
        ///     发送数据并同步等待数据返回
        /// </summary>
        /// <param name="sendAction">当发送完成时</param>
        /// <param name="onReceived">当采集到数据(返回的数据)的处理方法,当返回true时，表示接收数据是完整的，返回flase时，表示接收数据不完整，还需要继续接收</param>
        /// <returns>是否采集到数据</returns>
        void SendReceiving(Action<IQuestion<T>> sendAction, Func<IAnswer<T>, bool> onReceived);

        /// <summary>
        ///     自动发送数据并同步等待数据返回
        /// </summary>
        void AutoSend(Action<IQuestion<T>> sendAction);

        /// <summary>
        ///     当自动发送模式时，中断正在不断进行的自动模式
        /// </summary>
        void Break();

        /// <summary>
        ///     当数据采集模式发生变化时，即同步向异步，或者异步向同步发生变化时。
        /// </summary>
        event EventHandler<ChannelModeChangedEventArgs> ChannelModeChanged;

        /// <summary>
        ///     当有数据到达时。
        /// </summary>
        event EventHandler<ChannelAnswerDataEventArgs<T>> DataArrived;
    }
}