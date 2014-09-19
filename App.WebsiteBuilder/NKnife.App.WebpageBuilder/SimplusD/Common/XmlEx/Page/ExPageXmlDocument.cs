using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 扩展页面文件
    /// </summary>
    public class ExPageXmlDocument : PageXmlDocument
    {
        public ExPageXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {
        }
        void abc()
        {
            
        }
    }

    public class PageItem
    {
        public PageItem()
        {
            
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public string ValueFormatting { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }

        public virtual string ToXhtml()
        {
            return null;
        }
    }

    public class PageItemCollection : RichList<PageItem>
    {

    }
}
