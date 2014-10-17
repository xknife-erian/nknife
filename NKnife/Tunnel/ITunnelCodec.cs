using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel
{
    public interface ITunnelCodec
    {
        IDatagramCommandParser CommandParser { get; set; }
        IDatagramDecoder Decoder { get; set; }
        IDatagramEncoder Encoder { get; set; }
    }
}
