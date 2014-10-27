namespace NKnife.Protocol.Generic
{
    public abstract class StringProtocolUnPacker : IProtocolUnPacker<string>
    {
        void IProtocolUnPacker<string>.Execute(IProtocolContent<string> content, string data, string family, string command)
        {
            Execute((StringProtocolContent)content, data, family, command);
        }

        public abstract void Execute(StringProtocolContent content, string data, string family, string command);
    }
}