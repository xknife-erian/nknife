using NKnife.Tunnel;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketDatagramDecoder : IDatagramDecoder<string>
    {
        public abstract string[] Execute(byte[] data, out int finishedIndex);
    }
}