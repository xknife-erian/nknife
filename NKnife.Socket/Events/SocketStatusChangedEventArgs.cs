using System;
using SocketKnife.Common;

namespace SocketKnife.Events
{
    /// <summary>
    ///     Socket连接状态发生改变时包含数据的类
    /// </summary>
    public class SocketStatusChangedEventArgs : EventArgs
    {
        public SocketStatusChangedEventArgs(ConnectionStatus status)
        {
            Status = status;
        }

        public ConnectionStatus Status { get; private set; }
    }
}