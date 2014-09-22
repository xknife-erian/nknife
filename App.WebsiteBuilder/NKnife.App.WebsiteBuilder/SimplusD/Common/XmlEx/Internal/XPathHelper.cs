using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.SimplusD
{
    internal static class XPathHelper
    {
        static public XmlElement GetElementById(XmlNode node, string id,string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(id));

            //XPath»Á£∫(.//channel[@id='1234'])[1]
            return (XmlElement)node.SelectSingleNode(string.Format(@"(.//{1}[@id='{0}'])[1]", id, name));
        }

        static public XmlElement GetElementByName(XmlNode node, string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            //XPath»Á£∫(.//channel)[1]
            return (XmlElement)node.SelectSingleNode(string.Format(@"(.//{0})[1]", name));
        }

        static public XmlNodeList GetChildsByName(XmlNode node,string name)
        {
            return GetChildsByName(node, name, false);
        }
        static public XmlNodeList GetChildsByName(XmlNode node, string name,bool isGetDeleted)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            if (isGetDeleted)
            {
                return node.SelectNodes(string.Format(@"{0}", name));
            }
            else
            {
                return node.SelectNodes(string.Format(@"{0}[@IsDeleted='False']", name));
            }
        }
    }
}
