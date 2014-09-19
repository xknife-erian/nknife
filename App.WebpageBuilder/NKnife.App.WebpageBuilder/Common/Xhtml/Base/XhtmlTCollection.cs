using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Diagnostics;

namespace Jeelu
{
    /// <summary>
    ///	DesignBy: Jeelu.com Lukan, 2008.6.2 3:47
    /// </summary>
    public class XhtmlTCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>
    {
        private List<T> _List = new List<T>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in this._List)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }

        #region IList<T> 成员

        private bool HasItemChangedEvent { get; set; }

        public int IndexOf(T item)
        {
            return this._List.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Debug.Assert(index >= 0);
            Debug.Assert(item != null);

            this._List.Insert(index, item);
            OnInserted(new EventArgs<T>(item));
        }

        public void RemoveAt(int index)
        {
            T item = this._List[index];
            this._List.RemoveAt(index);
            OnRemoved(new EventArgs<T>(item));
        }

        public T this[int index]
        {
            get
            {
                Debug.Assert(index >= 0, "Index value cannot \" <0 \"");
                return this._List[index];
            }
            set
            {
                Debug.Assert(index >= 0, "Index value cannot \" <0 \"");

                T oldValue = this._List[index];
                if (!object.Equals(oldValue, value))
                {
                    if (this.HasItemChangedEvent)
                    {
                        RemoveAt(index);
                        Insert(index, value);
                    }
                    else
                    {
                        this._List[index] = value;
                        OnItemChanged(new ChangedEventArgs<T>(oldValue, value));
                    }
                }
            }
        }

        #endregion

        #region ICollection<T> 成员

        public void Add(T item)
        {
            Debug.Assert(item != null, "T Item is Null");
            this.Insert(this.Count, item);
        }

        public void Clear()
        {
            while (this._List.Count > 0)
            {
                this.RemoveAt(0);
            }
        }

        public bool Contains(T item)
        {
            return this._List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T item in array)
            {
                this.Insert(arrayIndex, item);
                arrayIndex++;
            }
        }

        public int Count
        {
            get { return this._List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            try
            {
                return this._List.Remove(item);
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
            return this._List.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MetasEnumerator(this);
        }

        #endregion

        #region 供IEnumerator使用的内部类
        class MetasEnumerator : IEnumerator
        {
            int _index = -1;
            XhtmlTCollection<T> _collection;

            internal MetasEnumerator(XhtmlTCollection<T> collection)
            {
                _collection = collection;
                if (_collection._List.Count == 0)
                {
                    _index = 0;
                }
            }

            #region IEnumerator 成员

            public object Current
            {
                get
                {
                    if (_index >= 0)
                    {
                        return _collection._List[_index];
                    }
                    else
                    {
                        throw new InvalidOperationException("You don't do that");
                    }
                }
            }

            public bool MoveNext()
            {
                if (_index < _collection._List.Count - 1)
                {
                    _index++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _index = -1;
            }

            #endregion
        }
        #endregion

        #region 事件

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
    }
}
