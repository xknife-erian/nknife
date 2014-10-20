using System;
using NKnife.Events;
using SocketKnife.Common;
using SocketKnife.Events;

namespace SocketKnife.Interfaces
{
    public interface ISocketClientFilter : ISocketFilter
    {
        /// <summary>
        ///     即将连接事件
        /// </summary>
        event EventHandler<ConnectioningEventArgs> Connectioning;

        /// <summary>
        ///     连接成功后事件
        /// </summary>
        event EventHandler<ConnectionedEventArgs> Connectioned;

        /// <summary>
        ///     Sokcet连接的状态发生改变后的事件
        /// </summary>
        event EventHandler<ChangedEventArgs<ConnectionStatus>> SocketStatusChanged;

        /// <summary>
        ///     当连接断开后发生的事件
        /// </summary>
        event EventHandler<ConnectionedEventArgs> ConnectionBroke;
    }
}