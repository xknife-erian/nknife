namespace NKnife.Protocol.Generic
{
    public abstract class StringProtocolPacker : IProtocolPacker<string>
    {
        string IProtocolPacker<string>.Combine(IProtocolContent<string> content)
        {
            return Combine((StringProtocolContent) content);
        }

        public abstract string Combine(StringProtocolContent content);
    }
}
