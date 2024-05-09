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