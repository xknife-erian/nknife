using System.Collections.Generic;
using NKnife.Tunnel;

namespace NKnife.Protocol
{
    public interface IProtocolProcessor<TData, TSource>
    {
        void Bind(ITunnelCodec<TData, TSource> codec, IProtocolFamily<TData, TSource> protocolFamily);

        /// <summary>
        /// ���ݰ���������������쳣������µģ�����ĽӰ���ճ��������
        /// </summary>
        /// <param name="dataPacket">��ǰ�µ����ݰ�</param>
        /// <param name="unFinished">δ��ɴ��������</param>
        /// <returns>δ�������,���¸����ݰ�����ʱ��Ҫ�������������(���)</returns>
        IEnumerable<IProtocol<TData>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished);
    }
}