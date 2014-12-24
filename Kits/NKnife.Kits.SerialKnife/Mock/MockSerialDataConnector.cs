using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using NKnife.Tunnel.Events;
using SerialKnife.Interfaces;

namespace NKnife.Kits.SerialKnife.Mock
{
    public class MockSerialDataConnector : IKnifeSerialConnector
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region IKnifeSerialConnector
        public int PortNumber { get; set; }
        #endregion

        #region IDataConnector
        public event EventHandler<SessionEventArgs<byte[], int>> SessionBuilt;
        public event EventHandler<SessionEventArgs<byte[], int>> SessionBroken;
        public event EventHandler<SessionEventArgs<byte[], int>> DataReceived;
        public event EventHandler<SessionEventArgs<byte[], int>> DataSent;

        public void Send(int id, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void SendAll(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void KillSession(int id)
        {
            throw new NotImplementedException();
        }

        public bool SessionExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            _logger.Info(string.Format("串口{0}关闭成功", PortNumber));
            var handler = SessionBroken;
            if (handler != null)
            {
                handler.Invoke(this, new SessionEventArgs<byte[], int>(new IntKnifeTunnelSession()
                {
                    Id = PortNumber
                }));
            }
            return true;
        }

        public bool Start()
        {
            _logger.Info(string.Format("串口{0}连接成功",PortNumber));
            var handler = SessionBuilt;
            if (handler != null)
            {
                handler.Invoke(this,new SessionEventArgs<byte[], int>(new IntKnifeTunnelSession()
                {
                    Id = PortNumber
                }));
            }
            return true;
        }
        #endregion



    }
}
