using System.Collections.Generic;

namespace NKnife.Protocol
{
    public interface IProtocolFamily<TSource, TConnector, T> : IDictionary<string, IProtocol<T>>
    {
        string Family { get; set; }

        void Bind(IProtocolHandler<TSource, TConnector, T> handler);

        void Add(IProtocol<T> protocol);

        IProtocol<T> NewProtocol(T command);
    }
}
