using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using NKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSessionMap : ConcurrentDictionary<EndPoint, ISocketSession>, ISocketSessionMap
    {
        public bool Remove(KeyValuePair<EndPoint, ISocketSession> item)
        {
            ISocketSession session;
            bool isRemoved = TryRemove(item.Key, out session);
            if (isRemoved)
                OnRemoved(new EventArgs<EndPoint>(item.Key));
            return isRemoved;
        }

        public bool Remove(EndPoint key)
        {
            ISocketSession session;
            bool isRemoved = TryRemove(key, out session);
            if (isRemoved)
                OnRemoved(new EventArgs<EndPoint>(key));
            return isRemoved;
        }

        public event EventHandler<EventArgs<EndPoint>> Removed;

        protected virtual void OnRemoved(EventArgs<EndPoint> e)
        {
            EventHandler<EventArgs<EndPoint>> handler = Removed;
            if (handler != null)
                handler(this, e);
        }
    }
}