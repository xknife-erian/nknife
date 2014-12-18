using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using Common.Logging;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Common;
using SocketKnife.Events;
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

        protected bool IsConnected; //连接状态，true表示已经连接上了
        private bool _ReconnectFlag;
        private bool _NeedReconnected;//是否重连

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        //protected Socket _Socket;
        protected SocketAsyncEventArgs AsyncReceiveEventArgs;
        protected SocketAsyncEventArgs AsyncSendEventArgs;
        protected SocketAsyncEventArgs AsyncConnectEventArgs;
        protected KnifeSocketSession SocketSession;
        private ConcurrentQueue<byte[]> _SendQueue = new ConcurrentQueue<byte[]>();

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
                handler.Invoke(this,new SessionEventArgs<byte[], EndPoint>(new KnifeTunnelSession()
                {
                    Id = EndPoint,
                }));

            try
            {
                AsyncConnectEventArgs.AcceptSocket.Shutdown(SocketShutdown.Both);
                AsyncDisconnect();
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
            WirteBase(data);
        }

        public void SendAll(byte[] data)
        {
            WirteBase(data);
        }

        public void KillSession(EndPoint id)
        {
            AsyncDisconnect();
        }

        public bool SessionExist(EndPoint id)
        {
            return (SocketSession.AcceptSocket != null && SocketSession.AcceptSocket.Connected);
        }
        #endregion

        #region 初始化
        protected void Initialize()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");

            AsyncReceiveEventArgs = new SocketAsyncEventArgs { RemoteEndPoint = EndPoint };
            AsyncReceiveEventArgs.Completed += AsynCompleted;

            AsyncSendEventArgs = new SocketAsyncEventArgs {RemoteEndPoint = EndPoint};
            AsyncSendEventArgs.Completed += AsynCompleted;

            AsyncConnectEventArgs = new SocketAsyncEventArgs { RemoteEndPoint = EndPoint };
            AsyncConnectEventArgs.Completed += AsynCompleted;

            var ipPoint = new IPEndPoint(_IpAddress, _Port);
            SocketSession = DI.Get<KnifeSocketSession>();
            SocketSession.Id = ipPoint;

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
                    if (!IsConnected) //未连接
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

        private void BuildSocket()
        {
            AsyncConnectEventArgs.AcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = Config.SendTimeout,
                ReceiveTimeout = Config.ReceiveTimeout,
                SendBufferSize = Config.MaxBufferSize,
                ReceiveBufferSize = Config.ReceiveBufferSize
            };
            SocketSession.AcceptSocket = AsyncConnectEventArgs.AcceptSocket;
        }
        #endregion

        #region IDisposable

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public KnifeSocketClient(bool reconnectFlag)
        {
            _ReconnectFlag = reconnectFlag;
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
                if (AsyncConnectEventArgs.AcceptSocket == null || !AsyncConnectEventArgs.AcceptSocket.Connected)
                {
                    BuildSocket();
                }
                if (!AsyncConnectEventArgs.AcceptSocket.ConnectAsync(AsyncConnectEventArgs))
                    ProcessConnect(AsyncConnectEventArgs);
            }
            catch (Exception e)
            {
                _logger.Error("客户端异步连接远端时异常.{0}", e);
            }
        }


        protected virtual void AsyncDisconnect()
        {
            AsyncConnectEventArgs.AcceptSocket.Shutdown(SocketShutdown.Both);
            if (!AsyncConnectEventArgs.AcceptSocket.DisconnectAsync(AsyncConnectEventArgs))
            {
                ProcessDisconnectAndCloseSocket(AsyncConnectEventArgs);
            }
        }

        protected virtual void AsynCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (null == e)
                return;

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
                        if(handler !=null)
                            handler.Invoke(this,new SessionEventArgs<byte[], EndPoint>(new KnifeTunnelSession()
                            {
                                Id = EndPoint,
                            }));

                        //如果有自动重连，则通知timer停止重连
                        StopReconnect();

                        _SynConnectWaitEventReset.Set(); //释放连接等待的阻塞信号
                        if (_SendQueue.Count > 0) //有数据要发送
                        {
                            byte[] dataToSend;
                            _SendQueue.TryPeek(out dataToSend); //发送成功后才移除
                            _logger.Debug(string.Format("ClientSend: {0}", dataToSend.ToHexString()));
                            AsyncSendEventArgs.SetBuffer(dataToSend, 0, dataToSend.Length);
                            AsyncSendEventArgs.AcceptSocket = AsyncConnectEventArgs.AcceptSocket;
                            if (!AsyncSendEventArgs.AcceptSocket.SendAsync(AsyncSendEventArgs))
                            {
                                ProcessSend(AsyncSendEventArgs);
                            }
                        }


                        _logger.Debug("client连接成功，开始接收数据");

                        var data = new byte[Config.ReceiveBufferSize];
                        AsyncReceiveEventArgs.SetBuffer(data, 0, data.Length); //设置数据包
                        AsyncReceiveEventArgs.AcceptSocket = AsyncConnectEventArgs.AcceptSocket;
                        if (!AsyncReceiveEventArgs.AcceptSocket.ReceiveAsync(AsyncReceiveEventArgs)) //开始接收数据包
                            ProcessReceive(AsyncReceiveEventArgs);
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
        }

        protected virtual void ProcessReceive(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success) //连接正常
                {
                    PrcessReceiveData(e); //处理收到的数据
                    if (_Config.EnableDisconnectAfterReceive) //接收后主动断开-短连接
                    {
                        _logger.Info(string.Format("Client接收数据完成，主动中断连接。"));
                        AsyncDisconnect();
                    }
                    else //继续接收
                    {
                        if (AsyncConnectEventArgs.AcceptSocket != null)
                        {
                            var data = new byte[Config.ReceiveBufferSize];
                            e.SetBuffer(data, 0, data.Length); //设置数据包
                            if (!AsyncConnectEventArgs.AcceptSocket.ReceiveAsync(e)) //开始读取数据包
                                ProcessReceive(e);
                        }
                        else
                        {
                            ProcessConnectionBrokenPassive();
                        }
                    }
                }
                else //连接异常了
                {
                    _logger.Info(string.Format("Client接收时发现连接中断。"));
                    ProcessConnectionBrokenPassive();
                }
            }
            catch (Exception ex) //接收处理异常了
            {
                ProcessConnectionBrokenPassive();
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
                handler.Invoke(this, new SessionEventArgs<byte[], EndPoint>(new KnifeTunnelSession()
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
                if (_SendQueue.Count > 0)
                {
                    byte[] giveUpData;
                    _SendQueue.TryDequeue(out giveUpData);
                }
            }
            else
            {
                _logger.Info(string.Format("Client发送时发现连接中断。"));
                IsConnected = false;

                var handler = SessionBroken;
                if(handler != null)
                    handler.Invoke(this,new SessionEventArgs<byte[], EndPoint>(new KnifeTunnelSession()
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
                AsyncConnectEventArgs.AcceptSocket.Close();
                AsyncConnectEventArgs.AcceptSocket = null;

                IsConnected = false;

                var handler = SessionBroken;
                if(handler !=null)
                    handler.Invoke(this,new SessionEventArgs<byte[], EndPoint>(new KnifeTunnelSession()
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

//        protected virtual void WirteProtocol(KnifeSocketSession session, StringProtocol protocol)
//        {
//            byte[] senddata = _Codec.StringEncoder.Execute(_Family.Generate(protocol));
//            WirteBase(session, senddata);
//        }
//
        private void WirteBase(byte[] data)
        {
            if (!IsConnected) //未连接
            {
                AddSendQueue(data);
                StartReconnect();
                _ReconnectResetEvent.Set(); //立刻发起连接

                //_SynConnectWaitEventReset.Reset();
                //_SynConnectWaitEventReset.WaitOne(_Config.ReconnectInterval * 2); //等待连接事件结束，等重连两次的时间，只有连接成功了， 信号才会被提前释放
            }
            else //已连接
            {
                _logger.Debug(string.Format("ClientSend: {0}", data.ToHexString()));
                AsyncSendEventArgs.SetBuffer(data, 0, data.Length);
                if (!AsyncConnectEventArgs.AcceptSocket.SendAsync(AsyncSendEventArgs))
                {
                    ProcessSend(AsyncSendEventArgs);
                }
            }
//            else //连接失败了
//            {
//                _logger.Warn("Clitent发送失败，因为无法连接服务端");
//            }
        }

        private void AddSendQueue(byte[] data)
        {
            byte[] giveUpData;
            if (_SendQueue.Count > 50)
                _SendQueue.TryDequeue(out giveUpData);
            _SendQueue.Enqueue(data);
        }

        #endregion


    }
}