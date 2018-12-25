using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using Common.Logging;
using NKnife.Channels.Base;
using NKnife.Channels.EventParams;
using NKnife.Channels.Interfaces;
using NKnife.Events;
using NKnife.Interface;

namespace NKnife.Channels.Serials
{
    public class SerialChannel : ChannelBase<IEnumerable<byte>>
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
                _serialPort.DataReceived += SyncDataReceived;
                _serialPort.DataReceived -= AsyncDataReceived;
            }
            else
            {
                _serialPort.DataReceived += AsyncDataReceived;
                _serialPort.DataReceived -= SyncDataReceived;
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
                _serialPort.DataReceived += SyncDataReceived;
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
                })
                {
                    Name = $"{_serialPort.PortName}-Thread",
                    IsBackground = true
                };
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
                        _serialPort.DataReceived -= SyncDataReceived;
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

        private void SetJobFunc(IJobPoolItem job)
        {
            if (!job.IsPool && job is IJob)
            {
                if (job is SerialQuestion question)
                    question.Run = Talk;
            }
            else
            {
                if (job is IJobPool pool)
                {
                    foreach (var poolItem in pool)
                        SetJobFunc(poolItem);
                }
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
            JobManager.Run();
        }

        /// <inheritdoc />
        public override void AsyncListen()
        {
            JobManager.Run();
        }

        private readonly AutoResetEvent _syncReceiveWaiter = new AutoResetEvent(false);

        /// <summary>
        /// ����Ƿ�����ĳ��question�ķ��͹�����
        /// </summary>
        private bool _onSyncSent = false;

        /// <summary>
        /// ��ͬ����ȡʱ��Buffer
        /// </summary>
        private byte[] _talkBuffer = new byte[0];

        protected bool Talk(IJob job)
        {
            var q = job as SerialQuestion;
            if (q == null)
                return false;
            _onSyncSent = true; //��������ǰ�ñ�ǣ��Ա�֤�ɶ�ȡ����
            var data = q.Data.ToArray();
            _serialPort.Write(data, 0, data.Length);

            var complete = false;
            while (!complete)
            {
                _onSyncSent = true;
                var buffer = new List<byte>();
                // ����ȴ���ͬ��ʱÿ�ζԻ��ĳ�ʱʱ������timeout
                if (_syncReceiveWaiter.WaitOne(q.Timeout)) //�������¼��յ����ص�����
                {
                    if (_talkBuffer.Length > 0)
                    {
                        buffer.AddRange(_talkBuffer);//����һ�ݣ���ֹ��ͻ
                        q.Answer = buffer;
                        //��Question����������У�飬�������δ����Ҫ�󣬽�ѭ�������ȴ�
                        if (q.Verify == null || (q.Verify != null && q.Verify(q)))
                        {
                            _talkBuffer = new byte[0];
                            complete = true;
                            //���Ի����ʱ������Question��Ӧ������¼�
                            q.OnAnswered(new EventArgs<IEnumerable<byte>>(buffer));
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///     ͬ�����յ�����
        /// </summary>
        protected virtual void SyncDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_onSyncSent)
            {
                // ��Ϊ�ǶԻ�ģʽ���ڷ� OnSyncReceive ���ڣ���ʱ��õ�������һ�Ŷ�����
                _serialPort.DiscardInBuffer();
                return;
            }

            try
            {
                var oldBufferLength = _talkBuffer.Length;
                var readLength = _serialPort.BytesToRead;
                //_talkBuffer�ǽ������ݵĻ�����
                //������ȡ�󣬶�ȡ����ȡ�����Ժ������һ��ѯ�ʶԻ�
                _talkBuffer = new byte[oldBufferLength + readLength];
                _serialPort.Read(_talkBuffer, oldBufferLength, readLength);
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
                OnDataArrived(new EventArgs<IEnumerable<byte>>(buffer));
            }
        }

    }
}