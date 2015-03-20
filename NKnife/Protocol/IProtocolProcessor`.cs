using System.Collections.Generic;
using NKnife.Tunnel;

namespace NKnife.Protocol
{
    public interface IProtocolProcessor<TData, TSource>
    {
        void Bind(ITunnelCodec<TData, TSource> codec, IProtocolFamily<TData, TSource> protocolFamily);

        /// <summary>
        /// 数据包处理。包含处理较异常的情况下的，半包的接包，粘包等现象。
        /// </summary>
        /// <param name="dataPacket">当前新的数据包</param>
        /// <param name="unFinished">未完成处理的数据</param>
        /// <returns>未处理完成,待下个数据包到达时将要继续处理的数据(半包)</returns>
        IEnumerable<IProtocol<TData>> ProcessDataPacket(byte[] dataPacket, byte[] unFinished);
    }
}