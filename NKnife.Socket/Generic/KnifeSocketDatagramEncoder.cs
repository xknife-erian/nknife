using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketDatagramEncoder : IDatagramEncoder<string>
    {
        public abstract byte[] Execute(string replay);
    }
}