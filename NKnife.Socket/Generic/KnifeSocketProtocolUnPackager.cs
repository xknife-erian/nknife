using NKnife.Protocol;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketProtocolUnPacker : IProtocolUnPacker<string>
    {
        public abstract short Version { get; }

        void IProtocolUnPacker<string>.Execute(ref IProtocolContent<string> content, string data, string family, string command)
        {
            Execute((KnifeSocketProtocolContent)content, data, family, command);
        }

        public abstract void Execute(KnifeSocketProtocolContent content, string data, string family, string command);
    }
}