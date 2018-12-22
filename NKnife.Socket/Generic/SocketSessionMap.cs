using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NKnife.Events;
using NKnife.Tunnel;
using NKnife.Tunnel.Common;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class SocketSessionMap : ISocketSessionMap
    {
        private readonly ConcurrentDictionary<long, SocketSession> _map = new ConcurrentDictionary<long, SocketSession>();

        public SocketSession this[long key]
        {
            get { return _map.ContainsKey(key) ? _map[key] : null; }
            set
            {
                var old = _map[key];
                _map[key] = value;
                if (!old.Equals(value))
                {
                    OnRemoved(new EventArgs<long>(key));
                    OnAdded(new EventArgs<SocketSession>(value));
                }
            }
        }

        bool IDictionary<long, ITunnelSession>.TryGetValue(long key, out ITunnelSession value)
        {
            SocketSession session;
            if (TryGetValue(key, out session))
            {
                value = session;
                return true;
            }
            value = null;
            return false;
        }

        ITunnelSession IDictionary<long, ITunnelSession>.this[long key]
        {
            get { return this[key]; }
            set { this[key] = (SocketSession) value; }
        }

        public ICollection<long> Keys
        {
            get { return _map.Keys; }
        }

        ICollection<ITunnelSession> IDictionary<long, ITunnelSession>.Values
        {
            get { return (ICollection<ITunnelSession>) Values(); }
        }

        void IDictionary<long, ITunnelSession>.Add(long key, ITunnelSession value)
        {
            Add(key, (SocketSession) value);
        }

        void ICollection<KeyValuePair<long, ITunnelSession>>.Add(KeyValuePair<long, ITunnelSession> item)
        {
            Add(item.Key, (SocketSession) item.Value);
        }

        public void Clear()
        {
            var list = new List<long>(_map.Count);
            list.AddRange(_map.Keys.ToArray());
            _map.Clear();
            foreach (var endPoint in list)
            {
                OnRemoved(new EventArgs<long>(endPoint));
            }
        }

        public bool ContainsKey(long key)
        {
            return Contains(key);
        }

        bool ICollection<KeyValuePair<long, ITunnelSession>>.Contains(KeyValuePair<long, ITunnelSession> item)
        {
            return Contains(item.Key);
        }

        public ICollection<SocketSession> Values()
        {
            return _map.Values;
        }

        [Obsolete("不推荐使用。Knife.")]
        void ICollection<KeyValuePair<long, ITunnelSession>>.CopyTo(KeyValuePair<long, ITunnelSession>[] array, int arrayIndex)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            ((IDictionary<long, ITunnelSession>) _map).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ((IDictionary<long, SocketSession>) _map).Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<long, SocketSession>) _map).IsReadOnly; }
        }

        IEnumerator<KeyValuePair<long, ITunnelSession>> IEnumerable<KeyValuePair<long, ITunnelSession>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<long, ITunnelSession>>) GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IDictionary<long, SocketSession>) _map).GetEnumerator();
        }

        bool ICollection<KeyValuePair<long, ITunnelSession>>.Remove(KeyValuePair<long, ITunnelSession> item)
        {
            if (_map.ContainsKey(item.Key))
            {
                return Remove(item.Key);
            }
            return false;
        }

        bool IDictionary<long, ITunnelSession>.Remove(long key)
        {
            return Remove(key);
        }

        public event EventHandler<EventArgs<long>> Removed;

        public event EventHandler<EventArgs<SocketSession>> Added;

        public bool TryGetValue(long key, out SocketSession value)
        {
            SocketSession session;
            if (_map.TryGetValue(key, out session))
            {
                value = session;
                return true;
            }
            value = null;
            return false;
        }

        public void Add(long key, SocketSession value)
        {
            _map.TryAdd(key, value);
            OnAdded(new EventArgs<SocketSession>(value));
        }

        protected static long _Count = 1;
        public long Add(SocketSession session)
        {
            session.Id = _Count;
            Add(_Count, session);
            _Count++;
            return session.Id;
        }

        public bool Contains(long key)
        {
            return _map.ContainsKey(key);
        }

        public virtual bool Remove(long key)
        {
            SocketSession session;
            var isRemoved = _map.TryRemove(key, out session);
            if (isRemoved)
                OnRemoved(new EventArgs<long>(key));
            return isRemoved;
        }

        protected virtual void OnAdded(EventArgs<SocketSession> e)
        {
            var handler = Added;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnRemoved(EventArgs<long> e)
        {
            var handler = Removed;
            if (handler != null)
                handler(this, e);
        }
    }
}