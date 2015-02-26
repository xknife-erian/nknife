using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface ITunnelCodec<T,TData>
    {
        IDatagramDecoder<T, TData> Decoder { get; set; }
        IDatagramEncoder<T, TData> Encoder { get; set; }
    }
}
