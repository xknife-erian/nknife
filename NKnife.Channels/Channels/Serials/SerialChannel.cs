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
        private SerialPort _SerialPort; //���ڲ����ࣨͨ��.net ��⣩

        public SerialChannel(SerialConfig serialConfig)
        {
            if(serialConfig==null)
                throw new ArgumentNullException(nameof(serialConfig), $"{nameof(serialConfig)}����Ϊ��");
            _SerialConfig = serialConfig;
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _logger.Warn($"SerialPortErrorReceived:{e.EventType}");
            _SerialPort.DiscardInBuffer();
        }

        #region Overrides of ChannelBase

        /// <summary>
        ///     �򿪲ɼ�ͨ��
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
                        _logger.Debug($"ͨѶ:׼���򿪴���:{_SerialPort.PortName}:{_SerialPort.BaudRate}......");
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
                openReset.WaitOne(1200); //�򿪴��ڵȴ�1.2��
                if (IsOpen)
                {
                    OnOpened();
                    _logger.Info($"ͨѶ:�ɹ��򿪴���:{_SerialPort.PortName}:{_SerialPort.BaudRate}");
                }
                else
                {
                    _logger.Warn($"�޷��򿪴���:COM{_SerialConfig.Port}");
                    thread.Abort();
                }
            }
            catch (Exception e)
            {
                _logger.Warn($"�޷��򿪴���:COM{_SerialConfig.Port}, {e.Message}", e);
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
        ///     �رղɼ�ͨ��
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
                    _logger.Warn($"ִ�йرմ���ǰ�¼��쳣:{_SerialPort.PortName}��{e.Message}", e);
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
                    _logger.Info($"ͨѶ:�ɹ��رմ���:{_SerialPort.PortName}��");
                }
                catch (Exception e)
                {
                    _logger.Warn($"�رմ����쳣:{_SerialPort.PortName}, {e.Message}", e);
                    return false;
                }
                try
                {
                    OnClosed();
                }
                catch (Exception e)
                {
                    _logger.Warn($"ִ�йرմ��ں��¼��쳣:{_SerialPort.PortName}��{e.Message}", e);
                }
            }
            IsOpen = false;
            return true;
        }

        private SerialQuestionGroup _QuestionGroup;

        /// <summary>
        ///     ���¼������͵�����
        /// </summary>
        /// <param name="questionGroup">�������͵�����</param>
        public override void UpdateQuestionGroup(IQuestionGroup<byte[]> questionGroup)
        {
            if (!(questionGroup is SerialQuestionGroup))
                throw new ArgumentException(nameof(questionGroup), $"{nameof(questionGroup)} need is {(typeof (SerialQuestionGroup)).Name}");
            UpdateQuestionGroup((SerialQuestionGroup) questionGroup);
        }

        /// <summary>
        ///     ���¼������͵�����
        /// </summary>
        /// <param name="questionGroup">�������͵�����</param>
        public void UpdateQuestionGroup(SerialQuestionGroup questionGroup)
        {
            _QuestionGroup = questionGroup;
        }

        /// <summary>
        ///     ���ô��ڶ�ȡ��ʱ
        /// </summary>
        /// <param name="writeTotalTimeoutConstant">д��ʱ</param>
        /// <param name="readTotalTimeoutConstant">����ʱ</param>
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
        /// ��ͬ����ȡʱ��Buffer
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
        ///     �������ݲ�ͬ���ȴ����ݷ���
        /// </summary>
        /// <param name="sendAction">���������ʱ</param>
        /// <param name="receivedFunc">���ɼ�������(���ص�����)�Ĵ�������������trueʱ����ʾ���������������ģ�����flaseʱ����ʾ�������ݲ�����������Ҫ�������ա�</param>
        /// <returns>�Ƿ�ɼ�������</returns>
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
                        if (_SyncReset.WaitOne((int) TalkTotalTimeout)) //�������¼��յ����ص�����
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
                _logger.Warn($"���ڷ��������ʱ�ײ��쳣:{e.Message}", e);
            }
            finally
            {
                _OnSyncReceive = false;
            }
        }

        /// <summary>
        ///     ���յ�����
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
                _logger.Warn($"���ڶ�ȡ��ʱ�쳣��{e.Message}", e);
            }
            catch (Exception e)
            {
                _logger.Warn($"���ڶ�ȡ�쳣��{e.Message}", e);
            }
            finally
            {
                _SyncReset.Set(); //֪ͨSendReceiving��������
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
                    Thread.Sleep(_SerialConfig.ReadWait); //�������¼��󣬵ȴ��������ݸ�������һЩ�ٽ��ж�ȡ
                /**
                 * ���� ReadBufferSize ����ֻ��ʾ Windows �����Ļ�������
                 * �� BytesToRead ���Գ��˱�ʾ Windows �����Ļ������⻹��ʾ SerialPort ��������
                 * ���� BytesToRead ���Կ��Է���һ���� ReadBufferSize ���Դ��ֵ�� 
                 * ���ջ���������������������Ľ��ջ������Լ� SerialPort ����������ڲ����塣
                 */
                buffer = new byte[_SerialPort.BytesToRead];
                _SerialPort.Read(buffer, 0, buffer.Length);
            }
            catch (TimeoutException e)
            {
                _logger.Warn($"���ڶ�ȡ��ʱ�쳣��{e.Message}", e);
            }
            catch (Exception e)
            {
                _logger.Warn($"���ڶ�ȡ�쳣��{e.Message}", e);
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
        ///     �Զ���������
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
            _logger.Info("�첽�Զ����������߳̿�ʼ..");
            autoEvent.WaitOne();
            autoSendTimer.Dispose();
            _logger.Info("�첽�Զ����������߳���ֹ..");
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
                    _logger.Warn($"���ڷ���ʱ(�첽)�ײ��쳣:{e.Message}", e);
                }
            }
        }

        /// <summary>
        ///     ���Զ�����ģʽʱ���ж����ڲ��Ͻ��е��Զ�ģʽ
        /// </summary>
        public override void Break()
        {
            _StatusChecker.IsStopFlag = true;
        }

        #endregion

        #endregion


    }
}