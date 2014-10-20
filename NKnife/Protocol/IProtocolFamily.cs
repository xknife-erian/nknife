using System.Collections.Generic;

namespace NKnife.Protocol
{
    public interface IProtocolFamily<T> : IDictionary<string, IProtocol<T>>
    {
        void Add(IProtocol<T> protocol);

        IProtocol<T> NewProtocol(T command);
    }
}
