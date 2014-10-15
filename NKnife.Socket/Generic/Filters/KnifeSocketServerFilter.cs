using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using SocketKnife.Common;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public abstract class KnifeSocketServerFilter : ISocketServerFilter
    {
        protected Func<IProtocolFamily> _FamilyGetter;
        protected Func<IProtocolHandler> _HandlerGetter;
        protected Func<ISocketSessionMap> _SessionMapGetter;

        public abstract void PrcoessReceiveData(ISocketSession socket, byte[] data);

        public virtual void Bind(Func<IProtocolFamily> familyGetter, Func<IProtocolHandler> handlerGetter, Func<ISocketSessionMap> mapGetter)
        {
            _FamilyGetter = familyGetter;
            _HandlerGetter = handlerGetter;
            _SessionMapGetter = mapGetter;
        }

        public event SocketAsyncDataComeInEventHandler DataComeInEvent;

        protected internal virtual void OnDataComeInEvent(byte[] data, EndPoint endpoint)
        {
            SocketAsyncDataComeInEventHandler handler = DataComeInEvent;
            if (handler != null) 
                handler(data, endpoint);
        }

        public event ListenToClientEventHandler ListenToClient;

        protected internal virtual void OnListenToClient(SocketAsyncEventArgs e)
        {
            ListenToClientEventHandler handler = ListenToClient;
            if (handler != null) 
                handler(e);
        }

        public event ConnectionBreakEventHandler ConnectionBreak;

        protected internal virtual void OnConnectionBreak(ConnectionBreakEventArgs e)
        {
            ConnectionBreakEventHandler handler = ConnectionBreak;
            if (handler != null) 
                handler(e);
        }
    }
}
