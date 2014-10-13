using SocketKnife.Common;
using SocketKnife.Interfaces;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Generic
{
    public class DefaultFilter : IFilter
    {
        public IDatagramCommandParser CommandParser { get; private set; }
        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }
        public event SocketAsyncDataComeInEventHandler DataComeInEvent;
        public event ListenToClientEventHandler ListenToClient;
        public event ConnectionBreakEventHandler ConnectionBreak;
        public event ReceiveDataParsedEventHandler ReceiveDataParsedEvent;
    }
}
