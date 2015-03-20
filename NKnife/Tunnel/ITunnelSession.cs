using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NKnife.Tunnel
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSessionId"></typeparam>
    /// <typeparam name="TSource">内容在原始状态时所使用的数据形式</typeparam>
    public interface ITunnelSession<out TSessionId, out TSource>
    {
        TSessionId Id { get; }
        /// <summary>
        /// 原始数据。内容在原始状态时所使用的数据形式。
        /// </summary>
        TSource Data { get; }
        /// <summary>
        /// 如Protocol来包装数据，可以使用该Tag来存放个性化的数据Wrapper
        /// </summary>
        object Tag { get; }
    }
}
