using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using NKnife.NetWork.Common;
using NKnife.NetWork.Interfaces;
using NKnife.NetWork.Protocol;
using NKnife.Utility;
using NLog;
using Timer = System.Timers.Timer;

namespace NKnife.NetWork.SocketClient
{
    public abstract class AliveSocketClient : AblySocketClient
    {
        protected AliveSocketClient(SocketMode mode, ProtocolFamilyType family) 
            : base(mode, family)
        {
        }
    }

    /// <summary>GEAN原创精巧Socket框架客户端。
    /// Ably:[D.J.:'eɪblɪ]，adv. 精明强干地；灵巧地；巧妙地； 
    /// </summary>
    public abstract class AblySocketClient : ISocketClient
    {
        #region 成员变量

        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        /// <summary>当断线后实行自动重连的Timer
        /// </summary>
        private readonly Timer _ConnectionWhileBreakTimer;

        /// <summary>释放等待线程
        /// </summary>
        private AutoResetEvent _AutoReset;

        /// <summary>当前Socket对应的主机IP地址
        /// </summary>
        private string _Host;

        /// <summary>当前Socket对应的主机IP地址的端口号
        /// </summary>
        private int _Port;

        /// <summary>SOCKET对象
        /// </summary>
        private Socket _Socket;

        /// <summary>重连计数
        /// </summary>
        private int _ConnectionCount = 0;

        /// <summary>接收的数据队列
        /// </summary>
        private readonly ReceiveQueue _ReceiveQueue = new ReceiveQueue();

        #endregion 成员变量

        #region 构造函数

        protected AblySocketClient(SocketMode mode, ProtocolFamilyType family)
        {
            Mode = mode; 
            FamilyType = family;

            Initialize();

            try
            {
                // 初始化当连接断掉后的重连Timer
                _ConnectionWhileBreakTimer = new Timer();
                _ConnectionWhileBreakTimer.Interval = Option.HeartRange / 3;
                _ConnectionWhileBreakTimer.Elapsed += ConnectionWhileBreakTimer;
            }
            catch (Exception e)
            {
                _Logger.WarnE("断线重连Timer启动异常:", e);
            }
        }

        /// <summary>初始化Sokcet对象相关
        /// </summary>
        private void Initialize()
        {
            if (_Socket != null)
            {
                Close();
            }
            _Socket = BuildSocket();

            switch (Mode)
            {
                case SocketMode.AsyncKeepAlive:
                    {
                        _AutoReset = new AutoResetEvent(false);
                        var thread = new Thread(ReceiveQueueMonitor);
                        thread.Start();
                        break;
                    }
                case SocketMode.Talk:
                    break;
            }
        }

        private Socket BuildSocket()
        {
            var s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.SendTimeout = Option.Timeout;
            s.ReceiveTimeout = Option.Timeout;
            s.SendBufferSize = Option.BufferSize;
            s.ReceiveBufferSize = Option.BufferSize;
            return s;
        }

        /// <summary>尝试 Ping 服务器，以判断网络状态
        /// </summary>
        private static bool PingServer(string ip)
        {
            bool status = false;
            try
            {
                status = UtilityNet.NetPing(ip);
            }
            catch (Exception e)
            {
                _Logger.Trace(string.Format("Ping服务器时异常。{0}", e.Message), e);
            }
            return status;
        }

        #endregion 构造函数

        #region ISocketClient成员

        #region 属性

        /// <summary>
        /// 是否连接成功
        /// </summary>
        private bool _IsConnection;

        /// <summary>
        /// 是否发起来同步交易，即发送数据成功
        /// </summary>
        private bool _IsTalkTo;

        /// <summary>是否启用新的线程进行连接与工作,仅对同步时起作用
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is user thread; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsThreadSync { get; }

        /// <summary>是否采用抽象基类中的重连机制(较简单:在有动作触发时，发现连接断开时，按频率尝试重连服务器)
        /// </summary>
        public virtual bool IsCustomTryConnectionMode
        {
            get { return false; }
        }

