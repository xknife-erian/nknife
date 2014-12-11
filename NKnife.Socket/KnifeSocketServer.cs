using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    public class KnifeSocketServer : TunnelBase, IKnifeSocketServer
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region 成员变量

        private readonly AutoResetEvent _MainAutoReset = new AutoResetEvent(false);
        private BufferContainer _BufferContainer;
        private bool _IsClose = true;
        private Socket _MainListenSocket;
        private SocketAsyncEventArgsPool _SocketAsynPool;
        protected KnifeSocketSessionMap SessionMap = DI.Get<KnifeSocketSessionMap>();
        protected KnifeSocketServerConfig _Config = DI.Get<KnifeSocketServerConfig>();

        #endregion

        #region ISocketServerKnife 接口实现

        public override bool Start()
        {
            try
            {
                Initialize();
                _MainAutoReset.Set();
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("SocketServer打开异常。{0}", e.Message), e);
                return false;
            }
        }

        public override bool ReStart()
        {
            if (Stop())
            {
                return Start();
            }
            return false;
        }

        public override bool Stop()
        {
            try
            {
                _MainAutoReset.Reset();
                _IsClose = true;
                foreach (KeyValuePair<EndPoint, KnifeSocketSession> pair in SessionMap)
                {
                    Socket client = pair.Value.Connector;
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                }
                SessionMap.Clear();
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
                _MainListenSocket.Close();
                _logger.Info("SocketServer关闭。");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("SocketServer关闭异常。{0}", e.Message), e);
                return false;
            }
        }

        public override void AddFilters(params KnifeSocketFilter[] filters)
        {
            base.AddFilters(filters);
            foreach (var filter in filters)
            {
                var serverFilter = (KnifeSocketServerFilter)filter;
                serverFilter.Bind(() => SessionMap);
            }
        }

        protected virtual void Initialize()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");
            _IsClose = false;

            var ipEndPoint = new IPEndPoint(_IpAddress, _Port);
            _MainListenSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _MainListenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            _MainListenSocket.Bind(ipEndPoint);
            _MainListenSocket.ReceiveBufferSize = Config.ReceiveBufferSize;
            _MainListenSocket.SendBufferSize = Config.SendBufferSize;
            _MainListenSocket.SendTimeout = Config.SendTimeout;
            _MainListenSocket.ReceiveTimeout = Config.ReceiveTimeout;

            //挂起连接队列的最大长度。
            _MainListenSocket.Listen(Config.MaxConnectCount);

            _BufferContainer = new BufferContainer(Config.MaxConnectCount*Config.MaxBufferSize, Config.MaxBufferSize);
            _BufferContainer.Initialize();

            //核心连接池的预创建
            _SocketAsynPool = new SocketAsyncEventArgsPool(Config.MaxConnectCount);

            for (int i = 0; i < Config.MaxConnectCount; i++)
            {
                var socketAsyn = new SocketAsyncEventArgs();
                socketAsyn.Completed += AsynCompleted;
                _SocketAsynPool.Push(socketAsyn);
            }

            Accept();

            _logger.InfoFormat("== {0} 已启动。端口:{1}", GetType().Name, _Port);
            _logger.InfoFormat("发送缓冲区:大小:{0}，超时:{1}", _MainListenSocket.SendBufferSize, _MainListenSocket.SendTimeout);
            _logger.InfoFormat("接收缓冲区:大小:{0}，超时:{1}", _MainListenSocket.ReceiveBufferSize, _MainListenSocket.ReceiveTimeout);
            _logger.InfoFormat("SocketAsyncEventArgs 连接池已创建。大小:{0}", Config.MaxConnectCount);
        }

        protected override void OnBound(params KnifeSocketProtocolHandler[] handlers)
        {
            foreach (KnifeSocketProtocolHandler handler in handlers)
            {
                var serverHandler = (KnifeSocketServerProtocolHandler) handler;
                serverHandler.Bind(WirteProtocol);
                serverHandler.Bind(WirteBase);
                serverHandler.SessionMap = SessionMap;
                _logger.InfoFormat("{0}绑定成功。", serverHandler.GetType().Name);
            }
        }

        #region 释放( IDisposable )

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public override KnifeSocketConfig Config
        {
            get { return _Config; }
            set { _Config = (KnifeSocketServerConfig) value; }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void SetFilterChain()
        {
            _FilterChain = DI.Get<ITunnelFilterChain<EndPoint, Socket>>("Server");
        }

        ~KnifeSocketServer()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed || disposing)
            {
                try
                {
                    if (_MainListenSocket != null)
                    {
                        _MainListenSocket.Close();
                        _IsClose = true;
                        for (int i = 0; i < _SocketAsynPool.Count; i++)
                        {
                            SocketAsyncEventArgs args = _SocketAsynPool.Pop();
                            _BufferContainer.FreeBuffer(args);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Fail(string.Format("Main Socket释放时发生异常。{0}", e.Message));
                }
                _IsDisposed = true;
            }
        }

        #endregion

        #endregion

        #region 监听

        protected virtual void AsynCompleted(object sender, SocketAsyncEventArgs e) // SocketAsyncEventArgs的Completed事件
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    ProcessAccept(e);
                    break;
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
            }
        }

        protected virtual void Accept() // 启动
        {
            if (_IsClose)
            {
                _logger.Info("Server: Socket已关闭。");
                return;
            }
            if (_SocketAsynPool.Count > 0)
            {
                SocketAsyncEventArgs sockAsyn = _SocketAsynPool.Pop();
                if (!_MainListenSocket.AcceptAsync(sockAsyn))
                    ProcessAccept(sockAsyn);
            }
            else
            {
                _logger.Warn("Server: Socket连接池中已达到最大连接数。");
            }
        }

        protected virtual void ProcessAccept(SocketAsyncEventArgs e)
        {
            try
            {
                switch (e.SocketError)
                {
                    case SocketError.Success:
                    {
                        WaitHandle.WaitAll(new WaitHandle[] {_MainAutoReset});
                        _MainAutoReset.Set();
                        if (_BufferContainer.SetBuffer(e))
                        {
                            if (!e.AcceptSocket.ReceiveAsync(e))
                                ProcessReceive(e);
                        }
                        //如果选用长连接服务时，将相应的连接置入一个Map以做处理
                        var iep = e.AcceptSocket.RemoteEndPoint as IPEndPoint;
                        if (iep != null)
                        {
                            string ip = iep.ToString();
                            if (!SessionMap.ContainsKey(iep))
                            {
                                var session = DI.Get<KnifeSocketSession>();
                                session.Source = iep;
                                session.Connector = e.AcceptSocket;
                                SessionMap.Add(iep, session);
                                _logger.InfoFormat("Server: IP地址:{0}的连接已放入客户端池中。池中:{1}", ip, SessionMap.Count);
                                foreach (var filter in _FilterChain)
                                {
                                    var serverFilter = (KnifeSocketServerFilter) filter;
                                    serverFilter.OnClientCome(new SocketSessionEventArgs(session));
                                }
                            }
                        }
                        else
                        {
                            _logger.Warn("e.AcceptSocket.RemoteEndPoint 不是正确的 IPEndPoint");
                        }
                        break;
                    }
                    case SocketError.OperationAborted:
                    {
                        _logger.Info("服务端: 停止服务.");
                        break;
                    }
                    default:
                    {
                        e.AcceptSocket = null;
                        _SocketAsynPool.Push(e);
                        _logger.WarnFormat("服务端:未处理状态,{0}", e.SocketError);
                        break;
                    }
                }
            }
            finally
            {
                Accept();
            }
        }

        protected virtual void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (_IsClose)
            {
                if (e.AcceptSocket != null)
                    e.AcceptSocket.Close();
                return;
            }
            if (e.SocketError == SocketError.Success) //连接正常
            {
                if (e.BytesTransferred > 0) //收到数据了
                {
                    PrcoessReceivedData(e);
                }
                else //没收到数据，但连接正常，继续收
                {
                    if (e.AcceptSocket != null && e.AcceptSocket.Connected)
                    {
                        if (!e.AcceptSocket.ReceiveAsync(e))
                            ProcessReceive(e);
                    }
                }
            }
            else //连接不正常
            {
                RemoveSession(e);
                e.AcceptSocket = null;
                _BufferContainer.FreeBuffer(e);
                _SocketAsynPool.Push(e);
                if (_SocketAsynPool.Count == 1)
                {
                    Accept();
                }
            }
        }

        protected virtual void RemoveSession(SocketAsyncEventArgs e)
        {
            _logger.TraceFormat("当RemoveSession时，Socket状态：{0}", e.SocketError);
            if (!e.AcceptSocket.Connected)
            {
                return;
            }
            EndPoint iep = e.AcceptSocket.RemoteEndPoint;
            _logger.InfoFormat("Server: >> 客户端:{0}, 连接中断.", iep);

            foreach (var filter in _FilterChain)
            {
                var serverFilter = (KnifeSocketServerFilter)filter;
                serverFilter.OnClientBroken(new ConnectionBrokenEventArgs(iep, BrokenCause.Passive));
            }

            if (iep != null)
            {
                string ip = iep.ToString();
                if (SessionMap.ContainsKey(iep))
                {
                    SessionMap.Remove(iep);
                    _logger.InfoFormat("服务端:IP地址:{0}的连接被移出客户端池。{1}", ip, SessionMap.Count);
                }
                else
                {
                    _logger.WarnFormat("服务端:打算清理IP地址{0}时，该地址未在池中。{1}", ip, SessionMap.Count);
                }
            }
            else
            {
                _logger.WarnFormat("e.AcceptSocket.RemoteEndPoint不是正确的IPEndPoint：null");
            }
        }

        /// <summary>
        /// 处理接收到的数据，此时e.BytesTransferred > 0
        /// </summary>
        /// <param name="e"></param>
        protected virtual void PrcoessReceivedData(SocketAsyncEventArgs e)
        {
            var data = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);

            foreach (var filter in _FilterChain)
            {
                var serverFilter = (KnifeSocketServerFilter) filter;
                EndPoint endPoint = e.AcceptSocket.RemoteEndPoint;
                serverFilter.OnDataFetched(new SocketDataFetchedEventArgs(endPoint, data)); // 触发数据到达事件

                KnifeSocketSession session = SessionMap[endPoint];
                serverFilter.PrcoessReceiveData(session, ref data); // 调用filter对数据进行处理
                if (!serverFilter.ContinueNextFilter)
                    break;
            }

            if (!e.AcceptSocket.ReceiveAsync(e))
                ProcessReceive(e);
        }

        protected virtual void AsynEndSend(IAsyncResult result)
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
                _logger.WarnFormat("结束挂起的异步发送异常.{0}", e.Message);
            }
        }

        #endregion

        #region 发送消息

        protected virtual void WirteBase(KnifeSocketSession session, byte[] data)
        {
            Socket socket = session.Connector;
            if (socket.Connected)
            {
                try
                {
                    socket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynEndSend, socket);
                    _logger.InfoFormat("ServerSend:{0}", data.ToHexString());
                }
                catch (SocketException e)
                {
                    _logger.WarnFormat("发送异常.{0},{1}. {2}", session.Source, data.ToHexString(), e.Message);
                }
            }
        }

        protected virtual void WirteProtocol(KnifeSocketSession session, StringProtocol protocol)
        {
            string replay = _Family.Generate(protocol);
            byte[] data = _Codec.SocketEncoder.Execute(replay);
            WirteBase(session, data);
            _logger.DebugFormat("ServerSend:{0}", replay);
        }

        #endregion
    }
}