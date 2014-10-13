using System;
using System.Collections.Generic;

namespace SocketKnife.Protocol.Interfaces
{
    public interface IProtocolFamily : IDictionary<string, IProtocol>
    {
        IDatagramCommandParser CommandParser { get; }
        IDatagramDecoder Decoder { get; }
        IDatagramEncoder Encoder { get; }
    }
}