        /// <summary>
        /// SocketClient选项
        /// </summary>
        /// <value>The option.</value>
        public abstract ISocketClientSetting Option { get; }

        /// <summary>
        /// 是否连接成功
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is connection suceess; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnectionSuceess
        {
            get { return _IsConnection; }
        }

        /// <summary>
        /// 远端服务器状态
        /// </summary>
        /// <value><c>true</c> if [server status]; otherwise, <c>false</c>.</value>
        public bool ServerStatus { get; private set; }

        /// <summary>
        /// Sokcet的工作模式
        /// </summary>
        /// <value>The mode.</value>
        public SocketMode Mode { get; private set; }

        /// <summary>
        /// 尝试 Ping 服务器，以判断网络状态
        /// </summary>
        public bool EnablePingServer
        {
            get { return Option.EnablePingServer; }
        }

        /// <summary>
        /// 接收到的消息的解析器
        /// </summary>
        /// <value>The decoder.</value>
        public IDatagramDecoder Decoder
        {
            get { return Option.Decoder; }
        }

        /// <summary>
        /// 字符=》;字节数组的转换器
        /// </summary>
        /// <value>The encoder.</value>
        public IDatagramEncoder Encoder
        {
            get { return Option.Encoder; }
        }

        /// <summary>
        /// 命令字解析器
        /// </summary>
        /// <value>The command parser.</value>
        public IDatagramCommandParser CommandParser
        {
            get { return Option.CommandParser; }
        }

        /// <summary>接收数据队列
        /// </summary>
        /// <value>The command parser.</value>
        public ReceiveQueue ReceiveQueue
        {
            get { return _ReceiveQueue; }
        }

        /// <summary>
        /// 协议家族
        /// </summary>
        /// <value>The type of the family.</value>
        public ProtocolFamilyType FamilyType { get; private set; }

        #endregion

        #region Socket Client 重点方法

        /// <summary>连接到指定的服务器
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public virtual bool ConnectTo(string host, int port)
        {
            _Host = host;
            _Port = port;
            if (EnablePingServer)
            {
                ServerStatus = PingServer(host);
                if (!ServerStatus)
                {
                    _Logger.Info(string.Format("尝试 Ping 服务器 {0}:{1} 未通。", host, port));
                    return false;
                }
            }
            ConnectionToMethod(host, port);

            switch (Mode)
            {
                case SocketMode.AsyncKeepAlive:
                    {
                        _AutoReset.WaitOne();
                        _AutoReset.Reset();
                        _ConnectionAutoReset.Reset();
                        break;
                    }
                case SocketMode.Talk:
                    break;
            }
            return _IsConnection;
        }
        private readonly AutoResetEvent _ConnectionAutoReset = new AutoResetEvent(false);

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public bool SendTo(string data)
        {
            return SendTo(data, false);
        }

        /// <summary>发送数据包
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        public bool SendTo(string data, bool isCompress = false)
        {
            byte[] senddata = Encoder.Execute(data, isCompress);
            var e = new SocketAsyncEventArgs();
            e.SetBuffer(senddata, 0, senddata.Length);
            bool isSuceess = false;
            if (_Socket != null)
            {
                isSuceess = _Socket.SendAsync(e);
                _Logger.Trace("To: {1},{0}", data, isSuceess);
                if (!isSuceess && IsCustomTryConnectionMode)
                {
                    //当发送不成功时，启动自动重新连接
                    _ConnectionWhileBreakTimer.Start();
                }
            }
            return isSuceess;
        }

        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="timeout">等待超时的时长</param>
        /// <returns></returns>
        public string TalkTo(string data, int timeout)
        {
            return TalkTo(data, timeout, false);
        }

        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        /// <returns></returns>
        public string TalkTo(string data, bool isCompress = false)
        {
            return TalkTo(data, Option.Timeout, isCompress);
        }

        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <returns></returns>
        public string TalkTo(string data)
        {
            return TalkTo(data, false);
        }

