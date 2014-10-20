using System;
using System.Collections;
using System.Collections.Generic;
using NKnife.Protocol;

namespace SocketKnife.Generic
{
    /// <summary>
    ///     协议族
    /// </summary>
    [Serializable]
    public class KnifeSocketProtocolFamily : IProtocolFamily<string>
    {
        protected Dictionary<string, KnifeSocketProtocol> _Map = new Dictionary<string, KnifeSocketProtocol>();

        public KnifeSocketProtocolFamily()
        {
        }

        public KnifeSocketProtocolFamily(string name)
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
            get { return _Map[command]; }
            set { _Map[command] = value; }
        }

        void IProtocolFamily<string>.Add(IProtocol<string> protocol)
        {
            Add((KnifeSocketProtocol) protocol);
        }

        IProtocol<string> IProtocolFamily<string>.NewProtocol(string command)
        {
            return NewProtocol(command);
        }

        public IEnumerator GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, IProtocol<string>>> IEnumerable<KeyValuePair<string, IProtocol<string>>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<string, IProtocol<string>>>) GetEnumerator();
        }

        void ICollection<KeyValuePair<string, IProtocol<string>>>.Add(KeyValuePair<string, IProtocol<string>> item)
        {
            Add(item.Key, (KnifeSocketProtocol) item.Value);
        }

        public void Clear()
        {
            _Map.Clear();
        }

        bool ICollection<KeyValuePair<string, IProtocol<string>>>.Contains(KeyValuePair<string, IProtocol<string>> item)
        {
            return _Map.ContainsKey(item.Key) && _Map.ContainsValue((KnifeSocketProtocol) item.Value);
        }

        void ICollection<KeyValuePair<string, IProtocol<string>>>.CopyTo(KeyValuePair<string, IProtocol<string>>[] array, int arrayIndex)
        {
        }

        public bool Remove(string command)
        {
            return _Map.Remove(command);
        }

        bool ICollection<KeyValuePair<string, IProtocol<string>>>.Remove(KeyValuePair<string, IProtocol<string>> item)
        {
            return Remove(item.Key);
        }

        public int Count
        {
            get { return _Map.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, KnifeSocketProtocol>)_Map).IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return _Map.ContainsKey(key);
        }

        void IDictionary<string, IProtocol<string>>.Add(string key, IProtocol<string> value)
        {
            Add(key, (KnifeSocketProtocol) value);
        }

        bool IDictionary<string, IProtocol<string>>.TryGetValue(string key, out IProtocol<string> value)
        {
            KnifeSocketProtocol protocol;
            if (_Map.TryGetValue(key, out protocol))
            {
                value = protocol;
                return true;
            }
            value = null;
            return false;
        }

        IProtocol<string> IDictionary<string, IProtocol<string>>.this[string key]
        {
            get { return this[key]; }
            set { this[key] = (KnifeSocketProtocol) value; }
        }

        ICollection<string> IDictionary<string, IProtocol<string>>.Keys
        {
            get { return _Map.Keys; }
        }

        ICollection<IProtocol<string>> IDictionary<string, IProtocol<string>>.Values
        {
            get { return null; }
        }

        public void Add(KnifeSocketProtocol protocol)
        {
            _Map.Add(protocol.Command, protocol);
        }

        public KnifeSocketProtocol NewProtocol(string command)
        {
            return _Map[command].NewInstance();
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
            return _Map.TryGetValue(key, out value);
        }
    }
}