using System;
using System.Threading;
using NKnife.NSerial.Interfaces;

namespace NKnife.NSerial.Wrappers
{
    public class SerialPortWrapperWinApi : ISerialPortWrapper
    {
        /// <summary>通过windows api实现的串口操作类
        /// </summary>
        private SerialPortWin32 _SerialPort;

        #region ISerialPortWrapper Members

        /// <summary>串口是否打开标记
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>初始化操作器通讯串口
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public bool InitPort(string portName)
        {
            _SerialPort = new SerialPortWin32 {Port = portName, BaudRate = 9600, ByteSize = 8, ReadTimeout = 150};

            try
            {
                if (_SerialPort.Opened)
                {
                    _SerialPort.Close();
                    _SerialPort.Open();
                }
                else
                {
                    if (_SerialPort.Open() < 0)
                    {
                        IsOpen = false;
                        return false;
                    }
                }
                IsOpen = true;
                return true;
            }
            catch
            {
                IsOpen = false;
                return false;
            }
        }

        /// <summary>关闭串口
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (_SerialPort.Opened)
                {
                    _SerialPort.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置串口读取超时
        /// </summary>
        /// <param name="timeout"></param>
        public void SetTimeOut(int timeout)
        {
            if (IsOpen)
            {
                _SerialPort.SetTimeOut(timeout);
            }
        }

        /// <summary>发送数据
        /// </summary>
        /// <param name="cmd">待发送的数据</param>
        /// <param name="recv">回复的数据</param>
        /// <returns>
        /// 回复的数据的长度
        /// </returns>
        public int SendData(byte[] cmd, out byte[] recv)
        {
            try
            {
                _SerialPort.Write(cmd, cmd.Length);
                Thread.Sleep(3);
                var buffer = new byte[64];
                int readcount = _SerialPort.Read(ref buffer, buffer.Length);
                if (readcount > 0)
                {
                    recv = new byte[readcount];
                    Array.Copy(buffer, recv, readcount);
                }
                else
                {
                    recv = null;
                }
                return readcount;
            }
            catch
            {
                recv = null;
                return 0;
            }
        }

        #endregion
    }
}