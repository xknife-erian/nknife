using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NKnife.Collections
{
    /// <summary>描述一个可序列化，且集合的内容发生改变时会发出通知的KeyValue的集合
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class NotifySerializableMap<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged, IXmlSerializable, ISerializable, ICloneable
    {
        private readonly SerializableMap<TKey, TValue> _map = new SerializableMap<TKey, TValue>();

        #region Implementation of INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<TKey,TValue>>

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _map.Clear();
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(e);
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _map.ContainsKey(item.Key) && _map.ContainsValue(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + _map.Count)
                Array.Resize(ref array, arrayIndex + _map.Count);
            foreach (var pair in _map)
            {
                array[arrayIndex] = pair;
                arrayIndex++;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public int Count
        {
            get { return _map.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] items)
        {
            foreach (var item in items)
                Add(item.Key, item.Value);
        }

        #endregion

        #region Implementation of IDictionary<TKey,TValue>

        public bool ContainsKey(TKey key)
        {
            return _map.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            if (_map.ContainsKey(key))
            {
                _map[key] = value;
                var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value);
                OnCollectionChanged(e);
            }
            else
            {
                _map.Add(key, value);
                var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value);
                OnCollectionChanged(e);
            }
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public bool Remove(TKey key)
        {
            if (_map.ContainsKey(key))
            {
                bool removeFlag = _map.Remove(key);
                if (removeFlag)
                {
                    var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, key);
                    OnCollectionChanged(e);
                    OnPropertyChanged("Count");
                    OnPropertyChanged("Item[]");
                }
                return removeFlag;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _map.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get { return _map[key]; }
            set { Add(key, value); }
        }

        public ICollection<TKey> Keys
        {
            get { return _map.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _map.Values; }
        }

        #endregion

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return ((IXmlSerializable) _map).GetSchema();
        }

        public void ReadXml(XmlReader reader)
        {
            ((IXmlSerializable) _map).ReadXml(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            ((IXmlSerializable) _map).WriteXml(writer);
        }

        #endregion

        #region Implementation of ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) _map).GetObjectData(info, context);
        }

        #endregion

        #region Implementation of ICloneable

        public object Clone()
        {
            return _map.Clone();
        }

        #endregion

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}