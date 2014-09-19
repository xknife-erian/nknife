using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jeelu.Billboard
{
    public class ExtractRule
    { 
        #region 字段或属性

        /// <summary>
        /// 规则的头部
        /// </summary>
        public virtual string Start { get; set; }

        /// <summary>
        /// 规则的尾部
        /// </summary>
        public virtual string End { get; set; }

        /// <summary>
        /// 格式字符串
        /// </summary>
        public virtual string Format { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 正文规则的状态
        /// </summary>
        public WebRuleState ExtractRuleState { get; set; }

        /// <summary>
        /// 状态是否改变
        /// </summary>
        public virtual bool IsStateChange { get; set; }

        /// <summary>
        /// 正文规则的ID
        /// </summary>
        public virtual long ID { get; set; }
        #endregion

        #region 构造函数

        public ExtractRule(string name,string start, string end, string format)
        {
            this.Name = name;
            this.Start = start;
            this.End = end;
            this.Format = format;
        }

        #endregion       

        #region 外部方法

        public virtual string Extract(string code)
        {
            Regex regex = new Regex(this.Start + "(?<content>.*)" + this.End, RegexOptions.Singleline);
            return regex.Replace(code, ReplaceCallback);
        }
        private string ReplaceCallback(Match m)
        {
            return m.Groups["content"].Value;
        }

        #endregion 
    }
}
