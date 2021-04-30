using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using NKnife.Events;
using NKnife.Interface;
using NKnife.Win.Forms.Common;

namespace NKnife.Win.Forms.Frames.Base
{
    /// <summary>
    ///     当在图板上设计(绘制矩形)时,绘制的矩形的集合
    /// </summary>
    public class RectangleList : IList<RectangleF>, IXml
    {
        private readonly List<RectangleF> _list = new List<RectangleF>();

        public RectangleList()
        {
            Current = new Selected();
        }

        /// <summary>
        ///     鼠标正悬停在这个矩形的上方
        /// </summary>
        public RectangleF Hover { get; set; }

        /// <summary>
        /// 已选择的矩形的列表
        /// </summary>
        public Selected Current { get; set; }

        #region Event

        /// <summary>
        ///     当集合内部发生变化时发生
        /// </summary>
        public event EventHandler CollectionChanged;

        protected virtual void OnCollectionChanged()
        {
            EventHandler handler = CollectionChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        #region IList

        public IEnumerator<RectangleF> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(RectangleF item)
        {
            _list.Add(item);
            OnCollectionChanged();
        }

        public void AddRange(IEnumerable<RectangleF> rects)
        {
            _list.AddRange(rects);
            OnCollectionChanged();
        }

        public void Clear()
        {
            _list.Clear();
            Current.Clear();
            OnCollectionChanged();
        }

        public bool Contains(RectangleF item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(RectangleF[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
            OnCollectionChanged();
        }

        public bool Remove(RectangleF item)
        {
            bool rs = _list.Remove(item);
            OnCollectionChanged();
            return rs;
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public int IndexOf(RectangleF item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, RectangleF item)
        {
            _list.Insert(index, item);
            OnCollectionChanged();
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            OnCollectionChanged();
        }

        public RectangleF this[int index]
        {
            get { return _list[index]; }
            set
            {
                _list[index] = value;
                OnCollectionChanged();
            }
        }

        bool ICollection<RectangleF>.IsReadOnly
        {
            get { return ((ICollection<RectangleF>) _list).IsReadOnly; }
        }

        #endregion

        #region IXml

        public XmlElement ToXml(XmlDocument parent)
        {
            XmlElement root = parent.CreateElement("Rectangle_List");
            foreach (RectangleF rect in this)
            {
                XmlElement ele = parent.CreateElement("Rectangle");
                rect.FillXmlElement(ele);
                root.AppendChild(ele);
            }
            return root;
        }

        public void Parse(XmlElement element)
        {
            foreach (object node in element.ChildNodes)
            {
                if (!(node is XmlElement))
                    continue;
                var ele = (XmlElement) node;
                if (ele.LocalName != "Rectangle")
                    continue;
                var rect = new RectangleF();
                Rectangles.ParseXmlElement(ref rect, ele);
                Add(rect);
            }
        }

        #endregion

        #region Class

        /// <summary>
        /// 描述一个已选择的矩形的列表的集合类，该类添加了集合发生变化时的事件
        /// </summary>
        public class Selected : IList<RectangleF>, ICloneable
        {
            private readonly List<RectangleF> _List = new List<RectangleF>();
            private SelectedMode _SelectedMode = SelectedMode.None;

            public Selected()
            {
                CollectionChanged += (s, e) =>
                {
                    SelectedMode = SelectedMode.None;
                };
            }

            /// <summary>
            /// 当前被选择的矩形集合的下一步工作模式
            /// </summary>
            public SelectedMode SelectedMode
            {
                get { return _SelectedMode; }
                set
                {
                    var old = _SelectedMode;
                    _SelectedMode = value;
                    OnSelectedModeChanged(new ChangedEventArgs<SelectedMode>(old, value));
                }
            }

            #region IList

            public IEnumerator<RectangleF> GetEnumerator()
            {
                return _List.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(RectangleF item)
            {
                _List.Add(item);
                OnCollectionChanged();
            }

            public void AddRange(IEnumerable<RectangleF> rects)
            {
                _List.AddRange(rects);
                OnCollectionChanged();
            }

            public void Clear()
            {
                _List.Clear();
                OnCollectionChanged();
            }

            public bool Contains(RectangleF item)
            {
                return _List.Contains(item);
            }

            public void CopyTo(RectangleF[] array, int arrayIndex)
            {
                _List.CopyTo(array, arrayIndex);
                OnCollectionChanged();
            }

            public bool Remove(RectangleF item)
            {
                bool rs = _List.Remove(item);
                OnCollectionChanged();
                return rs;
            }

            public int Count
            {
                get { return _List.Count; }
            }

            public int IndexOf(RectangleF item)
            {
                return _List.IndexOf(item);
            }

            public void Insert(int index, RectangleF item)
            {
                _List.Insert(index, item);
                OnCollectionChanged();
            }

            public void RemoveAt(int index)
            {
                _List.RemoveAt(index);
                OnCollectionChanged();
            }

            public RectangleF this[int index]
            {
                get { return _List[index]; }
                set
                {
                    _List[index] = value;
                    OnCollectionChanged();
                }
            }

            bool ICollection<RectangleF>.IsReadOnly
            {
                get { return ((ICollection<RectangleF>)_List).IsReadOnly; }
            }

            #endregion

            public event EventHandler<ChangedEventArgs<SelectedMode>> SelectedModeChanged;

            protected virtual void OnSelectedModeChanged(ChangedEventArgs<SelectedMode> e)
            {
                EventHandler<ChangedEventArgs<SelectedMode>> handler = SelectedModeChanged;
                if (handler != null) 
                    handler(this, e);
            }

            /// <summary>
            ///     当集合内部发生变化时发生
            /// </summary>
            public event EventHandler CollectionChanged;

            protected virtual void OnCollectionChanged()
            {
                EventHandler handler = CollectionChanged;
                if (handler != null)
                    handler(this, EventArgs.Empty);
            }

            public object Clone()
            {
                var list = new Selected();
                foreach (var rect in this)
                {
                    list.Add(rect);
                }
                return list;
            }
        }

        /// <summary>
        /// 当前被选择的矩形集合的下一步工作模式
        /// </summary>
        public enum SelectedMode
        {
            /// <summary>
            /// 下一步无动作
            /// </summary>
            None,
            /// <summary>
            /// 即将被剪切
            /// </summary>
            Cut,
            /// <summary>
            /// 即将被拷贝
            /// </summary>
            Copy            
        }

        #endregion

    }
}