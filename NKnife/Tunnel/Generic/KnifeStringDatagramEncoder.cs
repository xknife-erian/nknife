namespace NKnife.Tunnel.Generic
{
    public abstract class KnifeStringDatagramEncoder : IDatagramEncoder<string>
    {
        public abstract byte[] Execute(string replay);
    }
}