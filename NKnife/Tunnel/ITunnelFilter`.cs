using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelFilter<TSource, TConnector>
    {
        bool ContinueNextFilter { get; }

        void PrcoessReceiveData(ITunnelSession<TSource, TConnector> session, byte[] data);

        #region 事件

        /// <summary>
        /// 原始数据接收到后事件,得到的数据是Byte数组,这时的数据未经过整理
        /// </summary>
        event EventHandler<DataFetchedEventArgs<TSource>> DataFetched;

        /// <summary>
        /// 当数据解码后
        /// </summary>
        event EventHandler<DataDecodedEventArgs<TSource>> DataDecoded;

        #endregion
    }
}
