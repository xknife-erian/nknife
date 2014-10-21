using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface ITunnelCodec<T>
    {
        IDatagramDecoder<T> Decoder { get; set; }
        IDatagramEncoder<T> Encoder { get; set; }
    }
}
