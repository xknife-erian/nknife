using NKnife.Protocol;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketProtocolPackager : IProtocolPackager<string>
    {
        public abstract short Version { get; }

        string IProtocolPackager<string>.Combine(IProtocolContent<string> content)
        {
            return Combine((KnifeSocketProtocolContent) content);
        }

        public abstract string Combine(KnifeSocketProtocolContent content);
    }
}
