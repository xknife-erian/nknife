﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NKnife.Events;
using NKnife.Tunnel;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSessionMap : ISocketSessionMap
    {
        private readonly ConcurrentDictionary<long, KnifeSocketSession> _Map = new ConcurrentDictionary<long, KnifeSocketSession>();

        public KnifeSocketSession this[long key]
        {
            get { return _Map.ContainsKey(key) ? _Map[key] : null; }
            set
            {
                var old = _Map[key];
                _Map[key] = value;
                if (!old.Equals(value))
                {
                    OnRemoved(new EventArgs<long>(key));
                    OnAdded(new EventArgs<KnifeSocketSession>(value));
                }
            }
        }

        bool IDictionary<long, ITunnelSession>.TryGetValue(long key, out ITunnelSession value)
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

        ITunnelSession IDictionary<long, ITunnelSession>.this[long key]
        {
            get { return this[key]; }
            set { this[key] = (KnifeSocketSession) value; }
        }

        public ICollection<long> Keys
        {
            get { return _Map.Keys; }
        }

        ICollection<ITunnelSession> IDictionary<long, ITunnelSession>.Values
        {
            get { return (ICollection<ITunnelSession>) Values(); }
        }

        void IDictionary<long, ITunnelSession>.Add(long key, ITunnelSession value)
        {
            Add(key, (KnifeSocketSession) value);
        }

        void ICollection<KeyValuePair<long, ITunnelSession>>.Add(KeyValuePair<long, ITunnelSession> item)
        {
            Add(item.Key, (KnifeSocketSession) item.Value);
        }

        public void Clear()
        {
            var list = new List<long>(_Map.Count);
            list.AddRange(_Map.Keys.ToArray());
            _Map.Clear();
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

        public ICollection<KnifeSocketSession> Values()
        {
            return _Map.Values;
        }

        [Obsolete("不推荐使用。Knife.")]
        void ICollection<KeyValuePair<long, ITunnelSession>>.CopyTo(KeyValuePair<long, ITunnelSession>[] array, int arrayIndex)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            ((IDictionary<long, ITunnelSession>) _Map).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ((IDictionary<long, KnifeSocketSession>) _Map).Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<long, KnifeSocketSession>) _Map).IsReadOnly; }
        }

        IEnumerator<KeyValuePair<long, ITunnelSession>> IEnumerable<KeyValuePair<long, ITunnelSession>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<long, ITunnelSession>>) GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IDictionary<long, KnifeSocketSession>) _Map).GetEnumerator();
        }

        bool ICollection<KeyValuePair<long, ITunnelSession>>.Remove(KeyValuePair<long, ITunnelSession> item)
        {
            if (_Map.ContainsKey(item.Key))
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

        public event EventHandler<EventArgs<KnifeSocketSession>> Added;

        public bool TryGetValue(long key, out KnifeSocketSession value)
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

        public void Add(long key, KnifeSocketSession value)
        {
            _Map.TryAdd(key, value);
            OnAdded(new EventArgs<KnifeSocketSession>(value));
        }

        public bool Contains(long key)
        {
            return _Map.ContainsKey(key);
        }

        public virtual bool Remove(long key)
        {
            KnifeSocketSession session;
            var isRemoved = _Map.TryRemove(key, out session);
            if (isRemoved)
                OnRemoved(new EventArgs<long>(key));
            return isRemoved;
        }

        protected virtual void OnAdded(EventArgs<KnifeSocketSession> e)
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