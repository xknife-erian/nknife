using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Events;
using SocketKnife.Events;

namespace SocketKnife.Interfaces
{
    public interface ISocketClientFilter : ISocketFilter
    {
        /// <summary>
        ///     即将连接事件
        /// </summary>
        event EventHandler<ConnectioningEventArgs> ConnectioningEvent;

        /// <summary>
        ///     连接成功事件
        /// </summary>
        event EventHandler<ConnectionedEventArgs> ConnectionedEvent;

        /// <summary>
        ///     Sokcet连接的状态发生改变后的事件
        /// </summary>
        event EventHandler<SocketStatusChangedEventArgs> SocketStatusChangedEvent;

        /// <summary>
        ///     当连接断开发生的事件
        /// </summary>
        event EventHandler<ConnectionedEventArgs> ConnectionedWhileBreakEvent;
    }
}
