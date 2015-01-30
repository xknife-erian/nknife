using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Logging;
using NKnife.IoC;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    public class KnifeSocketClient : IDisposable, IKnifeSocketClient
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region 成员变量
        protected IPAddress _IpAddress;
        protected int _Port;

        /// <summary>
        ///  异步连接的控制，连接事件完成后释放信号，通过IsConnected判断是否连接成功
        /// </summary>
        private readonly ManualResetEvent _SynConnectWaitEventReset = new ManualResetEvent(false);
        /// <summary>
        /// 重连线程中的阻塞超时控制
        /// </summary>
        private readonly ManualResetEvent _ReconnectResetEvent = new ManualResetEvent(false);

        protected KnifeSocketClientConfig _Config = DI.Get<KnifeSocketClientConfig>();
        protected EndPoint EndPoint;

        private bool _IsConnecting; //true 正在进行连接, false表示连接动作完成
        protected bool IsConnected; //连接状态，true表示已经连接上了
        private bool _ReconnectFlag;
        private bool _NeedReconnected;//是否重连

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        //protected Socket _Socket;
        protected KnifeSocketSession SocketSession;

        private static object _lockObj = new object();

        public KnifeSocketClient()
        {

        }

        #endregion 成员变量

        #region IKnifeSocketClient接口

        public KnifeSocketConfig Config
        {
            get { return _Config; }
            set { _Config = (KnifeSocketClientConfig) value; }
        }

        public void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
            EndPoint = new IPEndPoint(ipAddress, port);
        }
        #endregion

        #region IDataConnector接口
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> SessionBuilt;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> SessionBroken;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> DataReceived;
        public event EventHandler<SessionEventArgs<byte[], EndPoint>> DataSent;

        public bool Start()
        {
            Initialize();
            AsyncConnect(_IpAddress,_Port);

            return true;
        }

        public bool Stop()
        {
            StopReconnect();

            _ReconnectFlag = false;
            _ReconnectResetEvent.Set();

            var handler = SessionBroken;
            if(handler !=null)
                handler.Invoke(this,new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                {
                    Id = EndPoint,
                }));

            try
            {
                SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                SocketSession.AcceptSocket.Disconnect(true);
                SocketSession.AcceptSocket.Close();
                return true;
            }
            catch (Exception e)
            {
                _logger.Debug("Socket客户端Shutdown异常。", e);
                return false;
            }
        }
        #endregion

        #region ISessionProvider
        public void Send(EndPoint id, byte[] data)
        {
            ProcessSendData(SocketSession.Id, SocketSession.AcceptSocket, data);
        }

        public void SendAll(byte[] data)
        {
            ProcessSendData(SocketSession.Id, SocketSession.AcceptSocket, data);
        }

        public void KillSession(EndPoint id)
        {
            ProcessConnectionBrokenActive();
        }
        #endregion

        #region 初始化
        protected void Initialize()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");

            var ipPoint = new IPEndPoint(_IpAddress, _Port);
            SocketSession = DI.Get<KnifeSocketSession>();
            SocketSession.Id = ipPoint;
//            _SocketAsyncEventArgs = new SocketAsyncEventArgs{RemoteEndPoint = ipPoint};
//            _SocketAsyncEventArgs.Completed += AsynCompleted;

            _ReconnectFlag = true;
            var reconnectedThread = new Thread(ReconnectedLoop);
            reconnectedThread.Start();
        }

        private void ReconnectedLoop()
        {
            while (_ReconnectFlag)
            {
                if (_Config.EnableReconnect && !IsConnected) //重连检查，仅启用了自动重连的时候做
                {
                    _NeedReconnected = true;
                }

                if (_NeedReconnected) //需要重连
                {
                    if (!IsConnected && !_IsConnecting) //未连接
                    {
                        _logger.Debug("Client发起重连尝试");
                        AsyncConnect(_IpAddress, _Port);
                        //阻塞
                        _ReconnectResetEvent.Reset();
                        _ReconnectResetEvent.WaitOne(_Config.ReconnectInterval);

                    }
                    else //已连接，则不需要重连了
                    {
                        _NeedReconnected = false;
                    }
                }
                else
                {
                    //阻塞
                    _ReconnectResetEvent.Reset();
                    _ReconnectResetEvent.WaitOne(_Config.ReconnectInterval);
                }

            }
            _logger.Debug("SocketClient退出重连循环");
        }

        /// <summary>
        ///     当非主动断开连接时，启动断线重连
        /// </summary>
