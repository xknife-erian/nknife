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
            foreach (KnifeSocketServerProtocolHandler handler in _Handlers)
            {
                //TODO:
//                handler.Bind(WirteProtocol);
//                handler.Bind(WirteBase);
//                handler.SessionMap = _SessionMap;
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

        private void AsyncConnect(IPAddress ipAddress, int port)
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

        /// <summary>
        ///     异步发送数据
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public bool SendTo(string data)
        {
            return SendTo(data, false);
        }

        /// <summary>
        ///     发送数据包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        public bool SendTo(string data, bool isCompress)
        {
            byte[] senddata = _Codec.SocketEncoder.Execute(data);
            var e = new SocketAsyncEventArgs();
            e.SetBuffer(senddata, 0, senddata.Length);
            bool isSuceess = false;
            if (_Socket != null)
            {
                isSuceess = _Socket.SendAsync(e);
                _logger.Trace(() => string.Format("Send:{0},\r\n{1}", data, isSuceess));
//                if (!isSuceess && IsCustomTryConnectionMode)
//                {
//                    //当发送不成功时，启动自动重新连接
//                    _ConnectionWhileBreakTimer.Start();
//                }
            }
            return isSuceess;
        }

        /// <summary>
        ///     同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="timeout">等待超时的时长</param>
        /// <returns></returns>
        public string TalkTo(string data, int timeout)
        {
            return TalkTo(data, timeout, false);
        }

        /// <summary>
        ///     同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        /// <returns></returns>
        public string TalkTo(string data, bool isCompress)
        {
            return TalkTo(data, Config.SendTimeout, isCompress);
        }

        /// <summary>
        ///     同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <returns></returns>
        public string TalkTo(string data)
        {
            return TalkTo(data, false);
        }

        /// <summary>
        ///     同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="timeout">等待超时的时长</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        /// <returns></returns>
        public string TalkTo(string data, int timeout, bool isCompress)
        {
            return "";
            // ++++++++++++ 发送部份
//            if (!IsConnectionSuceess)
//            {
//                _logger.Warn("远程未连接，无法发送数据。");
//                _IsTalkTo = false;
//                Stop();
//                return string.Empty;
//            }
//            byte[] senddata = Encoder.Execute(data, isCompress);
//            if (UtilityCollection.IsNullOrEmpty(senddata))
//            {
//                Stop();
//                return string.Empty;
//            }
//            try
//            {
//                _Socket.Send(senddata); //注意，这里采用的是同步
//                _IsTalkTo = true;
//            }
//            catch
//            {
//                _IsTalkTo = false;
//                throw; //异常交给上层来处理
//            }
//            _logger.Trace(() => string.Format("Client.Send: {0}", data));
//
//            string replay = string.Empty;
//            if (!_IsTalkTo)
//            {
//                Stop();
//                _logger.Warn(string.Format("TalkTo发送数据不成功{0}", data));
//                return replay; //发送数据不成功
//            }
//
//            // ++++++++++++ 接收部份
//            var mainRecvBytes = new byte[Option.BufferSize];
//            int currentOffset = 0;
//            try
//            {
//                // 一次接收的临时区
//                var oneRecvBytes = new byte[Option.TalkOneLength];
//                int oneRecvCount;
//
//                // 持续接收，直到接收到为空时
//                while ((oneRecvCount = _Socket.Receive(oneRecvBytes, 0, Option.TalkOneLength, SocketFlags.None)) > 0)
//                {
//                    if (currentOffset + oneRecvCount > mainRecvBytes.Length)
//                        Array.Resize(ref mainRecvBytes, currentOffset + oneRecvCount + 1);
//                    Array.Copy(oneRecvBytes, 0, mainRecvBytes, currentOffset, oneRecvCount);
//                    currentOffset += oneRecvCount;
//                    // 当接收到的数据量少于原定的一次接收缓存区，一般来讲，已接收完成
//                    if (oneRecvCount < Option.TalkOneLength)
//                        break;
//                }
//                _logger.Trace(() => string.Format("Client.Receive.Bytes.Count: {0}", currentOffset));
//            }
//            catch (Exception e)
//            {
//                _logger.Warn("TalkTo接收数据异常。", e);
//            }
//
//            if (currentOffset < 1)
//            {
//                return string.Empty;
//            }
//
//            switch (Mode)
//            {
//                case SocketMode.AsyncKeepAlive:
//                case SocketMode.Talk:
//                    break;
//            }
//            try
//            {
//                OnDataComeIn(mainRecvBytes, null); //触发字节数组数据到达事件
//
//                int i;
//                string[] recvStrings = Decoder.Execute(mainRecvBytes, out i);
//                if (recvStrings.Length > 0)
//                {
//                    replay = recvStrings[0].TrimEnd('\0');
//                }
//                _logger.Trace(() => string.Format("Client.Receive: {0}", replay));
//            }
//            catch (Exception e)
//            {
//                _logger.Warn("接收数据解析异常。", e);
//            }
//            return replay;
        }

        #endregion

    }
}
