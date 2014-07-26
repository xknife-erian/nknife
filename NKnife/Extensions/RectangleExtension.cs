using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Drawing
{
    public static class RectangleExtension
    {
        public static void FillXmlElement(this Rectangle rectangle, XmlElement element)
        {
            element.SetAttribute("left", rectangle.X.ToString());
            element.SetAttribute("top", rectangle.Y.ToString());
            element.SetAttribute("width", rectangle.Width.ToString());
            element.SetAttribute("height", rectangle.Height.ToString());
        }
    }
}
