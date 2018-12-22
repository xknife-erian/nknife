using System;
using Common.Logging;
using NKnife.IoC;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SerialKnife.Common;
using SerialKnife.Interfaces;

namespace SerialKnife
{
    public class SerialPortDataConnector : ISerialConnector
    {
        private static readonly ILog _Logger = LogManager.GetLogger<SerialPortDataConnector>();
        private ISerialPortWrapper _serial;

        public SerialPortDataConnector()
        {
            IsInitialized = false;
            SerialType = SerialType.DotNet; 
        }

        public SerialType SerialType { get; set; }

        #region IKnifeSerialConnector

        public bool IsInitialized { get; set; }

        #region event

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;
        public event EventHandler<SessionEventArgs> DataSent;

        protected virtual void OnSessionBuilt(SessionEventArgs e)
        {
            SessionBuilt?.Invoke(this, e);
        }

        protected virtual void OnSessionBuilt()
        {
            OnSessionBuilt(new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber
            }));
        }

        protected virtual void OnSessionBroken(SessionEventArgs e)
        {
            SessionBroken?.Invoke(this, e);
        }

        protected virtual void OnSessionBroken()
        {
            OnSessionBroken(new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber
            }));
        }

        protected virtual void OnDataReceived(SessionEventArgs e)
        {
            DataReceived?.Invoke(this, e);
        }

        protected virtual void OnDataReceived(byte[] data)
        {
            var e = new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber,
                Data = data
            });
            OnDataReceived(e);
        }

        protected virtual void OnDataSent(SessionEventArgs e)
        {
            DataSent?.Invoke(this, e);
        }

        protected virtual void OnDataSent(byte[] data)
        {
            var e = new SessionEventArgs(new TunnelSession
            {
                Id = PortNumber,
                Data = data
            });
            OnDataSent(e);
        }

        #endregion

        public int PortNumber { get; set; }

        public SerialConfig SerialConfig { get; set; }

        public void Send(long id, byte[] data)
        {
            if (_serial == null)
                return;
            byte[] received;
            _serial.SendReceived(data, out received);
            OnDataSent(data);
            if (received != null)
            {
                OnDataReceived(received);
            }
        }

        void IDataConnector.SendAll(byte[] data)
        {
            if (_serial == null)
                return;
            byte[] received;
            _serial.SendReceived(data, out received);
            OnDataSent(data); //激发发送完成事件
            if (received != null)
            {
                OnDataReceived(received); //激发接收到数据的事件
            }
        }

        public void KillSession(long id)
        {
            if (_serial.IsOpen)
            {
                _serial.Close();
            }
        }

        public bool SessionExist(long id)
        {
            CheckAndInitiate();
            return _serial.IsOpen;
        }

        public bool Stop()
        {
            CheckAndInitiate();
            if (!_serial.IsOpen)
            {
                return true;
            }
            var result = _serial.Close();
            if (result)
            {
                OnSessionBroken();
            }
            IsInitialized = false;
            return result;
        }

        public bool Start()
        {
            CheckAndInitiate();
            if (_serial.IsOpen)
            {
                return true;
            }
            var port = $"COM{PortNumber}";
            if (SerialConfig == null)
                SerialConfig = new SerialConfig();
            var result = _serial.Initialize(port, SerialConfig);
            if (result)
            {
                _Logger.Info($"串口{port}初始化完成：{true}");
                OnSessionBuilt();
            }
            else
            {
                _Logger.Warn($"串口{port}初始化完成：{false}");
            }
            IsInitialized = true;
            return result;
        }

        protected virtual void CheckAndInitiate()
        {
            if (_serial == null)
            {
                _serial = Di.Get<ISerialPortWrapper>(SerialType.ToString());
            }
        }

        #endregion
    }
}