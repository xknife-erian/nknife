using System;
using System.IO.Ports;
using System.Threading;
using Common.Logging;
using NKnife.Channels.Base;
using NKnife.Channels.EventParams;
using NKnife.Channels.Interfaces;

namespace NKnife.Channels.Serials
{
    public class SerialChannel : ChannelBase<byte[]>
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialChannel>();
        private readonly SerialConfig _SerialConfig;
        private SerialPort _SerialPort; //串口操作类（通过.net 类库）

        public SerialChannel(SerialConfig serialConfig)
        {
            if (serialConfig == null)
                throw new ArgumentNullException(nameof(serialConfig), $"{nameof(serialConfig)}不能为空");
            if (serialConfig.Port <= 0)
                throw new ArgumentNullException(nameof(serialConfig), $"串口号必须设置。");
            _SerialConfig = serialConfig;
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _logger.Warn($"SerialPortErrorReceived:{e.EventType}");
            _SerialPort.DiscardInBuffer();
        }

        #region Overrides of ChannelBase

        /// <summary>
        /// 打开串口默认等待的时长。默认1.5秒钟。
        /// </summary>
        public int OpenTimeout { get; set; } = 1500;

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
                openReset.WaitOne(OpenTimeout); //打开串口等待1.5秒
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
                _logger.Error($"无法打开串口:COM{_SerialConfig.Port}, {e.Message}", e);
                IsOpen = false;
            }
            return IsOpen;
        }

        /// <summary>
        ///     关闭采集通道
        /// </summary>
        /// <returns></returns>
        public override bool Close()
        {
            if (IsOpen && _SerialPort.IsOpen)
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
                    _AsyncStatusChecker.IsStopFlag = true;
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

        #endregion

        #region Sync-SendReceiving

        private readonly AutoResetEvent _SyncReset = new AutoResetEvent(false);
        private SyncStatusChecker _SyncStatusChecker = new SyncStatusChecker();
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
            ThreadPool.GetAvailableThreads(out int a, out int b);
            _logger.Trace($"WorkerThreads: {a}, CompletionPortThreads: {b}");
#endif
        }

        protected void SendReceiving(object param)
        {
            var autoEvent = new AutoResetEvent(false);
            _SyncStatusChecker.QuestionGroup = _QuestionGroup;
            _SyncStatusChecker.SerialPort = _SerialPort;
            _SyncStatusChecker.IsStopFlag = false;
            _SyncStatusChecker.Params = param as SyncSendReceivingParams;
            var autoSendTimer = new Timer(_SyncStatusChecker.Run, autoEvent, 0, TalkTotalTimeout);
            //autoSendTimer.Change(3, 3);
            //https://technet.microsoft.com/zh-CN/library/317hx6fa(v=vs.95)
            _logger.Info("异步自动发送数据线程开始..");
            autoEvent.WaitOne();
            autoSendTimer.Dispose();
            _logger.Info("异步自动发送数据线程中止..");

            SendReceiving((SyncSendReceivingParams) param);
        }

        protected void SendReceiving(SyncSendReceivingParams param)
        {
            try
            {
                while (_QuestionGroup.Count > 0) //只要指令集合有指令就一直持续循环
                {
                    var question = _QuestionGroup.PeekOrDequeue();
                    _SerialPort.Write(question.Data, 0, question.Data.Length);
                    _OnSyncReceive = true; //标记
                    param.SendAction.Invoke(question);
                    var complate = false;
                    while (!complate)
                    {
                        Console.Write("/");
                        // 发出后等待
                        _OnSyncReceive = true; //标记
                        var timeout = (int) TalkTotalTimeout;
                        if (question.LoopInterval > 0)
                            timeout = question.LoopInterval; //如果这条指定设置了等待时长
                        // 这个等待是同步时每次对话的时间
                        if (_SyncReset.WaitOne(timeout)) //监听从事件收到返回的数据
                        {
                            if (_SyncBuffer.Length > 0)
                            {
                                var currBuffer = new byte[_SyncBuffer.Length];
                                Buffer.BlockCopy(_SyncBuffer, 0, currBuffer, 0, _SyncBuffer.Length);
                                _SyncBuffer = new byte[0];
                                //将读取到的数据抛出进行解析，如果认为已完成本次询问进入下一次对话。
                                //如果未完成本次询问再次进入等待回答(读取)状态。
                                complate = param.ReceivedFunc.Invoke(new SerialAnswer(question.Instrument, currBuffer));
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
                // 因为是对话模式，在非 OnSyncReceive 期内，这时候得到的数据一概丢弃。
                _SerialPort.DiscardInBuffer();
                return;
            }
            try
            {
                //_SyncBuffer是交换数据的缓冲区
                //触发读取后，读取，读取本次以后进入下一次询问对话
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
                _OnSyncReceive = false;
                _SyncReset.Set(); //通知SendReceiving函数继续
            }
        }

        protected class SyncStatusChecker
        {
            public SerialQuestionGroup QuestionGroup { get; set; }
            public SerialPort SerialPort { get; set; }
            public bool IsStopFlag { private get; set; }
            public SyncSendReceivingParams Params { get; set; }

            public void Run(object stateInfo)//依靠Timer在指定的时间间隔不断的进行循环
            {
                var autoEvent = (AutoResetEvent)stateInfo;
                try
                {
                    if (QuestionGroup == null || QuestionGroup.Count <= 0)
                    {
                        autoEvent.Set();
                        return;
                    }
                    var question = QuestionGroup.PeekOrDequeue();
                    Params?.SendAction.Invoke(question);
                    SerialPort.Write(question.Data, 0, question.Data.Length);




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

        #endregion

        #region Async-SendReceiving

        private Thread _AutoSendThread;

        private readonly AsyncStatusChecker _AsyncStatusChecker = new AsyncStatusChecker();

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
                    OnDataArrived(new SerialChannelAnswerDataEventArgs(q.Instrument, buffer));
                }
                else
                {
                    OnDataArrived(new SerialChannelAnswerDataEventArgs(null, buffer));
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
            var autoEvent = new AutoResetEvent(false);
            _AsyncStatusChecker.QuestionGroup = _QuestionGroup;
            _AsyncStatusChecker.SerialPort = _SerialPort;
            _AsyncStatusChecker.IsStopFlag = false;
            _AsyncStatusChecker.SendAction = parameter as Action<IQuestion<byte[]>>;
            var autoSendTimer = new Timer(_AsyncStatusChecker.Run, autoEvent, 0, TalkTotalTimeout);
            _logger.Info("异步自动发送数据线程开始..");
            autoEvent.WaitOne();
            autoSendTimer.Dispose();
            _logger.Info("异步自动发送数据线程中止..");
        }

        protected class AsyncStatusChecker
        {
            public SerialQuestionGroup QuestionGroup { get; set; }
            public SerialPort SerialPort { get; set; }
            public bool IsStopFlag { private get; set; }
            public Action<IQuestion<byte[]>> SendAction { get; set; }

            public void Run(object stateInfo)//依靠Timer在指定的时间间隔不断的进行循环
            {
                var autoEvent = (AutoResetEvent)stateInfo;
                try
                {
                    if (QuestionGroup == null || QuestionGroup.Count <= 0)
                    {
                        autoEvent.Set();
                        return;
                    }
                    var question = QuestionGroup.PeekOrDequeue();
                    SendAction?.Invoke(question);
                    SerialPort.Write(question.Data, 0, question.Data.Length);
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
            _AsyncStatusChecker.IsStopFlag = true;
        }

        #endregion

        #endregion
    }
}