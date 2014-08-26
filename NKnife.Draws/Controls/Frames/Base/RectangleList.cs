using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using NKnife.Draws.Common;
using NKnife.Interface;

namespace NKnife.Draws.Controls.Frames.Base
{
    /// <summary>
    /// 当在图板上设计(绘制矩形)时,绘制的矩形的集合
    /// </summary>
    public class RectangleList : List<RectangleF>, IXml
    {
        public RectangleList()
        {
            Selected = new List<RectangleF>();
        }

        /// <summary>
        /// 选中的矩形
        /// </summary>
        public List<RectangleF> Selected { get; set; }

        /// <summary>
        /// 鼠标正悬停在这个矩形的上方
        /// </summary>
        public RectangleF Hover { get; set; }

        public XmlElement ToXml(XmlDocument parent)
        {
            var root = parent.CreateElement("Rectangle_List");
            foreach (var rect in this)
            {
                var ele = parent.CreateElement("Rectangle");
                rect.FillXmlElement(ele);
                root.AppendChild(ele);
            }
            return root;
        }

        public void Parse(XmlElement element)
        {
            foreach (var node in element.ChildNodes)
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
    }
}
