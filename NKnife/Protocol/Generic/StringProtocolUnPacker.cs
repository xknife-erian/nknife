namespace NKnife.Protocol.Generic
{
    public abstract class StringProtocolUnPacker : IProtocolUnPacker<string>
    {
        public abstract short Version { get; }

        void IProtocolUnPacker<string>.Execute(ref IProtocolContent<string> content, string data, string family, string command)
        {
            Execute((StringProtocolContent)content, data, family, command);
        }

        public abstract void Execute(StringProtocolContent content, string data, string family, string command);
    }
}