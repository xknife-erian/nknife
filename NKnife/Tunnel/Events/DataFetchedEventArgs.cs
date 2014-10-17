using System;
using System.Collections.Generic;

namespace NKnife.Tunnel.Events
{
    /// <summary>
    ///     当有数据异步接收到后事件发生时包含事件数据的类。
    /// </summary>
    public class DataFetchedEventArgs<T> : EventArgs
    {
        public DataFetchedEventArgs(T source, byte[] data)
        {
            Source = source;
            Data = data;
        }

        public T Source { get; private set; }

        public byte[] Data { get; private set; }
    }
}