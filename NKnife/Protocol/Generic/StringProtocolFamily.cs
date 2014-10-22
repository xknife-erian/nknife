using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        protected Dictionary<string,StringProtocol> _SeedMap = new Dictionary<string, StringProtocol>(); 
        private readonly StringProtocol _Seed = new StringProtocol(); //用于获取实例的Build方法，因此只需要在此有一个实例
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
            get { return _SeedMap[command].BuildMethod.Invoke(); }
            set
            {
                if (!_SeedMap.ContainsKey(command))
                {
                    _SeedMap.Add(command, value);
                }
                else
                {
                    _SeedMap[command] = value; //更换种子
                }
            }
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

        IProtocol<string> IProtocolFamily<string>.Build(string command)
        {
            return Build(command);
        }

        public IEnumerator GetEnumerator()
        {
            return _SeedMap.GetEnumerator();
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
            _SeedMap.Clear();
        }

        bool ICollection<KeyValuePair<string, IProtocol<string>>>.Contains(KeyValuePair<string, IProtocol<string>> item)
        {
            return _SeedMap.ContainsKey(item.Key) && _SeedMap.ContainsValue(((StringProtocol) item.Value));
        }

        [Obsolete("效率不高，不建议使用. NKnife")]
        void ICollection<KeyValuePair<string, IProtocol<string>>>.CopyTo(KeyValuePair<string, IProtocol<string>>[] array, int arrayIndex)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            ((ICollection<KeyValuePair<string, IProtocol<string>>>)_SeedMap).CopyTo(array, arrayIndex);
        }

        public bool Remove(string command)
        {
            return _SeedMap.Remove(command);
        }

        bool ICollection<KeyValuePair<string, IProtocol<string>>>.Remove(KeyValuePair<string, IProtocol<string>> item)
        {
            return Remove(item.Key);
        }

        public int Count
        {
            get { return _SeedMap.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<string, StringProtocol>)_SeedMap).IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return _SeedMap.ContainsKey(key);
        }

        void IDictionary<string, IProtocol<string>>.Add(string key, IProtocol<string> value)
        {
            Add(key, (StringProtocol) value);
        }

        bool IDictionary<string, IProtocol<string>>.TryGetValue(string key, out IProtocol<string> value)
        {
            StringProtocol protocol;
            if (_SeedMap.TryGetValue(key, out protocol))
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
            get { return _SeedMap.Keys; }
        }

        [Obsolete("效率不高，不建议使用. NKnife")]
        ICollection<IProtocol<string>> IDictionary<string, IProtocol<string>>.Values
        {
            get
            {
                var list = new List<IProtocol<string>>(_SeedMap.Count);
                list.AddRange(_SeedMap.Values);
                return list;
            }
        }

        public void Add(StringProtocol protocol)
        {
            _SeedMap.Add(protocol.Command, protocol);
        }

        public void Add(string key, StringProtocol value)
        {
            _SeedMap.Add(key, value);
        }

        public bool Contains(KeyValuePair<string, StringProtocol> item)
        {
            return _SeedMap.ContainsKey(item.Key) && _SeedMap.ContainsValue(item.Value);
        }

        public bool TryGetValue(string key, out StringProtocol value)
        {
            return _SeedMap.TryGetValue(key, out value);
        }

        public StringProtocol Build(string command)
        {
            Debug.Assert(!string.IsNullOrEmpty(Family), "未设置协议族名称");

            if (!_SeedMap.ContainsKey(command))
            {
                _SeedMap.Add(command, _Seed.BuildMethod.Invoke());
            }
            return _SeedMap[command].BuildMethod.Invoke();
        }

        public StringProtocol Build(string command,Func<StringProtocol> buildMethod)
        {
            Debug.Assert(!string.IsNullOrEmpty(Family), "未设置协议族名称");

            if (!_SeedMap.ContainsKey(command))
            {
                _SeedMap.Add(command, _Seed.BuildMethod.Invoke());
                _SeedMap[command].BuildMethod = buildMethod;
            }
            return _SeedMap[command].BuildMethod.Invoke();
        }
    }
}