using System;

namespace Jeelu
{
    public class EventArgs<T> : EventArgs
    {
        private T _item;
        public T Item
        {
            get { return _item; }
        }

        public EventArgs(T item)
        {
            _item = item;
        }
    }

    public class ChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 改变前的值
        /// </summary>
        public T OldItem { get; private set; }

        /// <summary>
        /// 改变后的值
        /// </summary>
        public T NewItem { get; private set; }

        public ChangedEventArgs(T oldItem,T newItem)
        {
            this.OldItem = oldItem;
            this.NewItem = newItem;
        }
    }
}
