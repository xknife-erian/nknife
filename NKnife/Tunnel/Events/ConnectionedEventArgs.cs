using System;

namespace NKnife.Tunnel.Events
{
    /// <summary>
    ///     当Socket连接后事件发生时包含事件数据的类(根据IsConnSucceed判断是否连接成功)
    /// </summary>
    public class ConnectionedEventArgs : EventArgs
    {
        public ConnectionedEventArgs(bool isConnSucceed, string message)
        {
            IsConnSucceed = isConnSucceed;
            Message = message;
        }

        public string Message { get; private set; }
        public bool IsConnSucceed { get; set; }
    }
}