using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.EventArg;

namespace Didaku.Engine.Timeaxis.Base.Common
{
    public class CountMap : IDictionary<string, Count>
    {
        private readonly ConcurrentDictionary<string, Count> _Map = new ConcurrentDictionary<string, Count>();

        #region Implementation of IEnumerable

        public IEnumerator<KeyValuePair<string, Count>> GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<string,int>>

        public void Add(KeyValuePair<string, Count> item)
        {
            ((ICollection<KeyValuePair<string, Count>>)_Map).Add(item);
        }

        public void Clear()
        {
            _Map.Clear();
        }

        public bool Contains(KeyValuePair<string, Count> item)
        {
            return ((ICollection<KeyValuePair<string, Count>>)_Map).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, Count>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, Count>>)_Map).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, Count> item)
        {
            return ((ICollection<KeyValuePair<string, Count>>)_Map).Remove(item);
        }

        public int Count
        {
            get { return _Map.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<string, Count>>)_Map).IsReadOnly; }
        }

        #endregion

        #region Implementation of IDictionary<string,int>

        public bool ContainsKey(string key)
        {
            return _Map.ContainsKey(key);
        }

        public void Add(string key, Count value)
        {
            _Map.TryAdd(key, value);
        }

        public bool Remove(string key)
        {
            Count count;
            return _Map.TryRemove(key, out count);
        }

        public bool TryGetValue(string key, out Count value)
        {
            return _Map.TryGetValue(key, out value);
        }

        public Count this[string key]
        {
            get { return _Map[key]; }
            set
            {
                if (_Map[key] != value)
                {
                    _Map[key] = value;
                    OnCountChanged(new CountChangedEventArgs(key, value));
                }
            }
        }

        public ICollection<string> Keys
        {
            get { return _Map.Keys; }
        }

        public ICollection<Count> Values
        {
            get { return _Map.Values; }
        }

        #endregion

        /// <summary>
        /// 当统计数量发生改变时的事件
        /// </summary>
        public event CountChangedEventHandler CountChangedEvent;

        protected virtual void OnCountChanged(CountChangedEventArgs e)
        {
            if (CountChangedEvent != null)
                CountChangedEvent(this, e);
        }
    }
}
