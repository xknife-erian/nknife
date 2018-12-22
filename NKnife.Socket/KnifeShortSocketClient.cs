using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common.Logging;
using NKnife.Extensions;
using NKnife.IoC;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    /// <summary>
    /// 实现了socket短连接客户端，使用异步事件模型，
    /// 一次交互依照连接-发送-接收-断开的顺序执行，每一步都是异步
    /// 没有实现发送指令的缓冲队列，因此每次发送都是等上次发送动作完成（成功或异常）后才能进行
    /// 可以扩展该类，增加发送指令缓冲队列，或增加通过发送接收方法等
    /// </summary>
    public class KnifeShortSocketClient : ISocketClient, IDisposable 
    {
        private static readonly ILog _Logger = LogManager.GetLogger<KnifeShortSocketClient>();

        #region 成员变量
        protected IPAddress _IpAddress;
        protected int _Port;

        protected SocketClientConfig _Config = Di.Get<SocketClientConfig>();
        protected EndPoint _EndPoint;

        private bool _onSending; //true 正在进行发送操作, false表示发送动作完成

        /// <summary>
        ///     SOCKET对象
        /// </summary>
        //protected Socket _Socket;
        protected SocketSession _SocketSession;

        private SocketAsyncEventArgs _socketAsyncEventArgs;

        #endregion 成员变量

        #region IKnifeSocketClient接口

        public SocketConfig Config
        {
            get { return _Config; }
            set { _Config = (SocketClientConfig)value; }
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
            return true;
        }

        public bool Stop()
        {
            var handler = SessionBroken;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));

            try
            {
                _SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                _SocketSession.AcceptSocket.Disconnect(true);
                _SocketSession.AcceptSocket.Close();
                return true;
            }
            catch (Exception e)
            {
                _Logger.Debug("Socket客户端Shutdown异常。", e);
                return false;
            }
        }

        public void Send(long id, byte[] data)
        {
            AsyncSendData(data);
        }

        public void SendAll(byte[] data)
        {
            AsyncSendData(data);
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
                throw new ObjectDisposedException(GetType().FullName + " is Disposed");

            var ipPoint = new IPEndPoint(_IpAddress, _Port);
            _SocketSession = Di.Get<SocketSession>();
//            SocketSession.Id = ipPoint;
            _socketAsyncEventArgs = new SocketAsyncEventArgs{RemoteEndPoint = ipPoint};
            _socketAsyncEventArgs.Completed += AsynCompleted;
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

        ~KnifeShortSocketClient()
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

        #region 异步动作

        /// <summary>
        /// 异步连接
        /// </summary>
        protected virtual void AsyncConnect()
        {
            try
            {
                if (_SocketSession.AcceptSocket == null || !_SocketSession.AcceptSocket.Connected)
                {
                    _SocketSession.AcceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                        ProtocolType.Tcp)
                    {
                        SendTimeout = Config.SendTimeout,
                        ReceiveTimeout = Config.ReceiveTimeout,
                        SendBufferSize = Config.MaxBufferSize,
                        ReceiveBufferSize = Config.ReceiveBufferSize
                    };
                }

                if (!_SocketSession.AcceptSocket.ConnectAsync(_socketAsyncEventArgs))
                        ProcessConnect(_socketAsyncEventArgs);
            }
            catch (Exception e)
            {
                _Logger.Error(string.Format("客户端异步连接远端时异常.{0}", e));
                SwitchOnSending(false);
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
                            _Logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));

                            var handler = SessionBuilt;
                            if (handler != null)
                            {
                                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
                            }

                            _Logger.Debug("client连接成功，开始发送数据");

                            if (!_SocketSession.AcceptSocket.SendAsync(e))
                                ProcessSend(e);
                        }
                        catch (Exception ex)
                        {
                            _Logger.Warn("当成功连接时的处理发生异常.", ex);
                            SwitchOnSending(false);
                        }
                        break;
                    }
                default: //连接失败
                    {

                        _Logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                        _Logger.Warn(string.Format("连接失败:{0}", e.SocketError));
                        SwitchOnSending(false);
                        break;
                    }
            }
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

                        _Logger.Info(string.Format("Client接收数据完成，主动中断连接。"));
                        ProcessConnectionBrokenActive();
                    }
                    else
                    {
                        _Logger.Info(string.Format("Client接收时发现无数据。"));
                        ProcessConnectionBrokenPassive();
                    }
                }
                else //连接异常了
                {
                    _Logger.Info(string.Format("Client接收时发现连接中断。"));
                    ProcessConnectionBrokenPassive();
                }
                SwitchOnSending(false);
            }
            catch (Exception ex) //接收处理异常了
            {
                ProcessConnectionBrokenPassive();
                SwitchOnSending(false);
            }
        }

        /// <summary>
        /// 处理连接被动中断，从远端发起的中断，本地接收或发送时出现异常货socket已经释放的情况
        /// </summary>
        protected virtual void ProcessConnectionBrokenPassive()
        {
            var handler = SessionBroken;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
        }

        protected virtual void ProcessConnectionBrokenActive()
        {
            try
            {
                _Logger.Debug("KnifeSocketClient执行主动断开");
                _SocketSession.AcceptSocket.Shutdown(SocketShutdown.Both);
                _SocketSession.AcceptSocket.Disconnect(true);
                _SocketSession.AcceptSocket.Close();
            }
            catch (Exception e)
            {
                _Logger.Debug("Socket客户端Shutdown异常。", e);
            }

            var handler = SessionBroken;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
        }

        protected virtual void PrcessReceiveData(SocketAsyncEventArgs e)
        {
            var data = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);

            var handler = DataReceived;
            if (handler != null)
            {
//                SocketSession.Id = EndPoint;
                _SocketSession.Data = data;
                handler.Invoke(this, new SessionEventArgs(_SocketSession));
            }
        }

        protected virtual void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success) //发送正常
            {
                try
                {
                    //发送成功
                    _Logger.Debug(string.Format("ClientSend: {0}", e.Buffer.ToHexString()));
                    var dataSentHandler = DataSent;
                    if (dataSentHandler != null)
                    {
                        dataSentHandler.Invoke(this,
                            new SessionEventArgs(new TunnelSession()
                            {
//                                Id = e.RemoteEndPoint,
                                Data = e.Buffer
                            }));
                    }
                    //开始接收
                    var data = new byte[Config.ReceiveBufferSize];
                    e.SetBuffer(data, 0, data.Length); //设置数据包
                    if (!_SocketSession.AcceptSocket.ReceiveAsync(e)) //开始接收数据包
                        ProcessReceive(e);
                }
                catch (Exception ex)
                {
                    _Logger.Warn("当成功发送时的处理发生异常.", ex);
                    ProcessConnectionBrokenPassive();
                    SwitchOnSending(false);
                }
            }
            else
            {
                _Logger.Warn(string.Format("Client发送时发现连接中断。"));
                ProcessConnectionBrokenPassive();
                SwitchOnSending(false);
            }
        }

        private void ProcessDisconnectAndCloseSocket(SocketAsyncEventArgs receiveSendEventArgs)
        {
            try
            {
                _SocketSession.AcceptSocket.Close();
                _SocketSession.AcceptSocket = null;

                var handler = SessionBroken;
                if (handler != null)
                    handler.Invoke(this, new SessionEventArgs(new TunnelSession()));
            }
            catch (Exception e)
            {
                _Logger.Warn("关闭Socket客户端异常。", e);
            }
        }

        private void SwitchOnSending(bool status)
        {
            _socketAsyncEventArgs.SetBuffer(null,0,0);
            _onSending = status;
        }
        #endregion

        #region 发送消息

        public void AsyncSendData(byte[] data, int timeout = 5000)
        {
            int localCount = 0;
            int timeoutCount = timeout/100;
            while (_onSending)
            {
                Thread.Sleep(100);
                localCount += 1;
                if (localCount > timeoutCount)
                {
                    _Logger.Warn(string.Format("AsyncSendData发送超时,timeout={0}",timeout));
                    return;
                }
            }
            SwitchOnSending(true);
            _socketAsyncEventArgs.SetBuffer(data, 0, data.Length);
            AsyncConnect();
        }
        #endregion


    }
}
