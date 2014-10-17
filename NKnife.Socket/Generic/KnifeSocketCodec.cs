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
        public IDatagramCommandParser CommandParser { get; set; }
        public IDatagramDecoder Decoder { get; set; }
        public IDatagramEncoder Encoder { get; set; }
    }
}
