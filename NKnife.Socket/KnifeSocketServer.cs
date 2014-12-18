using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Logging;
using NKnife.IoC;
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
        private SocketAsyncEventArgsPool _SocketAsynPool;
        protected KnifeSocketSessionMap SessionMap = DI.Get<KnifeSocketSessionMap>();
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
                try
                {
                    socket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynEndSend, socket);
                    _logger.InfoFormat("ServerSend:{0}", data.ToHexString());
                }
                catch (SocketException e)
                {
                    _logger.WarnFormat("发送异常.{0},{1}. {2}", id, data.ToHexString(), e.Message);
                }
            }
        }

        public void SendAll(byte[] data)
        {
            if (SessionMap.Count == 0)
            {
                _logger.Warn(string.Format("SessionMap为空"));
                return;
            }
            foreach (KnifeSocketSession session in SessionMap.Values())
            {
                Socket socket = session.AcceptSocket;
                if (socket.Connected)
                {
                    try
                    {
                        socket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynEndSend, socket);
                        _logger.InfoFormat("ServerSend:{0}", data.ToHexString());
                    }
                    catch (SocketException e)
                    {
                        _logger.WarnFormat("发送异常.{0},{1}. {2}", session.Id, data.ToHexString(), e.Message);
                    }
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

                        //如果选用长连接服务时，将相应的连接置入一个Map以做处理
                        var iep = e.AcceptSocket.RemoteEndPoint as IPEndPoint;
                        if (iep != null)
                        {
                            if (_BufferContainer.SetBuffer(e))
                            {
                                e.UserToken = iep; //将iep在UserToken中存一份，远端中断连接时，receive异常时，能够从UserToken中恢复iep
                                if (!e.AcceptSocket.ReceiveAsync(e))
                                    ProcessReceive(e);
                            }

                            if (!SessionMap.ContainsKey(iep))
                            {
                                var session = DI.Get<KnifeSocketSession>();
                                session.Id = iep;
                                session.AcceptSocket = e.AcceptSocket;
                                SessionMap.Add(iep, session);

                                _logger.InfoFormat("Server: IP地址:{0}的连接已放入客户端池中。池中:{1}", iep, SessionMap.Count);

                                var handler = SessionBuilt;
                                if(handler !=null)
                                    handler.Invoke(this,new SessionEventArgs<byte[], EndPoint>(session));
                                
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
                        e.UserToken = null;
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
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success) //连接正常
            {
//                if (e.BytesTransferred > 0) //收到数据了
//                {
                    PrcoessReceivedData(e);
//                }
//                else //没收到数据，但连接正常，继续收
//                {
//                    if (e.AcceptSocket != null && e.AcceptSocket.Connected)
//                    {
//                        if (!e.AcceptSocket.ReceiveAsync(e))
//                            ProcessReceive(e);
//                    }
//                }
            }
            else //连接不正常
            {
                RemoveSession(e);
                e.AcceptSocket = null;
                e.UserToken = null;
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

        }

//        protected virtual void WirteProtocol(KnifeSocketSession session, StringProtocol protocol)
//        {
//            string replay = _Family.Generate(protocol);
//            byte[] data = _Codec.SocketEncoder.Execute(replay);
//            WirteBase(session, data);
//            _logger.DebugFormat("ServerSend:{0}", replay);
//        }

        #endregion

    }
}