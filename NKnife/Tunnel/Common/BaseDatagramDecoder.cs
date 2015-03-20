namespace NKnife.Tunnel.Common
{
    public abstract class BaseDatagramDecoder<TData> : IDatagramDecoder<TData, byte[]>
    {
        public abstract TData[] Execute(byte[] data, out int finishedIndex);
    }
}