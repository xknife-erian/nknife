using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using NKnife.Channels.Base;
using NKnife.Channels.EventParams;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Jobs;

namespace NKnife.Channels.Serial
{
    public class SerialChannel : ChannelBase<IEnumerable<byte>>
    {
        private readonly SerialConfig _config;
        private SerialPort _serialPort; //���ڲ����ࣨͨ��.net ��⣩
        private bool _isUpdateJobFunc = false;

        public SerialChannel(SerialConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), $"{nameof(config)}����Ϊ��");
            if (config.Port <= 0)
                throw new ArgumentNullException(nameof(config), $"���ںű������á�");
            _config = config;
        }

        #region Overrides of ChannelBase

        /// <summary>
        /// �򿪴���Ĭ�ϵȴ���ʱ����Ĭ��1���ӡ�
        /// </summary>
        public int OpenTimeout { get; set; } = 1000;

        public override void UpdateJobFunc()
        {
            SetJobFunc(JobManager.Pool);
            _isUpdateJobFunc = true;
        }

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
            SetJobFunc(JobManager.Pool);
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
                PortName = $"COM{_config.Port}",
                StopBits = _config.StopBit,
                BaudRate = _config.BaudRate,
                DataBits = _config.DataBit, //8,
                WriteTimeout = _config.WriteTotalTimeoutConstant, //1000
                ReadTimeout = _config.ReadTotalTimeoutConstant, //1000,
                ReceivedBytesThreshold = _config.ReceivedBytesThreshold, //1,
                ReadBufferSize = _config.ReadBufferSize, //64,
                DtrEnable = _config.DtrEnable,
                Parity = _config.Parity,
                RtsEnable = _config.RtsEnable
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
                        //_Logger.Debug($"ͨѶ:׼���򿪴���:{_serialPort.PortName}:{_serialPort.BaudRate}......");
                        _serialPort.Open();
                        IsOpen = true;
                    }
                    catch (Exception e)
                    {
                        IsOpen = false;
                        //_Logger.Warn(e.Message, e);
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
                    //_Logger.Info($"ͨѶ:�ɹ��򿪴���:{_serialPort.PortName}:{_serialPort.BaudRate}");
                }
                else
                {
                    //_Logger.Warn($"�޷��򿪴���:COM{_config.Port}");
                    thread.Abort();
                }
            }
            catch (Exception e)
            {
                //_Logger.Error($"�޷��򿪴���:COM{_config.Port}, {e.Message}", e);
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
                    //_Logger.Warn($"ִ�йرմ���ǰ�¼��쳣:{_serialPort.PortName}��{e.Message}", e);
                }

                try
                {
                    if (IsSynchronous)
                        _serialPort.DataReceived -= SyncDataReceived;
                    else
                        _serialPort.DataReceived -= AsyncDataReceived;
                    _serialPort.Close();
                    //_Logger.Info($"ͨѶ:�ɹ��رմ���:{_serialPort.PortName}��");
                }
                catch (Exception e)
                {
                    //_Logger.Warn($"�رմ����쳣:{_serialPort.PortName}, {e.Message}", e);
                    return false;
                }

                try
                {
                    OnClosed();
                }
                catch (Exception e)
                {
                    //_Logger.Warn($"ִ�йرմ��ں��¼��쳣:{_serialPort.PortName}��{e.Message}", e);
                }
            }

            IsOpen = false;
            return true;
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //_Logger.Warn($"SerialPortErrorReceived:{e.EventType}");
            _serialPort.DiscardInBuffer();
        }

        /// <summary>
        ///     ���ô���д�����ȡ��ʱ
        /// </summary>
        /// <param name="writeTotalTimeoutConstant">д��ʱ</param>
        /// <param name="readTotalTimeoutConstant">����ʱ</param>
        public void UpdateSerialPortTimeout(int writeTotalTimeoutConstant, int readTotalTimeoutConstant)
        {
            _config.ReadTotalTimeoutConstant = readTotalTimeoutConstant;
            _serialPort.ReadTimeout = readTotalTimeoutConstant;
            _config.WriteTotalTimeoutConstant = writeTotalTimeoutConstant;
            _serialPort.WriteTimeout = writeTotalTimeoutConstant;
        }

        #endregion

        private void SetJobFunc(IJobPoolItem job)
        {
            if (!job.IsPool && job is IJob)
            {
                if (job is SerialQuestion question)
                {
                    if (IsSynchronous)
                        question.Run = Talk;
                    else
                        question.Run = Broadcast;
                }
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
            if(!_isUpdateJobFunc)
                UpdateJobFunc();
            JobManager.Run();
        }

        /// <inheritdoc />
        public override void AsyncListen()
        {
            if (!_isUpdateJobFunc)
                UpdateJobFunc();
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
                            //TODO:!!!!! q.OnAnswered(new EventArgs<IEnumerable<byte>>(buffer));
                        }
                    }
                }
            }

            return true;
        }

        protected bool Broadcast(IJob job)
        {
            var q = job as SerialQuestion;
            if (q == null)
                return false;
            var data = q.Data.ToArray();
            _serialPort.Write(data, 0, data.Length);
            return true;
        }

        /// <summary>
        ///     ͬ�����յ�����
        /// </summary>
        protected virtual void SyncDataReceived(object sender, SerialDataReceivedEventArgs ex)
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
            catch (TimeoutException e)
            {
                //TODO:_Logger.Warn(e,$"���ڶ�ȡ��ʱ�쳣��{e.Message}");
            }
            catch (Exception e)
            {
                //TODO:_Logger.Warn(e,$"���ڶ�ȡ�쳣��{e.Message}");
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
        protected virtual void AsyncDataReceived(object sender, SerialDataReceivedEventArgs ex)
        {
            byte[] buffer = null;
            try
            {
                if (_config.ReadWait > 0)
                    Thread.Sleep(_config.ReadWait); //�������¼��󣬵ȴ��������ݸ�������һЩ�ٽ��ж�ȡ
                /**
                 * ���� ReadBufferSize ����ֻ��ʾ Windows �����Ļ�������
                 * �� BytesToRead ���Գ��˱�ʾ Windows �����Ļ������⻹��ʾ SerialPort ��������
                 * ���� BytesToRead ���Կ��Է���һ���� ReadBufferSize ���Դ��ֵ�� 
                 * ���ջ���������������������Ľ��ջ������Լ� SerialPort ����������ڲ����塣
                 */
                buffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(buffer, 0, buffer.Length);
            }
            catch (TimeoutException e)
            {
                //_Logger.Warn(e, $"���ڶ�ȡ��ʱ�쳣��{e.Message}");
            }
            catch (Exception e)
            {
                //_Logger.Warn(e, $"���ڶ�ȡ�쳣��{e.Message}");
            }
            if (buffer != null && buffer.Length > 0)
            {
                OnDataArrived(new EventArgs<IEnumerable<byte>>(buffer));
            }
        }

    }
}