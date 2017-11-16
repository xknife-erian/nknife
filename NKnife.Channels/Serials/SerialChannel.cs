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
        private SerialPort _SerialPort; //���ڲ����ࣨͨ��.net ��⣩

        public SerialChannel(SerialConfig serialConfig)
        {
            if (serialConfig == null)
                throw new ArgumentNullException(nameof(serialConfig), $"{nameof(serialConfig)}����Ϊ��");
            if (serialConfig.Port <= 0)
                throw new ArgumentNullException(nameof(serialConfig), $"���ںű������á�");
            _SerialConfig = serialConfig;
        }

        #region Overrides of ChannelBase

        /// <summary>
        /// �򿪴���Ĭ�ϵȴ���ʱ����Ĭ��1.5���ӡ�
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
                openReset.WaitOne(OpenTimeout); //�򿪴��ڵȴ�1.5��
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
                _logger.Error($"�޷��򿪴���:COM{_SerialConfig.Port}, {e.Message}", e);
                IsOpen = false;
            }
            return IsOpen;
        }

        /// <summary>
        ///     �رղɼ�ͨ��
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
                    _AsyncStatusChecker.IsStopFlag = true;
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

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _logger.Warn($"SerialPortErrorReceived:{e.EventType}");
            _SerialPort.DiscardInBuffer();
        }

        private SerialQuestionGroup _QuestionGroup;

        /// <summary>
        ///     ���¼������͵�����
        /// </summary>
        /// <param name="questionGroup">�������͵�����</param>
        public override void UpdateQuestionGroup(IQuestionGroup<byte[]> questionGroup)
        {
            if (!(questionGroup is SerialQuestionGroup))
                throw new ArgumentException(nameof(questionGroup), $"{nameof(questionGroup)} need is {(typeof(SerialQuestionGroup)).Name}");
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

        #endregion

        #region Sync-SendReceiver

        private readonly AutoResetEvent _SyncLoopTimerWaiter = new AutoResetEvent(false);
        private readonly AutoResetEvent _SyncReceiveWaiter = new AutoResetEvent(false);

        private bool _OnSyncSent = false;
        private Timer _LoopTimer;

        /// <summary>
        /// ��ͬ����ȡʱ��Buffer
        /// </summary>
        private byte[] _SyncBuffer = new byte[0];

        protected class TalkInfo
        {
            public TalkInfo(Action<IQuestion<byte[]>> sendAction, Func<AnswerBase<byte[]>, bool> receivedFunc)
            {
                SendAction = sendAction;
                ReceivedFunc = receivedFunc;
            }

            public SerialQuestionGroup QuestionGroup { get; set; }
            public SerialPort SerialPort { get; set; }

            public Action<IQuestion<byte[]>> SendAction { get; set; }
            public Func<AnswerBase<byte[]>, bool> ReceivedFunc { get; set; }
        }

        /// <summary>
        ///     �������ݲ�ͬ���ȴ����ݷ���
        /// </summary>
        /// <param name="sendAction">���������ʱ</param>
        /// <param name="receivedFunc">���ɼ�������(���ص�����)�Ĵ�������������trueʱ����ʾ���������������ģ�����flaseʱ����ʾ�������ݲ�����������Ҫ�������ա�</param>
        /// <returns>�Ƿ�ɼ�������</returns>
        public override void SendReceiver(Action<IQuestion<byte[]>> sendAction, Func<IAnswer<byte[]>, bool> receivedFunc)
        {
            ThreadPool.QueueUserWorkItem(StartSendTimer, new TalkInfo(sendAction, receivedFunc));
#if DEBUG
            ThreadPool.GetAvailableThreads(out int a, out int b);
            _logger.Trace($"WorkerThreads: {a}, CompletionPortThreads: {b}");
#endif
        }

        protected void StartSendTimer(object param)
        {
            if (_QuestionGroup != null && _QuestionGroup.Count > 0)
            {
                var talkinfo = (TalkInfo) param;
                talkinfo.QuestionGroup = _QuestionGroup;
                talkinfo.SerialPort = _SerialPort;
                //������Ϊ�����
                _LoopTimer = new Timer(SyncMethodRun, talkinfo, 0, _QuestionGroup.First.LoopInterval);
                _logger.Info("ͬ���Զ����������߳̿�ʼ..");
                _SyncLoopTimerWaiter.WaitOne();//��QuestionGroup������Ҫ����ѭ����ѯ��ʱ�����̽������ڴˣ�ֱ���յ��ź�
                _LoopTimer.Dispose();
                _logger.Info("ͬ���Զ����������߳���ֹ..");
            }
            else
            {
                _logger.Info("SendReceiver�޴����͵�����");
            }
        }

        public async void SyncMethodRun(object param) //����Timer��ָ����ʱ�������ϵĽ���ѭ��
        {
            var methodParams = (TalkInfo) param;
            if (methodParams == null)
                return;
            if (_QuestionGroup.Count <= 0)//����ѯ�ʶ����������
            {
                _SyncLoopTimerWaiter.Set();
                return;
            }

            var complate = false;//�Ƿ��ȡ�������������ݣ�����ζԻ��Ƿ����
            var question = _QuestionGroup.PeekOrDequeue();
            try
            {
                methodParams.SendAction.Invoke(question);//�ص�ִ��ǰ����

                _OnSyncSent = true; //��������ǰ�ñ�ǣ��Ա�֤�ɶ�ȡ����
                _SerialPort.Write(question.Data, 0, question.Data.Length);

                while (!complate)
                {
                    Console.Write("/");
                    // ������ȴ�
                    _OnSyncSent = true; //���
                    // ����ȴ���ͬ��ʱÿ�ζԻ���ʱ��
                    if (_SyncReceiveWaiter.WaitOne(question.Timeout)) //�������¼��յ����ص�����
                    {
                        if (_SyncBuffer.Length > 0)
                        {
                            var currBuffer = new byte[_SyncBuffer.Length];
                            Buffer.BlockCopy(_SyncBuffer, 0, currBuffer, 0, _SyncBuffer.Length);
                            _SyncBuffer = new byte[0];
                            //����ȡ���������׳����н����������Ϊ����ɱ���ѯ�ʽ�����һ�ζԻ���
                            //���δ��ɱ���ѯ���ٴν���ȴ��ش�(��ȡ)״̬��
                            complate = methodParams.ReceivedFunc.Invoke(new SerialAnswer(question.Instrument, currBuffer));
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
                _OnSyncSent = false;
            }
        }

        /// <summary>
        ///     ���յ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void SyncSerialPortDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            if (!_OnSyncSent)
            {
                // ��Ϊ�ǶԻ�ģʽ���ڷ� OnSyncReceive ���ڣ���ʱ��õ�������һ�Ŷ�����
                _SerialPort.DiscardInBuffer();
                return;
            }
            try
            {
                //_SyncBuffer�ǽ������ݵĻ�����
                //������ȡ�󣬶�ȡ����ȡ�����Ժ������һ��ѯ�ʶԻ�
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
                _OnSyncSent = false;
                _SyncReceiveWaiter.Set(); //֪ͨSendReceiving��������
            }
        }

        #endregion

        #region Async-StartSendTimer

        private Thread _AutoSendThread;

        private readonly AsyncStatusChecker _AsyncStatusChecker = new AsyncStatusChecker();

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
                    OnDataArrived(new SerialChannelAnswerDataEventArgs(q.Instrument, buffer));
                }
                else
                {
                    OnDataArrived(new SerialChannelAnswerDataEventArgs(null, buffer));
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
            var autoEvent = new AutoResetEvent(false);
            _AsyncStatusChecker.QuestionGroup = _QuestionGroup;
            _AsyncStatusChecker.SerialPort = _SerialPort;
            _AsyncStatusChecker.IsStopFlag = false;
            _AsyncStatusChecker.SendAction = parameter as Action<IQuestion<byte[]>>;
            var autoSendTimer = new Timer(_AsyncStatusChecker.Run, autoEvent, 0, _QuestionGroup.GetMaxTimeout());
            _logger.Info("�첽�Զ����������߳̿�ʼ..");
            autoEvent.WaitOne();
            autoSendTimer.Dispose();
            _logger.Info("�첽�Զ����������߳���ֹ..");
        }

        protected class AsyncStatusChecker
        {
            public SerialQuestionGroup QuestionGroup { get; set; }
            public SerialPort SerialPort { get; set; }
            public bool IsStopFlag { private get; set; }
            public Action<IQuestion<byte[]>> SendAction { get; set; }

            public void Run(object stateInfo) //����Timer��ָ����ʱ�������ϵĽ���ѭ��
            {
                var autoEvent = (AutoResetEvent) stateInfo;
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
                    _logger.Warn($"���ڷ���ʱ(�첽)�ײ��쳣:{e.Message}", e);
                }
            }
        }

        #endregion

        /// <summary>
        ///     �ж����ڽ��еķ��ͽ��߹��̣��������첽��ͬ����
        /// </summary>
        public override void Break()
        {
            _AsyncStatusChecker.IsStopFlag = true;
        }

        #endregion
    }
}