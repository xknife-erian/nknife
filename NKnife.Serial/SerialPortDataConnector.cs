﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.IoC;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SerialKnife.Common;
using SerialKnife.Interfaces;

namespace SerialKnife
{
    public class SerialPortDataConnector : IKnifeSerialConnector
    {
        private ISerialPortWrapper _SerialComm = null;

        public SerialType SerialType { get; set; }

        public SerialPortDataConnector()
        {
            SerialType = SerialType.WinApi; //默认使用winapi实现
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
            _SerialComm.SendData(data, out received);
            InvokeDataSent(data);
            if (received != null)
            {
                InvokeDataReceived(received);
            }
        }

        public void SendAll(byte[] data)
        {
            byte[] received;
            _SerialComm.SendData(data, out received);
            InvokeDataSent(data);
            if (received != null)
            {
                InvokeDataReceived(received);
            }
        }

        public void KillSession(long id)
        {
            if (_SerialComm.IsOpen)
                _SerialComm.Close();
        }

        public bool SessionExist(long id)
        {
            CheckAndInitiate();
            return _SerialComm.IsOpen;
        }

        public bool Stop()
        {
            CheckAndInitiate();
            if (!_SerialComm.IsOpen)
            {
                return true;
            }
            var result = _SerialComm.Close();
            if (result)
                InvokeSessionBroken();
            return result;
        }

        public bool Start()
        {
            CheckAndInitiate();
            if (_SerialComm.IsOpen)
            {
                return true;
            }
            var result = _SerialComm.InitPort(string.Format("COM{0}", PortNumber));
            if (result)
                InvokeSessionBuilt();
            return result;
        }

        #endregion

        private void CheckAndInitiate()
        {
            if (_SerialComm == null)
            {
                _SerialComm = DI.Get<ISerialPortWrapper>(SerialType.ToString());
            }
        }

        private void InvokeDataSent(byte[] data)
        {
            var handler = DataSent;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()
                {
                    Id = PortNumber,
                    Data = data,
                }));
        }

        private void InvokeDataReceived(byte[] data)
        {
            var handler = DataReceived;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()
                {
                    Id = PortNumber,
                    Data = data,
                }));
        }

        private void InvokeSessionBroken()
        {
            var handler = SessionBroken;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()
                {
                    Id = PortNumber,
                }));
        }

        private void InvokeSessionBuilt()
        {
            var handler = SessionBuilt;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession()
                {
                    Id = PortNumber,
                }));
        }
    }
}
