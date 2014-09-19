using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public partial class XhtmlTags
    {
        public class Img : XhtmlTagElement
        {
            internal Img() { }

            //<IMG style="WIDTH: 500px; HEIGHT: 667px" alt="" hspace=11 
            //src="${srs_d43bab22b0a2469287257db914d95dd4}" align=baseline vspace=22 border=33 name=pic>

            // modify by fenggy 2008-07-1 11:03 增加属性
            public void Builder(CssSection style, string alt, string hspace, string src, Xhtml.Align align, string vspace, string border, string name)
            {
                this.Attributes.Add(new XhtmlAtts.Style(style));
                this.Attributes.Add(new XhtmlAtts.Alt(alt));
                this.Attributes.Add(new XhtmlAtts.Hspace(hspace));
                this.Attributes.Add(new XhtmlAtts.Src(src));
                this.Attributes.Add(new XhtmlAtts.Align(align));
                this.Attributes.Add(new XhtmlAtts.Vspace(vspace));
                this.Attributes.Add(new XhtmlAtts.Border(border));
                this.Attributes.Add(new XhtmlAtts.Name(name));
            }

            public string Src
            {
                get
                {
                    return this.Attributes["src"].Value;
                }
                set
                {
                    this.SetAttribute("src", value);
                }
            }

            public string Alt
            {
                get
                {
                    return this.Attributes["alt"].Value;
                }
                set
                {
                    this.SetAttribute("alt", value);
                }
            }

            public string Width
            {
                get
                {
                    string styleStr = this.GetAttribute("style");
                    CssSection section = CssSection.Parse(styleStr);
                    return section.Properties["width"].ToString();
                }
            }

            public string Height
            {
                get
                {
                    string styleStr = this.GetAttribute("style");
                    CssSection section = CssSection.Parse(styleStr);
                    return section.Properties["height"].ToString();
                }

            }
            public void SetStyle(string width, string height)
            {
                string styleStr = this.GetAttribute("style");
                CssSection section = CssSection.Parse(styleStr);
                section.Properties["width"] = width;
                section.Properties["height"] = height;
                this.SetAttribute("style",section.ToString());
            }

            public string Align
            {
                get
                {
                    return this.Attributes["align"].Value;
                }
                set
                {
                    this.SetAttribute("align", value);
                }
            }

            public string HSpace
            {
                get
                {
                    return this.Attributes["hspace"].Value;
                }
                set
                {
                    this.SetAttribute("hspace", value);
                }
            }

            public string VSpace
            {
                get
                {
                    return this.Attributes["vspace"].Value;
                }
                set
                {
                    this.SetAttribute("vspace", value);
                }
            }

            public string Border
            {
                get
                {
                    return this.Attributes["border"].Value;
                }
                set
                {
                    this.SetAttribute("border", value);
                }
            }
        }
    }
}
