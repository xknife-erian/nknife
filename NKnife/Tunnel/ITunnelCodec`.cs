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
    /// <typeparam name="TDataOut">内容在传输过程所使用的数据形式</typeparam>
    /// <typeparam name="TDataIn">内容在原始状态时所使用的数据形式</typeparam>
    public interface ITunnelCodec<TDataOut, TDataIn>
    {
        /// <summary>
        /// 解码器
        /// </summary>
        IDatagramDecoder<TDataOut, TDataIn> Decoder { get; set; }
        /// <summary>
        /// 编码器
        /// </summary>
        IDatagramEncoder<TDataOut, TDataIn> Encoder { get; set; }
    }
}
