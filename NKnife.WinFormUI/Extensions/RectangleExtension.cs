using System.Drawing;
using System.Xml;

namespace NKnife.Draws.Extensions
{
    public static class RectangleExtension
    {
        public static void FillXmlElement(this RectangleF rectangle, XmlElement element)
        {
            element.SetAttribute("left", rectangle.X.ToString());
            element.SetAttribute("top", rectangle.Y.ToString());
            element.SetAttribute("width", rectangle.Width.ToString());
            element.SetAttribute("height", rectangle.Height.ToString());
        }

        public static void FillXmlElement(this Rectangle rectangle, XmlElement element)
        {
            element.SetAttribute("left", rectangle.X.ToString());
            element.SetAttribute("top", rectangle.Y.ToString());
            element.SetAttribute("width", rectangle.Width.ToString());
            element.SetAttribute("height", rectangle.Height.ToString());
        }
    }
}
