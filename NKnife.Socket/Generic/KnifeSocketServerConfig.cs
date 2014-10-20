using System.Collections;
using System.Collections.Generic;
using NKnife.Mvvm;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketServerConfig : NotificationObject, ISocketConfig
    {
        protected IDictionary<string, object> _Map = new Dictionary<string, object>(5);

        public void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int readBufferSize)
        {
            ReadBufferSize = readBufferSize;
            MaxBufferSize = maxBufferSize;
            MaxConnectCount = maxConnectCount;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = sendTimeout;
        }

        public virtual int ReadBufferSize
        {
            get { return int.Parse(_Map["ReadBufferSize"].ToString()); }
            set
            {
                _Map["ReadBufferSize"] = value;
                RaisePropertyChanged(() => ReadBufferSize);
            }
        }

        public virtual int MaxBufferSize
        {
            get { return int.Parse(_Map["MaxBufferSize"].ToString()); }
            set
            {
                _Map["MaxBufferSize"] = value;
                RaisePropertyChanged(() => MaxBufferSize);
            }
        }

        public virtual int MaxConnectCount
        {
            get { return int.Parse(_Map["MaxConnectCount"].ToString()); }
            set
            {
                _Map["MaxConnectCount"] = value;
                RaisePropertyChanged(() => MaxConnectCount);
            }
        }

        public virtual int ReceiveTimeout
        {
            get { return int.Parse(_Map["ReceiveTimeout"].ToString()); }
            set
            {
                _Map["ReceiveTimeout"] = value;
                RaisePropertyChanged(() => ReceiveTimeout);
            }
        }

        public virtual int SendTimeout
        {
            get { return int.Parse(_Map["SendTimeout"].ToString()); }
            set
            {
                _Map["SendTimeout"] = value;
                RaisePropertyChanged(() => SendTimeout);
            }
        }

        #region IDictionary<string, object>

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            _Map.Add(item);
        }

        public void Clear()
        {
            _Map.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _Map.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _Map.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _Map.Remove(item);
        }

        public int Count
        {
            get { return _Map.Count; }
        }

        public bool IsReadOnly
        {
            get { return _Map.IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return _Map.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            _Map.Add(key, value);
        }

        public bool Remove(string key)
        {
            return _Map.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _Map.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get { return _Map[key]; }
            set { _Map[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return _Map.Keys; }
        }

        public ICollection<object> Values
        {
            get { return _Map.Values; }
        }

        #endregion
    }
}