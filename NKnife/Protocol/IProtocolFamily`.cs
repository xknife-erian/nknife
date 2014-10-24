using System.Collections.Generic;

namespace NKnife.Protocol
{
    public interface IProtocolFamily<T> : IDictionary<string, IProtocol<T>>
    {
        string FamilyName { get; set; }

        IProtocolCommandParser<T> CommandParser { get; set; }

        void Add(IProtocol<T> protocol);

        IProtocol<T> Build(T command);
    }
}
