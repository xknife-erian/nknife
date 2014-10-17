using System.Collections.Generic;

namespace NKnife.Protocol
{
    public interface IProtocolFamily : IDictionary<string, IProtocol>
    {
        void Add(IProtocol protocol);

        IProtocol NewProtocol(string command);
    }
}
