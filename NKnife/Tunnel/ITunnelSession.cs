using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NKnife.Tunnel
{
    public interface ITunnelSession<TSessionId, TData>
    {
        TSessionId Id { get; }
        TData Data { get; }
        /// <summary>
        /// Data经过每一层的Filter处理后，可以以更抽象的形式
        /// （相对于TData，TData是贯穿于所有Filter的统一的数据表现形式，例如byte[]），
        /// 如Protocol来包装数据，可以使用该Tag来存放个性化的数据Wrapper
        /// </summary>
        object Tag { get; }
    }
}
