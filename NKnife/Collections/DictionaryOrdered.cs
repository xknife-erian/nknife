using System;
using System.Collections;
using System.Collections.Generic;

namespace NKnife.Collections
{
    [Serializable]
    public class DictionaryOrdered<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Private Data members

        private readonly IList<TKey> _list;
        private readonly IDictionary<TKey, TValue> _map;

        #endregion

        #region Constructors

        public DictionaryOrdered()
        {
            _list = new List<TKey>();
            _map = new Dictionary<TKey, TValue>();
        }

        #endregion

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Add to key/value for both forward and reverse lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            // Add to map and list.
            _map.Add(key, value);
            _list.Add(key);
        }

        /// <summary>
        /// Determine if the key is contain in the forward lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return _map.ContainsKey(key);
        }

        /// <summary>
        /// Get a list of all the keys in the forward lookup.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return _map.Keys; }
        }

        /// <summary>
        /// Remove the key from the ordered dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            // Check.
            if (!_map.ContainsKey(key)) return false;

            int ndxKey = IndexOfKey(key);
            _map.Remove(key);
            _list.RemoveAt(ndxKey);
            return true;
        }

        /// <summary>
        /// Try to get the value from the forward lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _map.TryGetValue(key, out value);
        }

        /// <summary>
        /// Get the collection of values.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _map.Values; }
        }

        /// <summary>
        /// Set the key / value for bi-directional lookup.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get { return _map[key]; }
            set { _map[key] = value; }
        }

        /// <summary>
        /// Add to ordered lookup.
        /// </summary>
        /// <param name="item"></param>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Clears keys/value for bi-directional lookup.
        /// </summary>
        public void Clear()
        {
            _map.Clear();
            _list.Clear();
        }

        /// <summary>
        /// Determine if the item is in the forward lookup.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _map.Contains(item);
        }

        /// <summary>
        /// Copies the array of key/value pairs for both ordered dictionary.
        /// TO_DO: This needs to implemented.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _map.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get number of entries.
        /// </summary>
        public int Count
        {
            get { return _map.Count; }
        }

        /// <summary>
        /// Get whether or not this is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _map.IsReadOnly; }
        }

        /// <summary>
        /// Remove the item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            // Check.
            if (!_map.ContainsKey(item.Key)) return false;

            int ndxOfKey = IndexOfKey(item.Key);
            _list.RemoveAt(ndxOfKey);
            return _map.Remove(item);
        }

        /// <summary>
        /// Get the enumerator for the forward lookup.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        /// <summary>
        /// Get the enumerator for the forward lookup.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        #endregion

        #region IList methods

        /// <summary>
        /// Get/set the value at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TValue this[int index]
        {
            get
            {
                TKey key = _list[index];
                return _map[key];
            }
            set
            {
                TKey key = _list[index];
                _map[key] = value;
            }
        }

        /// <summary>
        /// Insert key/value at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(int index, TKey key, TValue value)
        {
            // Add to map and list.
            _map.Add(key, value);
            _list.Insert(index, key);
        }


        /// <summary>
        /// Get the index of the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int IndexOfKey(TKey key)
        {
            if (!_map.ContainsKey(key)) return -1;

            for (int ndx = 0; ndx < _list.Count; ndx++)
            {
                TKey keyInList = _list[ndx];
                if (keyInList.Equals(key))
                    return ndx;
            }
            return -1;
        }


        /// <summary>
        /// Remove the key/value item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            TKey key = _list[index];
            _map.Remove(key);
            _list.RemoveAt(index);
        }

        #endregion
    }
}