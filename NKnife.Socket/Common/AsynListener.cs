using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Common.Logging;

namespace SocketKnife.Common
{
    /// <summary>
    ///     一个应用在较简单场景且压力较小情况下的SocketServer。
    /// </summary>
    public class AsynListener : IDisposable
    {
        private static readonly ILog _Logger = LogManager.GetLogger<AsynListener>();
        private readonly ManualResetEvent _allDone = new ManualResetEvent(false);
        private readonly ManualResetEvent _sendDone = new ManualResetEvent(false);
        private int _bufferSize = 128;
        private bool _isWhileListen = true;
        private string _tcpIpServerIp = "";
        private int _tcpIpServerPort = 22033;

        #region 属性

        /// <summary>
        ///     获取或设置服务器IP地址
        /// </summary>
        public string IpAddress
        {
            get { return _tcpIpServerIp; }
            set { _tcpIpServerIp = value; }
        }

        /// <summary>
        ///     获取或设置服务器所使用的端口
        /// </summary>
        public int Port
        {
            get { return _tcpIpServerPort; }
            set { _tcpIpServerPort = value; }
        }

        /// <summary>
        ///     获取或设置服务端缓冲器大小
        /// </summary>
        public int BufferSize
        {
            get { return _bufferSize; }
            set { _bufferSize = value; }
        }

        /// <summary>
        ///     获取当前连接状态：false为断开 true为连接
        /// </summary>
        public bool Active
        {
            get { return _listener.Connected; }
        }

        /// <summary>
        ///     结束符
        /// </summary>
        public char Tail { get; set; }

        #endregion

        #region 开始、关闭监听

        private Socket _listener;
        private Thread _thread;

        /// <summary>
        ///     开启新线程监听访问
        /// </summary>
        public void BeginListening()
        {
            var id = Guid.NewGuid().ToString("N").Substring(0, 6);
            _thread = new Thread(BeginListeningThreadStart) {IsBackground = true, Name = string.Format("AsynListener-Listening-{0}", id)};
            _thread.Start();
        }

        /// <summary>
        ///     开始监听
        /// </summary>
        private void BeginListeningThreadStart()
        {
            try
            {
                _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                var ipAddress = _tcpIpServerIp.Trim() == "" ? IPAddress.Any : IPAddress.Parse(_tcpIpServerIp);
                var localEndPoint = new IPEndPoint(ipAddress, _tcpIpServerPort);

                _listener.Bind(localEndPoint);
                _listener.Listen(16);

                while (_isWhileListen)
                {
                    _allDone.Reset();
                    _listener.BeginAccept(AcceptCallback, _listener);
                    _allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                OnHasListenerException(new ListenerExceptionEventArgs(e, _listener));
            }
        }

        public void EndListening()
        {
            try
            {
                _allDone.Close();
                _sendDone.Close();
                _listener.Close(1);
                _isWhileListen = false;
                _thread.Abort();
            }
            catch (Exception e)
            {
                _Logger.Warn(string.Format("Listen关闭时有导常"), e);
            }
        }

        #endregion

        #region 接收

        /// <summary>
        ///     接收回调
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket client = null;
            try
            {
                var listener = (Socket) ar.AsyncState;
                client = listener.EndAccept(ar);
                _Logger.Trace(string.Format("监听到客户端连接：{0}", client.RemoteEndPoint));
                var state = new StateObject(_bufferSize, client) {Client = client};
                client.BeginReceive(state.Buffer, 0, _bufferSize, 0, ReadCallback, state);
            }
            catch (Exception e)
            {
                OnHasListenerException(new ListenerExceptionEventArgs(e, client));
            }
            finally
            {
                if (_isWhileListen)
                    _allDone.Set();
            }
        }


