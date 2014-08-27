﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using NKnife.Draws.Common;
using NKnife.Interface;

namespace NKnife.Draws.Controls.Frames.Base
{
    /// <summary>
    ///     当在图板上设计(绘制矩形)时,绘制的矩形的集合
    /// </summary>
    public class RectangleList : IList<RectangleF>, IXml
    {
        private readonly List<RectangleF> _List = new List<RectangleF>();

        public RectangleList()
        {
            Current = new Selected();
        }

        /// <summary>
        ///     鼠标正悬停在这个矩形的上方
        /// </summary>
        public RectangleF Hover { get; set; }

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
            get { return ((ICollection<RectangleF>) _List).IsReadOnly; }
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

        public class Selected : IList<RectangleF>
        {
            private readonly List<RectangleF> _List = new List<RectangleF>();

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
        }

        #endregion

    }
}