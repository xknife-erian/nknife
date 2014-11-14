using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using Common.Logging;
using NKnife.IoC;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;
using Timer = System.Timers.Timer;

namespace SocketKnife
{
    public class KnifeSocketClient : TunnelBase, IKnifeSocketClient
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region 成员变量

        protected readonly AutoResetEvent _ConnectionAutoReset = new AutoResetEvent(false);

        /// <summary>
        ///     释放等待线程
        /// </summary>
        protected AutoResetEvent _AutoReset;

        protected KnifeSocketClientConfig _Config = DI.Get<KnifeSocketClientConfig>();
        protected EndPoint _EndPoint;

        protected bool _IsConnection;
        protected bool _OnReconnected = false;//当重连时
        protected Timer _ReconnectTimer;

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        protected Socket _Socket;

        protected KnifeSocketSession _SocketSession;

        #endregion 成员变量

        #region IKnifeSocketClient

        public override KnifeSocketConfig Config
        {
            get { return _Config; }
            set { _Config = (KnifeSocketClientConfig) value; }
        }

        public override bool Start()
        {
            var thread = new Thread(ThreadConnect) {Name = string.Format("SocketClient:{0}:{1}", _IpAddress, _Port)};
            thread.Start();
            return true;
        }

        public override bool ReStart()
        {
            if (Stop())
                return Start();
            return false;
        }

        public override bool Stop()
        {
            if (_ReconnectTimer != null)
            {
                _ReconnectTimer.Stop();
                _ReconnectTimer.Close();
            }
            if (_FilterChain != null)
            {
                foreach (var filter in _FilterChain)
                {
                    var clientFilter = (KnifeSocketClientFilter) filter;
                    clientFilter.OnConnectionBroken(new ConnectionBrokenEventArgs(_EndPoint, BrokenCause.Aggressive));
                }
            }
            try
            {
                if (_Socket != null && _Socket.Connected)
                    _Socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                _logger.Debug("Socket客户端Shutdown异常。", e);
            }
            try
            {
                if (_Socket != null)
                    _Socket.Close();
                return true;
            }
            catch (Exception e)
            {
                _logger.Warn("关闭Socket客户端异常。", e);
                return false;
            }
        }

        public override void Configure(IPAddress ipAddress, int port)
        {
            base.Configure(ipAddress, port);
            _EndPoint = new IPEndPoint(ipAddress, port);
        }

        protected virtual void ThreadConnect()
        {
            AsyncConnect(_IpAddress, _Port);
            _AutoReset.WaitOne();
            _AutoReset.Reset();
            _ConnectionAutoReset.Reset();
        }

        protected override void OnBound(params KnifeSocketProtocolHandler[] handlers)
        {
            foreach (KnifeSocketProtocolHandler handler in handlers)
            {
                var clientHandler = (KnifeSocketClientProtocolHandler) handler;
                clientHandler.Bind(WirteProtocol);
                clientHandler.Bind(WirteBase);
                clientHandler.Session = _SocketSession;
                _logger.Info(string.Format("{0}绑定成功。", clientHandler.GetType().Name));
            }
        }

