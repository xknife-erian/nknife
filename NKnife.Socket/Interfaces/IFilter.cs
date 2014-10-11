using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Interfaces
{
    public interface IFilter
    {
        IDatagramCommandParser CommandParser { get; }
        IDatagramDecoder Decoder { get; }
        IDatagramEncoder Encoder { get; }
    }
}