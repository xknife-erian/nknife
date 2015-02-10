using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Logging;
using NKnife.IoC;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    public class KnifeSocketServer : IDisposable, IKnifeSocketServer
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region 成员变量

        private readonly AutoResetEvent _MainAutoReset = new AutoResetEvent(false);
        private BufferContainer _BufferContainer;
        private bool _IsClose = true;
        private Socket _MainListenSocket;
        private SocketAsyncEventArgsPool _AcceptSocketAsynPool;
        private SocketAsyncEventArgsPool _SendRecvSocketAsynPool;
        protected KnifeSocketSessionMap SessionMap = new KnifeSocketSessionMap();
        private KnifeSocketServerConfig _Config = DI.Get<KnifeSocketServerConfig>();

        private IPAddress _IpAddress;
        private int _Port;
        #endregion

        #region IKnifeSocketServer接口
        public KnifeSocketConfig Config
        {
            get { return _Config; }
            set { _Config = (KnifeSocketServerConfig)value; }
        }
        public virtual void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
        }
        #endregion

        #region IDataConnector接口
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> SessionBuilt;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> SessionBroken;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> DataReceived;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> DataSent;

        public bool Start()
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

        public bool Stop()
        {
            try
            {
                _MainAutoReset.Reset();
                _IsClose = true;

                foreach (var session in SessionMap.Values())
                {
                    if (null == session.AcceptSocket || !session.AcceptSocket.Connected)
                    {
                        continue;
                    }
                    session.AcceptSocket.Shutdown(SocketShutdown.Both);
                    session.AcceptSocket.Close();
                }
                SessionMap.Clear();
                foreach (SocketAsyncEventArgs async in _AcceptSocketAsynPool)
                {
                    if (null == async.AcceptSocket || !async.AcceptSocket.Connected)
                    {
                        continue;
                    }
                    async.AcceptSocket.Shutdown(SocketShutdown.Both);
                    async.AcceptSocket.Close();
                }
                _AcceptSocketAsynPool.Clear();

                foreach (SocketAsyncEventArgs async in _SendRecvSocketAsynPool)
                {
                    if (null == async.AcceptSocket || !async.AcceptSocket.Connected)
                    {
                        continue;
                    }
                    async.AcceptSocket.Shutdown(SocketShutdown.Both);
                    async.AcceptSocket.Close();
                }
                _SendRecvSocketAsynPool.Clear();

                //_MainListenSocket.Close();
                _logger.Info("SocketServer关闭。");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("SocketServer关闭异常。{0}", e.Message), e);
                return false;
            }
        }
        #endregion

        #region ISessionProvider
        public void Send(EndPoint id, byte[] data)
        {
            if (!SessionMap.ContainsKey(id))
            {
                _logger.Warn(string.Format("session:{0}不存在",id));
                return;
            }
            var session = SessionMap[id];
            Socket socket = session.AcceptSocket;
            if (socket.Connected)
            {
                ProcessSendData(id,socket, data);
            }
        }

        public void SendAll(byte[] data)
        {
            foreach (KnifeSocketSession session in SessionMap.Values())
            {
                Socket socket = session.AcceptSocket;
                if (socket.Connected)
                {
                    ProcessSendData(session.Id,socket,data);
                }
            }
        }

        public void KillSession(EndPoint id)
        {
            if (SessionMap.ContainsKey(id))
            {
                var session = SessionMap[id];
                if (null == session.AcceptSocket || !session.AcceptSocket.Connected)
                {
                    //session连接已经中断了
                }
                else
                {
                    session.AcceptSocket.Shutdown(SocketShutdown.Both);
                    session.AcceptSocket.Close();
                }
                SessionMap.Remove(id);
            }
            var handler = SessionBroken;
            var replySession = DI.Get<KnifeSocketSession>();
            replySession.Id = id;
            if(handler !=null)
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(replySession));
        }

        public bool SessionExist(EndPoint id)
        {
            return SessionMap.ContainsKey(id);
        }
        #endregion

        #region 初始化
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
            _AcceptSocketAsynPool = new SocketAsyncEventArgsPool(Config.MaxConnectCount);

            for (int i = 0; i < Config.MaxConnectCount; i++)
            {
                var socketAsyn = new SocketAsyncEventArgs();
                socketAsyn.Completed += AsynCompleted;
                _AcceptSocketAsynPool.Push(socketAsyn);
            }

            _SendRecvSocketAsynPool = new SocketAsyncEventArgsPool(Config.MaxConnectCount);

            for (int i = 0; i < Config.MaxConnectCount; i++)
            {
                var socketAsyn = new SocketAsyncEventArgs();
                socketAsyn.Completed += AsynCompleted;
                _SendRecvSocketAsynPool.Push(socketAsyn);
            }

            StartAccept();

            _logger.InfoFormat("== {0} 已启动。端口:{1}", GetType().Name, _Port);
            _logger.InfoFormat("发送缓冲区:大小:{0}，超时:{1}", _MainListenSocket.SendBufferSize, _MainListenSocket.SendTimeout);
            _logger.InfoFormat("接收缓冲区:大小:{0}，超时:{1}", _MainListenSocket.ReceiveBufferSize, _MainListenSocket.ReceiveTimeout);
            _logger.InfoFormat("SocketAsyncEventArgs 连接池已创建。大小:{0}", Config.MaxConnectCount);
        }

        #endregion

        #region 释放( IDisposable )

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
                        for (int i = 0; i < _AcceptSocketAsynPool.Count; i++)
                        {
                            SocketAsyncEventArgs args = _AcceptSocketAsynPool.Pop();
                            _BufferContainer.FreeBuffer(args);
                        }
                        for (int i = 0; i < _SendRecvSocketAsynPool.Count; i++)
                        {
                            SocketAsyncEventArgs args = _SendRecvSocketAsynPool.Pop();
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
//                case SocketAsyncOperation.Send:
//                    ProcessSend(e);
//                    break;
            }
        }

        protected virtual void StartAccept() // 启动
        {
            if (_IsClose)
            {
                _logger.Info("Server: Socket已关闭。");
                return;
            }
            if (_AcceptSocketAsynPool.Count > 0)
            {
                SocketAsyncEventArgs sockAsyn = _AcceptSocketAsynPool.Pop();
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
            if (e.SocketError != SocketError.Success)
            {
                StartAccept();

                e.AcceptSocket.Close();
                //Put the SAEA back in the pool.
                _AcceptSocketAsynPool.Push(e);

                return;
            }
            
            StartAccept();

            WaitHandle.WaitAll(new WaitHandle[] {_MainAutoReset});
            _MainAutoReset.Set();

            

            //如果选用长连接服务时，将相应的连接置入一个Map以做处理
            var iep = e.AcceptSocket.RemoteEndPoint;
            if (iep != null)
            {
                if (SessionMap.ContainsKey(iep))
                {
                    SessionMap.Remove(iep);
                }

                var session = DI.Get<KnifeSocketSession>();
                session.Id = iep;
                session.AcceptSocket = e.AcceptSocket;
                SessionMap.Add(iep, session);

                _logger.InfoFormat("Server: IP地址:{0}的连接已放入客户端池中。池中:{1}", iep, SessionMap.Count);

                var handler = SessionBuilt;
                if (handler != null)
                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(session));

                SocketAsyncEventArgs receiveSendEventArgs = _SendRecvSocketAsynPool.Pop();
                receiveSendEventArgs.AcceptSocket = e.AcceptSocket;

                e.AcceptSocket = null;
                _AcceptSocketAsynPool.Push(e);   

                StartReceive(receiveSendEventArgs);
            }
            else
            {
                _logger.Warn("e.AcceptSocket.RemoteEndPoint 不是正确的 IPEndPoint");
            }
        }

        protected virtual void StartReceive(SocketAsyncEventArgs receiveSendEventArgs)
        {
            if (_BufferContainer.SetBuffer(receiveSendEventArgs))
            {
                receiveSendEventArgs.UserToken = receiveSendEventArgs.AcceptSocket.RemoteEndPoint; //将iep在UserToken中存一份，远端中断连接时，receive异常时，能够从UserToken中恢复iep
                if (!receiveSendEventArgs.AcceptSocket.ReceiveAsync(receiveSendEventArgs))
                    ProcessReceive(receiveSendEventArgs);
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
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success) //TODO:连接正常
            {
                if (e.BytesTransferred > 0) //收到数据了
                {
                    PrcoessReceivedData(e);
                }
                //处理完成，继续接收
                //Thread.Sleep(10);
                if (e.AcceptSocket != null && e.AcceptSocket.Connected)
                {
                    bool willRaiseEvent = e.AcceptSocket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                        ProcessReceive(e);
                }
            }
            else //连接不正常
            {
                ProcessConnectionBrokenActive(e);
            }
        }

        protected virtual void ProcessConnectionBrokenActive(SocketAsyncEventArgs e)
        {
            RemoveSession(e);
            try
            {
                e.AcceptSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                                
            }
            e.AcceptSocket.Close();
            e.UserToken = null;
            _BufferContainer.FreeBuffer(e);
            _SendRecvSocketAsynPool.Push(e);
        }

        protected virtual void RemoveSession(SocketAsyncEventArgs e)
        {
            _logger.TraceFormat("当RemoveSession时，Socket状态：{0}", e.SocketError);
            EndPoint iep = null;
            try
            {
                iep = e.AcceptSocket.RemoteEndPoint;
                _logger.InfoFormat("Server: >> 客户端:{0}, 连接中断.", iep);
            }
            catch
            {
                iep = (EndPoint) e.UserToken;
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

            var session = DI.Get<KnifeSocketSession>();
            session.Id = iep;
            var handler = SessionBroken;
            if(handler !=null)
                handler.Invoke(this,new SessionEventArgs<byte[], EndPoint>(session));
        }

        /// <summary>
        /// 处理接收到的数据，此时e.BytesTransferred > 0
        /// </summary>
        /// <param name="e"></param>
        protected virtual void PrcoessReceivedData(SocketAsyncEventArgs e)
        {
            var data = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);

            _logger.Fatal("1>>>>>"+data.ToHexString());

            var handler = DataReceived;
            if (handler != null)
            {
                EndPoint endPoint = e.AcceptSocket.RemoteEndPoint;
                if (SessionMap.ContainsKey(endPoint))
                {
                    KnifeSocketSession session = SessionMap[endPoint];
                    session.Data = data;
                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(session));
                }
                else
                {
                    _logger.Fatal("2>>>>>" + data.ToHexString());
                }
            }
        }

