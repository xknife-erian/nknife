using System;
using Common.Logging;
using NKnife.IoC;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SerialKnife.Common;
using SerialKnife.Interfaces;

namespace SerialKnife
{
    public class SerialPortDataConnector : IKnifeSerialConnector
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialPortDataConnector>();
        private ISerialPortWrapper _Serial;

        public SerialPortDataConnector()
        {
            SerialType = SerialType.DotNet; //默认使用winapi实现
        }

        public SerialType SerialType { get; set; }

        private void CheckAndInitiate()
        {
            if (_Serial == null)
            {
                _Serial = DI.Get<ISerialPortWrapper>(SerialType.ToString());
            }
        }

        private void InvokeDataSent(byte[] data)
        {
            var handler = DataSent;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = PortNumber,
                    Data = data
                }));
            }
        }

        private void InvokeDataReceived(byte[] data)
        {
            var handler = DataReceived;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = PortNumber,
                    Data = data
                }));
            }
        }

        private void InvokeSessionBroken()
        {
            var handler = SessionBroken;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = PortNumber
                }));
            }
        }

        private void InvokeSessionBuilt()
        {
            var handler = SessionBuilt;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = PortNumber
                }));
            }
        }

        #region IKnifeSerialConnector

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;
        public event EventHandler<SessionEventArgs> DataSent;
        public int PortNumber { get; set; }

        public void Send(long id, byte[] data)
        {
            byte[] received;
            _Serial.SendData(data, out received);
            InvokeDataSent(data);
            if (received != null)
            {
                InvokeDataReceived(received);
            }
        }

        public void SendAll(byte[] data)
        {
            byte[] received;
            _Serial.SendData(data, out received);
            InvokeDataSent(data);
            if (received != null)
            {
                InvokeDataReceived(received);
            }
        }

        public void KillSession(long id)
        {
            if (_Serial.IsOpen)
            {
                _Serial.Close();
            }
        }

        public bool SessionExist(long id)
        {
            CheckAndInitiate();
            return _Serial.IsOpen;
        }

        public bool Stop()
        {
            CheckAndInitiate();
            if (!_Serial.IsOpen)
            {
                return true;
            }
            var result = _Serial.Close();
            if (result)
            {
                InvokeSessionBroken();
            }
            return result;
        }

        public bool Start()
        {
            CheckAndInitiate();
            if (_Serial.IsOpen)
            {
                return true;
            }
            var port = string.Format("COM{0}", PortNumber);
            var result = _Serial.InitPort(port, new SerialConfig());
            if (result)
            {
                _logger.Info(string.Format("串口{0}初始化完成：{1}", port, true));
                InvokeSessionBuilt();
            }
            else
            {
                _logger.Warn(string.Format("串口{0}初始化完成：{1}", port, false));
            }
            return result;
        }

        #endregion
    }
}