        /// <summary>同步发送数据
        /// </summary>
        /// <param name="data">将要发送的数据</param>
        /// <param name="timeout">等待超时的时长</param>
        /// <param name="isCompress">是否将数据压缩后发送</param>
        /// <returns></returns>
        public string TalkTo(string data, int timeout, bool isCompress = false)
        {
            // ++++++++++++ 发送部份
            if (!IsConnectionSuceess)
            {
                _Logger.Warn("远程未连接，无法发送数据。");
                _IsTalkTo = false;
                Close();
                return string.Empty;
            }
            byte[] senddata = Encoder.Execute(data, isCompress);
            if (UtilityCollection.IsNullOrEmpty(senddata))
            {
                Close();
                return string.Empty;
            }
            try
            {
                _Socket.Send(senddata);//注意，这里采用的是同步
                _IsTalkTo = true;
            }
            catch
            {
                _IsTalkTo = false;
                throw;//异常交给上层来处理
            }
            _Logger.Trace(string.Format("Client.Send: {0}", data));

            string replay = string.Empty;
            if (!_IsTalkTo)
            {
                Close();
                _Logger.Warn(string.Format("TalkTo发送数据不成功{0}", data));
                return replay; //发送数据不成功
            }

            // ++++++++++++ 接收部份
            var mainRecvBytes = new byte[Option.BufferSize];
            var currentOffset = 0;
            try
            {
                // 一次接收的临时区
                var oneRecvBytes = new byte[Option.TalkOneLength];
                int oneRecvCount = 0;

                // 持续接收，直到接收到为空时
                while ((oneRecvCount = _Socket.Receive(oneRecvBytes, 0, Option.TalkOneLength, SocketFlags.None)) > 0)
                {
                    if (currentOffset + oneRecvCount > mainRecvBytes.Length)
                        Array.Resize(ref mainRecvBytes, currentOffset + oneRecvCount + 1);
                    Array.Copy(oneRecvBytes, 0, mainRecvBytes, currentOffset, oneRecvCount);
                    currentOffset += oneRecvCount;
                }
                _Logger.Trace(string.Format("Client.Receive.Bytes.Count: {0}", currentOffset));
            }
            catch (Exception e)
            {
                _Logger.WarnE("TalkTo接收数据异常。", e);
            }

            if (currentOffset < 1)
            {
                return string.Empty;
            }

            switch (Mode)
            {
                case SocketMode.AsyncKeepAlive:
                case SocketMode.Talk:
                    break;
            }
            try
            {
                OnDataComeIn(mainRecvBytes, null); //触发字节数组数据到达事件

                int i;
                string[] recvStrings = Decoder.Execute(mainRecvBytes, out i);
                if (recvStrings.Length > 0)
                {
                    replay = recvStrings[0].TrimEnd('\0');
                }
                _Logger.Trace(string.Format("Client.Receive: {0}", replay));
            }
            catch (Exception e)
            {
                _Logger.WarnE("接收数据解析异常。", e);
            }
            return replay;
        }