//        protected virtual void OnFilterConnectionBroken(object sender, ConnectionBrokenEventArgs e)
//        {
//            if (e.BrokenCause != BrokenCause.Aggressive) //当非主动断开时，启动断线重连
//            {
//                if (_Config.EnableReconnect)
//                    StartReconnect();
//            }
//        }

        protected virtual void StartReconnect()
        {
            _logger.Info(string.Format("Client启用自动重连。"));
            _NeedReconnected = true;
        }

        protected virtual void StopReconnect()
        {
            _NeedReconnected = false;
        }

        #endregion

        #region IDisposable

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public KnifeSocketClient(bool reconnectFlag, bool isConnecting)
        {
            _ReconnectFlag = reconnectFlag;
            _IsConnecting = isConnecting;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~KnifeSocketClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed || disposing)
            {
                Stop();
                _IsDisposed = true;
            }
        }

        #endregion

        #region 监听

        /// <summary>
        /// 异步连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        protected virtual void AsyncConnect(IPAddress ipAddress, int port)
        {
            try
            {
                if (SocketSession.AcceptSocket == null || !SocketSession.AcceptSocket.Connected)
                {
                    SocketSession.AcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                        ProtocolType.Tcp)
                    {
                        SendTimeout = Config.SendTimeout,
                        ReceiveTimeout = Config.ReceiveTimeout,
                        SendBufferSize = Config.MaxBufferSize,
                        ReceiveBufferSize = Config.ReceiveBufferSize
                    };
                }

                _IsConnecting = true;

//                var ipPoint = new IPEndPoint(ipAddress, port);
//                try
//                {
//                    SocketSession.AcceptSocket.Connect(ipPoint);
//                }
//                catch (Exception ex)
//                {
//                    _IsConnecting = false;
//                    _logger.Warn(string.Format("远程连接失败。{0}", ex.Message), ex);
//                    return;
//                }

                var asyncConnectEventArgs = new SocketAsyncEventArgs { RemoteEndPoint = EndPoint };
                asyncConnectEventArgs.Completed += AsynCompleted;

                if (!SocketSession.AcceptSocket.ConnectAsync(asyncConnectEventArgs))
                    lock (_lockObj)
                    {
                        ProcessConnect(asyncConnectEventArgs);
                    }
            }
            catch (Exception e)
            {
                _IsConnecting = false;
                _logger.Error(string.Format("客户端异步连接远端时异常.{0}", e));
            }
        }

        protected virtual void AsynCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (null == e)
                return;
            lock (_lockObj)
            {
                switch (e.LastOperation)
                {
                    case SocketAsyncOperation.Connect:
                        ProcessConnect(e);
                        break;
                    case SocketAsyncOperation.Receive:
                        ProcessReceive(e);
                        break;
                    case SocketAsyncOperation.Send:
                        ProcessSend(e);
                        break;
                    case SocketAsyncOperation.Disconnect:
                        ProcessDisconnectAndCloseSocket(e);
                        break;
//                case SocketAsyncOperation.Disconnect:
//                {
//                    var sock = asyncEventArgs.AcceptSocket;
//                    if (sock != null && sock.Connected)
//                    {
//                        sock.Shutdown(SocketShutdown.Both);
//                        sock.Close();
//                    }
//                    break;
//                }
                }
            }
        }

        protected virtual void ProcessConnect(SocketAsyncEventArgs e)
        {
            switch (e.SocketError)
            {
                case SocketError.Success:
                case SocketError.IsConnected: //连接成功了
                {
                    try
                    {
                        _logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                        IsConnected = true;

                        var handler = SessionBuilt;
                        if (handler != null)
                        {
                            handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                            {
                                Id = EndPoint,
                            }));
                        }

                        //如果有自动重连，则通知timer停止重连
                        StopReconnect();

                        _SynConnectWaitEventReset.Set(); //释放连接等待的阻塞信号

                        _logger.Debug("client连接成功，开始接收数据");

                        var data = new byte[Config.ReceiveBufferSize];
                        e.SetBuffer(data, 0, data.Length); //设置数据包
                        if (!SocketSession.AcceptSocket.ReceiveAsync(e)) //开始接收数据包
                                ProcessReceive(e);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("当成功连接时的处理发生异常.", ex);
                    }
                    break;
                }
                default: //连接失败
                {
                    try
                    {
                        _logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                        _logger.Warn(string.Format("连接失败:{0}", e.SocketError));
                        IsConnected = false;

                        //如果启用自动重连，则尝试重连
                        if(_Config.EnableReconnect)
                            StartReconnect();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("当连接未成功时的处理发生异常.", ex);
                    }
                    break;
                }
            }
            _IsConnecting = false;
        }

        protected virtual void ProcessReceive(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError == SocketError.Success) //连接正常
                {
                    if (e.BytesTransferred > 0) //有数据
                    {
                        PrcessReceiveData(e); //处理收到的数据
                        if (_Config.EnableDisconnectAfterReceive) //接收后主动断开-短连接
                        {
                            _logger.Info(string.Format("Client接收数据完成，主动中断连接。"));
                            ProcessConnectionBrokenActive();
                        }
                        else
                        {
                            var data = new byte[Config.ReceiveBufferSize];
                            e.SetBuffer(data, 0, data.Length); //设置数据包
                            if (!SocketSession.AcceptSocket.ReceiveAsync(e)) //开始接收数据包
                                    ProcessReceive(e);
                        }
                    }
                    else
                    {
                        var data = new byte[Config.ReceiveBufferSize];
                        e.SetBuffer(data, 0, data.Length); //设置数据包
                        if (!SocketSession.AcceptSocket.ReceiveAsync(e)) //开始接收数据包
                            ProcessReceive(e);
                    }
                }
                else //连接异常了
                {
                    _logger.Info(string.Format("Client接收时发现连接中断。"));
                    //ProcessConnectionBrokenPassive();
                }
            }
            catch (Exception ex) //接收处理异常了
            {
                //ProcessConnectionBrokenPassive();
            }
        }

        /// <summary>
        /// 处理连接被动中断，从远端发起的中断，本地接收或发送时出现异常货socket已经释放的情况
        /// </summary>
        protected virtual void ProcessConnectionBrokenPassive()
        {
            IsConnected = false;

            var handler = SessionBroken;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                {
                    Id = EndPoint,
                }));

            //如果有自动重连，则需要启用自动重连
            if (_Config.EnableReconnect)
                StartReconnect();
        }

        protected virtual void ProcessConnectionBrokenActive()
        {
            try
            {
                _logger.Debug("KnifeSocketClient执行主动断开");
                SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                SocketSession.AcceptSocket.Disconnect(true);
                SocketSession.AcceptSocket.Close();
            }
            catch (Exception e)
            {
                _logger.Debug("Socket客户端Shutdown异常。", e);
            }
            IsConnected = false;

            var handler = SessionBroken;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession
                {
                    Id = EndPoint,
                }));

            //如果有自动重连，则需要启用自动重连
            if (_Config.EnableReconnect)
                StartReconnect();
        }

        protected virtual void PrcessReceiveData(SocketAsyncEventArgs e)
        {
            var data = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);

            var handler = DataReceived;
            if (handler != null)
            {
                SocketSession.Id = EndPoint;
                SocketSession.Data = data;
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(SocketSession));
            }
        }

        protected virtual void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success) //连接正常
            {
                //发送成功
                _logger.Debug(string.Format("ClientSend: {0}", e.Buffer.ToHexString()));
                var dataSentHandler = DataSent;
                if (dataSentHandler != null)
                {
                    dataSentHandler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                    {
                        Id = e.RemoteEndPoint,
                        Data = e.Buffer
                    }));
                }
            }
            else
            {
                _logger.Info(string.Format("Client发送时发现连接中断。"));
                IsConnected = false;

                var handler = SessionBroken;
                if(handler != null)
                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                    {
                        Id = EndPoint
                    }));
                //如果有自动重连，则需要启用自动重连
                if (_Config.EnableReconnect)
                    StartReconnect();
            }
        }

        private void ProcessDisconnectAndCloseSocket(SocketAsyncEventArgs receiveSendEventArgs)
        {
            try
            {
                SocketSession.AcceptSocket.Close();
                SocketSession.AcceptSocket = null;

                IsConnected = false;

                var handler = SessionBroken;
                if(handler !=null)
                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                    {
                        Id = EndPoint
                    }));
            }
            catch (Exception e)
            {
                _logger.Warn("关闭Socket客户端异常。", e);
            }
        }
        #endregion

        #region 发送消息

