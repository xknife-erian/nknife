using System;
using SocketKnife.Events;

namespace SocketKnife.Interfaces
{
    public interface ISocketServerFilter : ISocketFilter
    {
        /// <summary>
        ///     服务器侦听到新客户端连接事件
        /// </summary>
        event EventHandler<SocketSessionEventArgs> ClientCome;

        /// <summary>
        ///     连接出错或断开触发事件
        /// </summary>
        event EventHandler<SocketConnectionBreakEventArgs> ClientBroke;
    }
}