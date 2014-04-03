using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using NKnife.NetWork.Common;

namespace NKnife.NetWork.Interfaces
{
    public interface ISocketServer : ISocketBase
    {
        ISocketServerSetting Option { get; }

        /// <summary>接收数据队列MAP,Key是客户端,Value是接收到的数据的队列
        /// </summary>
        /// <value>The command parser.</value>
        ConcurrentDictionary<EndPoint, ReceiveQueue> ReceiveQueueMap { get; }

        int ReceiveTimeout { get; set; }
        int SendTimeout { get; set; }
        int MaxBufferSize { get; }
        int MaxConnectCount { get; }
        bool NoDelay { get; set; }
        event ListenToClientEventHandler ListenToClient;

        bool Open();
        void Disconnect(Socket socket);

        void StartAccept();
        void StopAccept();

        /// <summary>发送数据
        /// </summary>
        /// <param name="socket">客户端</param>
        /// <param name="data">The data.</param>
        void SendTo(Socket socket, string data);

        /// <summary>发送数据
        /// </summary>
        /// <param name="socket">客户端</param>
        /// <param name="data">The data.</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        void SendTo(Socket socket, string data, bool isCompress);

        /// <summary>发送数据
        /// </summary>
        /// <param name="ipaddress">客户端</param>
        /// <param name="data">The data.</param>
        void SendTo(string ipaddress, string data);

        /// <summary>发送数据
        /// </summary>
        /// <param name="ipaddress">客户端</param>
        /// <param name="data">The data.</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        void SendTo(string ipaddress, string data, bool isCompress);

        /// <summary>当是长连接时向多个客户端群发
        /// </summary>
        /// <param name="data">The data.</param>
        void MultiClientSendTo(string data);

        /// <summary>当是长连接时向多个客户端群发
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        void MultiClientSendTo(string data, bool isCompress);
    }
}