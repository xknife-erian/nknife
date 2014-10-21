using NKnife.Protocol;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketProtocolPacker : IProtocolPacker<string>
    {
        public abstract short Version { get; }

        string IProtocolPacker<string>.Combine(IProtocolContent<string> content)
        {
            return Combine((KnifeSocketProtocolContent) content);
        }

        public abstract string Combine(KnifeSocketProtocolContent content);
    }
}
