using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.Billboard
{
    public class ExtractRuleCollection : IList<ExtractRule>
    {
        #region 字段或属性

        private List<ExtractRule> _List = new List<ExtractRule>();

        /// <summary>
        /// 该规则所属的域名的名称，
        /// </summary>
        public string RuleName { get; set; }

        #endregion

        #region 公共方法

        /// <summary>
        /// 更具规则名称获得规则对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string name, out ExtractRule value)
        {
            value = this[name];
            if (value != null)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region IList<ExtractRule> 成员

        public int IndexOf(ExtractRule item)
        {
            return this._List.IndexOf(item);
        }

        public void Insert(int index, ExtractRule item)
        {
            Debug.Assert(index >= 0);
            Debug.Assert(item != null);

            this._List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ExtractRule item = this._List[index];
            if (item != null)
            {
                this._List.RemoveAt(index);
            }
        }

        public ExtractRule this[int index]
        {
            get
            {
                Debug.Assert(index >= 0, "Index value cannot \" <0 \"");
                return this._List[index];
            }
            set
            {
                Debug.Assert(index >= 0, "Index value cannot \" <0 \"");
                ExtractRule oldValue = this._List[index];
                if (!object.Equals(oldValue, value))
                {
                    this._List[index] = value;
                }
            }
        }

        public ExtractRule this[string szRuleName]
        {
            get
            {
                ExtractRule retWebRule = null;
                foreach (ExtractRule wr in this)
                {
                    if (szRuleName.Equals(wr.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        retWebRule = wr;
                        break;
                    }
                }
                return retWebRule;
            }
        }

        #endregion

        #region ICollection<ExtractRule> 成员

        public void Add(ExtractRule item)
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

        public bool Contains(ExtractRule item)
        {
            return this._List.Contains(item);
        }

        public void CopyTo(ExtractRule[] array, int arrayIndex)
        {
            foreach (ExtractRule item in array)
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

        public bool Remove(ExtractRule item)
        {
            Debug.Assert(item != null);

            bool bl = false;
            if (this.Contains(item))
            {
                bl = this._List.Remove(item);
            }
            return bl;
        }

        #endregion

        #region IEnumerable<ExtractRule> 成员

        public IEnumerator<ExtractRule> GetEnumerator()
        {
            return this._List.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_List).GetEnumerator();
        }

        #endregion
    }
}
