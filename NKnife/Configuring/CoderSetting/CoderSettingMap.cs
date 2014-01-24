using System;
using System.Collections;
using System.Collections.Generic;
using NKnife.Configuring.Interfaces;

namespace NKnife.Configuring.CoderSetting
{
    /// <summary>
    /// CoderSetting（程序员配置）集合
    /// </summary>
    public class CoderSettingMap : IDictionary<String, ICoderSetting>
    {
        private readonly Dictionary<string, ICoderSetting> _InnerMap = new Dictionary<string, ICoderSetting>();

        #region IDictionary<string,IOption> Members

        public void Add(string key, ICoderSetting value)
        {
            _InnerMap.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _InnerMap.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _InnerMap.Keys; }
        }

        public bool Remove(string key)
        {
            return _InnerMap.Remove(key);
        }

        public bool TryGetValue(string key, out ICoderSetting value)
        {
            return _InnerMap.TryGetValue(key, out value);
        }

        public ICollection<ICoderSetting> Values
        {
            get { return _InnerMap.Values; }
        }

        public ICoderSetting this[string key]
        {
            get { return _InnerMap[key]; }
            set { _InnerMap[key] = value; }
        }

        public void Add(KeyValuePair<string, ICoderSetting> item)
        {
            _InnerMap.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _InnerMap.Clear();
        }

        public bool Contains(KeyValuePair<string, ICoderSetting> item)
        {
            return _InnerMap.ContainsKey(item.Key);
        }

        /// <summary>
        /// 从指定的索引开始，将当前类型的元素复制到一个目标System.Array中。
        /// </summary>
        /// <param name="targetArray">复制的元素的目标位置的一维 System.Array.</param>
        /// <param name="arrayIndex">array 中从零开始的索引，从此处开始复制。</param>
        public void CopyTo(KeyValuePair<string, ICoderSetting>[] targetArray, int arrayIndex)
        {
            var sourceArray = new KeyValuePair<string, ICoderSetting>[_InnerMap.Count];
            int i = 0;
            foreach (var pair in _InnerMap)
            {
                sourceArray[i] = pair;
                i++;
            }
            Array.Copy(sourceArray, targetArray, arrayIndex);
        }

        public int Count
        {
            get { return _InnerMap.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, ICoderSetting> item)
        {
            return _InnerMap.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, ICoderSetting>> GetEnumerator()
        {
            return _InnerMap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _InnerMap.GetEnumerator();
        }

        #endregion
    }
}