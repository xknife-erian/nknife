using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Protocol.Generic;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketFilter : ISocketFilter
    {
        protected Func<StringProtocolFamily> _FamilyGetter;
        protected Func<KnifeSocketProtocolHandler[]> _HandlersGetter;
        protected Func<KnifeSocketCodec> _CodecGetter;

        public abstract bool ContinueNextFilter { get; }

        void ITunnelFilter<EndPoint, Socket>.PrcoessReceiveData(ITunnelSession<EndPoint, Socket> session, byte[] data)
        {
            PrcoessReceiveData((KnifeSocketSession) session, data);
        }

        public event EventHandler<DataFetchedEventArgs<EndPoint>> DataFetched;

        public event EventHandler<DataDecodedEventArgs<EndPoint>> DataDecoded;

        public virtual void BindGetter(Func<KnifeSocketCodec> codecFunc, Func<KnifeSocketProtocolHandler[]> handlerGetter, Func<StringProtocolFamily> familyGetter)
        {
            _FamilyGetter = familyGetter;
            _HandlersGetter = handlerGetter;
            _CodecGetter = codecFunc;
            OnBoundGetter();
        }

        /// <summary>
        /// 当核心对象获取器绑定完成时发生
        /// </summary>
        protected virtual void OnBoundGetter()
        {
        }

        public abstract void PrcoessReceiveData(KnifeSocketSession session, byte[] data);

        protected internal virtual void OnDataFetched(SocketDataFetchedEventArgs e)
        {
            EventHandler<DataFetchedEventArgs<EndPoint>> handler = DataFetched;
            if (handler != null) 
                handler(this, e);
        }

        protected internal virtual void OnDataDecoded(SocketDataDecodedEventArgs e)
        {
            EventHandler<DataDecodedEventArgs<EndPoint>> handler = DataDecoded;
            if (handler != null) 
                handler(this, e);
        }

    }
}