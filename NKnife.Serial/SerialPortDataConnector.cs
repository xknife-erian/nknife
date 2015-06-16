﻿using System;
using Common.Logging;
using NKnife.IoC;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SerialKnife.Common;
using SerialKnife.Interfaces;

namespace SerialKnife
{
    public class SerialPortDataConnector : ISerialConnector
    {
        private static readonly ILog _logger = LogManager.GetLogger<SerialPortDataConnector>();
        private ISerialPortWrapper _Serial;

        public SerialPortDataConnector()
        {
            SerialType = SerialType.DotNet; //默认使用winapi实现
        }

        public SerialType SerialType { get; set; }

        #region IKnifeSerialConnector

        #region event

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;
        public event EventHandler<SessionEventArgs> DataSent;

        protected virtual void OnSessionBuilt(SessionEventArgs e)
        {
            var handler = SessionBuilt;
            if (handler != null)
                handler(this, e);
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
            var handler = SessionBroken;
            if (handler != null)
                handler(this, e);
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
            var handler = DataReceived;
            if (handler != null)
                handler(this, e);
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
            var handler = DataSent;
            if (handler != null)
                handler(this, e);
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
            if (_Serial == null)
                return;
            byte[] received;
            _Serial.SendReceived(data, out received);
            OnDataSent(data);
            if (received != null)
            {
                OnDataReceived(received);
            }
        }

        public void SendAll(byte[] data)
        {
            if (_Serial == null)
                return;
            byte[] received;
            _Serial.SendReceived(data, out received);
            OnDataSent(data);
            if (received != null)
            {
                OnDataReceived(received);
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
                OnSessionBroken();
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
            if (SerialConfig == null)
                SerialConfig = new SerialConfig();
            var result = _Serial.Initialize(port, SerialConfig);
            if (result)
            {
                _logger.Info(string.Format("串口{0}初始化完成：{1}", port, true));
                OnSessionBuilt();
            }
            else
            {
                _logger.Warn(string.Format("串口{0}初始化完成：{1}", port, false));
            }
            return result;
        }

        protected virtual void CheckAndInitiate()
        {
            if (_Serial == null)
            {
                _Serial = DI.Get<ISerialPortWrapper>(SerialType.ToString());
            }
        }

        #endregion
    }
}