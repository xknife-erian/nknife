using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Events;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Base;
using NKnife.Tunnel.Filters;

namespace SerialKnife.Generic.Filters
{
    /// <summary>
    /// һ����򵥵�Э�鴦��Filter,������Handler����Э��ַ�,ֱ���׳�Э���յ��¼�
    /// </summary>
    public class SerialProtocolSimpleFilter : BytesProtocolFilter
    {
        private static readonly ILog _Logger = LogManager.GetLogger<SerialProtocolFilter>();
        private readonly byte[] _currentReceiveBuffer = new byte[1024];
        private int _currentReceiveByteLength;

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        protected virtual void OnProtocolsReceived(EventArgs<IEnumerable<IProtocol<byte[]>>> e)
        {
            EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> handler = ProtocolsReceived;
            if (handler != null)
                handler(this, e);
        }

        public override bool PrcoessReceiveData(ITunnelSession session)
        {
            byte[] data = session.Data;
            int len = data.Length;
            if (len == 0)
            {
                return false;
            }

            if (_currentReceiveByteLength + len > 1024) //����������ˣ�ֻ������1024λ
            {
                //��ʱ��������ֱ������
                _Logger.Warn("�յ������ݳ���1024�������������������������");
                return false;
            }

            var tempData = new byte[_currentReceiveByteLength + len];
            Array.Copy(_currentReceiveBuffer, 0, tempData, 0, _currentReceiveByteLength);
            Array.Copy(data, 0, tempData, _currentReceiveByteLength, data.Length);

            //���ɸ���Ĵ���������
            var unfinished = new byte[] {};
            IEnumerable<IProtocol<byte[]>> protocols = ProcessDataPacket(tempData, ref unfinished);

            //��δ��ɽ����������ݴ棬���´��յ����ݺ���д���
            if (unfinished.Length > 0)
            {
                Array.Copy(unfinished, 0, _currentReceiveBuffer, 0, unfinished.Length);
                _currentReceiveByteLength = unfinished.Length;
            }
            else
            {
                _currentReceiveByteLength = 0;
            }

            if (protocols != null)
            {
                OnProtocolsReceived(new EventArgs<IEnumerable<IProtocol<byte[]>>>(protocols));
            }
            return true;
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
        }

        public override void ProcessSendToAll(byte[] data)
        {
        }

        public override void ProcessSessionBroken(long id)
        {
        }

        public override void ProcessSessionBuilt(long id)
        {
        }
    }
}