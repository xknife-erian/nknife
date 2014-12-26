using System.Collections.Generic;
using NKnife.Tunnel;

namespace NKnife.Protocol
{
    public interface IKnifeProtocolProcessor<T>
    {
        void Bind(ITunnelCodec<T> codec, IProtocolFamily<T> protocolFamily);

        /// <summary>
        /// ���ݰ�������Ҫ������쳣������µģ�����ĽӰ���ճ��������
        /// </summary>
        /// <param name="dataPacket">��ǰ�µ����ݰ�</param>
        /// <param name="unFinished">δ��ɴ��������</param>
        /// <returns>δ�������,���¸����ݰ�����ʱ��Ҫ�������������(���)</returns>
        IEnumerable<IProtocol<T>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished);
    }
}