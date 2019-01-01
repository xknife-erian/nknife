using System;
using System.Collections;
using System.Collections.Generic;
using NKnife.Base;
using NKnife.Channels.Serials;

namespace NKnife.ChannelKnife.Model
{
    public class SerialChannelService : IDictionary<ushort, Pair<SerialConfig, SerialChannel>>
    {
        private readonly Dictionary<ushort, Pair<SerialConfig, SerialChannel>> _Map = 
            new Dictionary<ushort, Pair<SerialConfig, SerialChannel>>(SerialUtils.LocalSerialPorts.Count);

        public event EventHandler ChannelCountChanged;

        protected virtual void OnChannelCountChanged()
        {
            ChannelCountChanged?.Invoke(this, EventArgs.Empty);
        }

        public SerialChannel AddSerial(SerialConfig config)
        {
            var serialChannel = new SerialChannel(config);
            serialChannel.Closed += (s, e) =>
            {
                Remove(config.Port);
            };
            serialChannel.IsSynchronous = false;
            _Map.Add(config.Port, Pair<SerialConfig, SerialChannel>.Build(config, serialChannel));
            OnChannelCountChanged();
            return serialChannel;
        }

        /// <summary>
        /// 确定 <see cref="T:System.Collections.Generic.IDictionary`2"/> 是否包含具有指定键的元素。
        /// </summary>
        /// <returns>
        /// 如果 <see cref="T:System.Collections.Generic.IDictionary`2"/> 包含带有该键的元素，则为 true；否则，为 false。
        /// </returns>
        /// <param name="key">要在 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中定位的键。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception>
        public bool ContainsKey(ushort key)
        {
            return _Map.ContainsKey(key);
        }

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中移除带有指定键的元素。
        /// </summary>
        /// <returns>
        /// 如果该元素已成功移除，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中没有找到 <paramref name="key"/>，该方法也会返回 false。
        /// </returns>
        /// <param name="key">要移除的元素的键。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.IDictionary`2"/> 为只读。</exception>
        public bool Remove(ushort key)
        {
            if (_Map.Remove(key))
            {
                OnChannelCountChanged();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取包含 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的键的 <see cref="T:System.Collections.Generic.ICollection`1"/>。
        /// </summary>
        /// <returns>
        /// 一个 <see cref="T:System.Collections.Generic.ICollection`1"/>，它包含实现 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的对象的键。
        /// </returns>
        public ICollection<ushort> Keys => _Map.Keys;

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除所有项。
        /// </summary>
        /// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 是只读的。</exception>
        public void Clear()
        {
            _Map.Clear();
            OnChannelCountChanged();
        }

        /// <summary>
        /// 获取 <see cref="T:System.Collections.Generic.ICollection`1"/> 中包含的元素数。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> 中包含的元素数。
        /// </returns>
        public int Count => _Map.Count;

        #region Implementation

        /// <summary>
        /// 在 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中添加一个带有所提供的键和值的元素。
        /// </summary>
        /// <param name="key">用作要添加的元素的键的对象。</param><param name="value">用作要添加的元素的值的对象。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception><exception cref="T:System.ArgumentException"><see cref="T:System.Collections.Generic.IDictionary`2"/> 中已存在具有相同键的元素。</exception><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.IDictionary`2"/> 为只读。</exception>
        void IDictionary<ushort, Pair<SerialConfig, SerialChannel>>.Add(ushort key, Pair<SerialConfig, SerialChannel> value)
        {
            _Map.Add(key,value);
        }

        /// <summary>
        /// 获取与指定的键相关联的值。
        /// </summary>
        /// <returns>
        /// 如果实现 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的对象包含具有指定键的元素，则为 true；否则，为 false。
        /// </returns>
        /// <param name="key">要获取其值的键。</param><param name="value">当此方法返回时，如果找到指定键，则返回与该键相关联的值；否则，将返回 <paramref name="value"/> 参数的类型的默认值。该参数未经初始化即被传递。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception>
        bool IDictionary<ushort, Pair<SerialConfig, SerialChannel>>.TryGetValue(ushort key, out Pair<SerialConfig, SerialChannel> value)
        {
            return _Map.TryGetValue(key, out value);
        }

        /// <summary>
        /// 获取或设置具有指定键的元素。
        /// </summary>
        /// <returns>
        /// 带有指定键的元素。
        /// </returns>
        /// <param name="key">要获取或设置的元素的键。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">检索了属性但没有找到 <paramref name="key"/>。</exception><exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.Generic.IDictionary`2"/> 为只读。</exception>
        Pair<SerialConfig, SerialChannel> IDictionary<ushort, Pair<SerialConfig, SerialChannel>>.this[ushort key]
        {
            get { return _Map[key]; }
            set { _Map[key] = value; }
        }

        /// <summary>
        /// 获取包含 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中的值的 <see cref="T:System.Collections.Generic.ICollection`1"/>。
        /// </summary>
        /// <returns>
        /// 一个 <see cref="T:System.Collections.Generic.ICollection`1"/>，它包含实现 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的对象中的值。
        /// </returns>
        ICollection<Pair<SerialConfig, SerialChannel>> IDictionary<ushort, Pair<SerialConfig, SerialChannel>>.Values => _Map.Values;

        #region Implementation of IEnumerable

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1"/>。
        /// </returns>
        public IEnumerator<KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>>> GetEnumerator()
        {
            return _Map.GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator"/> 对象。
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<ushort,Pair<SerialConfig,SerialChannel>>>

        /// <summary>
        /// 将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1"/> 中。
        /// </summary>
        /// <param name="item">要添加到 <see cref="T:System.Collections.Generic.ICollection`1"/> 的对象。</param><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 是只读的。</exception>
        void ICollection<KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>>>.Add(KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>> item)
        {
            _Map.Add(item);
        }

        /// <summary>
        /// 确定 <see cref="T:System.Collections.Generic.ICollection`1"/> 是否包含特定值。
        /// </summary>
        /// <returns>
        /// 如果在 <see cref="T:System.Collections.Generic.ICollection`1"/> 中找到 <paramref name="item"/>，则为 true；否则为 false。
        /// </returns>
        /// <param name="item">要在 <see cref="T:System.Collections.Generic.ICollection`1"/> 中定位的对象。</param>
        bool ICollection<KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>>>.Contains(KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>> item)
        {
            var map = (IDictionary<ushort, Pair<SerialConfig, SerialChannel>>) _Map;
            return map.Contains(item);
        }

        /// <summary>
        /// 从特定的 <see cref="T:System.Array"/> 索引处开始，将 <see cref="T:System.Collections.Generic.ICollection`1"/> 的元素复制到一个 <see cref="T:System.Array"/> 中。
        /// </summary>
        /// <param name="array">作为从 <see cref="T:System.Collections.Generic.ICollection`1"/> 复制的元素的目标位置的一维 <see cref="T:System.Array"/>。<see cref="T:System.Array"/> 必须具有从零开始的索引。</param><param name="arrayIndex"><paramref name="array"/> 中从零开始的索引，将在此处开始复制。</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> 为 null。</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> 小于 0。</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> 是多维数组。- 或 -源 <see cref="T:System.Collections.Generic.ICollection`1"/> 中的元素数大于从 <paramref name="arrayIndex"/> 到目标 <paramref name="array"/> 结尾处之间的可用空间。- 或 -无法自动将类型 <paramref name="T"/> 强制转换为目标 <paramref name="array"/> 的类型。</exception>
        void ICollection<KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>>>.CopyTo(KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>>[] array, int arrayIndex)
        {
            var map = (IDictionary<ushort, Pair<SerialConfig, SerialChannel>>)_Map;
            map.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <returns>
        /// 如果已从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中成功移除 <paramref name="item"/>，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.Generic.ICollection`1"/> 中没有找到 <paramref name="item"/>，该方法也会返回 false。
        /// </returns>
        /// <param name="item">要从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除的对象。</param><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 是只读的。</exception>
        bool ICollection<KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>>>.Remove(KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>> item)
        {
            var map = (IDictionary<ushort, Pair<SerialConfig, SerialChannel>>)_Map;
            return map.Remove(item);
        }

        /// <summary>
        /// 获取一个值，该值指示 <see cref="T:System.Collections.Generic.ICollection`1"/> 是否为只读。
        /// </summary>
        /// <returns>
        /// 如果 <see cref="T:System.Collections.Generic.ICollection`1"/> 为只读，则为 true；否则为 false。
        /// </returns>
        bool ICollection<KeyValuePair<ushort, Pair<SerialConfig, SerialChannel>>>.IsReadOnly
        {
            get
            {
                var map = (IDictionary<ushort, Pair<SerialConfig, SerialChannel>>) _Map;
                return map.IsReadOnly;
            }
        }

        #endregion

        #endregion

    }
}