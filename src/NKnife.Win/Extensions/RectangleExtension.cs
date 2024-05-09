using System.Drawing;
using System.Globalization;
using System.Xml;

// ReSharper disable once CheckNamespace
namespace System.Drawing
{
    public static class RectangleExtension
    {
        public static void FillXmlElement(this RectangleF rectangle, XmlElement element)
        {
            element.SetAttribute("left", rectangle.X.ToString(CultureInfo.InvariantCulture));
            element.SetAttribute("top", rectangle.Y.ToString(CultureInfo.InvariantCulture));
            element.SetAttribute("width", rectangle.Width.ToString(CultureInfo.InvariantCulture));
            element.SetAttribute("height", rectangle.Height.ToString(CultureInfo.InvariantCulture));
        }

        public static void FillXmlElement(this Rectangle rectangle, XmlElement element)
        {
            element.SetAttribute("left", rectangle.X.ToString(CultureInfo.InvariantCulture));
            element.SetAttribute("top", rectangle.Y.ToString(CultureInfo.InvariantCulture));
            element.SetAttribute("width", rectangle.Width.ToString(CultureInfo.InvariantCulture));
            element.SetAttribute("height", rectangle.Height.ToString(CultureInfo.InvariantCulture));
        }
    }
}
