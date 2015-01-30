using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;

namespace NKnife.Kits.SerialKnife.Filters
{
    public class ProtocolHandleFilter : KnifeProtocolProcessorBase<byte[]>, ITunnelFilter<byte[], int>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly byte[] _CurrentReceiveBuffer = new byte[1024];
        private int _CurrentReceiveByteLength = 0;

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        #region Interface
        public bool PrcoessReceiveData(ITunnelSession<byte[], int> session)
        {
            var data = session.Data;
            var len = data.Length;
            if (len == 0)
                return false;
            if (_CurrentReceiveByteLength + len > 1024) //缓冲区溢出了，只保留后1024位
            {
                //暂时不做处理，直接抛弃
                _logger.Warn("收到的数据超出1024，缓冲区溢出，该条数据抛弃");
                return false;
            }

            var tempData = new byte[_CurrentReceiveByteLength + len];
            Array.Copy(_CurrentReceiveBuffer, 0, tempData, 0, _CurrentReceiveByteLength);
            Array.Copy(data, 0, tempData, _CurrentReceiveByteLength,data.Length);
            var unfinished = new byte[]{};
            var protocols = ProcessDataPacket(tempData, unfinished);

            if (unfinished.Length > 0)
            {
                Array.Copy(unfinished, 0, _CurrentReceiveBuffer,0,unfinished.Length);
                _CurrentReceiveByteLength = unfinished.Length;
            }
            else
            {
                _CurrentReceiveByteLength = 0;
            }

            if (protocols != null)
            {
                var handler = ProtocolsReceived;
                if (handler != null)
                {
                    handler.Invoke(this, new EventArgs<IEnumerable<IProtocol<byte[]>>>(protocols));
                }
            }
            return true;
        }

        public void ProcessSessionBroken(int id)
        {
        }

        public void ProcessSessionBuilt(int id)
        {
        }

        public void ProcessSendToSession(ITunnelSession<byte[], int> session)
        {

        }

        public void ProcessSendToAll(byte[] data)
        {
            
        }

        public event EventHandler<SessionEventArgs<byte[], int>> OnSendToSession;
        public event EventHandler<EventArgs<byte[]>> OnSendToAll;
        public event EventHandler<EventArgs<int>> OnKillSession;

        #endregion
    }
}
