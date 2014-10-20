using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketFilter : ISocketFilter
    {
        protected Func<KnifeSocketProtocolFamily> _FamilyGetter;
        protected Func<KnifeSocketProtocolHandler[]> _HandlersGetter;
        protected Func<KnifeSocketSessionMap> _SessionMapGetter;
        protected Func<KnifeSocketCodec> _CodecGetter;

        public abstract bool ContinueNextFilter { get; }

        void ITunnelFilter<EndPoint, Socket>.PrcoessReceiveData(ITunnelSession<EndPoint, Socket> socket, byte[] data)
        {
            PrcoessReceiveData((KnifeSocketSession) socket, data);
        }

        public event EventHandler<DataFetchedEventArgs<EndPoint>> DataFetched;

        public event EventHandler<DataDecodedEventArgs<EndPoint>> DataDecoded;

        public virtual void Bind(
            Func<KnifeSocketProtocolFamily> familyGetter,
            Func<KnifeSocketProtocolHandler[]> handlerGetter,
            Func<KnifeSocketSessionMap> sessionMapGetter,
            Func<KnifeSocketCodec> codecFunc)
        {
            _FamilyGetter = familyGetter;
            _HandlersGetter = handlerGetter;
            _SessionMapGetter = sessionMapGetter;
            _CodecGetter = codecFunc;
            OnBoundGetter(_FamilyGetter, _HandlersGetter, _SessionMapGetter, _CodecGetter);
        }

        protected virtual void OnBoundGetter(Func<KnifeSocketProtocolFamily> familyGetter, Func<KnifeSocketProtocolHandler[]> handlerGetter, Func<KnifeSocketSessionMap> sessionMapGetter, Func<KnifeSocketCodec> codecGetter)
        {
        }

        public abstract void PrcoessReceiveData(KnifeSocketSession socket, byte[] data);

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