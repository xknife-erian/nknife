namespace NKnife.Tunnel.Base
{
    public abstract class BaseDatagramDecoder<TData> : IDatagramDecoder<TData>
    {
        public abstract TData[] Execute(byte[] data, out int finishedIndex);
    }
}