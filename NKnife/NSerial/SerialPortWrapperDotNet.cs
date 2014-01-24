using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using NKnife.NSerial.Base;
using NLog;

namespace NKnife.NSerial
{
    /// <summary>
    ///     通过.net实现的串口操作类
    /// </summary>
    public class SerialPortWrapperDotNet : ISerialPortWrapper
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        protected readonly ManualResetEventSlim _Reset = new ManualResetEventSlim(false);

        protected byte[] _Buffer = new byte[64];
        protected int _CurrReadLength;
        protected bool _OnReceive;

        /// <summary>
        ///     串口操作类（通过.net 类库）
        /// </summary>
        protected SerialPort _SerialPort;

        //protected byte[] _TempBuffer = new byte[64];
        protected int _TimeOut = 150;

        #region ISerialPortWrapper Members

        /// <summary>
        ///     串口是否打开标记
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        ///     初始化操作器通讯串口
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public bool InitPort(string portName)
        {
            _SerialPort = new SerialPort
                              {
                                  PortName = portName,
                                  BaudRate = 9600,
                                  DataBits = 8,
                                  ReadTimeout = 150,
                                  ReceivedBytesThreshold = 1,
                                  ReadBufferSize = 32,
                              };
            _SerialPort.DataReceived += SerialPortDataReceived;
            _SerialPort.ErrorReceived += SerialPortErrorReceived;
            try
            {
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                    _SerialPort.Open();
                }
                else
                {
                    _SerialPort.Open();
                }
                IsOpen = _SerialPort.IsOpen;
                if (IsOpen)
                    _Logger.Info(string.Format("通讯:成功打开串口:{0}。{1},{2},{3}", portName, _SerialPort.BaudRate,
                                               _SerialPort.ReceivedBytesThreshold, _SerialPort.ReadTimeout));
                return _SerialPort.IsOpen;
            }
            catch (Exception e)
            {
                _Logger.WarnE("无法打开串口:" + portName, e);
                IsOpen = false;
                return false;
            }
        }

        /// <summary>
        ///     关闭串口
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (_SerialPort.IsOpen)
                {
                    _SerialPort.Close();
                    _Logger.Info(string.Format("通讯:成功关闭串口:{0}。", _SerialPort.PortName));
                }
                IsOpen = false;
                return true;
            }
            catch (Exception e)
            {
                _Logger.WarnE("关闭串口异常:" + _SerialPort.PortName, e);
                return false;
            }
        }

        /// <summary>
        ///     设置串口读取超时
        /// </summary>
        /// <param name="timeout"></param>
        public void SetTimeOut(int timeout)
        {
            if (!IsOpen)
                return;
            _SerialPort.ReadTimeout = timeout;
            _TimeOut = timeout;
        }

        /// <summary>
        ///     发送数据
        /// </summary>
        /// <param name="cmd">待发送的数据</param>
        /// <param name="recv">回复的数据</param>
        /// <returns>回复的数据的长度</returns>
        public virtual int SendData(byte[] cmd, out byte[] recv)
        {
            try
            {
                _CurrReadLength = 0;
                _SerialPort.Write(cmd, 0, cmd.Length);
                _OnReceive = true;
                _Reset.Reset();
                _Reset.Wait(_TimeOut); //收到返回
                _OnReceive = false;

                int currReadLength = _CurrReadLength;
                if (currReadLength > 0)
                {
                    recv = new byte[currReadLength];
                    Array.Copy(_Buffer, recv, currReadLength);
                    _Buffer = new byte[64];
                    _CurrReadLength = 0;
                }
                else
                {
                    recv = null;
                }
                return currReadLength;
            }
            catch
            {
                recv = null;
                return 0;
            }
        }

        #endregion

        /// <summary>
        ///     接收到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_OnReceive)
            {
                _SerialPort.DiscardInBuffer();
                return;
            }
            var readedBuffer = new byte[64];
            int recvCount = 0;
            try
            {
                recvCount = _SerialPort.Read(readedBuffer, 0, readedBuffer.Length);
                //_SerialPort.DiscardInBuffer();
            }
            catch (TimeoutException ex)
            {
                
            }
            catch (IOException ex)
            {
                
            }
            if (recvCount > 0 && _CurrReadLength + recvCount <= _Buffer.Length)
            {
                Buffer.BlockCopy(readedBuffer, 0, _Buffer, _CurrReadLength, recvCount);
                _CurrReadLength += recvCount;
                if (readedBuffer[recvCount - 1] == 0xFF) //校验指令结束符FF
                {
                    _Reset.Set();
                    _OnReceive = false;
                }
            }
            else
            {
                _Buffer = new byte[64];
                _CurrReadLength = 0;
            }
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _SerialPort.DiscardInBuffer();
        }
    }
}