using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Default
{
    public class DefaultFilter : IFilter
    {
        public IDatagramCommandParser CommandParser { get; private set; }
        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }
    }
}
