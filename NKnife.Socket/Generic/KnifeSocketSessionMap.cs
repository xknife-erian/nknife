using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using NKnife.Events;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSessionMap : ISocketSessionMap
    {
        private readonly ConcurrentDictionary<EndPoint, KnifeSocketSession> _Map = new ConcurrentDictionary<EndPoint, KnifeSocketSession>();

        public KnifeSocketSession this[EndPoint key]
        {
            get
            {
                return _Map.ContainsKey(key) ? _Map[key] : null;
            }
            set
            {
                var old = _Map[key];
                _Map[key] = value;
                if (!old.Equals(value))
                {
                    OnRemoved(new EventArgs<EndPoint>(key));
                    OnAdded(new EventArgs<KnifeSocketSession>(value));
                }
            }
        }

        bool IDictionary<EndPoint, ITunnelSession<byte[], EndPoint>>.TryGetValue(EndPoint key, out ITunnelSession<byte[], EndPoint> value)
        {
            KnifeSocketSession session;
            if (TryGetValue(key, out session))
            {
                value = session;
                return true;
            }
            value = null;
            return false;
        }

        ITunnelSession<byte[], EndPoint> IDictionary<EndPoint, ITunnelSession<byte[], EndPoint>>.this[EndPoint key]
        {
            get { return this[key]; }
            set { this[key] = (KnifeSocketSession)value; }
        }

        public ICollection<EndPoint> Keys
        {
            get { return _Map.Keys; }
        }

        ICollection<ITunnelSession<byte[], EndPoint>> IDictionary<EndPoint, ITunnelSession<byte[], EndPoint>>.Values
        {
            get { return (ICollection<ITunnelSession<byte[], EndPoint>>)Values(); }
        }

        public ICollection<KnifeSocketSession> Values()
        {
            return _Map.Values;
        }

        void IDictionary<EndPoint, ITunnelSession<byte[], EndPoint>>.Add(EndPoint key, ITunnelSession<byte[], EndPoint> value)
        {
            Add(key, (KnifeSocketSession)value);
        }

        void ICollection<KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>>.Add(KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>> item)
        {
            Add(item.Key, (KnifeSocketSession)item.Value);
        }

        public void Clear()
        {
            var list = new List<EndPoint>(_Map.Count);
            list.AddRange(_Map.Keys.ToArray());
            _Map.Clear();
            foreach (var endPoint in list)
            {
                OnRemoved(new EventArgs<EndPoint>(endPoint));
            }
        }

        public bool ContainsKey(EndPoint key)
        {
            return Contains(key);
        }

        bool ICollection<KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>>.Contains(KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>> item)
        {
            return Contains(item.Key);
        }

        [Obsolete("不推荐使用。Knife.")]
        void ICollection<KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>>.CopyTo(KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>[] array, int arrayIndex)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            ((IDictionary<EndPoint, ITunnelSession<byte[], EndPoint>>)_Map).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _Map.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<EndPoint, KnifeSocketSession>)_Map).IsReadOnly; }
        }

        IEnumerator<KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>> IEnumerable<KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>>)GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        bool ICollection<KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>>>.Remove(KeyValuePair<EndPoint, ITunnelSession<byte[], EndPoint>> item)
        {
            if (_Map.ContainsKey(item.Key))
            {
                return Remove(item.Key);
            }
            return false;
        }

        bool IDictionary<EndPoint, ITunnelSession<byte[], EndPoint>>.Remove(EndPoint key)
        {
            return Remove(key);
        }

        public event EventHandler<EventArgs<EndPoint>> Removed;

        public event EventHandler<EventArgs<KnifeSocketSession>> Added;

        public bool TryGetValue(EndPoint key, out KnifeSocketSession value)
        {
            KnifeSocketSession session;
            if (_Map.TryGetValue(key, out session))
            {
                value = session;
                return true;
            }
            value = null;
            return false;
        }

        public void Add(EndPoint key, KnifeSocketSession value)
        {
            _Map.TryAdd(key, value);
            OnAdded(new EventArgs<KnifeSocketSession>(value));
        }

        public bool Contains(EndPoint key)
        {
            return _Map.ContainsKey(key);
        }

        public virtual bool Remove(EndPoint key)
        {
            KnifeSocketSession session;
            bool isRemoved = _Map.TryRemove(key, out session);
            if (isRemoved)
                OnRemoved(new EventArgs<EndPoint>(key));
            return isRemoved;
        }

        protected virtual void OnAdded(EventArgs<KnifeSocketSession> e)
        {
            EventHandler<EventArgs<KnifeSocketSession>> handler = Added;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnRemoved(EventArgs<EndPoint> e)
        {
            EventHandler<EventArgs<EndPoint>> handler = Removed;
            if (handler != null)
                handler(this, e);
        }
    }
}