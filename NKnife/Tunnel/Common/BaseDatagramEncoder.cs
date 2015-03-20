namespace NKnife.Tunnel.Common
{
    public abstract class BaseDatagramEncoder<TData> : IDatagramEncoder<TData, byte[]>
    {
        public abstract byte[] Execute(TData data);
    }
}