using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace Jeelu
{
    public class RichList<T> : IList<T>
    {
        List<T> _innerList;
        /// <summary>
        /// 获取是否没有ItemChanged事件。（true则没有ItemChanged事件，针对Item的赋值转换成Remove和Insert的调用）
        /// </summary>
        public bool NoItemChangedEvent { get; private set; }

        #region 构造函数
        public RichList()
        {
            _innerList = new List<T>();
        }
        public RichList(IEnumerable<T> collection)
        {
            _innerList = new List<T>(collection);
        }
        public RichList(int capacity)
        {
            _innerList = new List<T>(capacity);
        }
        public RichList(bool noItemChangedEvent)
        {
            this.NoItemChangedEvent = noItemChangedEvent;
            _innerList = new List<T>();
        }
        #endregion

        #region IList<T> 成员

        public int IndexOf(T item)
        {
            Debug.Assert(item != null);

            return IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Debug.Assert(index >= 0);
            Debug.Assert(item != null);
            
            try
            {
                _innerList.Insert(index, item);
            }
            finally
            {
                OnInserted(new EventArgs<T>(item));
            }
        }

        public void RemoveAt(int index)
        {
            Debug.Assert(index >= 0);

            T item = this[index];

            _innerList.RemoveAt(index);

            OnRemoved(new EventArgs<T>(item));
        }

        public T this[int index]
        {
            get
            {
                Debug.Assert(index >= 0);

                return _innerList[index];
            }
            set
            {
                Debug.Assert(index >= 0);

                ///先取出旧值
                T oldValue = _innerList[index];

                ///比较旧值和新值，不相等则继续
                if (!object.Equals(oldValue, value))
                {
                    if (NoItemChangedEvent)
                    {
                        RemoveAt(index);
                        Insert(index, value);
                    }
                    else
                    {
                        ///赋新值
                        _innerList[index] = value;

                        ///触发事件
                        OnItemChanged(new ChangedEventArgs<T>(oldValue, value));
                    }
                }
            }
        }

        #endregion

        #region ICollection<T> 成员

        public void Add(T item)
        {
            Debug.Assert(item != null);

            ///调用本身的Insert方法，以共享Inserted事件
            this.Insert(this.Count, item);
        }

        public void Clear()
        {
            while (_innerList.Count > 0)
            {
                RemoveAt(0);
            }
        }

        public void ClearWithout(T withoutItem)
        {
            int i = 0;
            while (_innerList.Count > i)
            {
                T item = _innerList[i];
                if (object.Equals(item, withoutItem))
                {
                    i++;
                    continue;
                }
                RemoveAt(i);
            }
        }

        public bool Contains(T item)
        {
            Debug.Assert(item != null);
            return _innerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _innerList.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            Debug.Assert(item != null);
            try
            {
                return _innerList.Remove(item);
            }
            finally
            {
                OnRemoved(new EventArgs<T>(item));
            }
        }

        #endregion

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_innerList).GetEnumerator();
        }

        #endregion

        #region 添加一些事件

        public event EventHandler<ChangedEventArgs<T>> ItemChanged;
        protected virtual void OnItemChanged(ChangedEventArgs<T> e)
        {
            if (ItemChanged != null)
            {
                ItemChanged(this, e);
            }
        }

        public event EventHandler<EventArgs<T>> Removed;
        protected virtual void OnRemoved(EventArgs<T> e)
        {
            if (Removed != null)
            {
                Removed(this, e);
            }
        }

        public event EventHandler<EventArgs<T>> Inserted;
        protected virtual void OnInserted(EventArgs<T> e)
        {
            if (Inserted != null)
            {
                Inserted(this, e);
            }
        }

        #endregion

        #region 添加List所拥有的一些方法

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T t in collection)
            {
                Add(t);
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly()
        {
            return _innerList.AsReadOnly();
        }

        public int BinarySearch(T item)
        {
            return _innerList.BinarySearch(item);
        }
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return _innerList.BinarySearch(item, comparer);
        }
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return _innerList.BinarySearch(index, count, item, comparer);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            _innerList.CopyTo(index, array, arrayIndex, count);
        }
        public bool Exists(Predicate<T> match)
        {
            return _innerList.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return _innerList.Find(match);
        }
        public List<T> FindAll(Predicate<T> match)
        {
            return _innerList.FindAll(match);
        }
        public int FindIndex(Predicate<T> match)
        {
            return _innerList.FindIndex(match);
        }
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return _innerList.FindIndex(startIndex, match);
        }
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return _innerList.FindIndex(startIndex, count, match);
        }
        public T FindLast(Predicate<T> match)
        {
            return _innerList.FindLast(match);
        }
        public int FindLastIndex(Predicate<T> match)
        {
            return _innerList.FindLastIndex(match);
        }
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return _innerList.FindLastIndex(startIndex, match);
        }
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return _innerList.FindLastIndex(startIndex, count, match);
        }
        public void ForEach(Action<T> action)
        {
            _innerList.ForEach(action);
        }

        public List<T> GetRange(int index, int count)
        {
            return _innerList.GetRange(index, count);
        }
        public int IndexOf(T item, int index)
        {
            return _innerList.IndexOf(item, index);
        }
        public int IndexOf(T item, int index, int count)
        {
            return _innerList.IndexOf(item, index, count);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            int insertIndex = index;
            foreach (T t in collection)
            {
                _innerList.Insert(insertIndex++, t);
            }
        }
        public int LastIndexOf(T item)
        {
            return _innerList.LastIndexOf(item);
        }
        public int LastIndexOf(T item, int index)
        {
            return _innerList.LastIndexOf(item, index);
        }
        public int LastIndexOf(T item, int index, int count)
        {
            return _innerList.LastIndexOf(item, index, count);
        }

        public int RemoveAll(Predicate<T> match)
        {
            int index = 0;
            int count = 0;
            while (index < this.Count)
            {
                if (match(this[index]))
                {
                    RemoveAt(index);
                    count++;
                }
                else
                {
                    index++;
                }
            }
            return count;
        }

        public void RemoveRange(int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveAt(index);
            }
        }
        public void Reverse()
        {
            T[] tempArrs = this.ToArray();

            this.Clear();
            for (int i = Count - 1; i >= 0; i--)
            {
                this.Add(tempArrs[i]);
            }
        }
        //by zhucai：暂不提供 in 2008年3月2日
        //public void Reverse(int index, int count)
        //{
        //    if (index + count > this.Count)
        //    {
        //        throw new ArgumentOutOfRangeException();
        //    }
        //    List<T> temp = new List<T>(ToArray());

        //    int lastReverseIndex = count + index - 1;
        //    for (int i = index; i <= lastReverseIndex; i++)
        //    {
        //        temp[i] = _innerList[lastReverseIndex - (i - index)];
        //    }

        //    _innerList = temp;

        //    OnReversed(EventArgs.Empty);
        //}
        public void TrimExcess()
        {
            _innerList.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return _innerList.TrueForAll(match);
        }
        public T[] ToArray()
        {
            T[] arr = new T[Count];
            _innerList.CopyTo(arr);
            return arr;
        }

        #endregion
    }
}
