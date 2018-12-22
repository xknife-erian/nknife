using System;
using System.IO.Ports;
using System.Threading;
using Common.Logging;
using NKnife.Channels.Base;
using NKnife.Channels.EventParams;
using NKnife.Channels.Interfaces;
using NKnife.Interface;
using NKnife.Timers;

namespace NKnife.Channels.Serials
{
    public class SerialChannel : ChannelBase<byte[]>
    {
        private static readonly ILog _Logger = LogManager.GetLogger<SerialChannel>();
        private readonly SerialConfig _serialConfig;
        private SerialPort _serialPort; //���ڲ����ࣨͨ��.net ��⣩

        public SerialChannel(SerialConfig serialConfig)
        {
            if (serialConfig == null)
                throw new ArgumentNullException(nameof(serialConfig), $"{nameof(serialConfig)}����Ϊ��");
            if (serialConfig.Port <= 0)
                throw new ArgumentNullException(nameof(serialConfig), $"���ںű������á�");
            _serialConfig = serialConfig;
        }

        #region Overrides of ChannelBase

        /// <summary>
        /// �򿪴���Ĭ�ϵȴ���ʱ����Ĭ��1���ӡ�
        /// </summary>
        public int OpenTimeout { get; set; } = 1000;

        protected override void OnChannelModeChanged(ChannelModeChangedEventArgs e)
        {
            if (_serialPort == null)
            {
                base.OnChannelModeChanged(e);
                return;
            }

            if (e.IsSynchronous)
            {
                _serialPort.DataReceived += SyncSerialPortDataReceived;
                _serialPort.DataReceived -= AsyncDataReceived;
            }
            else
            {
                _serialPort.DataReceived += AsyncDataReceived;
                _serialPort.DataReceived -= SyncSerialPortDataReceived;
            }

            base.OnChannelModeChanged(e);
        }

        /// <summary>
        ///     �򿪲ɼ�ͨ��
        /// </summary>
        /// <returns></returns>
        public override bool Open()
        {
            _serialPort = new SerialPort
            {
                PortName = $"COM{_serialConfig.Port}",
                StopBits = _serialConfig.StopBit,
                BaudRate = _serialConfig.BaudRate,
                DataBits = _serialConfig.DataBit, //8,
                WriteTimeout = _serialConfig.WriteTotalTimeoutConstant, //1000
                ReadTimeout = _serialConfig.ReadTotalTimeoutConstant, //1000,
                ReceivedBytesThreshold = _serialConfig.ReceivedBytesThreshold, //1,
                ReadBufferSize = _serialConfig.ReadBufferSize, //64,
                DtrEnable = _serialConfig.DtrEnable,
                Parity = _serialConfig.Parity,
                RtsEnable = _serialConfig.RtsEnable
            };

            if (IsSynchronous)
            {
                _serialPort.DataReceived += SyncSerialPortDataReceived;
            }
            else
            {
                _serialPort.DataReceived += AsyncDataReceived;
            }

            _serialPort.ErrorReceived += SerialPortErrorReceived;

            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                OnOpening();
                var openReset = new AutoResetEvent(false);
                var thread = new Thread(() =>
                {
                    try
                    {
                        _Logger.Debug($"ͨѶ:׼���򿪴���:{_serialPort.PortName}:{_serialPort.BaudRate}......");
                        _serialPort.Open();
                        IsOpen = true;
                    }
                    catch (Exception e)
                    {
                        IsOpen = false;
                        _Logger.Warn(e.Message, e);
                    }
                    finally
                    {
                        openReset.Set();
                    }
                }) {Name = $"{_serialPort.PortName}-Thread"};
                thread.IsBackground = true;
                thread.Start();
                openReset.WaitOne(OpenTimeout); //�򿪴��ڵȴ�1.5��
                if (IsOpen)
                {
                    OnOpened();
                    _Logger.Info($"ͨѶ:�ɹ��򿪴���:{_serialPort.PortName}:{_serialPort.BaudRate}");
                }
                else
                {
                    _Logger.Warn($"�޷��򿪴���:COM{_serialConfig.Port}");
                    thread.Abort();
                }
            }
            catch (Exception e)
            {
                _Logger.Error($"�޷��򿪴���:COM{_serialConfig.Port}, {e.Message}", e);
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
            if (IsOpen && _serialPort.IsOpen)
            {
                try
                {
                    OnClosing();
                }
                catch (Exception e)
                {
                    _Logger.Warn($"ִ�йرմ���ǰ�¼��쳣:{_serialPort.PortName}��{e.Message}", e);
                }

                try
                {
                    if (IsSynchronous)
                        _serialPort.DataReceived -= SyncSerialPortDataReceived;
                    else
                        _serialPort.DataReceived -= AsyncDataReceived;
                    _serialPort.Close();
                    _Logger.Info($"ͨѶ:�ɹ��رմ���:{_serialPort.PortName}��");
                }
                catch (Exception e)
                {
                    _Logger.Warn($"�رմ����쳣:{_serialPort.PortName}, {e.Message}", e);
                    return false;
                }

                try
                {
                    OnClosed();
                }
                catch (Exception e)
                {
                    _Logger.Warn($"ִ�йرմ��ں��¼��쳣:{_serialPort.PortName}��{e.Message}", e);
                }
            }

            IsOpen = false;
            return true;
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _Logger.Warn($"SerialPortErrorReceived:{e.EventType}");
            _serialPort.DiscardInBuffer();
        }