//        private void WriteBase(byte[] data)
//        {
////            var e = new SocketAsyncEventArgs();
////            e.Completed += AsynCompleted;
////            e.SetBuffer(data, 0, data.Length);
////            if (SocketSession.AcceptSocket != null)
////            {
////                bool isSuceess = SocketSession.AcceptSocket.SendAsync(e);
////                if(!isSuceess)
////                    lock (_lockObj)
////                    {
////                        ProcessSend(e);
////                    }
////            }
//            ProcessSendData(SocketSession.Id, SocketSession.AcceptSocket, data);
//
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
            }
            catch (SocketException e)
            {
                _logger.Info(string.Format("Client发送时发现连接中断。"));
                IsConnected = false;

                var handler = SessionBroken;
                if (handler != null)
                    handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                    {
                        Id = EndPoint
                    }));
                //如果有自动重连，则需要启用自动重连
                if (_Config.EnableReconnect)
                    StartReconnect();
            }


        }

        protected virtual void AsynEndSend(IAsyncResult result)
        {
            try
            {
                var session = result.AsyncState as KnifeSocketSession;
                if (session != null)
                {
                    session.AcceptSocket.EndSend(result);
                    var data = session.Data;
                    var id = session.Id;
                    //发送成功
                    _logger.Debug(string.Format("ClientSend: {0}", data.ToHexString()));
                    var dataSentHandler = DataSent;
                    if (dataSentHandler != null)
                    {
                        dataSentHandler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new EndPointKnifeTunnelSession()
                        {
                            Id = id,
                            Data = data
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