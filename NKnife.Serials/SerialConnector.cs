using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using NLog;

namespace NKnife.Serials
{
    /// <summary>
    ///     对串口的操作进行更易用的封装，并兼容Linux。特点：受控读取串口数据。
    /// </summary>
    public sealed class SerialConnector
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 串口连接工作锁
        /// </summary>
        private readonly object _accessLock = new object();

        /// <summary>
        /// 连接状态侦听线程
        /// </summary>
        private Thread _connectionWatcher;
        /// <summary>
        /// 读串口线程
        /// </summary>
        private Thread _reader;

        /// <summary>
        /// Read/Write错误状态标记，当false时连接线程将启动重连机制
        /// </summary>
        private bool _gotReadWriteError = true;
        private bool _disconnectRequested;
        private string _portName = "";

        private SerialConfig _serialConfig;
        private SerialPort _serialPort;

        #region 事件

        /// <summary>
        ///     定义委托：当连接状态发生变化时。
        /// </summary>
        public delegate void ConnectionStatusChangedEventHandler(object sender, ConnectionStatusChangedEventArgs args);

        /// <summary>
        ///     当连接状态发生变化时。
        /// </summary>
        public event ConnectionStatusChangedEventHandler ConnectionStatusChanged;

        /// <summary>
        ///     定义委托：当收到数据时。
        /// </summary>
        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs args);

        /// <summary>
        ///     当收到数据时。
        /// </summary>
        public event MessageReceivedEventHandler MessageReceived;

        private void OnConnectionStatusChanged(ConnectionStatusChangedEventArgs args)
        {
            ConnectionStatusChanged?.Invoke(this, args);
        }

        private void OnMessageReceived(MessageReceivedEventArgs args)
        {
            MessageReceived?.Invoke(this, args);
        }

        #endregion

        #region 外部方法

        /// <summary>
        /// 连接
        /// </summary>
        public bool Connect()
        {
            if (_disconnectRequested)
                return false;
            lock (_accessLock)
            {
                Disconnect();
                Open();
                _connectionWatcher = new Thread(ConnectionWatcherTask);
                _connectionWatcher.Start();
            }

            return IsConnected;
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void Disconnect()
        {
            if (_disconnectRequested)
                return;
            _disconnectRequested = true;
            Close();
            lock (_accessLock)
            {
                if (_connectionWatcher != null)
                {
                    if (!_connectionWatcher.Join(5000))
                        _connectionWatcher.Abort();
                    _connectionWatcher = null;
                }

                _disconnectRequested = false;
            }
        }

        /// <summary>
        ///     是否连接
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool IsConnected => _serialPort != null && !_gotReadWriteError && !_disconnectRequested;

        /// <summary>
        ///     获取与设置侦听串口数据时的间隔时间。默认20ms。
        /// </summary>
        public uint IntervalBetweenReadings { get; set; } = 20;

        /// <summary>
        ///     设置串口
        /// </summary>
        /// <param name="portName">串口名</param>
        /// <param name="config">串口配置</param>
        public void SetPort(string portName, SerialConfig config)
        {
            if (_portName != portName)
            {
                // 当串口名发生变化时，置重连标记
                _gotReadWriteError = true;
            }

            _portName = portName;
            _serialConfig = config;
        }

        /// <summary>
        ///     发送字节数组数据
        /// </summary>
        public bool SendMessage(params byte[] message)
        {
            var success = false;
            if (IsConnected)
                try
                {
                    _serialPort.Write(message, 0, message.Length);
                    success = true;
                }
                catch (Exception e)
                {
                    _Logger.Warn(e, e.Message);
                }

            return success;
        }

        /// <summary>
        ///     发送字符串数据
        /// </summary>
        public bool SendMessage(string str)
        {
            var success = false;
            if (IsConnected)
                try
                {
                    _serialPort.WriteLine(str);
                    success = true;
                }
                catch (Exception e)
                {
                    _Logger.Warn(e, e.Message);
                }

            return success;
        }

        #endregion

        #region 内部方法

        #region 串口控制

        private bool Open()
        {
            var success = false;
            lock (_accessLock)
            {
                Close();
                try
                {
                    var tryOpen = true;
                    if (Environment.OSVersion.Platform.ToString().StartsWith("Win") == false) 
                        tryOpen = (File.Exists(_portName));
                    if (tryOpen)
                    {
                        _serialPort = new SerialPort();
                        _serialPort.ErrorReceived += HandleErrorReceived;
                        _serialPort.PortName = _portName;
                        _serialPort.BaudRate = (int) _serialConfig.BaudRate;
                        _serialPort.StopBits = _serialConfig.StopBit;
                        _serialPort.Parity = _serialConfig.Parity;
                        _serialPort.DataBits = (int) _serialConfig.DataBit;
                        _serialPort.DtrEnable = _serialConfig.DtrEnable;
                        _serialPort.RtsEnable = _serialConfig.RtsEnable;

                        // 没有使用serialPort.DataReceived事件来接收数据，因为这在Linux/Mono下是行不通的。
                        // 使用readerTask来代替（见下文）。
                        _serialPort.Open();
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    _Logger.Error(e);
                    Close();
                }

                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _gotReadWriteError = false;
                    // 启动一个串口的读取线程
                    _reader = new Thread(ReaderTask) {Name = $"{_portName}-ReaderTask", IsBackground = true};
                    _reader.Start();
                    OnConnectionStatusChanged(new ConnectionStatusChangedEventArgs(true));
                }
            }

            return success;
        }

        private void Close()
        {
            lock (_accessLock)
            {
                // Stop the Reader task
                if (_reader != null)
                {
                    if (!_reader.Join(300))
                        _reader.Abort();
                    _reader = null;
                }

                if (_serialPort != null)
                {
                    _serialPort.ErrorReceived -= HandleErrorReceived;
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Close();
                        OnConnectionStatusChanged(new ConnectionStatusChangedEventArgs(false));
                    }

                    _serialPort.Dispose();
                    _serialPort = null;
                }

                _gotReadWriteError = true;
            }
        }

        private void HandleErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            var error = string.Empty;
            switch (e.EventType)
            {
                case SerialError.Frame:
                    error = "硬件检测到一个组帧(分帧)错误。";
                    break;
                case SerialError.Overrun:
                    error = "发生字符串缓冲区溢出，下一个字符会被丢失";
                    break;
                case SerialError.RXOver:
                    error = "发生输入缓冲区溢出。输入缓冲区空间不足，或在文件尾 (EOF) 字符之后接收到字符。";
                    break;
                case SerialError.RXParity:
                    error = "硬件检测到奇偶校验错误。";
                    break;
                case SerialError.TXFull:
                    error = "应用程序尝试传输一个字符，但是输出缓冲区已满。";
                    break;
            }

            _Logger.Warn($"ErrorReceived: {e.EventType},{error}");
        }

        #endregion

        #region 背景任务

        private void ReaderTask()
        {
            while (IsConnected)
            {
                try
                {
                    var msgLen = _serialPort.BytesToRead;
                    if (msgLen > 0)
                    {
                        var message = new byte[_serialPort.BytesToRead];
                        var readBytes = 0;
                        while (_serialPort.Read(message, readBytes, message.Length - readBytes) <= 0)
                        {
                            // 当没有读到数据时，就别歇着了
                            Thread.Sleep(1);
                        }

                        if (MessageReceived != null)
                            OnMessageReceived(new MessageReceivedEventArgs(message)); //new ArraySegment<byte>(message)));
                    }
                    else
                    {
                        Thread.Sleep((int) IntervalBetweenReadings);
                    }
                }
                catch (Exception e)
                {
                    _Logger.Warn(e, e.Message);
                    _gotReadWriteError = true;
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// 这个任务负责自动重新连接接口。
        /// 当连接中断或发生I/O错误时，会出现以下情况。
        /// </summary>
        private void ConnectionWatcherTask()
        {
            while (!_disconnectRequested)
            {
                if (_gotReadWriteError)
                {
                    try
                    {
                        Close();
                        // 等待半秒后重连
                        Thread.Sleep(500);
                        if (!_disconnectRequested)
                        {
                            try
                            {
                                Open();
                            }
                            catch (Exception e)
                            {
                                _Logger.Error(e);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _Logger.Error(e);
                    }
                }

                //每半秒检查一次是否需求重连
                if (!_disconnectRequested)
                    Thread.Sleep(500);
            }
        }

        #endregion

        #endregion
    }
}