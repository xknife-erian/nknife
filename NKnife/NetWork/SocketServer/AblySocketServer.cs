using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Gean;
using Gean.Network.Common;
using Gean.Network.Protocol;
using NKnife.Extensions;
using NKnife.NetWork.Interfaces;
using NKnife.NetWork.Protocol;
using NKnife.Utility;
using NLog;
using BufferContainer = Gean.Net.SocketServer.BufferContainer;
using SocketAsyncEventArgsPool = Gean.Net.SocketServer.SocketAsyncEventArgsPool;

namespace NKnife.Net.SocketServer
{
    public abstract class AliveSocketServer : AblySocketServer
    {
        protected AliveSocketServer(SocketMode mode, ProtocolFamilyType family, string host, int port, int maxConnectCount, int maxBufferSize) : 
            base(mode, family, host, port, maxConnectCount, maxBufferSize)
        {
        }
    }

    /// <summary>GEAN原创精巧Socket框架服务器端。
    /// Ably：[D.J.:'eɪblɪ]，adv. 精明强干地；灵巧地；巧妙地； 
    /// </summary>
    public abstract class AblySocketServer : IDisposable, ISocketServer
    {
        #region 成员变量

        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        protected AutoResetEvent[] _AutoReset;

        /// <summary>数据包管理
        /// </summary>
        protected BufferContainer _BufferContainer;

        /// <summary>当长连接时的客户端集合,Key为IP地址,Value为Socket实例
        /// </summary>
        /// <value>The session.</value>
        protected ConcurrentDictionary<string, Socket> _ClientMap;

        protected readonly ConcurrentDictionary<EndPoint, ReceiveQueue> _ReceiveQueueMap = new ConcurrentDictionary<EndPoint, ReceiveQueue>();

        /// <summary>
        /// IP
        /// </summary>
        protected string _Host;

        protected bool _IsClose = true;

        /// <summary>
        /// Socket对象
        /// </summary>
        protected Socket _MainSocket;

        /// <summary>
        /// 端口
        /// </summary>
        protected int _Port;

        /// <summary>
        /// Socket异步对象池
        /// </summary>
        private SocketAsyncEventArgsPool _SocketAsynPool;

        #endregion

        #region 构造函数

        protected AblySocketServer(
            SocketMode mode, ProtocolFamilyType family,
            string host, int port,
            int maxConnectCount, int maxBufferSize)
        {
            Mode = mode;
            FamilyType = family;
            MaxBufferSize = maxBufferSize;
            MaxConnectCount = maxConnectCount;

            _Port = port;
            _Host = host;
            _ClientMap = new ConcurrentDictionary<string, Socket>();

            _AutoReset = new AutoResetEvent[1];
            _AutoReset[0] = new AutoResetEvent(false);

            try
            {
                Run();
            }
            catch (Exception e)
            {
                _Logger.Warn(string.Format(string.Format("服务器启动时异常。{0}", e.Message), e));
            }
        }

        #endregion

        #region 属性

        public abstract ISocketServerSetting Option { get; }

        public SocketMode Mode { get; internal set; }

        public ProtocolFamilyType FamilyType { get; internal set; }

        /// <summary>接收数据队列MAP,Key是客户端,Value是接收到的数据的队列
        /// </summary>
        /// <value>The command parser.</value>
        public ConcurrentDictionary<EndPoint, ReceiveQueue> ReceiveQueueMap
        {
            get { return _ReceiveQueueMap; } 
        }

        public ConcurrentDictionary<string, Socket> ClientMap { get { return _ClientMap; } }

        /// <summary>
        /// 接收包大小
        /// </summary>
        public int MaxBufferSize { get; private set; }

        /// <summary>
        /// 最大用户连接数
        /// </summary>
        public int MaxConnectCount { get; private set; }

        /// <summary>
        /// 是否关闭SOCKET Delay算法
        /// </summary>
        public bool NoDelay
        {
            get { return _MainSocket.NoDelay; }
            set { _MainSocket.NoDelay = value; }
        }

        /// <summary>
        /// SOCKET 的 ReceiveTimeout属性
        /// </summary>
        public int ReceiveTimeout
        {
            get { return _MainSocket.ReceiveTimeout; }
            set { _MainSocket.ReceiveTimeout = value; }
        }

