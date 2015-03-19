using System.Collections.Generic;
using NKnife.Tunnel;

namespace NKnife.Protocol
{
    public interface IProtocolProcessor<TOriginal>
    {
        void Bind(ITunnelCodec<TOriginal, byte[]> codec, IProtocolFamily<TOriginal> protocolFamily);

        /// <summary>
        /// ���ݰ���������������쳣������µģ�����ĽӰ���ճ��������
        /// </summary>
        /// <param name="dataPacket">��ǰ�µ����ݰ�</param>
        /// <param name="unFinished">δ��ɴ��������</param>
        /// <returns>δ�������,���¸����ݰ�����ʱ��Ҫ�������������(���)</returns>
        IEnumerable<IProtocol<TOriginal>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished);
    }
}