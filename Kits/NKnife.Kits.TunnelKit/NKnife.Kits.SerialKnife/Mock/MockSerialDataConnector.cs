using System;
using Common.Logging;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SerialKnife.Common;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.Mock
{
    public class MockSerialDataConnector : ISerialConnector
    {
        private static readonly ILog _logger = LogManager.GetLogger<MockSerialDataConnector>();

        #region IKnifeSerialConnector

        public int PortNumber { get; set; }

        public SerialType SerialType { get; set; }

        public SerialConfig SerialConfig { get; set; }

        #endregion

        #region IDataConnector

        public event EventHandler<SessionEventArgs> SessionBuilt;
        public event EventHandler<SessionEventArgs> SessionBroken;
        public event EventHandler<SessionEventArgs> DataReceived;

        public void SendAll(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void KillSession(long id)
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            _logger.Info(string.Format("串口{0}关闭成功", PortNumber));
            EventHandler<SessionEventArgs> handler = SessionBroken;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = PortNumber
                }));
            }
            return true;
        }

        public bool Start()
        {
            _logger.Info(string.Format("串口{0}连接成功", PortNumber));
            EventHandler<SessionEventArgs> handler = SessionBuilt;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = PortNumber
                }));
            }
            return true;
        }

        public event EventHandler<SessionEventArgs> DataSent;

        public void Send(long id, byte[] data)
        {
            EventHandler<SessionEventArgs> handler = DataSent;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = id,
                    Data = data,
                }));
        }

        public bool SessionExist(long id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 模拟方法

        public void MockReceive(byte[] data)
        {
            EventHandler<SessionEventArgs> handler = DataReceived;
            if (handler != null)
                handler.Invoke(this, new SessionEventArgs(new TunnelSession
                {
                    Id = 1,
                    Data = data,
                }));
        }

        #endregion
    }
}