using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketCodec : ISocketCodec
    {
        public abstract KnifeSocketDatagramDecoder SocketDecoder { get; set; }
        public abstract KnifeSocketDatagramEncoder SocketEncoder { get; set; }

        IDatagramCommandParser<string> ITunnelCodec<string>.CommandParser
        {
            get { return SocketCommandParser; }
            set { SocketCommandParser = (ISocketCommandParser) value; }
        }
        public ISocketCommandParser SocketCommandParser { get; set; }

        IDatagramDecoder<string> ITunnelCodec<string>.Decoder
        {
            get { return SocketDecoder; }
            set { SocketDecoder = (KnifeSocketDatagramDecoder) value; }
        }

        IDatagramEncoder<string> ITunnelCodec<string>.Encoder
        {
            get { return SocketEncoder; }
            set { SocketEncoder = (KnifeSocketDatagramEncoder) value; }
        }
    }
}
