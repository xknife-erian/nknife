using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public partial class XhtmlTags
    {
        /// <summary>
        /// Xhtml文件的链接节点
        /// </summary>
        public class A : XhtmlTagElement 
        { 
            internal A() { }

            #region 字段和属性

            public string Href
            {
                get { return this.Attributes["href"].Value; }
                set { this.SetAttribute("href",value); }
            }

            public string Title
            {
                get { return this.Attributes["title"].Value; }
                set { this.SetAttribute("title",value); }
            }

            public string Target
            {
                get { return this.Attributes["target"].Value; }
                set { this.SetAttribute("target",value); }
            }

            public string TabIndex
            {
                get { return this.Attributes["tabindex"].Value; }
                set { this.SetAttribute("tabindex", value); }
            }

            public string Accesskey
            {
                get { return this.Attributes["accesskey"].Value; }
                set { this.SetAttribute("accesskey", value); }
            }

            public string LinkText
            {
                get { return this.Attributes["text"].Value; }
                set { this.SetAttribute("text",value);}
            }

            #endregion

            //<a href="www.jeelu.com" tabindex="2" title="this is a Title" accesskey="5" target="_blank">珍珠玛瑙</a>

            /// <summary>
            /// 创链接节点的所有属性
            /// </summary>
            /// <param name="href">链接地址，为空时，使用"#"，即为空链接</param>
            /// <param name="title">链接标题</param>
            /// <param name="target">打开链接的目标</param>
            /// <param name="tabindex">tab键的索引</param>
            /// <param name="accesskey">激活键</param>
            public void Builder(string href, string title, Xhtml.Target target, int tabindex, char accesskey)
            {
                if (string.IsNullOrEmpty(href))
                {
                    href = "#";
                }
                this.Attributes.Add(new XhtmlAtts.Href(href));
                this.Attributes.Add(new XhtmlAtts.Title(title));
                this.Attributes.Add(new XhtmlAtts.Target(target));
                this.Attributes.Add(new XhtmlAtts.Tabindex(tabindex.ToString()));
                this.Attributes.Add(new XhtmlAtts.Accesskey(accesskey.ToString()));
            }
        }
    }
}
