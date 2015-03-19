using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    /// <summary>
    /// 协议的编解码器
    /// </summary>
    /// <typeparam name="TOriginal">内容在编程过程所使用的数据形式</typeparam>
    /// <typeparam name="TData">内容在传输过程所使用的数据形式</typeparam>
    public interface ITunnelCodec<TOriginal, TData>
    {
        IDatagramDecoder<TOriginal, TData> Decoder { get; set; }
        IDatagramEncoder<TOriginal, TData> Encoder { get; set; }
    }
}
