using SocketKnife.Common;
using SocketKnife.Protocol;

namespace SocketKnife.Interfaces
{
    public interface ISocketBase
    {
        /// <summary>
        ///     Sokcet的工作模式
        /// </summary>
        /// <value>The mode.</value>
        SocketMode Mode { get; }

        /// <summary>
        ///     协议家族
        /// </summary>
        /// <value>The type of the family.</value>
        string FamilyType { get; }

        /// <summary>
        ///     接收到的消息的解析器
        /// </summary>
        /// <value>The decoder.</value>
        IDatagramDecoder Decoder { get; }

        /// <summary>
        ///     字符=>字节数组的转换器
        /// </summary>
        /// <value>The encoder.</value>
        IDatagramEncoder Encoder { get; }

        /// <summary>
        ///     命令字解析器
        /// </summary>
        /// <value>The command parser.</value>
        IDatagramCommandParser CommandParser { get; }

        /// <summary>
        ///     异步接收到数据时
        /// </summary>
        event SocketAsyncDataComeInEventHandler DataComeInEvent;

        /// <summary>
        ///     连接出错或连接断开时
        /// </summary>
        event ConnectionBreakEventHandler ConnectionBreak;

        /// <summary>
        ///     异步接收到数据
        /// </summary>
        event ReceiveDataParsedEventHandler ReceiveDataParsedEvent;

        /// <summary>
        ///     关闭当前的Sokcet工作
        /// </summary>
        /// <returns></returns>
        bool Close();
    }
}