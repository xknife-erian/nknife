using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Protocol
{
    /// <summary>
    /// 协议的尾部标记
    /// </summary>
    public interface IProtocolTail
    {
        byte[] Tail { get; }
    }
}
