using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ninject;
using NKnife.IoC;

namespace NKnife.Protocol.Generic
{
    /// <summary>
    ///     协议族
    /// </summary>
    [Serializable]
    public class StringProtocolFamily : IProtocolFamily<string>
    {
        protected Dictionary<string, StringProtocol> _Map = new Dictionary<string, StringProtocol>();

        public StringProtocolFamily()
        {
        }

        public StringProtocolFamily(string name)
        {
            Family = name;
        }

        /// <summary>
        ///     协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; set; }

        [Inject]
        public StringProtocolCommandParser CommandParser { get; set; }

        public StringProtocol this[string command]
        {
            get { return _Map[command]; }
            set { _Map[command] = value; }
        }

        IProtocolCommandParser<string> IProtocolFamily<string>.CommandParser
        {
            get { return CommandParser; }
            set { CommandParser = (StringProtocolCommandParser) value; }
        }

        void IProtocolFamily<string>.Add(IProtocol<string> protocol)
        {
            Add((StringProtocol) protocol);
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
            Add(item.Key, (StringProtocol) item.Value);
        }

        public void Clear()
        {
            _Map.Clear();
        }

        bool ICollection<KeyValuePair<string, IProtocol<string>>>.Contains(KeyValuePair<string, IProtocol<string>> item)
        {
            return _Map.ContainsKey(item.Key) && _Map.ContainsValue((StringProtocol) item.Value);
        }

        [Obsolete("效率不高，不建议使用. NKnife")]
        void ICollection<KeyValuePair<string, IProtocol<string>>>.CopyTo(KeyValuePair<string, IProtocol<string>>[] array, int arrayIndex)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            ((ICollection<KeyValuePair<string, IProtocol<string>>>)_Map).CopyTo(array, arrayIndex);
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
            get { return ((IDictionary<string, StringProtocol>)_Map).IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return _Map.ContainsKey(key);
        }

        void IDictionary<string, IProtocol<string>>.Add(string key, IProtocol<string> value)
        {
            Add(key, (StringProtocol) value);
        }

        bool IDictionary<string, IProtocol<string>>.TryGetValue(string key, out IProtocol<string> value)
        {
            StringProtocol protocol;
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
            set { this[key] = (StringProtocol) value; }
        }

        ICollection<string> IDictionary<string, IProtocol<string>>.Keys
        {
            get { return _Map.Keys; }
        }

        [Obsolete("效率不高，不建议使用. NKnife")]
        ICollection<IProtocol<string>> IDictionary<string, IProtocol<string>>.Values
        {
            get
            {
                var list = new List<IProtocol<string>>(_Map.Count);
                list.AddRange(_Map.Values);
                return list;
            }
        }

        public void Add(StringProtocol protocol)
        {
            _Map.Add(protocol.Command, protocol);
        }

        public StringProtocol NewProtocol(string command)
        {
            return _Map[command].NewInstance();
        }

        public void Add(string key, StringProtocol value)
        {
            _Map.Add(key, value);
        }

        public bool Contains(KeyValuePair<string, StringProtocol> item)
        {
            return _Map.ContainsKey(item.Key) && _Map.ContainsValue(item.Value);
        }

        public bool TryGetValue(string key, out StringProtocol value)
        {
            return _Map.TryGetValue(key, out value);
        }

        public StringProtocol Build(string command)
        {
            var protocol = DI.Get<StringProtocol>();
            Debug.Assert(!string.IsNullOrEmpty(Family), "未设置协议族名称");
            protocol.Family = Family;
            protocol.Command = command;
            return protocol;
        }
    }
}