        /// <summary>关闭Socket客户端
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                _Socket.Shutdown(SocketShutdown.Both);
            }
            catch {}
            try
            {
                _Socket.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 事件的定义

        /// <summary>
        /// 当Socket启动连接后发生的事件
        /// </summary>
        public event ConnectioningEventHandler ConnectioningEvent;

        /// <summary>
        /// Socket连接后事件(根据IsConnSucceed判断是否连接成功)
        /// </summary>
        public event ConnectionedEventHandler ConnectionedEvent;

        /// <summary>当断线后重新连接的事件
        /// </summary>
        public event ConnectionedWhileBreakEventHandler ConnectionedWhileBreakEvent;

        /// <summary>当有数据异步接收到后发生的事件，一般不建议使用该事件，如使用请不必对其中的数据进行解析。
        /// 可使用ReceiveDataParsedEvent事件。
        /// </summary>
        public event SocketAsyncDataComeInEventHandler DataComeInEvent;

        /// <summary>
        /// 出错或断开触发事件
        /// </summary>
        public event ConnectionBreakEventHandler ConnectionBreak;

        /// <summary>
        /// Socket连接状态发生改变
        /// </summary>
        public event SocketStatusChangedEventHandler SocketStatusChangedEvent;

        /// <summary>接收到的数据基础解析完成并转换成可用的数据后的事件
        /// </summary>
        public event ReceiveDataParsedEventHandler ReceiveDataParsedEvent;

        protected virtual void OnConnectioning(ConnectioningEventArgs e)
        {
            if (ConnectioningEvent != null)
                ConnectioningEvent(e);
        }

        protected virtual void OnConnectioned(ConnectionedEventArgs e)
        {
            if (ConnectionedEvent != null)
                ConnectionedEvent(e);
        }

        protected virtual void OnConnectionedWhileBreak(ConnectionedEventArgs e)
        {
            if (ConnectionedWhileBreakEvent != null)
                ConnectionedWhileBreakEvent(e);
        }

        protected virtual void OnDataComeIn(byte[] data, EndPoint endPoint)
        {
            if (DataComeInEvent != null)
                DataComeInEvent(data, endPoint);
        }

        protected virtual void OnConnectionBreak(ConnectionBreakEventArgs e)
        {
            if (ConnectionBreak != null)
                ConnectionBreak(e);
        }

        protected virtual void OnSocketStatusChanged(SocketStatusChangedEventArgs e)
        {
            if (SocketStatusChangedEvent != null)
                SocketStatusChangedEvent(this, e);
        }

        protected virtual void OnReceiveDataParsed(ReceiveDataParsedEventArgs e, EndPoint endPoint)
        {
            if (ReceiveDataParsedEvent != null)
            {
                try
                {
                    ReceiveDataParsedEvent(e, endPoint);
                }
                catch (Exception ex)
                {
                    _Logger.ErrorE("当数据解析完成后触发事件异常。",ex);
                }
            }
        }

        #endregion

        #region 断线重连的机制

        /// <summary>断线重连
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        protected virtual void ConnectionWhileBreakTimer(object sender, ElapsedEventArgs e)
        {
            Initialize();
            if (ConnectTo(_Host, _Port))
            {
                OnConnectionedWhileBreak(new ConnectionedEventArgs(_IsConnection, "Connection OK!"));
                _ConnectionWhileBreakTimer.Stop();
            }
        }

        #endregion

        #endregion ISocketClient成员

        #region 内部方法

        /// <summary>建立Socket连接的具体逻辑
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        protected virtual void ConnectionToMethod(string host, int port)
        {
            IPEndPoint ipPoint = null;
            try
            {
                ipPoint = new IPEndPoint(IPAddress.Parse(host), port);
            }
            catch (FormatException)
            {
                IPHostEntry p = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress s in p.AddressList)
                {
                    if (!s.IsIPv6LinkLocal)
                        ipPoint = new IPEndPoint(s, port);
                }
            }
            // 根据连接模式选用同步或是异步连接
            ConnectionToSubMethod(ipPoint);
        }

        /// <summary>建立Socket连接的具体逻辑 - 根据连接模式选用同步或是异步连接
        /// </summary>
        /// <param name="ipPoint">The ip point.</param>
        private void ConnectionToSubMethod(IPEndPoint ipPoint)
        {
            switch (Mode)
            {
                case SocketMode.AsyncKeepAlive:
                    {
                        ConnectionToSubMethodByAsync(ipPoint);
                        break;
                    }
                case SocketMode.Talk:
                    {
                        ConnectionToSubMethodBySynchro(ipPoint);
                        break;
                    }
            }
        }

