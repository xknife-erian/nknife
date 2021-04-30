using System;

namespace NKnife.App.XPath
{
    public class DisplayForm
    {
        public static bool isVerbose = false;

        public static string GetAllElementsName(string elementName)
        {
            var str = "[";
            if (elementName.IndexOf(str, 0, elementName.Length) != -1)
            {
                return elementName.Remove(elementName.LastIndexOf(str),
                    elementName.Length - elementName.LastIndexOf(str));
            }
            return elementName;
        }

        public static string GetAttrName(string attrName)
        {
            if (isVerbose)
            {
                return (Verbose.attrText + attrName);
            }
            return ("@" + attrName);
        }

        public static string GetElementName(string prefix, string elementName)
        {
            if (isVerbose)
            {
                return (Verbose.childText + prefix + elementName);
            }
            return (prefix + elementName);
        }

        public static string GetPositionName(int Pos)
        {
            if (isVerbose)
            {
                return Verbose.GetPositionText(Pos);
            }
            return Convert.ToString(Pos);
        }

        public static string GetTextName()
        {
            if (isVerbose)
            {
                return (Verbose.childText + "text()");
            }
            return "text()";
        }
    }
}