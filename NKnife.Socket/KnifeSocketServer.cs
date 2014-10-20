﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ninject;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Tunnel.Events;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Exceptions;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    public class KnifeSocketServer : KnifeSocketServerBase
    {
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();

        #region 成员变量

        private readonly AutoResetEvent _MainAutoReset;

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

        protected KnifeSocketCodec _Codec;
        protected KnifeSocketServerConfig _Config = new KnifeSocketServerConfig();
        protected KnifeSocketProtocolFamily _Family;
        protected KnifeSocketFilterChain _FilterChain;
        protected KnifeSocketProtocolHandler _Handler;
        private IPAddress _IpAddress;
        private int _Port;
        protected KnifeSocketSessionMap _SessionMap;

        public override void Configure(IPAddress ipAddress, int port)
        {
            _IpAddress = ipAddress;
            _Port = port;
        }

        public override void AddFilter(KnifeSocketServerFilter filter)
        {
            filter.Bind(GetFamily, GetHandle, GetSessionMap, GetCodec);
            _FilterChain.AddLast(filter);
        }

        public override void Bind(KnifeSocketCodec codec, KnifeSocketProtocolFamily protocolFamily,
            KnifeSocketProtocolHandler handler)
        {
            _Codec = codec;
            _Family = protocolFamily;
            _Handler = handler;
            _Handler.Bind(WirteBase);
            _Handler.Bind(WirteProtocol);
            _Handler.SessionMap = _SessionMap;
        }

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
                _MainSocket.Close();
                foreach (KeyValuePair<EndPoint, KnifeSocketSession> pair in _SessionMap)
                {
                    Socket client = pair.Value.Connector;
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                }
                _SessionMap.Clear();
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
                _logger.Info(string.Format("SocketServer关闭。"));
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("SocketServer关闭异常。{0}", e.Message), e);
                return false;
            }
        }

        private KnifeSocketSessionMap GetSessionMap()
        {
            return _SessionMap;
        }

        private KnifeSocketProtocolHandler GetHandle()
        {
            return _Handler;
        }

        private KnifeSocketProtocolFamily GetFamily()
        {
            return _Family;
        }

        private KnifeSocketCodec GetCodec()
        {
            return _Codec;
        }

        #region 释放( IDisposable )

        /// <summary>
        ///     用来确定是否以释放
        /// </summary>
        private bool _IsDisposed;

        public override ISocketServerConfig Config
        {
            get { return _Config; }
        }

        public override void Dispose()
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
                catch (Exception e)
                {
                    Debug.Fail(string.Format("Main Socket释放时发生异常。{0}", e.Message));
                }
                _IsDisposed = true;
            }
        }

        #endregion

        #endregion

        #region 构造函数,初始化

        [Inject]
        public KnifeSocketServer(KnifeSocketSessionMap sessionMap, KnifeSocketFilterChain filterChain)
        {
            _SessionMap = sessionMap;
            _FilterChain = filterChain;
            _MainAutoReset = new AutoResetEvent(false);
        }

        protected virtual void Initialize()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");
            _IsClose = false;

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

            _logger.Info(string.Format("== {0} 已启动。端口:{1}", GetType().Name, _Port));
            _logger.Info(string.Format("发送缓冲区:大小:{0}，超时:{1}", _MainSocket.SendBufferSize, _MainSocket.SendTimeout));
            _logger.Info(string.Format("接收缓冲区:大小:{0}，超时:{1}", _MainSocket.ReceiveBufferSize, _MainSocket.ReceiveTimeout));
            _logger.Info(string.Format("SocketAsyncEventArgs 连接池已创建。大小:{0}", Config.MaxConnectCount));
        }

        #endregion

        #region 内部方法

        protected virtual void AsynCompleted(object sender, SocketAsyncEventArgs e) // SocketAsyncEventArgs的Completed事件
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    BeginAccept(e);
                    break;
                case SocketAsyncOperation.Receive:
                    BeginReceive(e);
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
                if (!_MainSocket.AcceptAsync(sockAsyn))
                    BeginAccept(sockAsyn);
            }
            else
            {
                _logger.Warn("Server: Socket连接池中已达到最大连接数。");
            }
        }

        protected virtual void BeginAccept(SocketAsyncEventArgs e)
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
                                BeginReceive(e);
                        }
                        //如果选用长连接服务时，将相应的连接置入一个Map以做处理
                        var iep = e.AcceptSocket.RemoteEndPoint as IPEndPoint;
                        if (iep != null)
                        {
                            string ip = iep.ToString();
                            if (!_SessionMap.ContainsKey(iep))
                            {
                                var session = DI.Get<KnifeSocketSession>();
                                session.Source = iep;
                                session.Connector = e.AcceptSocket;
                                _SessionMap.Add(iep, session);
                                _logger.Info(string.Format("Server: IP地址:{0}的连接已放入客户端池中。池中:{1}", ip, _SessionMap.Count));
                                foreach (KnifeSocketServerFilter filter in _FilterChain)
                                {
                                    filter.OnClientCome(new SocketSessionEventArgs(session));
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
                        _logger.Warn(string.Format("服务端:未处理状态,{0}", e.SocketError));
                        break;
                    }
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

        private void RemoveSession(SocketAsyncEventArgs e)
        {
            _logger.Trace(() => string.Format("当RemoveSession时，Socket状态：{0}", e.SocketError));
            if (!e.AcceptSocket.Connected)
            {
                return;
            }
            string message = string.Format("Server: >> 客户端:{0}, 连接中断.", e.AcceptSocket.RemoteEndPoint);
            _logger.Info(message);

            var iep = e.AcceptSocket.RemoteEndPoint;
            foreach (KnifeSocketServerFilter filter in _FilterChain)
            {
                filter.OnClientBroke(new ConnectionBreakEventArgs<EndPoint>(iep, message));
            }

            if (iep != null)
            {
                string ip = iep.ToString();
                if (_SessionMap.ContainsKey(iep))
                {
                    _SessionMap.Remove(iep);
                    _logger.Info(string.Format("服务端:IP地址:{0}的连接被移出客户端池。{1}", ip, _SessionMap.Count));
                }
                else
                {
                    _logger.Warn(string.Format("服务端:打算清理IP地址{0}时，该地址未在池中。{1}", ip, _SessionMap.Count));
                }
            }
            else
            {
                _logger.Warn(string.Format("e.AcceptSocket.RemoteEndPoint不是正确的IPEndPoint：null"));
            }
        }

        protected virtual void PrcoessReceiveData(SocketAsyncEventArgs e)
        {
            var data = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);

            foreach (KnifeSocketServerFilter filter in _FilterChain)
            {
                EndPoint endPoint = e.AcceptSocket.RemoteEndPoint;
                filter.OnDataFetched(new DataFetchedEventArgs<EndPoint>(endPoint, data)); // 触发数据到达事件
                KnifeSocketSession session = _SessionMap[endPoint];
                filter.PrcoessReceiveData(session, data); // 调用filter对数据进行处理
                if (!filter.ContinueNextFilter)
                    break;
            }

            if (!e.AcceptSocket.ReceiveAsync(e))
                BeginReceive(e);
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

        protected virtual void WirteBase(KnifeSocketSession session, byte[] data)
        {
            Socket socket = session.Connector;
            try
            {
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, AsynCallBackSend, socket);
                _logger.Trace(() => string.Format("Server.Send:{0}", data.ToHexString()));
            }
            catch
            {
                throw new SocketClientDisOpenedException(string.Format("TRY-DisOpened,远端无法连接."));
            }
        }

        protected virtual void WirteProtocol(KnifeSocketSession session, KnifeSocketProtocol protocol)
        {
            string replay = protocol.Generate();
            byte[] data = _Codec.SocketEncoder.Execute(replay);
            WirteBase(session, data);
            _logger.Trace(() => string.Format("Server.Send:{0}", replay));
        }

        #endregion
    }
}