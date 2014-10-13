using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using SocketKnife.Common;
using SocketKnife.Interfaces;
using SocketKnife.Protocol.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public abstract class FilterBase : IFilter
    {
        public IDatagramCommandParser CommandParser { get; private set; }
        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }

        protected Func<IProtocolFamily> _FamilyGetter;
        protected Func<IProtocolHandler> _HandlerGetter;
        protected Func<ISocketSessionMap> _SessionMapGetter; 

        public abstract void PrcoessReceiveData(Socket socket, byte[] data);

        public virtual void Bind(Func<IProtocolFamily> familyGetter, Func<IProtocolHandler> handlerGetter, Func<ISocketSessionMap> mapGetter)
        {
            _FamilyGetter = familyGetter;
            _HandlerGetter = handlerGetter;
            _SessionMapGetter = mapGetter;
        }

        public event SocketAsyncDataComeInEventHandler DataComeInEvent;

        protected virtual void OnDataComeInEvent(byte[] data, EndPoint endpoint)
        {
            SocketAsyncDataComeInEventHandler handler = DataComeInEvent;
            if (handler != null) 
                handler(data, endpoint);
        }

        public event ListenToClientEventHandler ListenToClient;

        protected virtual void OnListenToClient(SocketAsyncEventArgs e)
        {
            ListenToClientEventHandler handler = ListenToClient;
            if (handler != null) 
                handler(e);
        }

        public event ConnectionBreakEventHandler ConnectionBreak;

        protected virtual void OnConnectionBreak(ConnectionBreakEventArgs e)
        {
            ConnectionBreakEventHandler handler = ConnectionBreak;
            if (handler != null) 
                handler(e);
        }
    }
}
