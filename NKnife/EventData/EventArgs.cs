using System;

namespace Gean
{
    public class EventArgs<T> : EventArgs
    {
        public T Item { get; private set; }
        public EventArgs(T item)
        {
            this.Item = item;
        }
    }

}
