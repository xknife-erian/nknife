using System.Collections.Generic;

namespace SocketKnife.Interfaces
{
    public interface IProtocolFamily : IDictionary<string, IProtocol>
    {
        IDatagramCommandParser CommandParser { get; set; }
        IDatagramDecoder Decoder { get; set; }
        IDatagramEncoder Encoder { get; set; }

        void Add(IProtocol protocol);
    }
}
