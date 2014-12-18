using System;
using System.Collections.Generic;
using System.Windows.Documents;
using NKnife.Tunnel;

namespace NKnife.Protocol
{
    public interface IProtocolHandler<TData, TSessionId, T>
    {
        List<T> Commands { get; set; }

        void Recevied(TSessionId session, IProtocol<T> protocol);

        void Write(TSessionId session, TData data);

        void Write(TSessionId session, IProtocol<T> data);
    }
}