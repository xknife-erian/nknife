using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace Jeelu.Billboard
{
    public class WebRuleCollection : IList<WebRule>
    {
        #region 字段或属性

        /// <summary>
        /// 规则集合
        /// </summary>
        private List<WebRule> _List = new List<WebRule>();

        /// <summary>
        /// 网站域名
        /// </summary>
        public string SiteName { get; set; }


        public long ID { get; set; }

        #endregion

        #region 构造函数

        public WebRuleCollection()
        {

        }

        public WebRuleCollection(string sitename)
        {
            this.SiteName = sitename;
        }

        public WebRuleCollection(Uri uri)
        {
            this.SiteName = uri.Host;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 根据域名得到对应的规则集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string name, out WebRule value)
        {
            value = this[name];
            if (value != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据规则名，判断该规则是否存在
        /// </summary>
        /// <param name="szRuleNmae"></param>
        /// <returns></returns>
        public bool IsExistRule(string szRuleNmae)
        {
            WebRule wr = this[szRuleNmae];
            //TryGetValue(szRuleNmae, out wr);
            if (wr != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除一个规则
        /// </summary>
        /// <param name="wr"></param>
        public void DeleteRule(WebRule webRule)
        {
            //只标记他，并不真的删除 
            if (this.Contains(webRule))
            {
                webRule.RuleState = WebRuleState.Delete;
            }

        }

        #endregion

        #region IList<WebRule> 成员

        public int IndexOf(WebRule item)
        {
            return this._List.IndexOf(item);
        }

        public void Insert(int index, WebRule item)
        {
            Debug.Assert(index >= 0);
            Debug.Assert(item != null);

            this._List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            WebRule item = this._List[index];
            if (item != null)
            {
                this._List.RemoveAt(index);
            }
        }

        public WebRule this[int index]
        {
            get
            {
                Debug.Assert(index >= 0, "Index value cannot \" <0 \"");
                return this._List[index];
            }
            set
            {
                Debug.Assert(index >= 0, "Index value cannot \" <0 \"");
                WebRule oldValue = this._List[index];
                if (!object.Equals(oldValue, value))
                {
                    this._List[index] = value;
                }
            }
        }

        public WebRule this[string szRuleName]
        {
            get
            {
                WebRule retWebRule = null;
                foreach (WebRule wr in this)
                {
                    if (szRuleName.Equals(wr.RuleName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        retWebRule = wr;
                        break;
                    }
                }
                return retWebRule;
            }
        }
        #endregion

        #region ICollection<WebRule> 成员

        public void Add(WebRule item)
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

        public bool Contains(WebRule item)
        {
            return this._List.Contains(item);
        }

        public void CopyTo(WebRule[] array, int arrayIndex)
        {
            foreach (WebRule item in array)
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

        public bool Remove(WebRule item)
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

        #region IEnumerable<WebRule> 成员

        public IEnumerator<WebRule> GetEnumerator()
        {
            return this._List.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_List).GetEnumerator();
        }

        #endregion

    }

}
