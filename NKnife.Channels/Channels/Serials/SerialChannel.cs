using System;
using System.IO.Ports;
using System.Threading;
using Common.Logging;
using NKnife.Channels.Channels.Base;
using NKnife.Channels.Channels.EventParams;
using NKnife.Channels.Interfaces.Channels;

namespace NKnife.Channels.Channels.Serials
{
    public class SerialChannel : ChannelBase<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialChannel>();
        private readonly SerialConfig _SerialConfig;
        private SerialPort _SerialPort; //串口操作类（通过.net 类库）

        public SerialChannel(SerialConfig serialConfig)
        {
            if(serialConfig==null)
                throw new ArgumentNullException(nameof(serialConfig), $"{nameof(serialConfig)}不能为空");
            _SerialConfig = serialConfig;
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _logger.Warn($"SerialPortErrorReceived:{e.EventType}");
            _SerialPort.DiscardInBuffer();
        }

        #region Overrides of ChannelBase

        /// <summary>
        ///     打开采集通道
        /// </summary>
        /// <returns></returns>
        public override bool Open()
        {
            _SerialPort = new SerialPort
            {
                PortName = $"COM{_SerialConfig.Port}",
                StopBits = _SerialConfig.StopBits,
                BaudRate = _SerialConfig.BaudRate,
                DataBits = _SerialConfig.DataBits, //8,
                WriteTimeout = _SerialConfig.WriteTotalTimeoutConstant, //1000
                ReadTimeout = _SerialConfig.ReadTotalTimeoutConstant, //1000,
                ReceivedBytesThreshold = _SerialConfig.ReceivedBytesThreshold, //1,
                ReadBufferSize = _SerialConfig.ReadBufferSize, //64,
                DtrEnable = _SerialConfig.DtrEnable,
                Parity = _SerialConfig.Parity,
                RtsEnable = _SerialConfig.RtsEnable
            };

            if (IsSynchronous)
            {
                _SerialPort.DataReceived += SyncSerialPortDataReceived;
            }
            else
            {
                _SerialPort.DataReceived += AsyncDataReceived;
            }

            _SerialPort.ErrorReceived += SerialPortErrorReceived;

            try
            {
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                }
                OnOpening();
                var openReset = new AutoResetEvent(false);
                var thread = new Thread(() =>
                {
                    try
                    {
                        _logger.Debug($"通讯:准备打开串口:{_SerialPort.PortName}:{_SerialPort.BaudRate}......");
                        _SerialPort.Open();
                        IsOpen = true;
                    }
                    catch (Exception e)
                    {
                        IsOpen = false;
                        _logger.Warn(e.Message, e);
                    }
                    finally
                    {
                        openReset.Set();
                    }
                }) {Name = $"{_SerialPort.PortName}-Thread"};
                thread.IsBackground = true;
                thread.Start();
                openReset.WaitOne(1200); //打开串口等待1.2秒
                if (IsOpen)
                {
                    OnOpened();
                    _logger.Info($"通讯:成功打开串口:{_SerialPort.PortName}:{_SerialPort.BaudRate}");
                }
                else
                {
                    _logger.Warn($"无法打开串口:COM{_SerialConfig.Port}");
                    thread.Abort();
                }
            }
            catch (Exception e)
            {
                _logger.Warn($"无法打开串口:COM{_SerialConfig.Port}, {e.Message}", e);
                IsOpen = false;
            }
            return IsOpen;
        }

        #region Overrides of ChannelBase

        protected override void OnChannelModeChanged(ChannelModeChangedEventArgs e)
        {
            if (_SerialPort == null)
            {
                base.OnChannelModeChanged(e);
                return;
            }
            if (e.IsSynchronous)
            {
                _SerialPort.DataReceived += SyncSerialPortDataReceived;
                _SerialPort.DataReceived -= AsyncDataReceived;
            }
            else
            {
                _SerialPort.DataReceived += AsyncDataReceived;
                _SerialPort.DataReceived -= SyncSerialPortDataReceived;
            }
            base.OnChannelModeChanged(e);
        }

        #endregion

        /// <summary>
        ///     关闭采集通道
        /// </summary>
        /// <returns></returns>
        public override bool Close()
        {
            if (_SerialPort.IsOpen)
            {
                try
                {
                    OnCloseing();
                }
                catch (Exception e)
                {
                    _logger.Warn($"执行关闭串口前事件异常:{_SerialPort.PortName}。{e.Message}", e);
                }
                try
                {
                    if (IsSynchronous)
                        _SerialPort.DataReceived -= SyncSerialPortDataReceived;
                    else
                        _SerialPort.DataReceived -= AsyncDataReceived;
                    if (_AutoSendThread != null && _AutoSendThread.IsAlive)
                        _AutoSendThread.Abort();
                    _StatusChecker.IsStopFlag = true;
                    _SerialPort.Close();
                    _logger.Info($"通讯:成功关闭串口:{_SerialPort.PortName}。");
                }
                catch (Exception e)
                {
                    _logger.Warn($"关闭串口异常:{_SerialPort.PortName}, {e.Message}", e);
                    return false;
                }
                try
                {
                    OnClosed();
                }
                catch (Exception e)
                {
                    _logger.Warn($"执行关闭串口后事件异常:{_SerialPort.PortName}。{e.Message}", e);
                }
            }
            IsOpen = false;
            return true;
        }

        private SerialQuestionGroup _QuestionGroup;

        /// <summary>
        ///     更新即将发送的数据
        /// </summary>
        /// <param name="questionGroup">即将发送的数据</param>
        public override void UpdateQuestionGroup(IQuestionGroup<byte[]> questionGroup)
        {
            if (!(questionGroup is SerialQuestionGroup))
                throw new ArgumentException(nameof(questionGroup), $"{nameof(questionGroup)} need is {(typeof (SerialQuestionGroup)).Name}");
            UpdateQuestionGroup((SerialQuestionGroup) questionGroup);
        }

        /// <summary>
        ///     更新即将发送的数据
        /// </summary>
        /// <param name="questionGroup">即将发送的数据</param>
        public void UpdateQuestionGroup(SerialQuestionGroup questionGroup)
        {
            _QuestionGroup = questionGroup;
        }

        /// <summary>
        ///     设置串口读取超时
        /// </summary>
        /// <param name="writeTotalTimeoutConstant">写超时</param>
        /// <param name="readTotalTimeoutConstant">读超时</param>
        public void UpdateSerialPortTimeout(int writeTotalTimeoutConstant, int readTotalTimeoutConstant)
        {
            _SerialConfig.ReadTotalTimeoutConstant = readTotalTimeoutConstant;
            _SerialPort.ReadTimeout = readTotalTimeoutConstant;
            _SerialConfig.WriteTotalTimeoutConstant = writeTotalTimeoutConstant;
            _SerialPort.WriteTimeout = writeTotalTimeoutConstant;
        }

        #region Sync-SendReceiving

        private readonly AutoResetEvent _SyncReset = new AutoResetEvent(false);
        /// <summary>
        /// 当同步读取时的Buffer
        /// </summary>
        private byte[] _SyncBuffer = new byte[0]; 
        private bool _OnSyncReceive;

        protected class SyncSendReceivingParams
        {
            public SyncSendReceivingParams(Action<IQuestion<byte[]>> sendAction, Func<AnswerBase<byte[]>, bool> receivedFunc)
            {
                SendAction = sendAction;
                ReceivedFunc = receivedFunc;
            }

            public Action<IQuestion<byte[]>> SendAction { get; set; }
            public Func<AnswerBase<byte[]>, bool> ReceivedFunc { get; set; }
        }

        /// <summary>
        ///     发送数据并同步等待数据返回
        /// </summary>
        /// <param name="sendAction">当发送完成时</param>
        /// <param name="receivedFunc">当采集到数据(返回的数据)的处理方法。当返回true时，表示接收数据是完整的；返回flase时，表示接收数据不完整，还需要继续接收。</param>
        /// <returns>是否采集到数据</returns>
        public override void SendReceiving(Action<IQuestion<byte[]>> sendAction, Func<IAnswer<byte[]>, bool> receivedFunc)
        {
            ThreadPool.QueueUserWorkItem(SendReceiving, new SyncSendReceivingParams(sendAction, receivedFunc));
#if DEBUG
            int a, b = 0;
            ThreadPool.GetAvailableThreads(out a, out b);
            _logger.Trace($"WorkerThreads: {a}, CompletionPortThreads: {b}");
#endif
        }

        protected void SendReceiving(object param)
        {
            var w = (SyncSendReceivingParams) param;
            try
            {
                foreach (var ask in _QuestionGroup)
                {
                    _SerialPort.Write(ask.Data, 0, ask.Data.Length);
                    _OnSyncReceive = true;
                    w.SendAction.Invoke(ask);
                    var complate = false;
                    while (!complate)
                    {
                        if (_SyncReset.WaitOne((int) TalkTotalTimeout)) //监听从事件收到返回的数据
                        {
                            if (_SyncBuffer.Length > 0)
                            {
                                var currBuffer = new byte[_SyncBuffer.Length];
                                Buffer.BlockCopy(_SyncBuffer, 0, currBuffer, 0, _SyncBuffer.Length);
                                complate = w.ReceivedFunc.Invoke(new SerialAnswer(this, ask.Device, ask.Exhibit, currBuffer));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Warn($"串口发送与接收时底层异常:{e.Message}", e);
            }
            finally
            {
                _OnSyncReceive = false;
            }
        }

        /// <summary>
        ///     接收到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void SyncSerialPortDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            if (!_OnSyncReceive)
            {
                _SerialPort.DiscardInBuffer();
                return;
            }
            try
            {
                _SyncBuffer = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(_SyncBuffer, 0, _SyncBuffer.Length);
            }
            catch (TimeoutException e)
            {
                _logger.Warn($"串口读取超时异常：{e.Message}", e);
            }
            catch (Exception e)
            {
                _logger.Warn($"串口读取异常：{e.Message}", e);
            }
            finally
            {
                _SyncReset.Set(); //通知SendReceiving函数继续
            }
        }

        #endregion

        #region Async-SendReceiving

        private Thread _AutoSendThread;

        private readonly StatusChecker _StatusChecker = new StatusChecker();

        protected virtual void AsyncDataReceived(object sender, SerialDataReceivedEventArgs eventArgs)
        {
            byte[] buffer = null;
            try
            {
                if (_SerialConfig.ReadWait > 0)
                    Thread.Sleep(_SerialConfig.ReadWait); //当进入事件后，等待串口数据更加完整一些再进行读取
                /**
                 * 由于 ReadBufferSize 属性只表示 Windows 创建的缓冲区，
                 * 而 BytesToRead 属性除了表示 Windows 创建的缓冲区外还表示 SerialPort 缓冲区，
                 * 所以 BytesToRead 属性可以返回一个比 ReadBufferSize 属性大的值。 
                 * 接收缓冲区包括串行驱动程序的接收缓冲区以及 SerialPort 对象自身的内部缓冲。
                 */
                buffer = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(buffer, 0, buffer.Length);
            }
            catch (TimeoutException e)
            {
                _logger.Warn($"串口读取超时异常：{e.Message}", e);
            }
            catch (Exception e)
            {
                _logger.Warn($"串口读取异常：{e.Message}", e);
            }
            if (buffer != null && buffer.Length > 0)
            {
                if (_QuestionGroup != null && _QuestionGroup.Count > 0)
                {
                    var q = _QuestionGroup[_QuestionGroup.CurrentIndex];
                    OnDataArrived(new SerialChannelAnswerDataEventArgs(this, q.Device, q.Exhibit, buffer));
                }
                else
                {
                    OnDataArrived(new SerialChannelAnswerDataEventArgs(this, null, null, buffer));
                }
            }
        }

        /// <summary>
        ///     自动发送数据
        /// </summary>
        public override void AutoSend(Action<IQuestion<byte[]>> sendAction)
        {
            _AutoSendThread?.Abort();
            _AutoSendThread = new Thread(AutoSendThread) {Name = $"{_SerialPort.PortName}-AutoSendThread"};
            _AutoSendThread.IsBackground = true;
            _AutoSendThread.Start(sendAction);
        }

        protected void AutoSendThread(object parameter)
        {
            _StatusChecker.QuestionGroup = _QuestionGroup;
            _StatusChecker.SerialPort = _SerialPort;
            _StatusChecker.IsStopFlag = false;
            _StatusChecker.SendAction = parameter as Action<IQuestion<byte[]>>;
            var autoEvent = new AutoResetEvent(false);
            var autoSendTimer = new Timer(_StatusChecker.Run, autoEvent, 0, TalkTotalTimeout);
            _logger.Info("异步自动发送数据线程开始..");
            autoEvent.WaitOne();
            autoSendTimer.Dispose();
            _logger.Info("异步自动发送数据线程中止..");
        }

        protected class StatusChecker
        {
            public SerialQuestionGroup QuestionGroup { get; set; }
            public SerialPort SerialPort { get; set; }
            public bool IsStopFlag { private get; set; }
            public Action<IQuestion<byte[]>> SendAction { get; set; }

            public void Run(object stateInfo)
            {
                var autoEvent = (AutoResetEvent)stateInfo;
                try
                {
                    if (QuestionGroup == null || QuestionGroup.Count <= 0)
                    {
                        autoEvent.Set();
                        return;
                    }
                    var question = QuestionGroup.Current;
                    SendAction?.Invoke(question);
                    SerialPort.Write(question.Data, 0, question.Data.Length);
                    if (!question.IsLoop)
                    {
                        QuestionGroup.Remove(question);
                    }
                    else
                    {
                        if (QuestionGroup.CurrentIndex < QuestionGroup.Count - 1)
                            QuestionGroup.CurrentIndex++;
                        else
                            QuestionGroup.CurrentIndex = 0;
                    }
                    if (IsStopFlag)
                    {
                        autoEvent.Set();
                    }
                }
                catch (Exception e)
                {
                    _logger.Warn($"串口发送时(异步)底层异常:{e.Message}", e);
                }
            }
        }

        /// <summary>
        ///     当自动发送模式时，中断正在不断进行的自动模式
        /// </summary>
        public override void Break()
        {
            _StatusChecker.IsStopFlag = true;
        }

        #endregion

        #endregion


    }
}