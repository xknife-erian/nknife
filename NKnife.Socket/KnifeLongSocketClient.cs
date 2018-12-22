using System;
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
    public class KnifeLongSocketClient : ISocketClient, IDisposable
    {
        private static readonly ILog _Logger = LogManager.GetLogger<KnifeLongSocketClient>();
        public KnifeLongSocketClient(bool reconnectFlag, bool isConnecting)
        {
            _reconnectFlag = reconnectFlag;
            _isConnecting = isConnecting;
        }

        /// <summary>
        ///     处理连接被动中断，从远端发起的中断，本地接收或发送时出现异常货socket已经释放的情况
        /// </summary>
        protected virtual void ProcessConnectionBrokenPassive()
        {
            _IsConnected = false;

            var handler = SessionBroken;
            if (handler != null)
            {
                var session = new TunnelSession();
                handler.Invoke(this, new SessionEventArgs(session));
            }

            //如果有自动重连，则需要启用自动重连
            StartReconnect();
        }

        protected virtual void ProcessConnectionBrokenActive()
        {
            try
            {
                _Logger.Debug("KnifeSocketClient执行主动断开");
                _SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                _SocketSession.AcceptSocket.Close();
            }
            catch (Exception e)
            {
                _Logger.Error("Socket客户端Shutdown异常。", e);
            }
            _IsConnected = false;

            var handler = SessionBroken;
            if (handler != null)
            {
                var session = new TunnelSession();
                handler.Invoke(this, new SessionEventArgs(session));
            }

            //如果有自动重连，则需要启用自动重连
            StartReconnect();
        }

        private void Close()
        {
            lock (_LockObj)
            {
                _SocketSession.ResetBuffer();

                try
                {
                    _SocketSession.State = SessionState.Closed;
                    if (_SocketSession.AcceptSocket != null)
                    {
                        _SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                        _SocketSession.AcceptSocket.Disconnect(true);
                        _SocketSession.AcceptSocket.Close();
                    }
                }
                catch (Exception e)
                {
                    _Logger.Warn("Socket客户端关闭时有异常", e);
                }
                _Logger.Debug("Socket客户端关闭");
            }
        }

        #region 成员变量

        protected IPAddress _IpAddress;
        protected int _Port;

        /// <summary>
        ///     异步连接的控制，连接事件完成后释放信号，通过IsConnected判断是否连接成功
        /// </summary>
        private readonly ManualResetEvent _synConnectWaitEventReset = new ManualResetEvent(false);

        /// <summary>
        ///     重连线程中的阻塞超时控制
        /// </summary>
        private readonly ManualResetEvent _reconnectResetEvent = new ManualResetEvent(false);

        protected SocketClientConfig _Config = Di.Get<SocketClientConfig>();
        protected EndPoint _EndPoint;

        private bool _isConnecting; //true 正在进行连接, false表示连接动作完成
        protected bool _IsConnected; //连接状态，true表示已经连接上了
        private bool _reconnectFlag = true;
        private bool _needReconnected; //是否重连
        private Thread _reconnectedThread;

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        protected SocketSession _SocketSession;

        private static readonly object _LockObj = new object();

        public KnifeLongSocketClient()
        {
        }

        #endregion 成员变量

        #region IKnifeSocketClient接口

        public SocketConfig Config
        {
            get { return _Config; }
            set { _Config = (SocketClientConfig) value; }
        }

        public void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
            _EndPoint = new IPEndPoint(ipAddress, port);
        }

        #endregion

        #region IDataConnector接口

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;
        public event EventHandler<SessionEventArgs> DataSent;

        public bool Start()
        {
            Initialize();
            AsyncConnect(_IpAddress, _Port);
            _reconnectedThread.Start();
            return true;
        }

        public bool Stop()
        {
            StopReconnect();

            _reconnectFlag = false;
            _reconnectResetEvent.Set();

            var handler = SessionBroken;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
            }

            try
            {
                _SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                //_SocketSession.AcceptSocket.Disconnect(true);
                _SocketSession.AcceptSocket.Close();
                return true;
            }
            catch (Exception e)
            {
                _Logger.Debug("Socket客户端Shutdown异常。", e);
                return false;
            }
        }

        #endregion

        #region ISessionProvider

        public void Send(long id, byte[] data)
        {
            ProcessSendData(data);
        }

        public void SendAll(byte[] data)
        {
            ProcessSendData(data);
        }

        public void KillSession(long id)
        {
            ProcessConnectionBrokenActive();
        }

        #endregion

        #region 初始化

        protected void Initialize()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");
            }

            var ipPoint = new IPEndPoint(_IpAddress, _Port);
            _SocketSession = Di.Get<SocketSession>();

            _reconnectFlag = true;
            _reconnectedThread = new Thread(ReconnectedLoop);
        }

        private void ReconnectedLoop()
        {
            while (_reconnectFlag)
            {
                if (!_IsConnected) //重连检查，仅启用了自动重连的时候做
                {
                    _needReconnected = true;
                }

                if (_needReconnected) //需要重连
                {
                    if (!_IsConnected && !_isConnecting) //未连接
                    {
                        _Logger.Debug("Client发起重连尝试");
                        AsyncConnect(_IpAddress, _Port);
                        _reconnectResetEvent.Reset(); //阻塞
                        _reconnectResetEvent.WaitOne(_Config.ReconnectInterval);
                    }
                    else //已连接，则不需要重连了
                    {
                        _needReconnected = false;
                    }
                }
                else
                {
                    //阻塞
                    _reconnectResetEvent.Reset();
                    _reconnectResetEvent.WaitOne(_Config.ReconnectInterval);
                }
            }
            _Logger.Debug("SocketClient退出重连循环");
        }

        protected virtual void StartReconnect()
        {
            _Logger.Info(string.Format("Client启用自动重连。"));
            _needReconnected = true;
        }

        protected virtual void StopReconnect()
        {
            _needReconnected = false;
        }

        #endregion

        #region IDisposable

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~KnifeLongSocketClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed || disposing)
            {
                Stop();
                _isDisposed = true;
            }
        }

        #endregion

        #region 监听

        /// <summary>
        ///     异步连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        protected virtual void AsyncConnect(IPAddress ipAddress, int port)
        {
            try
            {
                //Close();

                if (_SocketSession.AcceptSocket == null || !_SocketSession.AcceptSocket.Connected)
                {
                    _SocketSession.AcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        SendTimeout = Config.SendTimeout,
                        ReceiveTimeout = Config.ReceiveTimeout,
                        SendBufferSize = Config.MaxBufferSize,
                        ReceiveBufferSize = Config.ReceiveBufferSize
                    };
                }

                _isConnecting = true;

                _synConnectWaitEventReset.Reset();
                _SocketSession.AcceptSocket.BeginConnect(ipAddress, port, EndAsyncConnect, this);

                _synConnectWaitEventReset.WaitOne();
            }
            catch (Exception e)
            {
                _isConnecting = false;
                _Logger.Error(string.Format("客户端异步连接远端时异常.{0}", e));
            }
        }

        private void EndAsyncConnect(IAsyncResult ar)
        {
            _isConnecting = false;
            _IsConnected = true;
            //如果有自动重连，则通知timer停止重连
            StopReconnect();
            _synConnectWaitEventReset.Set(); //释放连接等待的阻塞信号

            var handler = SessionBuilt;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
            }

            ReceiveDatagram();
        }

        private void ReceiveDatagram()
        {
            try // 一个客户端连续做连接 或连接后立即断开，容易在该处产生错误，系统不认为是错误
            {
                // 开始接受来自该客户端的数据
                _SocketSession.AcceptSocket.BeginReceive(_SocketSession.ReceiveBuffer, 0, _SocketSession.ReceiveBufferSize, SocketFlags.None, EndReceiveDatagram, this);
            }
            catch (Exception err) // 读 Socket 异常，准备关闭该会话
            {
                _SocketSession.DisconnectType = DisconnectType.Exception;
                _SocketSession.State = SessionState.Inactive;

                OnSessionReceiveException();
            }
        }

        private void EndReceiveDatagram(IAsyncResult iar)
        {
            if (!_SocketSession.AcceptSocket.Connected)
            {
                OnSessionReceiveException();
                return;
            }

            try
            {
                // Shutdown 时将调用 ReceiveData，此时也可能收到 0 长数据包
                var readBytesLength = _SocketSession.AcceptSocket.EndReceive(iar);
                iar.AsyncWaitHandle.Close();

                if (readBytesLength == 0)
                {
                    _SocketSession.DisconnectType = DisconnectType.Normal;
                    _SocketSession.State = SessionState.Inactive;
                }
                else // 正常数据包
                {
                    _SocketSession.LastSessionTime = DateTime.Now;
                    // 合并报文，按报文头、尾字符标志抽取报文，将包交给数据处理器
                    var data = new byte[readBytesLength];
                    Array.Copy(_SocketSession.ReceiveBuffer, 0, data, 0, readBytesLength);
                    PrcessReceiveData(data);
                    ReceiveDatagram();
                }
            }
            catch (Exception err) // 读 socket 异常，关闭该会话，系统不认为是错误（这种错误可能太多）
            {
                if (_SocketSession.State == SessionState.Active)
                {
                    _SocketSession.DisconnectType = DisconnectType.Exception;
                    _SocketSession.State = SessionState.Inactive;

                    OnSessionReceiveException();
                }
            }
        }

        private void OnSessionReceiveException()
        {
            ProcessConnectionBrokenPassive();
        }

        protected virtual void PrcessReceiveData(byte[] data)
        {
            var handler = DataReceived;
            if (handler != null)
            {
//                _SocketSession.Id = _EndPoint;
                _SocketSession.Data = data;
                handler.Invoke(this, new SessionEventArgs(_SocketSession));
            }
        }

        #endregion

        #region 发送消息

        protected virtual void ProcessSendData(byte[] data)
        {
            try
            {
                if (_SocketSession.AcceptSocket != null && _SocketSession.AcceptSocket.Connected)
                    _SocketSession.AcceptSocket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynEndSend, data);
            }
            catch (SocketException e)
            {
                _Logger.Info(string.Format("Client发送时发现连接中断。{0}", e.Data), e);
                _IsConnected = false;

                var handler = SessionBroken;
                if (handler != null)
                {
                    handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
                }
                //如果有自动重连，则需要启用自动重连
                StartReconnect();
            }
        }

        protected virtual void AsynEndSend(IAsyncResult result)
        {
            try
            {
                var data = result.AsyncState as byte[];
                _SocketSession.AcceptSocket.EndSend(result);

                var dataSentHandler = DataSent;
                if (dataSentHandler != null)
                {
                    dataSentHandler.Invoke(this, new SessionEventArgs(new TunnelSession
                    {
//                        Id = _SocketSession.AcceptSocket.RemoteEndPoint,
                        Data = data
                    }));
                }
            }
            catch (Exception e)
            {
                _Logger.WarnFormat("结束挂起的异步发送异常.{0}", e.Message);
            }
        }

        #endregion
    }
}