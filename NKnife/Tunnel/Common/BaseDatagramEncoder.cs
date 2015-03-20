namespace NKnife.Tunnel.Common
{
    public abstract class BaseDatagramEncoder<TData> : IDatagramEncoder<byte[], TData>
    {
        public abstract byte[] Execute(TData data);
    }
}