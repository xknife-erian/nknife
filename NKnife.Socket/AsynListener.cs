using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NLog;

namespace SocketKnife
{
    public class AsynListener : Component
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        private readonly ManualResetEvent _AllDone = new ManualResetEvent(false);
        private readonly ManualResetEvent _SendDone = new ManualResetEvent(false);
        private bool _IsWhileListen = true;
        private int _BufferSize = 2048;
        private Socket _Listener;
        private string _TcpIpServerIp = "";
        private int _TcpIpServerPort = 22033;
        private Thread _Thread;

        #region 属性

        /// <summary>
        ///     得到或设置服务器IP地址
        /// </summary>
        public string IpAddress
        {
            get { return _TcpIpServerIp; }
            set { _TcpIpServerIp = value; }
        }

        /// <summary>
        ///     得到或设置服务器所使用的端口
        /// </summary>
        public int Port
        {
            get { return _TcpIpServerPort; }
            set { _TcpIpServerPort = value; }
        }

        /// <summary>
        ///     得到或设置服务端缓冲器大小
        /// </summary>
        public int BufferSize
        {
            get { return _BufferSize; }
            set { _BufferSize = value; }
        }

        /// <summary>
        ///     得到当前连接状态 false为断开 true为连接
        /// </summary>
        public bool Active
        {
            get { return _Listener.Connected; }
        }

        #endregion

        #region 开始监听

        /// <summary>
        ///     开始监听访问
        /// </summary>
        public void Listening()
        {
            _Thread = new Thread(StartListening) {IsBackground = true, Name = "AsynListener-Listening"};
            _Thread.Start();
        }

        /// <summary>
        ///     开始监听
        /// </summary>
        private void StartListening()
        {
            try
            {
                _Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress ipAddress = _TcpIpServerIp.Trim() == "" ? IPAddress.Any : IPAddress.Parse(_TcpIpServerIp);
                var localEndPoint = new IPEndPoint(ipAddress, _TcpIpServerPort);

                _Listener.Bind(localEndPoint);
                _Listener.Listen(8);

                while (_IsWhileListen)
                {
                    _AllDone.Reset();
                    _Listener.BeginAccept(AcceptCallback, _Listener);
                    _AllDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                OnHasListenerException(new ListenerExceptionEventArgs(e, _Listener));
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
            Socket handler = null;
            try
            {
                var listener = (Socket) ar.AsyncState;
                handler = listener.EndAccept(ar);
                var state = new StateObject(_BufferSize, handler) {Client = handler};
                handler.BeginReceive(state.Buffer, 0, _BufferSize, 0, ReadCallback, state);
            }
            catch (Exception e)
            {
                OnHasListenerException(new ListenerExceptionEventArgs(e, handler));
            }
            finally
            {
                _AllDone.Set();
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

                    int bytesRead = client.EndReceive(ar);

                    if (bytesRead > 0)
                    {
                        state.StringBuilder.Append(Encoding.Default.GetString(state.Buffer, 0, bytesRead));
                        String content = state.StringBuilder.ToString();
                        if (content.IndexOf("@", StringComparison.Ordinal) > -1)
                        {
                            OnReceivedData(new ReceivedDataEventArgs(content, client));
                            state.Reset();
                        }
                        if (client.Connected)
                        {
                            client.BeginReceive(state.Buffer, 0, state.Buffer.Length, 0, ReadCallback, state);
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

        #endregion

        #region 发送

        public void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.Default.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, handler);
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
                int bytesSent = client.EndSend(ar);
                _Logger.Info("Sent {0} bytes to client.", bytesSent);
            }
            catch (Exception e)
            {
                OnHasListenerException(new ListenerExceptionEventArgs(e, client));
            }
            finally
            {
                _SendDone.Set();
            }
        }

        #endregion

        #region 构造函数,类基本函数

        /// <summary>
        ///     构造函数 带参数
        /// </summary>
        /// <param name="container"></param>
        public AsynListener(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        public AsynListener()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     重写Dispose函数
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            Abort();
        }

        /// <summary>
        ///     中止服务
        /// </summary>
        public void Abort()
        {
            if (_Thread != null)
            {
                _IsWhileListen = false;
                _Listener.Close();
                _Thread.Abort();
            }
        }

        private void InitializeComponent()
        {
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
            private readonly Exception _Exception;
            private readonly Socket _ServerSocket;

            public ListenerExceptionEventArgs(Exception exception, Socket serverSocket)
            {
                _Exception = exception;
                _ServerSocket = serverSocket;
            }

            public Exception Exception
            {
                get { return _Exception; }
            }

            public Socket ServerSocket
            {
                get { return _ServerSocket; }
            }
        }

        public class ReceivedDataEventArgs : EventArgs
        {
            private readonly string _Data;
            private readonly Socket _Client;
            public ReceivedDataEventArgs(string data, Socket client)
            {
                _Data = data;
                this._Client = client;
            }

            public Socket Client { get { return _Client; } }

            public string Data
            {
                get { return _Data; }
            }
        }

        private class StateObject
        {
            public byte[] Buffer { get; private set; }

            public Socket Client { get; set; }

            public StateObject(int bufferSize, Socket workSocket)
            {
                Buffer = new byte[bufferSize];
                Client = workSocket;
                StringBuilder = new StringBuilder(bufferSize);
            }

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