        /// <summary>
        ///     ���ô���д�����ȡ��ʱ
        /// </summary>
        /// <param name="writeTotalTimeoutConstant">д��ʱ</param>
        /// <param name="readTotalTimeoutConstant">����ʱ</param>
        public void UpdateSerialPortTimeout(int writeTotalTimeoutConstant, int readTotalTimeoutConstant)
        {
            _serialConfig.ReadTotalTimeoutConstant = readTotalTimeoutConstant;
            _serialPort.ReadTimeout = readTotalTimeoutConstant;
            _serialConfig.WriteTotalTimeoutConstant = writeTotalTimeoutConstant;
            _serialPort.WriteTimeout = writeTotalTimeoutConstant;
        }

        #endregion

        private readonly AutoResetEvent _syncReceiveWaiter = new AutoResetEvent(false);

        private bool _onSyncSent = false;

        /// <summary>
        /// ��ͬ����ȡʱ��Buffer
        /// </summary>
        private byte[] _talkBuffer = new byte[0];

        protected void Talk(SerialQuestion question)
        {
            var complate = false; //�Ƿ��ȡ�������������ݣ�����ζԻ��Ƿ����
            _onSyncSent = true; //��������ǰ�ñ�ǣ��Ա�֤�ɶ�ȡ����
            _serialPort.Write(question.Data, 0, question.Data.Length);

            while (!complate)
            {
                // ������ȴ�
                _onSyncSent = true; //���
                // ����ȴ���ͬ��ʱÿ�ζԻ���ʱ��
                if (_syncReceiveWaiter.WaitOne(question.Timeout)) //�������¼��յ����ص�����
                {
                    if (_talkBuffer.Length > 0)
                    {
                        var currBuffer = new byte[_talkBuffer.Length];
                        Buffer.BlockCopy(_talkBuffer, 0, currBuffer, 0, _talkBuffer.Length);
                        _talkBuffer = new byte[0];
                        //����ȡ���������׳����н����������Ϊ����ɱ���ѯ�ʽ�����һ�ζԻ���
                        //���δ��ɱ���ѯ���ٴν���ȴ��ش�(��ȡ)״̬��
                    }
                }
            }
        }

        /// <summary>
        ///     ͬ�����յ�����
        /// </summary>
        protected virtual void SyncSerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_onSyncSent)
            {
                // ��Ϊ�ǶԻ�ģʽ���ڷ� OnSyncReceive ���ڣ���ʱ��õ�������һ�Ŷ�����
                _serialPort.DiscardInBuffer();
                return;
            }

            try
            {
                //_talkBuffer�ǽ������ݵĻ�����
                //������ȡ�󣬶�ȡ����ȡ�����Ժ������һ��ѯ�ʶԻ�
                _talkBuffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(_talkBuffer, 0, _talkBuffer.Length);
            }
            catch (TimeoutException ex)
            {
                _Logger.Warn($"���ڶ�ȡ��ʱ�쳣��{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _Logger.Warn($"���ڶ�ȡ�쳣��{ex.Message}", ex);
            }
            finally
            {
                _onSyncSent = false;
                _syncReceiveWaiter.Set(); //֪ͨ Talk ��������
            }
        }

        /// <summary>
        ///     �첽���յ�����
        /// </summary>
        protected virtual void AsyncDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = null;
            try
            {
                if (_serialConfig.ReadWait > 0)
                    Thread.Sleep(_serialConfig.ReadWait); //�������¼��󣬵ȴ��������ݸ�������һЩ�ٽ��ж�ȡ
                /**
                 * ���� ReadBufferSize ����ֻ��ʾ Windows �����Ļ�������
                 * �� BytesToRead ���Գ��˱�ʾ Windows �����Ļ������⻹��ʾ SerialPort ��������
                 * ���� BytesToRead ���Կ��Է���һ���� ReadBufferSize ���Դ��ֵ�� 
                 * ���ջ���������������������Ľ��ջ������Լ� SerialPort ����������ڲ����塣
                 */
                buffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(buffer, 0, buffer.Length);
            }
            catch (TimeoutException ex)
            {
                _Logger.Warn($"���ڶ�ȡ��ʱ�쳣��{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _Logger.Warn($"���ڶ�ȡ�쳣��{ex.Message}", ex);
            }

            if (buffer != null && buffer.Length > 0)
            {
                OnDataArrived(new SerialChannelAnswerDataEventArgs(q.Instrument, buffer));
            }
        }

        /// <summary>
        ///     �ж����ڽ��еķ��ͽ��߹��̣��������첽��ͬ����
        /// </summary>
        public override void Break()
        {
            JobManager.Break();
        }

        /// <inheritdoc />
        public override void SyncListen()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void AsyncListen()
        {
            throw new NotImplementedException();
        }
    }
}