//        protected virtual void ProcessSend(SocketAsyncEventArgs e)
//        {
//            var id = e.RemoteEndPoint;
//            var data = e.Buffer;
//            if (e.SocketError == SocketError.Success) //连接正常
//            {
//                //发送成功
//                var handler = DataSent;
//                if (handler != null)
//                {
//                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
//                    {
//                        Data = data,
//                        Id = id
//                    }));
//                }
//            }
//            else
//            {
//                _logger.WarnFormat("发送失败.{0},{1}.", id, data.ToHexString());
//            }
//        }

        protected virtual void ProcessSendData(EndPoint id, Socket socket, byte[] data)
        {
            try
            {
                var socketSession = new KnifeSocketSession
                {
                    AcceptSocket = socket,
                    Data = data,
                    Id = id,
                };
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynEndSend, socketSession);
//                var e = new SocketAsyncEventArgs{RemoteEndPoint = id};
//                e.Completed += AsynCompleted;
//                e.SetBuffer(data, 0, data.Length);
//
//                bool isSuceess = socket.SendAsync(e);
//                if (!isSuceess)
//                        ProcessSend(e);
            }
            catch (SocketException e)
            {
                _logger.WarnFormat("发送异常.{0},{1}. {2}", id, data.ToHexString(), e.Message);
            }


        }

        protected virtual void AsynEndSend(IAsyncResult result)
        {
            try
            {
                var session = result.AsyncState as KnifeSocketSession;
                if (session!=null)
                {
                    session.AcceptSocket.EndSend(result);
                    var data = session.Data;
                    var id = session.Id;
                    _logger.Debug(string.Format("ServerSend:{0}", data.ToHexString()));

                    var handler = DataSent;
                    if (handler != null)
                    {
                        handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                        {
                            Data = data,
                            Id = id
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.WarnFormat("结束挂起的异步发送异常.{0}", e.Message);
            }
        }

        #endregion
    }
}