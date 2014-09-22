using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;

namespace Jeelu
{
    /// <summary>
    /// Xhtml节点的属性，类似：title="This is a Image!"。
    /// design by Lukan, 2008年6月10日0时58分
    /// </summary>
    public class XhtmlAttribute
    {
        /// <summary>
        /// Xhtml节点的属性，类似：title="This is a Image!"。
        /// </summary>
        /// <param name="key">键，属性名。与大小写无关，全部强制转换成小写字母</param>
        /// <param name="value">值，属性值</param>
        internal XhtmlAttribute(string key, string value)
        {
            this.Key = key.ToLower();//CultureInfo.CurrentCulture);
            this.Value = value;
        }

        /// <summary>
        /// 属性名
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Jeelu.com重写。
        /// </summary>
        public override bool Equals(object obj)
        {
            //return base.Equals(obj);
            XhtmlAttribute att = (XhtmlAttribute)obj;
            bool boolKey = false;
            bool boolValue = false;
            if (this.Key == att.Key)
            {
                boolKey = true;
            }
            if (this.Value == att.Value)
            {
                boolValue = true;
            }
            return boolKey & boolValue;
        }
        /// <summary>
        /// Jeelu.com重写。
        /// </summary>
        public override int GetHashCode()
        {
            return this.Key.GetHashCode() ^ this.Value.GetHashCode();
        }
        /// <summary>
        /// Jeelu.com重写。生成真实的做为Xhtml中的属性的字符串格式。
        /// 属性名与大小写无关，全部转换成小写字母
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Key).Append("=\"").Append(this.Value).Append("\"");
            return sb.ToString();
        }
    }
}
