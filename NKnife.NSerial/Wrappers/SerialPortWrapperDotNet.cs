using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.NSerial.Interfaces;

namespace NKnife.NSerial.Wrappers
{
    /// <summary>
    ///     通过.net实现的串口操作类
    /// </summary>
    public class SerialPortWrapperDotNet : ISerialPortWrapper
    {
        private static readonly ILogger _Logger = LogFactory.GetCurrentClassLogger();

        protected readonly ManualResetEventSlim Reset = new ManualResetEventSlim(false);

        protected byte[] Buffer = new byte[64];
        protected int CurrReadLength;
        protected bool OnReceive;

        /// <summary>
        ///     串口操作类（通过.net 类库）
        /// </summary>
        protected SerialPort SerialPort;

        protected int TimeOut = 150;

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
            SerialPort = new SerialPort
                              {
                                  PortName = portName,
                                  BaudRate = 9600,
                                  DataBits = 8,
                                  ReadTimeout = 150,
                                  ReceivedBytesThreshold = 1,
                                  ReadBufferSize = 32,
                              };
            SerialPort.DataReceived += SerialPortDataReceived;
            SerialPort.ErrorReceived += SerialPortErrorReceived;
            try
            {
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                    SerialPort.Open();
                }
                else
                {
                    SerialPort.Open();
                }
                IsOpen = SerialPort.IsOpen;
                if (IsOpen)
                    _Logger.Info(string.Format("通讯:成功打开串口:{0}。{1},{2},{3}", portName, SerialPort.BaudRate,
                                               SerialPort.ReceivedBytesThreshold, SerialPort.ReadTimeout));
                return SerialPort.IsOpen;
            }
            catch (Exception e)
            {
                _Logger.Warn("无法打开串口:" + portName, e);
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
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                    _Logger.Info(string.Format("通讯:成功关闭串口:{0}。", SerialPort.PortName));
                }
                IsOpen = false;
                return true;
            }
            catch (Exception e)
            {
                _Logger.Warn("关闭串口异常:" + SerialPort.PortName, e);
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
            SerialPort.ReadTimeout = timeout;
            TimeOut = timeout;
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
                CurrReadLength = 0;
                SerialPort.Write(cmd, 0, cmd.Length);
                OnReceive = true;
                Reset.Reset();
                Reset.Wait(TimeOut); //收到返回
                OnReceive = false;

                int currReadLength = CurrReadLength;
                if (currReadLength > 0)
                {
                    recv = new byte[currReadLength];
                    Array.Copy(Buffer, recv, currReadLength);
                    Buffer = new byte[64];
                    CurrReadLength = 0;
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
        /// 接收到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!OnReceive)
            {
                SerialPort.DiscardInBuffer();
                return;
            }
            var readedBuffer = new byte[64];
            int recvCount = 0;
            try
            {
                recvCount = SerialPort.Read(readedBuffer, 0, readedBuffer.Length);
            }
            catch (TimeoutException ex)
            {
                
            }
            catch (IOException ex)
            {
                
            }
            if (recvCount > 0 && CurrReadLength + recvCount <= Buffer.Length)
            {
                System.Buffer.BlockCopy(readedBuffer, 0, Buffer, CurrReadLength, recvCount);
                CurrReadLength += recvCount;
                //TODO 结束符要做成活的，不能使用固定的0xFF
                if (readedBuffer[recvCount - 1] == 0xFF) //校验指令结束符FF
                {
                    Reset.Set();
                    OnReceive = false;
                }
            }
            else
            {
                Buffer = new byte[64];
                CurrReadLength = 0;
            }
        }

        protected virtual void SerialPortErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialPort.DiscardInBuffer();
        }
    }
}