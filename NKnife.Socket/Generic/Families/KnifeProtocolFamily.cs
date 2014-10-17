using System;
using System.Collections;
using System.Collections.Generic;
using NKnife.Protocol;
using SocketKnife.Generic.Protocols;

namespace SocketKnife.Generic.Families
{
    /// <summary>
    ///     协议族
    /// </summary>
    [Serializable]
    public class KnifeProtocolFamily : IProtocolFamily
    {
        protected Dictionary<string, IProtocol> _Map = new Dictionary<string, IProtocol>();

        public KnifeProtocolFamily()
        {
        }

        public KnifeProtocolFamily(string name)
        {
            Family = name;
        }

        /// <summary>
        ///     协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; private set; }

        public KnifeSocketProtocol this[string command]
        {
            get { return _Map[command] as KnifeSocketProtocol; }
            set { _Map[command] = value; }
        }

        void IProtocolFamily.Add(IProtocol protocol)
        {
            Add((KnifeSocketProtocol) protocol);
        }

        IProtocol IProtocolFamily.NewProtocol(string command)
        {
            return NewProtocol(command);
        }

        public IEnumerator GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, IProtocol>> IEnumerable<KeyValuePair<string, IProtocol>>.GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        void ICollection<KeyValuePair<string, IProtocol>>.Add(KeyValuePair<string, IProtocol> item)
        {
            Add(item.Key, (KnifeSocketProtocol) item.Value);
        }

        public void Clear()
        {
            _Map.Clear();
        }

        bool ICollection<KeyValuePair<string, IProtocol>>.Contains(KeyValuePair<string, IProtocol> item)
        {
            return _Map.ContainsKey(item.Key) && _Map.ContainsValue(item.Value);
        }

        void ICollection<KeyValuePair<string, IProtocol>>.CopyTo(KeyValuePair<string, IProtocol>[] array, int arrayIndex)
        {
            ((IDictionary<string, IProtocol>) _Map).CopyTo(array, arrayIndex);
        }

        public bool Remove(string command)
        {
            return _Map.Remove(command);
        }

        bool ICollection<KeyValuePair<string, IProtocol>>.Remove(KeyValuePair<string, IProtocol> item)
        {
            return Remove(item.Key);
        }

        public int Count
        {
            get { return _Map.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, IProtocol>) _Map).IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return _Map.ContainsKey(key);
        }

        void IDictionary<string, IProtocol>.Add(string key, IProtocol value)
        {
            Add(key, (KnifeSocketProtocol) value);
        }

        bool IDictionary<string, IProtocol>.TryGetValue(string key, out IProtocol value)
        {
            return _Map.TryGetValue(key, out value);
        }

        IProtocol IDictionary<string, IProtocol>.this[string key]
        {
            get { return this[key]; }
            set { this[key] = (KnifeSocketProtocol) value; }
        }

        ICollection<string> IDictionary<string, IProtocol>.Keys
        {
            get { return _Map.Keys; }
        }

        ICollection<IProtocol> IDictionary<string, IProtocol>.Values
        {
            get { return _Map.Values; }
        }

        public void Add(KnifeSocketProtocol protocol)
        {
            _Map.Add(protocol.Command, protocol);
        }

        public KnifeSocketProtocol NewProtocol(string command)
        {
            return _Map[command].NewInstance() as KnifeSocketProtocol;
        }

        public void Add(string key, KnifeSocketProtocol value)
        {
            _Map.Add(key, value);
        }

        public bool Contains(KeyValuePair<string, KnifeSocketProtocol> item)
        {
            return _Map.ContainsKey(item.Key) && _Map.ContainsValue(item.Value);
        }

        public bool TryGetValue(string key, out KnifeSocketProtocol value)
        {
            IProtocol protocol;
            if (_Map.TryGetValue(key, out protocol))
            {
                value = (KnifeSocketProtocol) protocol;
                return true;
            }
            value = null;
            return false;
        }
    }
}