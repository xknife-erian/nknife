using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NKnife.Adapters;
using NKnife.Events;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using NKnife.Utility;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    public class KnifeSocketClient : TunnelBase, IKnifeSocketClient
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        #region 成员变量

        /// <summary>
        ///     释放等待线程
        /// </summary>
        protected AutoResetEvent _AutoReset;

        protected readonly AutoResetEvent _ConnectionAutoReset = new AutoResetEvent(false);

        /// <summary>
        ///     重连计数
        /// </summary>
        protected int _ConnectionCount;

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        protected Socket _Socket;

        protected KnifeSocketSession _SocketSession;

        protected bool _IsConnection;

        #endregion 成员变量

        #region IKnifeSocketClient

        public override ISocketConfig Config { get; set; }

        public override bool Start()
        {
            Initialize();

            AsyncConnect(_IpAddress, _Port);
            _AutoReset.WaitOne();
            _AutoReset.Reset();
            _ConnectionAutoReset.Reset();
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
            try
            {
                if (_Socket != null)
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

        protected override void OnBound()
        {
            foreach (KnifeSocketProtocolHandler handler in _Handlers)
            {
                var clientHandler = (KnifeSocketClientProtocolHandler) handler;
                clientHandler.Bind(WirteProtocol);
                clientHandler.Bind(WirteBase);
                clientHandler.Session = _SocketSession;
            }
        }

        protected virtual void Initialize()
        {
            if (_Socket != null)
                Stop();
            _Socket = BuildSocket();
            _AutoReset = new AutoResetEvent(false);
        }

        protected virtual Socket BuildSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
//                SendTimeout = Option.Timeout,
//                ReceiveTimeout = Option.Timeout,
//                SendBufferSize = Option.BufferSize,
//                ReceiveBufferSize = Option.BufferSize
            };
            return socket;
        }

        protected override void SetFilterChain()
        {
            _FilterChain = DI.Get<ITunnelFilterChain<EndPoint, Socket>>("Client");
        }

        #region Implementation of IDisposable

        public override void Dispose()
        {
            Stop();
        }

        #endregion

        #endregion

        #region 重点方法

        protected virtual void AsyncConnect(IPAddress ipAddress, int port)
        {
            var ipPoint = new IPEndPoint(ipAddress, port);
            try
            {
                try
                {
                    _Socket.Connect(ipPoint);
                    _IsConnection = true;
                }
                catch (Exception ex)
                {
                    _IsConnection = false;
                    _logger.Warn(string.Format("远程连接失败。{0}", ex.Message), ex);
                }
                _SocketSession = DI.Get<KnifeSocketSession>();
                _SocketSession.Connector = _Socket;
                _SocketSession.Source = ipPoint;
                foreach (var filter in _FilterChain)
                {
                    var clientFilter = (KnifeSocketClientFilter) filter;
                    clientFilter.OnConnectioning(new ConnectioningEventArgs(ipPoint));
                }
                var e = new SocketAsyncEventArgs { RemoteEndPoint = ipPoint };
                e.Completed += CompletedEvent;
                if (_ConnectionCount > 0)
                    _Socket = BuildSocket();
                if (!_Socket.ConnectAsync(e))
                {
                    CompletedEvent(this, e);
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
            if (e.SocketError == SocketError.Success)
            {
                try
                {
                    _ConnectionCount++;
                    _IsConnection = true;
                    _AutoReset.Set();

                    foreach (var filter in _FilterChain)
                    {
                        var clientFilter = (KnifeSocketClientFilter) filter;
                        clientFilter.OnConnectioned(new ConnectionedEventArgs(true, "Connection Success."));
                        clientFilter.OnSocketStatusChanged(new ChangedEventArgs<ConnectionStatus>(ConnectionStatus.Break, ConnectionStatus.Normal));
                    }

                    var data = new byte[Config.ReadBufferSize];
                    e.SetBuffer(data, 0, data.Length); //设置数据包

                    if (!_Socket.ReceiveAsync(e)) //开始读取数据包
                        CompletedEvent(this, e);
                }
                catch (Exception ex)
                {
                    _logger.Error("当成功连接时的处理发生异常.", ex);
                }
            }
            else
            {
                try
                {
                    _logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                    _IsConnection = false;
                    _AutoReset.Set();

                    foreach (var filter in _FilterChain)
                    {
                        var clientFilter = (KnifeSocketClientFilter)filter;
                        clientFilter.OnConnectioned(new ConnectionedEventArgs(false, "Connection FAIL."));
                        clientFilter.OnSocketStatusChanged(new ChangedEventArgs<ConnectionStatus>(ConnectionStatus.Normal, ConnectionStatus.Break));
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("当连接未成功时的处理发生异常.", ex);
                }
            }
            _ConnectionAutoReset.Set();
        }

        protected virtual void BeginReceive(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                PrcessReceiveData(e);
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
                    EndPoint endPoint = e.AcceptSocket.RemoteEndPoint;

                    var clientFilter = (KnifeSocketClientFilter)filter;
                    clientFilter.OnDataFetched(new SocketDataFetchedEventArgs(endPoint, data));
                    clientFilter.PrcoessReceiveData(_SocketSession, data); // 调用filter对数据进行处理

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
                if (_Socket != null && _Socket.Connected) //继续异步从服务端 Socket 接收数据
                    _Socket.ReceiveAsync(e);
                else
                    _logger.Warn("Client -> 继续异步从服务端 Socket 接收数据时 Socket 无效。");
            }
            catch (Exception ex)
            {
                _logger.Error("继续异步地从服务端 Socket 接收数据异常。", ex);
            }
        }

        //**************************************************

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
                bool isSuceess = _Socket.SendAsync(e);
                _logger.Trace(() => string.Format("Send:{0},\r\n{1}", data, isSuceess));
            }
        }

        #endregion

    }
}
