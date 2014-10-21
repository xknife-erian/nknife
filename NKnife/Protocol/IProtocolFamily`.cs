using System.Collections.Generic;

namespace NKnife.Protocol
{
    public interface IProtocolFamily<T> : IDictionary<string, IProtocol<T>>
    {
        string Family { get; set; }

        IProtocolCommandParser<T> CommandParser { get; set; }

        void Add(IProtocol<T> protocol);

        IProtocol<T> NewProtocol(T command);
    }
}
