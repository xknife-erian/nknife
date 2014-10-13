using System;
using System.Collections.Generic;

namespace SocketKnife.Protocol.Interfaces
{
    public interface IProtocolFamily : IDictionary<string, Type>
    {
        IProtocol Get(string familyType, string command);
    }
}