        protected virtual void Initialize()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");
            if (_Socket != null)
                _Socket.Close();
            _Socket = BuildSocket();
            _AutoReset = new AutoResetEvent(false);
        }

        public override void AddFilters(params KnifeSocketFilter[] filters)
        {
            base.AddFilters(filters);
            foreach (KnifeSocketFilter filter in filters)
            {
                var clientFilter = (KnifeSocketClientFilter) filter;
                clientFilter.Bind(() => _SocketSession);
                clientFilter.ConnectionBroken += OnFilterConnectionBroken;
            }
        }

        /// <summary>
        ///     当非主动断开连接时，启动断线重连
        /// </summary>
        protected virtual void OnFilterConnectionBroken(object sender, ConnectionBrokenEventArgs e)
        {
            if (e.BrokenCause != BrokenCause.Aggressive) //当非主动断开时，启动断线重连
            {
                if (_Config.EnableReconnect)
                    Reconnect();
            }
        }

        protected virtual void Reconnect()
        {
            Stop();
            _OnReconnected = true;
            _ReconnectTimer = new Timer {Interval = _Config.ReconnectTime};
            _ReconnectTimer.Elapsed += TimerReconnect;
            _ReconnectTimer.Start();
        }

        protected virtual void TimerReconnect(object sender, ElapsedEventArgs e)
        {
            AsyncConnect(_IpAddress, _Port);
        }

        protected virtual Socket BuildSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = Config.SendTimeout,
                ReceiveTimeout = Config.ReceiveTimeout,
                SendBufferSize = Config.MaxBufferSize,
                ReceiveBufferSize = Config.ReceiveBufferSize
            };
            return socket;
        }

        protected override void SetFilterChain()
        {
            _FilterChain = DI.Get<ITunnelFilterChain<EndPoint, Socket>>("Client");
        }

        #region Implementation of IDisposable

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public override void Dispose()
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

        #endregion

        #region 监听

        protected virtual void AsyncConnect(IPAddress ipAddress, int port)
        {
            Initialize();
            var ipPoint = new IPEndPoint(ipAddress, port);
            try
            {
                if (_Socket != null)
                {
                    _Socket.Connect(ipPoint);
                }
            }
            catch (Exception e)
            {
                _IsConnection = false;
                _logger.Warn(string.Format("连接Server失败。{0}", e.Message), e);
                if (_Config.EnableReconnect && !_OnReconnected)
                    Reconnect();
                return;
            }
            if (_ReconnectTimer != null)
            {
                _ReconnectTimer.Stop();
                _ReconnectTimer.Close();
                _logger.Debug("监视重连的Timer关闭...");
            }
            _logger.Info(string.Format("连接Server[{0}:{1}]完成。", ipAddress, port));
            _OnReconnected = false;
            try
            {
                _SocketSession = DI.Get<KnifeSocketSession>();
                _SocketSession.Connector = _Socket;
                _SocketSession.Source = ipPoint;
                foreach (var filter in _FilterChain)
                {
                    var clientFilter = (KnifeSocketClientFilter) filter;
                    clientFilter.OnConnecting(new ConnectingEventArgs(ipPoint));
                }
                var asyncEventArgs = new SocketAsyncEventArgs {RemoteEndPoint = ipPoint};
                asyncEventArgs.Completed += CompletedEvent;
                if (_Socket != null && !_Socket.ConnectAsync(asyncEventArgs))
                {
                    CompletedEvent(this, asyncEventArgs);
                }
            }
            catch (Exception e)
            {
                _logger.Error("客户端异步连接远端时异常.{0}", e);
            }
        }

        protected virtual void CompletedEvent(object sender, SocketAsyncEventArgs e)
        {
            if (null == e)
                return;
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
                case SocketAsyncOperation.Receive:
                    BeginReceive(e);
                    break;
            }
        }

        protected virtual void ProcessConnect(SocketAsyncEventArgs e)
        {
            switch (e.SocketError)
            {
                case SocketError.Success:
                case SocketError.IsConnected:
                {
                    try
                    {
                        _logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                        _IsConnection = true;
                        _AutoReset.Set();

                        foreach (var filter in _FilterChain)
                        {
                            var clientFilter = (KnifeSocketClientFilter) filter;
                            clientFilter.OnConnected(new ConnectedEventArgs(true, "Connection Success."));
                        }

                        var data = new byte[Config.ReceiveBufferSize];
                        e.SetBuffer(data, 0, data.Length); //设置数据包

                        if (!_Socket.ReceiveAsync(e)) //开始读取数据包
                            CompletedEvent(this, e);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("当成功连接时的处理发生异常.", ex);
                    }
                    break;
                }
                default:
                {
                    try
                    {
                        _logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                        _logger.Warn(string.Format("连接失败:{0}", e.SocketError));
                        _IsConnection = false;
                        _AutoReset.Set();

                        foreach (var filter in _FilterChain)
                        {
                            var clientFilter = (KnifeSocketClientFilter) filter;
                            clientFilter.OnConnected(new ConnectedEventArgs(false, "Connection FAIL."));
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("当连接未成功时的处理发生异常.", ex);
                    }
                    break;
                }
            }
            _ConnectionAutoReset.Set();
        }

        protected virtual void BeginReceive(SocketAsyncEventArgs e)
        {
            switch (e.SocketError)
            {
                case SocketError.Success:
                {
                    if (e.BytesTransferred > 0)
                    {
                        PrcessReceiveData(e);
                    }
                    else
                    {
                        _logger.Info(string.Format("Client监测到来自Server的断开连接活动。被动连接中断。"));
                        _IsConnection = false;
                        foreach (var filter in _FilterChain)
                        {
                            var clientFilter = (KnifeSocketClientFilter) filter;
                            clientFilter.OnConnectionBroken(new ConnectionBrokenEventArgs(_EndPoint, BrokenCause.Passive));
                        }
                    }
                    break;
                }
                default:
                {
                    _logger.Debug(string.Format("当BeginReceive时，未处理的Socket状态:{0}", e.SocketError));
                    break;
                }
            }
        }

        protected virtual void PrcessReceiveData(SocketAsyncEventArgs e)
        {
            try
            {
                var data = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);
                // 触发数据到达事件
                foreach (var filter in _FilterChain)
                {
                    var clientFilter = (KnifeSocketClientFilter) filter;
                    clientFilter.OnDataFetched(new SocketDataFetchedEventArgs(_SocketSession.Source, data));
                    clientFilter.PrcoessReceiveData(_SocketSession, ref data); // 调用filter对数据进行处理

                    if (!clientFilter.ContinueNextFilter)
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("接收数据时读取Buffer异常。", ex);
            }

            try
            {
                if (_Socket != null) //继续异步从服务端 Socket 接收数据
                    _Socket.ReceiveAsync(e);
                else
                    _logger.Warn("Client -> 继续异步从服务端 Socket 接收数据时 Socket 无效。");
            }
            catch (Exception ex)
            {
                _logger.Error("继续异步地从服务端 Socket 接收数据异常。", ex);
            }
        }

        #endregion

        #region 发送消息

        protected virtual void WirteProtocol(KnifeSocketSession session, StringProtocol protocol)
        {
            byte[] senddata = _Codec.SocketEncoder.Execute(protocol.Generate());
            WirteBase(session, senddata);
        }

        protected virtual void WirteBase(KnifeSocketSession session, byte[] data)
        {
            var e = new SocketAsyncEventArgs();
            e.SetBuffer(data, 0, data.Length);
            if (_Socket != null)
            {
                _Socket.SendAsync(e);
                _logger.Info(string.Format("ClientSend:{0}", data.ToHexString()));
            }
        }

        #endregion
    }
}