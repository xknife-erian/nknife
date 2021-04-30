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
        private SerialPort _serialPort; //串口操作类（通过.net 类库）
        private bool _isUpdateJobFunc = false;

        public SerialChannel(SerialConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), $"{nameof(config)}不能为空");
            if (config.Port <= 0)
                throw new ArgumentNullException(nameof(config), $"串口号必须设置。");
            _config = config;
        }

        #region Overrides of ChannelBase

        /// <summary>
        /// 打开串口默认等待的时长。默认1秒钟。
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
        ///     打开采集通道
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
                        //_Logger.Debug($"通讯:准备打开串口:{_serialPort.PortName}:{_serialPort.BaudRate}......");
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
                openReset.WaitOne(OpenTimeout); //打开串口等待1.5秒
                if (IsOpen)
                {
                    OnOpened();
                    //_Logger.Info($"通讯:成功打开串口:{_serialPort.PortName}:{_serialPort.BaudRate}");
                }
                else
                {
                    //_Logger.Warn($"无法打开串口:COM{_config.Port}");
                    thread.Abort();
                }
            }
            catch (Exception e)
            {
                //_Logger.Error($"无法打开串口:COM{_config.Port}, {e.Message}", e);
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
            if (IsOpen && _serialPort.IsOpen)
            {
                try
                {
                    OnClosing();
                }
                catch (Exception e)
                {
                    //_Logger.Warn($"执行关闭串口前事件异常:{_serialPort.PortName}。{e.Message}", e);
                }

                try
                {
                    if (IsSynchronous)
                        _serialPort.DataReceived -= SyncDataReceived;
                    else
                        _serialPort.DataReceived -= AsyncDataReceived;
                    _serialPort.Close();
                    //_Logger.Info($"通讯:成功关闭串口:{_serialPort.PortName}。");
                }
                catch (Exception e)
                {
                    //_Logger.Warn($"关闭串口异常:{_serialPort.PortName}, {e.Message}", e);
                    return false;
                }

                try
                {
                    OnClosed();
                }
                catch (Exception e)
                {
                    //_Logger.Warn($"执行关闭串口后事件异常:{_serialPort.PortName}。{e.Message}", e);
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
        ///     设置串口写入与读取超时
        /// </summary>
        /// <param name="writeTotalTimeoutConstant">写超时</param>
        /// <param name="readTotalTimeoutConstant">读超时</param>
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
        ///     中断正在进行的发送接线过程，无论是异步与同步。
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
        /// 标记是否正在某个question的发送过程中
        /// </summary>
        private bool _onSyncSent = false;

        /// <summary>
        /// 当同步读取时的Buffer
        /// </summary>
        private byte[] _talkBuffer = new byte[0];

        protected bool Talk(IJob job)
        {
            var q = job as SerialQuestion;
            if (q == null)
                return false;
            _onSyncSent = true; //即将发出前置标记，以保证可读取数据
            var data = q.Data.ToArray();
            _serialPort.Write(data, 0, data.Length);

            var complete = false;
            while (!complete)
            {
                _onSyncSent = true;
                var buffer = new List<byte>();
                // 这个等待是同步时每次对话的超时时长，即timeout
                if (_syncReceiveWaiter.WaitOne(q.Timeout)) //监听从事件收到返回的数据
                {
                    if (_talkBuffer.Length > 0)
                    {
                        buffer.AddRange(_talkBuffer);//复制一份，防止冲突
                        q.Answer = buffer;
                        //当Question设置了数据校验，如果数据未满足要求，将循环继续等待
                        if (q.Verify == null || (q.Verify != null && q.Verify(q)))
                        {
                            _talkBuffer = new byte[0];
                            complete = true;
                            //当对话完成时，激发Question的应答就绪事件
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
        ///     同步接收到数据
        /// </summary>
        protected virtual void SyncDataReceived(object sender, SerialDataReceivedEventArgs ex)
        {
            if (!_onSyncSent)
            {
                // 因为是对话模式，在非 OnSyncReceive 期内，这时候得到的数据一概丢弃。
                _serialPort.DiscardInBuffer();
                return;
            }

            try
            {
                var oldBufferLength = _talkBuffer.Length;
                var readLength = _serialPort.BytesToRead;
                //_talkBuffer是交换数据的缓冲区
                //触发读取后，读取，读取本次以后进入下一次询问对话
                _talkBuffer = new byte[oldBufferLength + readLength];
                _serialPort.Read(_talkBuffer, oldBufferLength, readLength);
            }
            catch (TimeoutException e)
            {
                //TODO:_Logger.Warn(e,$"串口读取超时异常：{e.Message}");
            }
            catch (Exception e)
            {
                //TODO:_Logger.Warn(e,$"串口读取异常：{e.Message}");
            }
            finally
            {
                _onSyncSent = false;
                _syncReceiveWaiter.Set(); //通知 Talk 函数继续
            }
        }

        /// <summary>
        ///     异步接收到数据
        /// </summary>
        protected virtual void AsyncDataReceived(object sender, SerialDataReceivedEventArgs ex)
        {
            byte[] buffer = null;
            try
            {
                if (_config.ReadWait > 0)
                    Thread.Sleep(_config.ReadWait); //当进入事件后，等待串口数据更加完整一些再进行读取
                /**
                 * 由于 ReadBufferSize 属性只表示 Windows 创建的缓冲区，
                 * 而 BytesToRead 属性除了表示 Windows 创建的缓冲区外还表示 SerialPort 缓冲区，
                 * 所以 BytesToRead 属性可以返回一个比 ReadBufferSize 属性大的值。 
                 * 接收缓冲区包括串行驱动程序的接收缓冲区以及 SerialPort 对象自身的内部缓冲。
                 */
                buffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(buffer, 0, buffer.Length);
            }
            catch (TimeoutException e)
            {
                //_Logger.Warn(e, $"串口读取超时异常：{e.Message}");
            }
            catch (Exception e)
            {
                //_Logger.Warn(e, $"串口读取异常：{e.Message}");
            }
            if (buffer != null && buffer.Length > 0)
            {
                OnDataArrived(new EventArgs<IEnumerable<byte>>(buffer));
            }
        }

    }
}