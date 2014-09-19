using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.Billboard
{
    public class WebRule
    {
        #region 字段或者属性

        /// <summary>
        /// 规则的状态
        /// </summary>
        public WebRuleState RuleState { get; set; }
        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; }

        protected virtual Uri Uri { get; set; }
        /// <summary>
        /// 所有动态参数的键值
        /// </summary>
        public Dictionary<string, ParamState> ParamStates { get; set; }

        /// <summary>
        /// 正文抽取規則集合
        /// </summary>
        public ExtractRuleCollection ExtractRuleCollection { get; set; }

        /// <summary>
        /// 规则是否是动态的
        /// </summary>
        public bool isUrlDynamic { get; set; }

        public long ID { get; set; }

        #endregion

        #region 构造函数

        public WebRule()
        {
            ExtractRuleCollection = new ExtractRuleCollection();
        }

        public WebRule(Uri uri)
        {
            this.Uri = uri;
            this.RuleName = GetRuleFormUrl(uri);
            this.ParamStates = this.GetSegments(this.Uri.Query);
            this.isUrlDynamic = !string.IsNullOrEmpty(this.Uri.Query);

            RuleState = WebRuleState.New;
            ExtractRuleCollection = new ExtractRuleCollection();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 设置该参数的无效状态
        /// </summary>
        /// <param name="key"></param>
        public void SetParamSate(string key)
        {
            ParamState value;
            if (!this.ParamStates.TryGetValue(key, out value))
            {
                Debug.Fail(key + " is error!");
            }
            value.KeyState = WebKeyState.Availability;
            value.IsStateChange = false;
        }

        /// <summary>
        /// 设置某参数的状态
        /// </summary>
        /// <param name="key"></param>
        public void SetParamSate(string key, WebKeyState keyState, bool paramSate)
        {
            ParamState value;
            if (!this.ParamStates.TryGetValue(key, out value))
            {
                Debug.Fail(key + " is error!");
            }
            value.KeyState = keyState;
            value.IsStateChange = paramSate;
        }

        /// <summary>
        /// 增加一个默认value为空 WebKeyState为有效的 参数
        /// </summary>
        /// <param name="key"></param>
        public void AddParam(string key)
        {
            AddParam(key, WebKeyState.Availability);
        }

        /// <summary>
        /// 为该规则增加一个参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="keyState"></param>
        public void AddParam(string key, WebKeyState keyState)
        {
            ParamState ps = new ParamState(key, keyState);
            if (this.ParamStates == null)
            {
                ParamStates = new Dictionary<string, ParamState>();
            }
            Debug.Assert(!this.ParamStates.ContainsKey(key), key + " is Exist");
            this.ParamStates.Add(key, ps);
        }

        /// <summary>
        /// 为该规则增加一个参数
        /// </summary>
        /// <param name="ps"></param>
        public void AddParam(ParamState ps)
        {
            Debug.Assert(ps != null, " ParamState is null");


            if (this.ParamStates == null)
            {
                ParamStates = new Dictionary<string, ParamState>();
            }
            Debug.Assert(!ParamStates.ContainsKey(ps.ParamKey), " ParamState is Exist");

            this.ParamStates.Add(ps.ParamKey, ps);

        }

        /// <summary>
        /// 根据参数名 删除某个参数
        /// </summary>
        /// <param name="szParamName"></param>
        public void DeleteParam(string szParamName)
        {
            if (this.ParamStates.ContainsKey(szParamName))
            {
                this.ParamStates.Remove(szParamName);
            }
        }

        /// <summary>
        /// 根据参数对象删除某个参数
        /// </summary>
        /// <param name="ps"></param>
        public void DeleteParam(ParamState ps)
        {
            DeleteParam(ps.ParamKey);
        }

        public override string ToString()
        {
            return "";
        }

        #endregion

        #region 内部方法

        private Dictionary<string, ParamState> GetSegments(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            Dictionary<string, ParamState> pss = new Dictionary<string, ParamState>();

            string[] querys = url.Substring(1).Split('&');
            foreach (var item in querys)
            {
                ParamState ps = new ParamState(item);
                pss.Add(ps.ParamKey, ps);
            }
            return pss;
        }

        /// <summary>
        /// 根据URL生成对应的规则名称
        /// </summary>
        /// <returns></returns>
        internal static string GetRuleFormUrl(Uri url)
        {
            string szRuleName = url.Host + url.AbsolutePath;
            SortedDictionary<string, string> qDic = new SortedDictionary<string, string>();
            if (!string.IsNullOrEmpty(url.Query))
            {
                string[] querys = url.Query.Substring(1).Split('&');
                foreach (var query in querys)
                {
                    string[] x = query.Split('=');
                    if (x != null && x.Length != 0)
                    {
                        if (x.Length >= 1)
                        {
                            qDic[x[0]] = "";
                        }
                    }
                }
            }

            string szParam = string.Empty;
            foreach (string key in qDic.Keys)
            {
                szParam += key + "_";
            }
            if (szParam.Length > 0 && szParam.EndsWith("_"))
            {
                szRuleName += " " + szParam.Remove(szParam.Length - 1);
            }
            return szRuleName;
        }

        #endregion

    }
}
