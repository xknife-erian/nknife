namespace NKnife.Protocol.Generic
{
    public abstract class StringProtocolUnPacker : IProtocolUnPacker<string>
    {
        void IProtocolUnPacker<string>.Execute(IProtocol<string> protocol, string data, string family, string command)
        {
            Execute((StringProtocol)protocol, data, family, command);
        }

        public abstract void Execute(StringProtocol protocol, string data, string family, string command);
    }
}