using System;
using System.Net;

namespace NKnife.Tunnel.Events
{
    /// <summary>
    ///     当出错或断开触发事件发生时包含事件数据的类，继承自 SocketAsyncEventArgs
    /// </summary>
    public class ConnectionBreakEventArgs<TSource> : EventArgs
    {
        public ConnectionBreakEventArgs(TSource endPoint, string message)
        {
            Message = message;
            EndPoint = endPoint;
        }

        public TSource EndPoint { get; set; }
        public string Message { get; private set; }
    }
}