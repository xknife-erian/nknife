using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace Jeelu
{
    /// <summary>
    /// 属性名值对的集合，实现IList接口及简单迭代。
    /// 一般用来表示一个标记节点的多个属性。
    /// design by Lukan, 2008年6月10日1时3分
    /// </summary>
    public class XhtmlAttributeCollection : 
        IList<XhtmlAttribute>, 
        ICollection<XhtmlAttribute>, 
        IEnumerable<XhtmlAttribute>
    {
        private List<XhtmlAttribute> _List = new List<XhtmlAttribute>();

        /// <summary>
        /// 构造函数，生成一个XhtmlAttributeCollection实例
        /// </summary>
        public XhtmlAttributeCollection() { }

        /// <summary>
        /// 构造函数，生成一个XhtmlAttributeCollection实例
        /// </summary>
        /// <param name="element">该属性集合所附属（父级）的节点</param>
        public XhtmlAttributeCollection(XhtmlElement element)
        {
            this.ParentElement = element;
        }


        /// <summary>
        /// 父节点
        /// </summary>
        internal XhtmlElement ParentElement { get; set; }

        #region IList<XhtmlAttribute> 成员

        /// <summary>
        /// 搜索指定的对象，并返回第一个匹配项的从零开始的索引。
        /// </summary>
        public int IndexOf(XhtmlAttribute item)
        {
            return this._List.IndexOf(item);
        }


        /// <summary>
        /// 将元素插入集合的指定索引处。
        /// </summary>
        /// <param name="index">指定的索引</param>
        /// <param name="item">元素</param>
        public void Insert(int index, XhtmlAttribute item)
        {
            this._List.Insert(index, item);
            this.ParentElement.SetAttribute(item);
        }


        /// <summary>
        /// 移除指定索引处的元素
        /// </summary>
        /// <param name="index">指定的索引</param>
        public void RemoveAt(int index)
        {
            XhtmlAttribute item = this._List[index];
            this.Remove(item);
        }


        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index">指定的索引</param>
        public XhtmlAttribute this[int index]
        {
            get
            {
                Debug.Assert(!(index < 0 && index > this._List.Count), index.ToString());
                return this._List[index];
            }
            set
            {
                Debug.Assert(!(index < 0 && index > this._List.Count), index.ToString());
                XhtmlAttribute item = this._List[index];
                this.Replace(value, item);
            }
        }


        /// <summary>
        /// 根据属性名获取或设置集合中的元素，与属性的值无关。
        /// </summary>
        /// <param name="attributeName">属性名</param>
        public XhtmlAttribute this[string attributeName]
        {
            get
            {
                foreach (XhtmlAttribute item in this._List)
                {
                    if (item.Key == attributeName)
                    {
                        return item;
                    }
                }
                return null;
            }
            set
            {
                foreach (XhtmlAttribute item in this._List)
                {
                    if (item.Key == attributeName)
                    {
                        this.Replace(value, item);
                    }
                }
            }
        }


        #endregion

        #region ICollection<XhtmlAttribute> 成员

        /// <summary>
        /// 增加一个属性
        /// </summary>
        /// <param name="item">属性</param>
        public void Add(XhtmlAttribute item)
        {
            this.Insert(this._List.Count, item);
        }


        /// <summary>
        /// 将指定集合的属性添加到
        /// </summary>
        /// <param name="attributes">属性数组</param>
        public void AddRange(XhtmlAttribute[] attributes)
        {
            foreach (XhtmlAttribute item in attributes)
            {
                this.Add(item);
            }
        }


        /// <summary>
        /// 清除所有属性
        /// </summary>
        public void Clear()
        {
            this._List.Clear();
            //this.ParentElement.RemoveAllAttributes();
        }


        /// <summary>
        /// 是否包含指定的属性，属性名与属性值均为一致
        /// </summary>
        /// <param name="item">属性</param>
        public bool Contains(XhtmlAttribute item)
        {
            return this._List.Contains(item);
        }


        /// <summary>
        /// 是否包含指定属性名的属性，与值无关
        /// </summary>
        /// <param name="itemName">属性名</param>
        /// <param name="attrItem">如果包含指定属性名的属性，返回这个属性</param>
        public bool Contains(string itemName, out XhtmlAttribute attrItem)
        {
            foreach (XhtmlAttribute item in this._List)
            {
                if (item.Key == itemName)
                {
                    attrItem = item;
                    return true;
                }
            }
            attrItem = null;
            return false;
        }


        /// <summary>
        /// 将所有属性拷贝到一个属性类型的一维数组中
        /// </summary>
        /// <param name="array">属性类型的一维数组</param>
        /// <param name="arrayIndex">array 中从零开始的索引，在此处开始复制。</param>
        public void CopyTo(XhtmlAttribute[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取集合中实际包含的元素数。
        /// </summary>
        public int Count
        {
            get { return this._List.Count; }
        }


        /// <summary>
        /// 集合是否为只读的，true为只读的，false为可读写的集合
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }


        /// <summary>
        /// 移除指定的元素
        /// </summary>
        /// <param name="item">指定的属性元素</param>
        public bool Remove(XhtmlAttribute item)
        {
            try
            {
                if (this.Contains(item))
                {
                    this._List.Remove(item);
                    //this.ParentElement.RemoveAttribute(item.Key);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                Debug.Fail("Remove Fail!");
                return false;
            }
        }


        /// <summary>
        /// 用指定的元素替换一个引用的元素
        /// </summary>
        /// <param name="newItem">新的指定的元素</param>
        /// <param name="oldItem">引用的元素</param>
        public void Replace(XhtmlAttribute newItem, XhtmlAttribute oldItem)
        {
            this.Remove(oldItem);
            this.Add(newItem);
        }


        #endregion

        #region IEnumerable<XhtmlAttribute> 成员

        public IEnumerator<XhtmlAttribute> GetEnumerator()
        {
            return this._List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new AttributeCollectionEnumerator(this);
        }

        #region 供IEnumerator使用的内部类
        class AttributeCollectionEnumerator : IEnumerator
        {
            int _index = -1;
            XhtmlAttributeCollection _collection;

            internal AttributeCollectionEnumerator(XhtmlAttributeCollection collection)
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

        #endregion

        /// <summary>
        /// 已重写，返回真实的属性集合字符串
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (XhtmlAttribute attr in this._List)
            {
                sb.Append(attr.ToString()).Append(" ");
            }
            return sb.ToString().TrimEnd(' ');
        }

    }
}
