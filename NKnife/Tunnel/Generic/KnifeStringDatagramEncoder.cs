namespace NKnife.Tunnel.Generic
{
    public abstract class KnifeStringDatagramEncoder : IDatagramEncoder<string,byte[]>
    {
        public abstract byte[] Execute(string replay);
    }
}