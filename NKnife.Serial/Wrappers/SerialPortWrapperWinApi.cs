using System;
using System.Threading;
using SerialKnife.Common;
using SerialKnife.Interfaces;

namespace SerialKnife.Wrappers
{
    public class SerialPortWrapperWinApi : ISerialPortWrapper
    {
        /// <summary>通过windows api实现的串口操作类
        /// </summary>
        private SerialPortWin32 _serialPort;

        private string _portName;
        private SerialConfig _serialConfig;

        #region ISerialPortWrapper Members

        /// <summary>串口是否打开标记
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>初始化通讯串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool Initialize(string portName, SerialConfig config)
        {
            _portName = portName;
            _serialConfig = config;
            _serialPort = new SerialPortWin32
            {
                Port = portName,
                BaudRate = config.BaudRate,
                ByteSize = (byte) config.DataBits,
                ReadTimeout = config.ReadTimeout,
            };

            try
            {
                if (_serialPort._Opened)
                {
                    _serialPort.Close();
                    _serialPort.Open();
                }
                else
                {
                    if (_serialPort.Open() < 0)
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
                if (_serialPort._Opened)
                {
                    _serialPort.Close();
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
                _serialPort.SetTimeOut(timeout);
            }
        }

        /// <summary>发送数据
        /// </summary>
        /// <param name="cmd">待发送的数据</param>
        /// <param name="recv">回复的数据</param>
        /// <returns>
        /// 回复的数据的长度
        /// </returns>
        public int SendReceived(byte[] cmd, out byte[] recv)
        {
            try
            {
                _serialPort.Write(cmd, cmd.Length);
                Thread.Sleep(3);//TODO:发送后的读取延时有待考究
                var buffer = new byte[_serialConfig.ReadBufferSize];
                int readcount = _serialPort.Read(ref buffer, buffer.Length);
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