        /// <summary>
        /// SOCKET 的 SendTimeout
        /// </summary>
        public int SendTimeout
        {
            get { return _MainSocket.SendTimeout; }
            set { _MainSocket.SendTimeout = value; }
        }

        /// <summary>接收到的数据的解析器
        /// </summary>
        /// <value>The decoder.</value>
        public IDatagramDecoder Decoder
        {
            get { return Option.Decoder; }
        }

        /// <summary>字符 向 字节数组的转换器
        /// </summary>
        /// <value>The encoder.</value>
        public IDatagramEncoder Encoder
        {
            get { return Option.Encoder; }
        }

        /// <summary>命令字解析器
        /// </summary>
        /// <value>The command parser.</value>
        public IDatagramCommandParser CommandParser
        {
            get { return Option.CommandParser; }
        }

        #endregion

        #region 公共方法

        public void StartAccept()
        {
            _AutoReset[0].Set();
        }

        public void StopAccept()
        {
            _AutoReset[0].Reset();
        }

        public bool Close()
        {
            try
            {
                _IsClose = true;
                _MainSocket.Close();
                foreach (Socket client in _ClientMap.Values)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                }
                foreach (SocketAsyncEventArgs async in _SocketAsynPool)
                {
                    if (null == async.AcceptSocket || !async.AcceptSocket.Connected)
                    {
                        continue;
                    }
                    async.AcceptSocket.Shutdown(SocketShutdown.Both);
                    async.AcceptSocket.Close();
                }
                _SocketAsynPool.Clear();
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("SocketServer关闭异常。{0}", e.Message), e);
                return false;
            }
        }

        public bool Open()
        {
            try
            {
                Run();
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("SocketServer打开异常。{0}", e.Message), e);
                return false;
            }
        }

        /// <summary>断开此SOCKET
        /// </summary>
        /// <param name="socket"></param>
        public void Disconnect(Socket socket)
        {
            if (_MainSocket != null && _MainSocket.Connected)
                socket.BeginDisconnect(false, AsynCallBackDisconnect, socket);
        }

        /// <summary>发送数据
        /// </summary>
        /// <param name="ipaddress">客户端</param>
        /// <param name="data">The data.</param>
        public void SendTo(string ipaddress, string data)
        {
            SendTo(ipaddress, data, false);
        }

        /// <summary>发送数据
        /// </summary>
        /// <param name="socket">客户端</param>
        /// <param name="data">The data.</param>
        public void SendTo(Socket socket, string data)
        {
            SendTo(socket, data, false);
        }

        /// <summary>指定IP地址发送，该方法一般在是长连接服务时使用。
        /// </summary>
        /// <param name="ipaddress">The ipaddress.</param>
        /// <param name="data">The data.</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        public void SendTo(string ipaddress, string data, bool isCompress = false)
        {
            Socket socket;
            if (!_ClientMap.TryGetValue(ipaddress, out socket))
            {
                _Logger.Info(string.Format("客户端IP地址:({0})不在列表中，列表数:{1}", ipaddress, _ClientMap.Count));
                return;
            }
            SendTo(socket, data, isCompress);
        }

        /// <summary>发送数据包
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        public void SendTo(Socket socket, string data, bool isCompress = false)
        {
            byte[] senddata;
            try
            {
                //File.WriteAllText(@"C:\Users\yys\Desktop\5.xml", data);
                senddata =Encoder.Execute(data, isCompress);
               
            }
            catch (Exception e)
            {
                _Logger.Warn(string.Format("协议编码异常.未发送.{0},{1}", e.Message, data), e);
                return;
            }
            try
            {
                switch (Mode)
                {
                    case SocketMode.AsyncKeepAlive:
                        socket.BeginSend(senddata, 0, senddata.Length, SocketFlags.None, AsynCallBackSend, socket);
                        break;
                    case SocketMode.Talk:
                        SocketError errCode;
                       socket.Send(senddata, 0, senddata.Length, SocketFlags.None, out errCode);
                        //File.WriteAllText(@"C:\Users\yys\Desktop\6.xml", Encoding.UTF8.GetString(senddata));
                        break;
                }
                _Logger.Trace(string.Format("Server.Send:{0}", data));
            }
            catch
            {
                throw new SocketClientDisOpenedException(string.Format("TRY-DisOpened,远端无法连接."));
            }
        }

        /// <summary>当是长连接时向多个客户端群发
        /// </summary>
        /// <param name="data">The data.</param>
        public void MultiClientSendTo(string data)
        {
            MultiClientSendTo(data, false);
        }

        /// <summary>当是长连接时向多个客户端群发
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        public void MultiClientSendTo(string data, bool isCompress = false)
        {
            switch (Mode)
            {
                case SocketMode.AsyncKeepAlive:
                    foreach (var socket in _ClientMap.Values)
                        SendTo(socket, data, isCompress);
                    break;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>启动并运行
        /// </summary>
        protected void Run()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");

            var ipEndPoint = new IPEndPoint(IPAddress.Any, _Port);
            if (!_Host.Equals("any", StringComparison.CurrentCultureIgnoreCase))
            {
                if (!String.IsNullOrWhiteSpace(_Host))
                {
                    IPHostEntry p = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress ipaddress in p.AddressList)
                    {
                        if (!ipaddress.IsIPv6LinkLocal)
                            ipEndPoint = new IPEndPoint(ipaddress, _Port);
                    }
                }
                else
                {
                    try
                    {
                        if (_Host != null)
                            ipEndPoint = new IPEndPoint(IPAddress.Parse(_Host), _Port);
                    }
                    catch (FormatException)
                    {
                        IPHostEntry p = Dns.GetHostEntry(Dns.GetHostName());
                        foreach (IPAddress ipaddress in p.AddressList)
                        {
                            if (!ipaddress.IsIPv6LinkLocal)
                                ipEndPoint = new IPEndPoint(ipaddress, _Port);
                        }
                    }
                }
            }

            _MainSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            _MainSocket.Bind(ipEndPoint);
            _MainSocket.ReceiveBufferSize = MaxBufferSize;
            _MainSocket.SendBufferSize = MaxBufferSize;

            //挂起连接队列的最大长度。
            _MainSocket.Listen(64);

            SendTimeout = 1000;
            ReceiveTimeout = 1000;

            _BufferContainer = new BufferContainer(MaxConnectCount*MaxBufferSize, MaxBufferSize);
            _BufferContainer.Initialize();

            #region 核心连接池的预创建

            _SocketAsynPool = new SocketAsyncEventArgsPool(MaxConnectCount);

            for (int i = 0; i < MaxConnectCount; i++)
            {
                var socketAsyn = new SocketAsyncEventArgs();
                socketAsyn.Completed += AsynCompleted; 
                _SocketAsynPool.Push(socketAsyn);
            }

            #endregion

            _IsClose = false;
            Accept();

            _Logger.Info(string.Format("== {0} 已启动。端口:{1}", GetType().Name, _Port));
            _Logger.Info(string.Format("发送缓冲区:大小:{0}，超时:{1}", _MainSocket.SendBufferSize, _MainSocket.SendTimeout));
            _Logger.Info(string.Format("接收缓冲区:大小:{0}，超时:{1}", _MainSocket.ReceiveBufferSize, _MainSocket.ReceiveTimeout));
            _Logger.Info(string.Format("SocketAsyncEventArgs 连接池已创建。大小:{0}", MaxConnectCount));
        }

        protected virtual void Accept()
        {
            if (_IsClose)
            {
                _Logger.Warn("Server: Socket已关闭。");
                return;
            }
            if (_SocketAsynPool.Count > 0)
            {
                SocketAsyncEventArgs sockAsyn = _SocketAsynPool.Pop();
                if (!_MainSocket.AcceptAsync(sockAsyn))
                    BeginAccep(sockAsyn);
            }
            else
            {
                _Logger.Warn("Server: Socket连接池中已达到最大连接数。");
            }
        }

        protected virtual void BeginAccep(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError == SocketError.Success)
                {
                    WaitHandle.WaitAll(_AutoReset);
                    _AutoReset[0].Set();
                    if (_BufferContainer.SetBuffer(e))
                    {
                        if (!e.AcceptSocket.ReceiveAsync(e))
                            BeginReceive(e);
                    }
                    //如果选用长连接服务时，将相应的连接置入一个Map以做处理
                    var iep = e.AcceptSocket.RemoteEndPoint as IPEndPoint;
                    if (iep != null)
                    {
                        string ip = iep.ToString();
                        if (!_ClientMap.ContainsKey(ip))
                        {
                            _ClientMap.TryAdd(ip, e.AcceptSocket);
                            _Logger.Info(string.Format("Server: IP地址:{0}的连接已放入客户端池中。", ip));
                            OnListenToClient(e);
                        }
                    }
                    else
                    {
                        _Logger.Warn("e.AcceptSocket.RemoteEndPoint 不是正确的 IPEndPoint");
                    }
                }
                else
                {
                    e.AcceptSocket = null;
                    _SocketAsynPool.Push(e);
                    _Logger.Debug(string.Format("{0}, BeginAccep时未接收。SocketError Type:{1}", GetType().Name, e.SocketError));
                }
            }
            finally
            {
                Accept();
            }
        }

        protected virtual void BeginReceive(SocketAsyncEventArgs e)
        {
            if (_IsClose)
            {
                if (e.AcceptSocket != null)
                    e.AcceptSocket.Close();
                return;
            }
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                PrcoessReceiveData(e);
            }
            else
            {
                string message = string.Format("Server: >> Client: {0}, Connection Break.", e.AcceptSocket.RemoteEndPoint);
                _Logger.Info(message);
                OnConnectionBreak(new ConnectionBreakEventArgs(message));

                e.AcceptSocket = null;
                _BufferContainer.FreeBuffer(e);
                _SocketAsynPool.Push(e);
                if (_SocketAsynPool.Count == 1)
                {
                    Accept();
                }
            }
        }

        protected virtual void PrcoessReceiveData(SocketAsyncEventArgs e)
        {
            var data = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);
            ReceiveQueue receive = null;
            if(!_ReceiveQueueMap.TryGetValue(e.AcceptSocket.RemoteEndPoint, out receive))
            {
                receive = new ReceiveQueue();
                _ReceiveQueueMap.TryAdd(e.AcceptSocket.RemoteEndPoint, receive);
                InitializeDataMonitor(new KeyValuePair<EndPoint, ReceiveQueue>(e.AcceptSocket.RemoteEndPoint, receive));
            }
            // 触发数据到达事件
            OnDataComeIn(data, e);
            receive.Enqueue(data);

            if (!e.AcceptSocket.ReceiveAsync(e))
                BeginReceive(e);
        }

        protected virtual void AsynCompleted(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    BeginAccep(e);
                    break;
                case SocketAsyncOperation.Receive:
                    BeginReceive(e);
                    break;
            }
        }

        protected virtual void AsynCallBackSend(IAsyncResult result)
        {
            try
            {
                var sock = result.AsyncState as Socket;
                if (sock != null)
                {
                    sock.EndSend(result);
                }
            }
            catch (Exception e)
            {
                _Logger.Warn(string.Format("结束挂起的异步发送异常.{0}", e.Message));
            }
        }

        protected virtual void AsynCallBackDisconnect(IAsyncResult result)
        {
            var socket = result.AsyncState as Socket;
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.EndDisconnect(result);
            }
        }

        #endregion

        #region 数据处理

        protected virtual void InitializeDataMonitor(KeyValuePair<EndPoint, ReceiveQueue> pair)
        {
            var t = new Thread(ReceiveQueueMonitor);
            t.Start(pair);
        }

        /// <summary>核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected void ReceiveQueueMonitor(object obj)
        {
            var pair = (KeyValuePair<EndPoint, ReceiveQueue>)obj;
            _Logger.Info(string.Format("启动基于{0}的ReceiveQueue队列的监听。", pair.Key));
            var receiveQueue = pair.Value;
            var nodone = new byte[] {};
            while (true)
            {
                if (receiveQueue.Count > 0)
                {
                    var data = receiveQueue.Dequeue();
                    if (!UtilityCollection.IsNullOrEmpty(nodone))
                    {
                        // 当有半包数据时，进行接包操作
                        int srcLen = data.Length;
                        var list = new List<byte>(data.Length + nodone.Length);
                        list.AddRange(nodone);
                        list.AddRange(data);
                        data = list.ToArray();
                        _Logger.Trace(string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", nodone.Length, srcLen, data.Length));
                        nodone = new byte[] { };
                    }
                    int done;
                    DataProcessBase(pair.Key, data, out done);
                    if (data.Length > done)
                    {
                        // 暂存半包数据，留待下条队列数据接包使用
                        nodone = new byte[data.Length - done];
                        Buffer.BlockCopy(data, done, nodone, 0, nodone.Length);
                        _Logger.Trace(string.Format("半包数据暂存,数据长度:{0}", nodone.Length));
                    }
                }
                else
                {
                    receiveQueue.AutoResetEvent.WaitOne();
                }
            }
        }

        protected virtual void DataProcessBase(EndPoint endpoint, byte[] data, out int done)
        {
            string[] datagram = Decoder.Execute(data, out done);
            if (UtilityCollection.IsNullOrEmpty(datagram))
            {
                _Logger.Debug("协议消息无内容。");
                return;
            }

            foreach (string dg in datagram)
            {
                if (string.IsNullOrWhiteSpace(dg))
                    continue;
                try
                {
                    string command = CommandParser.GetCommand(dg);
                    IProtocol protocol = Protocols.Factory(FamilyType.ToString(), command);
                    _Logger.Trace(string.Format("Server.OnDataComeIn::命令字:{0},数据包:{1}", command, dg));
                    if (protocol != null)
                    {
                        protocol.Parse(dg);
                        // 触发数据基础解析后发生的数据到达事件
                        OnReceiveDataParsed(new ReceiveDataParsedEventArgs(protocol), endpoint);
                    }
                }
                catch (Exception ex)
                {
                    _Logger.WarnE(string.Format("协议字符串预处理异常。{0}", dg), ex);
                }
            }
        }

        #endregion

        #region 释放

        /// <summary>
        /// 用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AblySocketServer()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed || disposing)
            {
                try
                {
                    _MainSocket.Shutdown(SocketShutdown.Both);
                    _MainSocket.Close();
                    _IsClose = true;
                    for (int i = 0; i < _SocketAsynPool.Count; i++)
                    {
                        SocketAsyncEventArgs args = _SocketAsynPool.Pop();
                        _BufferContainer.FreeBuffer(args);
                    }
                }
                catch
                {
                }
                _IsDisposed = true;
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 数据异步接收到后事件,得到的数据是Byte数组,一般情况下没有必要使用该事件,使用 ReceiveDataParsedEvent 会比较方便。
        /// </summary>
        public event SocketAsyncDataComeInEventHandler DataComeInEvent;

        /// <summary>
        /// 服务器侦听到新客户端连接事件
        /// </summary>
        public event ListenToClientEventHandler ListenToClient;

        /// <summary>
        /// 连接出错或断开触发事件
        /// </summary>
        public event ConnectionBreakEventHandler ConnectionBreak;

        /// <summary>
        /// 接收到的数据解析完成后发生的事件
        /// </summary>
        public event ReceiveDataParsedEventHandler ReceiveDataParsedEvent;

        protected virtual void OnDataComeIn(byte[] data, SocketAsyncEventArgs e)
        {
            if (DataComeInEvent != null)
                DataComeInEvent(data, e.AcceptSocket.RemoteEndPoint);
        }

        protected virtual void OnListenToClient(SocketAsyncEventArgs e)
        {
            if (ListenToClient != null)
                ListenToClient(e);
        }

        protected virtual void OnConnectionBreak(ConnectionBreakEventArgs e)
        {
            if (ConnectionBreak != null)
                ConnectionBreak(e);
        }

        protected virtual void OnReceiveDataParsed(ReceiveDataParsedEventArgs e, EndPoint endPoint)
        {
            if (ReceiveDataParsedEvent != null)
                ReceiveDataParsedEvent(e, endPoint);
        }

        #endregion
    }
}