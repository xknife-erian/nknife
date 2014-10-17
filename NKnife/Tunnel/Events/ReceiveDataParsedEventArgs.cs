using System;
using NKnife.Protocol;

namespace NKnife.Tunnel.Events
{
    /// <summary>
    ///     当接收到的数据解析完成后事件发生时包含事件数据的类
    /// </summary>
    public class ReceiveDataParsedEventArgs : EventArgs
    {
        public ReceiveDataParsedEventArgs(IProtocol protocol)
        {
            Protocol = protocol;
        }

        public IProtocol Protocol { get; private set; }
    }
}