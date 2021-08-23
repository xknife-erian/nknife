using System.Collections.Generic;

namespace NKnife.Channels.Interfaces
{
    /// <summary>
    /// 描述一个多通道的数据采集来源。多通道中，可以是多个串口，TCPIP端口，各种驱动调用等连接方式的复合调用。
    /// </summary>
    public interface IMultiChannel<T> : ICollection<IChannel<T>>
    {
    }
}
