using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace NKnife.Collections
{
    /// <summary>一个同步安全的队列类型，内部包含一个AutoResetEvent，可通过该AutoResetEvent处理本队列的监听。
    /// </summary>
    public class SyncQueue<T> : ICollection
    {
        protected readonly ConcurrentQueue<T> _Q = new ConcurrentQueue<T>();

        public SyncQueue()
        {
            AddEvent = new AutoResetEvent(false);
        }

        public SyncQueue(IEnumerable<T> collection)
            : this()
        {
            foreach (T t in collection)
            {
                Enqueue(t);
            }
        }

        public AutoResetEvent AddEvent { get; protected set; }

        public void Clear()
        {
            bool isDequeueSucess = true;
            while (isDequeueSucess)
            {
                T value;
                isDequeueSucess = _Q.TryDequeue(out value);
            }
        }

        public bool TryDequeue(out T value)
        {
            return _Q.TryDequeue(out value);
        }

        /// <summary>向队列中压入一条指定类型的数据
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            _Q.Enqueue(item);
            AddEvent.Set();
        }

        public bool TryPeek(out T value)
        {
            return _Q.TryPeek(out value);
        }

        public T[] ToArray()
        {
            return _Q.ToArray();
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator"/> 对象。
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return _Q.GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection

        /// <summary>
        /// 从特定的 <see cref="T:System.Array"/> 索引处开始，将 <see cref="T:System.Collections.ICollection"/> 的元素复制到一个 <see cref="T:System.Array"/> 中。
        /// </summary>
        /// <param name="array">作为从 <see cref="T:System.Collections.ICollection"/> 复制的元素的目标位置的一维 <see cref="T:System.Array"/>。<see cref="T:System.Array"/> 必须具有从零开始的索引。</param>
        /// <param name="index"><paramref name="array"/> 中从零开始的索引，将在此处开始复制。</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array"/> 为 null。</exception>
        ///   
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index"/> 小于零。</exception>
        ///   
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="array"/> 是多维的。- 或 -源 <see cref="T:System.Collections.ICollection"/> 中的元素数目大于从 <paramref name="index"/> 到目标 <paramref name="array"/> 末尾之间的可用空间。</exception>
        ///   
        /// <exception cref="T:System.ArgumentException">源 <see cref="T:System.Collections.ICollection"/> 的类型无法自动转换为目标 <paramref name="array"/> 的类型。</exception>
        void ICollection.CopyTo(Array array, int index)
        {
            if (!(array is T[]))
                throw new ArgumentException("传入数据不是指定的类型，应传入T[]。");
            _Q.CopyTo((T[]) array, index);
        }

        /// <summary>
        /// 获取 <see cref="T:System.Collections.ICollection"/> 中包含的元素数。
        /// </summary>
        /// <returns>
        ///   <see cref="T:System.Collections.ICollection"/> 中包含的元素数。</returns>
        public int Count => _Q.Count;

        /// <summary>
        /// 获取一个可用于同步对 <see cref="T:System.Collections.ICollection"/> 的访问的对象。
        /// </summary>
        /// <returns>可用于同步对 <see cref="T:System.Collections.ICollection"/> 的访问的对象。</returns>
        object ICollection.SyncRoot => ((ICollection)_Q).SyncRoot;

        /// <summary>
        /// 获取一个值，该值指示是否同步对 <see cref="T:System.Collections.ICollection"/> 的访问（线程安全）。
        /// </summary>
        /// <returns>如果对 <see cref="T:System.Collections.ICollection"/> 的访问是同步的（线程安全），则为 true；否则为 false。</returns>
        bool ICollection.IsSynchronized => ((ICollection)_Q).IsSynchronized;

        #endregion
    }
}