        private void ConnectionToSubMethodBySynchro(IPEndPoint ipPoint)
        {
            try
            {
                _Socket.Connect(ipPoint);
                _IsConnection = true;
            }
            catch (Exception e)
            {
                _IsConnection = false;
                _Logger.Warn(string.Format("远程连接失败。{0}", e.Message), e);
            }
        }

        private void ConnectionToSubMethodByAsync(IPEndPoint ipPoint)
        {
            try
            {
                OnConnectioning(new ConnectioningEventArgs(ipPoint));
                var e = new SocketAsyncEventArgs {RemoteEndPoint = ipPoint};
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
                _Logger.ErrorE("客户端异步连接远端时异常.{0}", e);
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

        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                try
                {
                    _ConnectionCount++;
                    _IsConnection = true;
                    _AutoReset.Set();

                    OnConnectioned(new ConnectionedEventArgs(true, "Connection Success."));
                    OnSocketStatusChanged(new SocketStatusChangedEventArgs(ConnectionStatus.Normal));

                    var data = new byte[Option.BufferSize];
                    e.SetBuffer(data, 0, data.Length); //设置数据包

                    if (!_Socket.ReceiveAsync(e)) //开始读取数据包
                        CompletedEvent(this, e);
                }
                catch (Exception ex)
                {
                    _Logger.ErrorE("当成功连接时的处理发生异常.", ex);
                }
            }
            else
            {
                try
                {
                    _Logger.Debug(string.Format("当前SocketAsyncEventArgs工作状态:{0}", e.SocketError));
                    _IsConnection = false;
                    _AutoReset.Set();
                    OnConnectioned(new ConnectionedEventArgs(false, "Connection FAIL."));
                    OnSocketStatusChanged(new SocketStatusChangedEventArgs(ConnectionStatus.Break));
                }
                catch (Exception ex)
                {
                    _Logger.ErrorE("当连接未成功时的处理发生异常.", ex);
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

        private void PrcessReceiveData(SocketAsyncEventArgs e)
        {
            try
            {
                var data = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, data, 0, data.Length);
                // 触发数据到达事件
                OnDataComeIn(data, e.RemoteEndPoint);
                _ReceiveQueue.Enqueue(data);
            }
            catch (Exception ex)
            {
                _Logger.ErrorE("接收数据时读取Buffer异常。", ex);
            }
            
            try
            {
                if (_Socket != null && _Socket.Connected)//继续异步从服务端 Socket 接收数据
                    _Socket.ReceiveAsync(e);
                else
                    _Logger.Warn("Client -> 继续异步从服务端 Socket 接收数据时 Socket 无效。");
            }
            catch (Exception ex)
            {
                _Logger.ErrorE("继续异步地从服务端 Socket 接收数据异常。", ex);
            }
        }

        /// <summary>核心方法:监听 ReceiveQueue 队列
        /// </summary>
        protected void ReceiveQueueMonitor()
        {
            _Logger.Info("启动ReceiveQueue队列的监听。");
            var nodone = new byte[] {};
            var flag = true;
            while (flag)
            {
                if (_ReceiveQueue.Count > 0)
                {
                    var data = _ReceiveQueue.Dequeue();
                    var needRestart = !GetDataList(ref nodone, ref data);
                    if (needRestart)
                    {
                        flag = false;
                    }
                }
                else
                {
                    _ReceiveQueue.AutoResetEvent.WaitOne(5000); //不要无限制等下去
                }
            }
            //重启
            _ConnectionWhileBreakTimer.Start();
        }


        private bool GetDataList(ref byte[] nodone, ref byte[] data)
        {
            //TODO:待解决：半包处理有问题，如果半包开始字节不是长度字节，则永远无法处理，一直积累半包
            if (!UtilityCollection.IsNullOrEmpty(nodone))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = data.Length;
                var list = new List<byte>(data.Length + nodone.Length);
                list.AddRange(nodone);
                list.AddRange(data);
                data = list.ToArray();
                _Logger.Trace(string.Format("接包操作:半包:{0},原始包:{1},接包后:{2}", nodone.Length, srcLen, data.Length));
                nodone = new byte[] { };
            }
            int done;
            var needRestart = !DataProcessBase(null, data, out done);
            if (needRestart) return false;
            if (data.Length > done)
            {
                // 暂存半包数据，留待下条队列数据接包使用
                nodone = new byte[data.Length - done];
                Buffer.BlockCopy(data, done, nodone, 0, nodone.Length);
                _Logger.Trace(string.Format("半包数据暂存,数据长度:{0}", nodone.Length));
            }
            return true;
        }

        /// <summary>处理协议数据
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="data"></param>
        /// <param name="done"></param>
        private bool DataProcessBase(EndPoint endpoint, byte[] data, out int done)
        {
            string[] datagram = Decoder.Execute(data, out done);
            if (UtilityCollection.IsNullOrEmpty(datagram) && data.Length == done && done > 0)
            {
                _Logger.Trace(string.Format("因半包错误而重启socket"));
                return false;
            }
            if (UtilityCollection.IsNullOrEmpty(datagram))
                return true;

            foreach (string dg in datagram)
            { 
                if (string.IsNullOrWhiteSpace(dg))
                    continue;
                string command = CommandParser.GetCommand(dg);
                IProtocol protocol = Protocols.Factory(FamilyType.ToString(), command);
                _Logger.Trace(string.Format("From:命令字:{0},数据包:{1}", command, dg));
                if (protocol != null)
                {
                    protocol.Parse(dg);
                    // 触发数据基础解析后发生的数据到达事件
                    OnReceiveDataParsed(new ReceiveDataParsedEventArgs(protocol), endpoint);
                }
            }
            return true;
        }

        protected sealed class DataWrapper
        {
            public volatile EndPoint EndPoint;
            public volatile byte[] Datagram;
            public volatile Thread Thread;

            public void ThreadDispoes()
            {
                if (null != Thread)
                    Thread.Abort();
            }

            public static DataWrapper Build(EndPoint endPoint, byte[] datagram)
            {
                var sow = new DataWrapper();
                sow.EndPoint = endPoint;
                sow.Datagram = datagram;
                return sow;
            }
        }

        #endregion

        #region 多线程同步连接的创建,有待考验,先不使用

        /// <summary>
        /// 在指定时间内尝试连接指定主机上的指定端口。
        /// </summary>
        public static bool Connect(Socket socket, IPEndPoint iPAddress, int millisecondsTimeout)
        {
            var cs = new ConnectorParms();
            cs.Socket = socket;
            cs.IpEndPoint = iPAddress;
            ThreadPool.QueueUserWorkItem(ConnectThreaded, cs);
            if (cs.Completed.WaitOne(millisecondsTimeout, false))
            {
                if (cs.Socket != null)
                    return true;
                throw cs.Exception;
            }
            else
            {
                cs.Abort();
                throw new SocketException();
            }
        }

        private static void ConnectThreaded(object state)
        {
            var cs = (ConnectorParms) state;
            cs.Thread = Thread.CurrentThread;
            try
            {
                if (cs.Aborted)
                {
                    try
                    {
                        cs.Socket.Close();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    cs.Socket.Connect(cs.IpEndPoint);
                    cs.Completed.Set();
                }
            }
            catch (Exception e)
            {
                cs.Exception = e;
                cs.Completed.Set();
            }
        }

        private sealed class ConnectorParms
        {
            public readonly ManualResetEvent Completed = new ManualResetEvent(false);
            public volatile bool Aborted;
            public volatile Exception Exception;
            public IPEndPoint IpEndPoint;
            public volatile Socket Socket;
            public volatile Thread Thread;

            public void Abort()
            {
                if (Aborted != true)
                {
                    Aborted = true;
                    try
                    {
                        Thread.Abort();
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion
    }
}