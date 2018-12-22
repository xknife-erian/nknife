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
        private SerialPort _serialPort; //串口操作类（通过.net 类库）

        public SerialChannel(SerialConfig serialConfig)
        {
            if (serialConfig == null)
                throw new ArgumentNullException(nameof(serialConfig), $"{nameof(serialConfig)}不能为空");
            if (serialConfig.Port <= 0)
                throw new ArgumentNullException(nameof(serialConfig), $"串口号必须设置。");
            _serialConfig = serialConfig;
        }

        #region Overrides of ChannelBase

        /// <summary>
        /// 打开串口默认等待的时长。默认1秒钟。
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
        ///     打开采集通道
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
                        _Logger.Debug($"通讯:准备打开串口:{_serialPort.PortName}:{_serialPort.BaudRate}......");
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
                openReset.WaitOne(OpenTimeout); //打开串口等待1.5秒
                if (IsOpen)
                {
                    OnOpened();
                    _Logger.Info($"通讯:成功打开串口:{_serialPort.PortName}:{_serialPort.BaudRate}");
                }
                else
                {
                    _Logger.Warn($"无法打开串口:COM{_serialConfig.Port}");
                    thread.Abort();
                }
            }
            catch (Exception e)
            {
                _Logger.Error($"无法打开串口:COM{_serialConfig.Port}, {e.Message}", e);
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
                    _Logger.Warn($"执行关闭串口前事件异常:{_serialPort.PortName}。{e.Message}", e);
                }

                try
                {
                    if (IsSynchronous)
                        _serialPort.DataReceived -= SyncSerialPortDataReceived;
                    else
                        _serialPort.DataReceived -= AsyncDataReceived;
                    _serialPort.Close();
                    _Logger.Info($"通讯:成功关闭串口:{_serialPort.PortName}。");
                }
                catch (Exception e)
                {
                    _Logger.Warn($"关闭串口异常:{_serialPort.PortName}, {e.Message}", e);
                    return false;
                }

                try
                {
                    OnClosed();
                }
                catch (Exception e)
                {
                    _Logger.Warn($"执行关闭串口后事件异常:{_serialPort.PortName}。{e.Message}", e);
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
        ///     设置串口写入与读取超时
        /// </summary>
        /// <param name="writeTotalTimeoutConstant">写超时</param>
        /// <param name="readTotalTimeoutConstant">读超时</param>
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
        /// 当同步读取时的Buffer
        /// </summary>
        private byte[] _talkBuffer = new byte[0];

        protected void Talk(SerialQuestion question)
        {
            var complate = false; //是否读取到了完整的数据，即这次对话是否完成
            _onSyncSent = true; //即将发出前置标记，以保证可读取数据
            _serialPort.Write(question.Data, 0, question.Data.Length);

            while (!complate)
            {
                // 发出后等待
                _onSyncSent = true; //标记
                // 这个等待是同步时每次对话的时间
                if (_syncReceiveWaiter.WaitOne(question.Timeout)) //监听从事件收到返回的数据
                {
                    if (_talkBuffer.Length > 0)
                    {
                        var currBuffer = new byte[_talkBuffer.Length];
                        Buffer.BlockCopy(_talkBuffer, 0, currBuffer, 0, _talkBuffer.Length);
                        _talkBuffer = new byte[0];
                        //将读取到的数据抛出进行解析，如果认为已完成本次询问进入下一次对话。
                        //如果未完成本次询问再次进入等待回答(读取)状态。
                    }
                }
            }
        }

        /// <summary>
        ///     同步接收到数据
        /// </summary>
        protected virtual void SyncSerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_onSyncSent)
            {
                // 因为是对话模式，在非 OnSyncReceive 期内，这时候得到的数据一概丢弃。
                _serialPort.DiscardInBuffer();
                return;
            }

            try
            {
                //_talkBuffer是交换数据的缓冲区
                //触发读取后，读取，读取本次以后进入下一次询问对话
                _talkBuffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(_talkBuffer, 0, _talkBuffer.Length);
            }
            catch (TimeoutException ex)
            {
                _Logger.Warn($"串口读取超时异常：{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _Logger.Warn($"串口读取异常：{ex.Message}", ex);
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
        protected virtual void AsyncDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = null;
            try
            {
                if (_serialConfig.ReadWait > 0)
                    Thread.Sleep(_serialConfig.ReadWait); //当进入事件后，等待串口数据更加完整一些再进行读取
                /**
                 * 由于 ReadBufferSize 属性只表示 Windows 创建的缓冲区，
                 * 而 BytesToRead 属性除了表示 Windows 创建的缓冲区外还表示 SerialPort 缓冲区，
                 * 所以 BytesToRead 属性可以返回一个比 ReadBufferSize 属性大的值。 
                 * 接收缓冲区包括串行驱动程序的接收缓冲区以及 SerialPort 对象自身的内部缓冲。
                 */
                buffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(buffer, 0, buffer.Length);
            }
            catch (TimeoutException ex)
            {
                _Logger.Warn($"串口读取超时异常：{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _Logger.Warn($"串口读取异常：{ex.Message}", ex);
            }

            if (buffer != null && buffer.Length > 0)
            {
                OnDataArrived(new SerialChannelAnswerDataEventArgs(q.Instrument, buffer));
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
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void AsyncListen()
        {
            throw new NotImplementedException();
        }
    }
}