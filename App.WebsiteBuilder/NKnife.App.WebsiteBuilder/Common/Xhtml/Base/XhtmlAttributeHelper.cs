using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml;
using Jeelu;

namespace Jeelu
{
    public static class AttributeHelper
    {
        static Dictionary<string, Dictionary<string, string>> _dic = new Dictionary<string, Dictionary<string, string>>();

        static Dictionary<string, string> GetAttributeGroup(string attributeName)
        {
            Dictionary<string, string> enumStringDic;
            if (!_dic.TryGetValue(attributeName, out enumStringDic))
            {
                enumStringDic = new Dictionary<string, string>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("");//TODO:XhtmlRes.attEnum);
                XmlNode node = doc.SelectSingleNode(string.Format("//{0}", attributeName));
                Debug.Assert(node != null);
                foreach (XmlNode item in node.ChildNodes)
                {
                    if (item.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement ele = (XmlElement)item;
                    enumStringDic.Add(ele.GetAttribute("k"), ele.InnerText);
                }
                _dic.Add(attributeName, enumStringDic);
            }
            return enumStringDic;
        }

        public static string GetValue(string attributeName, string enumKey)
        {
            Dictionary<string, string> dic = GetAttributeGroup(attributeName);

            Debug.Assert(dic != null, "Dictionary is Null!");
            Debug.Assert(!string.IsNullOrEmpty(dic[enumKey]));

            return dic[enumKey];
        }
    }
}
