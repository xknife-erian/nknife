using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace NKnife.Draws.Common
{
    public class Rectangles
    {
        public static void ParseXmlElement(ref RectangleF rectangle, XmlElement element)
        {
            rectangle.X = float.Parse(element.GetAttribute("left"));
            rectangle.Y = float.Parse(element.GetAttribute("top"));
            rectangle.Width = float.Parse(element.GetAttribute("width"));
            rectangle.Height = float.Parse(element.GetAttribute("height"));
        }

        public static void ParseXmlElement(ref Rectangle rectangle, XmlElement element)
        {
            rectangle.X = int.Parse(element.GetAttribute("left"));
            rectangle.Y = int.Parse(element.GetAttribute("top"));
            rectangle.Width = int.Parse(element.GetAttribute("width"));
            rectangle.Height = int.Parse(element.GetAttribute("height"));
        }
    }
}