        /// <summary>
        ///     读取回调内容
        /// </summary>
        /// <param name="ar">异步结果</param>
        private void ReadCallback(IAsyncResult ar)
        {
            Socket client = null;
            try
            {
                lock (ar)
                {
                    var state = (StateObject) ar.AsyncState;
                    client = state.Client;

                    var bytesRead = client.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        string content;
                        var isComplate = ReceivedChecker(state, bytesRead, out content);
                        if (isComplate)
                        {
                            OnReceivedData(new ReceivedDataEventArgs(content, client));
                            state.Reset();
                        }
                        if (client.Connected)
                        {
                            client.BeginReceive(state.Buffer, 0, state.Buffer.Length, 0, ReadCallback, state);
                        }
                        else
                        {
                            state.Reset();
                        }
                    }
                    else
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                    }
                }
            }
            catch (Exception e)
            {
                OnHasListenerException(new ListenerExceptionEventArgs(e, client));
            }
        }

        /// <summary>
        ///     子类可重写。当收到数据后的处理(协议的解析)方式及是否收取完成的校验。
        /// </summary>
        protected virtual bool ReceivedChecker(StateObject state, int bytesRead, out string content)
        {
            state.StringBuilder.Append(Encoding.Default.GetString(state.Buffer, 0, bytesRead));
            content = state.StringBuilder.ToString();
            return content.IndexOf(Tail) > -1;
        }

        #endregion

        #region 发送

        /// <summary>
        ///     子类可重写。给指定的客户端发送数据。
        /// </summary>
        /// <param name="client">指定的客户端</param>
        /// <param name="data">数据</param>
        public virtual void Send(Socket client, string data)
        {
            _Logger.Trace(string.Format("Send:{0}", data));
            var byteData = Encoding.Default.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, client);
        }

        public virtual void Send(Socket client, byte[] data)
        {
            client.BeginSend(data, 0, data.Length, 0, SendCallback, client);
        }


        /// <summary>
        ///     发送回调内容
        /// </summary>
        /// <param name="ar">异步结果</param>
        private void SendCallback(IAsyncResult ar)
        {
            var client = (Socket) ar.AsyncState;
            try
            {
                var bytesSent = client.EndSend(ar);
                _Logger.Debug(string.Format("Sent {0} bytes to client.", bytesSent));
            }
            catch (Exception e)
            {
                OnHasListenerException(new ListenerExceptionEventArgs(e, client));
            }
            finally
            {
                _sendDone.Set();
            }
        }

        #endregion

        #region 构造函数,类基本函数

        /// <summary>
        ///     构造函数 带参数
        /// </summary>
        public AsynListener()
        {
            Tail = '`';
        }

        public void Dispose()
        {
            EndListening();
        }

        #endregion

        #region 事件

        /// <summary>
        ///     接收到数据事件
        /// </summary>
        public event EventHandler<ReceivedDataEventArgs> ReceivedData;

        /// <summary>
        ///     发生错误事件
        /// </summary>
        public event EventHandler<ListenerExceptionEventArgs> HasListenerException;

        protected virtual void OnReceivedData(ReceivedDataEventArgs e)
        {
            if (ReceivedData != null)
            {
                ReceivedData(this, e);
            }
        }

        protected virtual void OnHasListenerException(ListenerExceptionEventArgs e)
        {
            if (HasListenerException != null)
            {
                HasListenerException(this, e);
            }
        }

        #endregion

        #region 内部类

        public class ListenerExceptionEventArgs : EventArgs
        {
            private readonly Exception _exception;
            private readonly Socket _serverSocket;

            public ListenerExceptionEventArgs(Exception exception, Socket serverSocket)
            {
                _exception = exception;
                _serverSocket = serverSocket;
            }

            public Exception Exception
            {
                get { return _exception; }
            }

            public Socket ServerSocket
            {
                get { return _serverSocket; }
            }
        }

        public class ReceivedDataEventArgs : EventArgs
        {
            private readonly Socket _client;
            private readonly string _data;

            public ReceivedDataEventArgs(string data, Socket client)
            {
                _data = data;
                _client = client;
            }

            public Socket Client
            {
                get { return _client; }
            }

            public string Data
            {
                get { return _data; }
            }
        }

        protected class StateObject
        {
            public StateObject(int bufferSize, Socket workSocket)
            {
                Buffer = new byte[bufferSize];
                Client = workSocket;
                StringBuilder = new StringBuilder(bufferSize);
            }

            public byte[] Buffer { get; private set; }
            public Socket Client { get; set; }
            public StringBuilder StringBuilder { get; private set; }

            public void Reset()
            {
                Buffer = new byte[Buffer.Length];
                StringBuilder = new StringBuilder(Buffer.Length);
            }
        }

        #endregion
    }
}