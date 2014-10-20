using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelFilter<TSource, TConnector, TCommand>
    {
        bool ContinueNextFilter { get; }

        void PrcoessReceiveData(ITunnelSession<TSource, TConnector> session, byte[] data);

        ITunnelCodec<TCommand> Codec { get; set; }

        #region 事件

        /// <summary>
        ///     数据异步接收到后事件,得到的数据是Byte数组
        /// </summary>
        event EventHandler<DataFetchedEventArgs<TSource>> DataFetched;

        event EventHandler<DataDecodedEventArgs<TSource>> DataDecoded;

        /// <summary>
        ///     服务器侦听到新客户端连接事件
        /// </summary>
        event EventHandler<SessionEventArgs<TSource, TConnector>> ClientCome;

        /// <summary>
        ///     连接出错或断开触发事件
        /// </summary>
        event EventHandler<ConnectionBreakEventArgs<TSource>> ClientBroke;

        #endregion
    }
}
