using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel.Common
{
    /// <summary>
    /// TSessionId的类型为int,可用于serialport等串口协议,用int串口号做标记
    /// </summary>
    public class IntKnifeTunnelSession : KnifeTunnelSession<int>
    {
    }
}
