using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife
{
    public class TcpServerKnife : ISocketServerKnife
    {
        #region 成员变量

        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        private readonly WaitHandle[] _AutoReset;

        private IPAddress _IpAddress;
        private int _Port;

        /// <summary>
        ///     数据包管理
        /// </summary>
        private BufferContainer _BufferContainer;

        private bool _IsClose = true;

        /// <summary>
        ///     Socket对象
        /// </summary>
        private Socket _MainSocket;

        /// <summary>
        ///     Socket异步对象池
        /// </summary>
        private SocketAsyncEventArgsPool _SocketAsynPool;

        #endregion

        #region ISocketServerKnife 接口实现

        protected IProtocolFamily _Family;
        protected IProtocolHandler _Handler;
        protected ISocketPlan _SocketPlan;
        [Inject] protected ISocketSessionMap _SessionMap;
        [Inject] protected ISocketPolicy _Policy;

        public void Bind(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
        }

        public void Bind(ProtocolHandlerBase handler)
        {
            _Handler = handler;
            _Handler.Bind(WirteBase);
            _Handler.Bind(WirteProtocol);
        }

        [Inject]
        public ISocketServerConfig Config { get; private set; }

        public void AddFilter(KeepAliveFilter filter)
        {
            filter.Bind(GetFamily,GetHandle,GetSessionMap);
            _Policy.AddLast(filter);
        }

        private ISocketSessionMap GetSessionMap()
        {
            return _SessionMap;
        }

        private IProtocolHandler GetHandle()
        {
            return _Handler;
        }

        private IProtocolFamily GetFamily()
        {
            return _Family;
        }

        public virtual void Attach(IProtocolFamily protocolFamily)
        {
            _Family = protocolFamily;
        }

        public virtual void Attach(ISocketPlan socketPlan)
        {
            _SocketPlan = socketPlan;
        }

        public virtual bool Start()
        {
            try
            {
                Initialize();
                ((AutoResetEvent)_AutoReset[0]).Set();
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("SocketServer打开异常。{0}", e.Message), e);
                return false;
            }
        }

        public virtual bool ReStart()
        {
            if (Stop())
            {
                return Start();
            }
            return false;
        }

        public virtual bool Stop()
        {
            try
            {
                ((AutoResetEvent)_AutoReset[0]).Reset();
                _IsClose = true;
                _MainSocket.Close();
                foreach (ISocketSession session in _SessionMap.Values)
                {
                    var client = session.Socket;
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
                _logger.Error(string.Format("SocketServer关闭异常。{0}", e.Message), e);
                return false;
            }
        }

        #endregion

        #region 构造函数，及面向CodeFirst方式的设置方法

        public TcpServerKnife()
        {
            _AutoReset = new WaitHandle[1];
            _AutoReset[0] = new AutoResetEvent(false);
        }

        #endregion

        public SocketMode Mode { get; set; }

        #region 公共方法

        protected virtual void Initialize()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");

            var ipEndPoint = new IPEndPoint(_IpAddress, _Port);
            _MainSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            _MainSocket.Bind(ipEndPoint);
            _MainSocket.ReceiveBufferSize = Config.MaxBufferSize;
            _MainSocket.SendBufferSize = Config.MaxBufferSize;

            //挂起连接队列的最大长度。
            _MainSocket.Listen(64);

            Config.SendTimeout = 1000;
            Config.ReceiveTimeout = 1000;

            _BufferContainer = new BufferContainer(Config.MaxConnectCount * Config.MaxBufferSize, Config.MaxBufferSize);
            _BufferContainer.Initialize();

            #region 核心连接池的预创建

            _SocketAsynPool = new SocketAsyncEventArgsPool(Config.MaxConnectCount);

            for (int i = 0; i < Config.MaxConnectCount; i++)
            {
                var socketAsyn = new SocketAsyncEventArgs();
                socketAsyn.Completed += AsynCompleted;
                _SocketAsynPool.Push(socketAsyn);
            }

            #endregion

            _IsClose = false;
            Accept();

            _logger.Info(string.Format("== {0} 已启动。端口:{1}", GetType().Name, _Port));
            _logger.Info(string.Format("发送缓冲区:大小:{0}，超时:{1}", _MainSocket.SendBufferSize, _MainSocket.SendTimeout));
            _logger.Info(string.Format("接收缓冲区:大小:{0}，超时:{1}", _MainSocket.ReceiveBufferSize, _MainSocket.ReceiveTimeout));
            _logger.Info(string.Format("SocketAsyncEventArgs 连接池已创建。大小:{0}", Config.MaxConnectCount));
        }

        protected virtual void Accept()
        {
            if (_IsClose)
            {
                _logger.Warn("Server: Socket已关闭。");
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
                _logger.Warn("Server: Socket连接池中已达到最大连接数。");
            }
        }

        protected virtual void BeginAccep(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError == SocketError.Success)
                {
                    WaitHandle.WaitAll(_AutoReset);
                    ((AutoResetEvent)_AutoReset[0]).Set();
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
                        if (!_SessionMap.ContainsKey(iep))
                        {
                            var session = DI.Get<ISocketSession>();
                            session.Point = iep;
                            session.Socket = e.AcceptSocket;
                            _SessionMap.Add(iep, session);
                            _logger.Info(string.Format("Server: IP地址:{0}的连接已放入客户端池中。{1}", ip, _SessionMap.Count));
                            OnListenToClient(e);
                        }
                    }
                    else
                    {
                        _logger.Warn("e.AcceptSocket.RemoteEndPoint 不是正确的 IPEndPoint");
                    }
                }
                else
                {
                    e.AcceptSocket = null;
                    _SocketAsynPool.Push(e);
                    _logger.Error("Server: Don't Accep.");
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
                _logger.Info(message);
                OnConnectionBreak(new ConnectionBreakEventArgs(message));
                var iep = e.AcceptSocket.RemoteEndPoint as IPEndPoint;
                if (iep != null)
                {
                    string ip = iep.ToString();
                    if (_SessionMap.ContainsKey(iep))
                    {
                        _SessionMap.Remove(iep);
                        _logger.Info(string.Format("Server: IP地址:{0}的连接被移出客户端池。{1}", ip, _SessionMap.Count));
                    }
                }
                else
                {
                    _logger.Warn("e.AcceptSocket.RemoteEndPoint 不是正确的 IPEndPoint");
                }

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

            foreach (FilterBase filter in _Policy)
            {
                filter.PrcoessReceiveData(e.AcceptSocket, data);
            }

            // 触发数据到达事件
            OnDataComeIn(data, e);

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
                _logger.Warn(string.Format("结束挂起的异步发送异常.{0}", e.Message));
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

        private void WirteBase(ISocketSession session, byte[] data)
        {
            var socket = session.Socket;
            try
            {
                switch (Mode)
                {
                    case SocketMode.AsyncKeepAlive:
                        socket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynCallBackSend, socket);
                        break;
                    case SocketMode.Talk:
                        SocketError errCode;
                        socket.Send(data, 0, data.Length, SocketFlags.None, out errCode);
                        break;
                }
                _logger.Trace(string.Format("Server.Send:{0}", data));
            }
            catch
            {
                throw new SocketClientDisOpenedException(string.Format("TRY-DisOpened,远端无法连接."));
            }
        }

        private void WirteProtocol(ISocketSession session, IProtocol protocol)
        {
            var replay = protocol.Protocol();
            var data = _Family.Encoder.Execute(replay);
            WirteBase(session, data);
        }

        #endregion

        #region 释放

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TcpServerKnife()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed || disposing)
            {
                try
                {
                    if (_MainSocket != null)
                    {
                        _MainSocket.Close();
                        _IsClose = true;
                        for (int i = 0; i < _SocketAsynPool.Count; i++)
                        {
                            SocketAsyncEventArgs args = _SocketAsynPool.Pop();
                            _BufferContainer.FreeBuffer(args);
                        }
                    }
                }
                catch(Exception e)
                {
                    Debug.Fail(string.Format("Main Socket释放时发生异常。{0}", e.Message));
                }
                _IsDisposed = true;
            }
        }

        /// <summary>
        ///     断开此SOCKET
        /// </summary>
        /// <param name="socket"></param>
        public void Disconnect(Socket socket)
        {
            if (_MainSocket != null && _MainSocket.Connected)
                socket.BeginDisconnect(false, AsynCallBackDisconnect, socket);
        }

        #endregion

        #region 事件

        /// <summary>
        ///     数据异步接收到后事件,得到的数据是Byte数组,一般情况下没有必要使用该事件,使用 ReceiveDataParsedEvent 会比较方便。
        /// </summary>
        public event SocketAsyncDataComeInEventHandler DataComeInEvent;

        /// <summary>
        ///     服务器侦听到新客户端连接事件
        /// </summary>
        public event ListenToClientEventHandler ListenToClient;

        /// <summary>
        ///     连接出错或断开触发事件
        /// </summary>
        public event ConnectionBreakEventHandler ConnectionBreak;

        /// <summary>
        ///     接收到的数据解析完成后发生的事件
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
