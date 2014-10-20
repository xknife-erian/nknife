using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketCodec : ISocketCodec
    {
        IDatagramCommandParser<string> ITunnelCodec<string>.CommandParser
        {
            get { return SocketCommandParser; }
            set { SocketCommandParser = (ISocketCommandParser) value; }
        }
        public ISocketCommandParser SocketCommandParser { get; set; }
        public IDatagramDecoder Decoder { get; set; }
        public IDatagramEncoder Encoder { get; set; }
    }
}
