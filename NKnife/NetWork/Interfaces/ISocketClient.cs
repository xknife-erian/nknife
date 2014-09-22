using NKnife.NetWork.Common;

namespace NKnife.NetWork.Interfaces
{
    public interface ISocketClient : ISocketBase
    {
        /// <summary>选项
        /// </summary>
        /// <value>The option.</value>
        ISocketClientSetting Option { get; }

        /// <summary>是否连接成功
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is connection suceess; otherwise, <c>false</c>.
        /// </value>
        bool IsConnectionSuceess { get; }

        /// <summary>远端服务器状态
        /// </summary>
        /// <value><c>true</c> if [server status]; otherwise, <c>false</c>.</value>
        bool ServerStatus { get; }

        /// <summary>接收数据队列
        /// </summary>
        /// <value>The command parser.</value>
        ReceiveQueue ReceiveQueue { get; }

        /// <summary>即将连接事件
        /// </summary>
        event ConnectioningEventHandler ConnectioningEvent;

        /// <summary>连接成功事件
        /// </summary>
        event ConnectionedEventHandler ConnectionedEvent;

        /// <summary>Sokcet连接的状态发生改变后的事件
        /// </summary>
        event SocketStatusChangedEventHandler SocketStatusChangedEvent;

        /// <summary>当连接断开发生的事件
        /// </summary>
        event ConnectionedWhileBreakEventHandler ConnectionedWhileBreakEvent;

        /// <summary>异步发送数据
        /// </summary>
        /// <param name="data">The data.</param>
        bool SendTo(string data);

        /// <summary>异步发送数据
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        bool SendTo(string data, bool isCompress);

        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="timeout">等待超时的时长</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        /// <returns></returns>
        string TalkTo(string data, int timeout, bool isCompress);

        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="timeout">等待超时的时长</param>
        /// <returns></returns>
        string TalkTo(string data, int timeout);

        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        /// <returns></returns>
        string TalkTo(string data, bool isCompress);


        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <returns></returns>
        string TalkTo(string data);

        /// <summary>将本类型连接到远端
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        bool ConnectTo(string host, int port);
    }
}