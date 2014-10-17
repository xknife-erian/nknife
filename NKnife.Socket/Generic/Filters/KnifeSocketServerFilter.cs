using System;
using System.Net;
using System.Net.Sockets;
using NKnife.Protocol;
using NKnife.Tunnel;
using NKnife.Tunnel.Events;
using SocketKnife.Events;
using SocketKnife.Generic.Families;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public abstract class KnifeSocketServerFilter : ISocketServerFilter
    {
        protected Func<KnifeProtocolFamily> _FamilyGetter;
        protected Func<KnifeSocketProtocolHandler> _HandlerGetter;
        protected Func<KnifeSocketSessionMap> _SessionMapGetter;
        protected Func<KnifeSocketCodec> _CodecGetter;

        public abstract bool ContinueNextFilter { get; }

        void ITunnelFilter<EndPoint, Socket>.PrcoessReceiveData(ITunnelSession<EndPoint, Socket> socket, byte[] data)
        {
            PrcoessReceiveData((ISocketSession) socket, data);
        }

        ITunnelCodec ITunnelFilter<EndPoint, Socket>.Codec
        {
            get { return SocketCodec; }
            set { SocketCodec = (KnifeSocketCodec) value; }
        }

        public event EventHandler<DataFetchedEventArgs<EndPoint>> DataFetched;

        public event EventHandler<DataDecodedEventArgs<EndPoint>> DataDecoded;

        public event EventHandler<SessionEventArgs<EndPoint, Socket>> ClientCome;

        public event EventHandler<ConnectionBreakEventArgs<EndPoint>> ClientBroke;

        public virtual void Bind(
            Func<IProtocolFamily> familyGetter,
            Func<IProtocolHandler<EndPoint, Socket>> handlerGetter, 
            Func<ISocketSessionMap> sessionMapGetter, 
            Func<ISocketCodec> codecFunc)
        {
            _FamilyGetter = (Func<KnifeProtocolFamily>) familyGetter;
            _HandlerGetter = (Func<KnifeSocketProtocolHandler>) handlerGetter;
            _SessionMapGetter = (Func<KnifeSocketSessionMap>) sessionMapGetter;
            _CodecGetter = (Func<KnifeSocketCodec>) codecFunc;
            OnBoundGetter(_FamilyGetter, _HandlerGetter, _SessionMapGetter, _CodecGetter);
        }

        protected virtual void OnBoundGetter(Func<KnifeProtocolFamily> familyGetter, Func<KnifeSocketProtocolHandler> handlerGetter, Func<KnifeSocketSessionMap> sessionMapGetter, Func<KnifeSocketCodec> codecGetter)
        {
        }

        public KnifeSocketCodec SocketCodec { get; set; }

        public abstract void PrcoessReceiveData(ISocketSession socket, byte[] data);

        protected internal virtual void OnDataFetched(DataFetchedEventArgs<EndPoint> e)
        {
            EventHandler<DataFetchedEventArgs<EndPoint>> handler = DataFetched;
            if (handler != null) handler(this, e);
        }

        protected internal virtual void OnDataDecoded(DataDecodedEventArgs<EndPoint> e)
        {
            EventHandler<DataDecodedEventArgs<EndPoint>> handler = DataDecoded;
            if (handler != null) handler(this, e);
        }

        protected internal virtual void OnClientCome(SocketSessionEventArgs e)
        {
            EventHandler<SessionEventArgs<EndPoint, Socket>> handler = ClientCome;
            if (handler != null)
                handler(this, e);
        }

        protected internal virtual void OnClientBroke(ConnectionBreakEventArgs<EndPoint> e)
        {
            EventHandler<ConnectionBreakEventArgs<EndPoint>> handler = ClientBroke;
            if (handler != null) handler(this, e);
        }
    }
}