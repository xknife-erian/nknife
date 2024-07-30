#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

namespace NKnife.Events
{
    public class DataChangedEventArgs<T> : EventArgs<T>
    {
        public DataChangedEventArgs(T newData) : this(default, newData) { }

        public DataChangedEventArgs(T? oldData, T newData) : base(newData)
        {
            OldData = oldData;
        }

        public T? OldData { get; set; }
    }
}

#pragma warning disable CS8632
