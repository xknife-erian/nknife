using System;

namespace NKnife.Tunnel.Events
{
    public class DataDecodedEventArgs<T> : EventArgs
    {
        public DataDecodedEventArgs(T source, string[] datas)
        {
            Source = source;
            Datas = datas;
        }

        public T Source { get; private set; }

        public string[] Datas { get; private set; }
    }
}