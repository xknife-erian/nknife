using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Resources;
using Jeelu.CssLib;

namespace Jeelu
{
    public static class CssResources
    {
        private static Dictionary<string, CssResourceList> _dic = new Dictionary<string, CssResourceList>();
        
        /// <summary>
        /// 获取Css内容集合。key是属性名，如display；value是此属性的值的集合(CssResourceList型)
        /// </summary>
        public static Dictionary<string, CssResourceList> Resources
        {
            get 
            {
                return _dic; 
            }
        }

        /// <summary>
        /// 检测当前的值是否非法
        /// </summary>
        /// <param name="cssName">Css属性的名称</param>
        /// <param name="curText">当前的值</param>
        /// <param name="canUsingNum">是否可以用数字值</param>
        /// <returns>非法返回true，合法返回false</returns>
        public static bool CheckValue(string cssName, string curText,bool canUsingNum)
        {
            if (string.IsNullOrEmpty(curText))
            {
                return false;
            }
            CssResourceList list = CssResources.Resources[cssName];
            if (list.HasValue(curText))
            {
                curText = list.GetValue(curText);
                return false;
            }
            if (canUsingNum)
            {
                double d = 0;
                return !Double.TryParse(curText, out d);
            }
            return true;
        }

        static public void Initialize()
        {
            SetDic();
        }

        private static void SetDic()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(CssLibResource.CssResources);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("item");
            foreach (XmlNode node in nodes)
            {
                XmlElement ele = (XmlElement)node;
                CssResourceList list = new CssResourceList();
                XmlNodeList subNodes = ele.SelectNodes("subitem");
                foreach (XmlNode sNode in subNodes)
                {
                    XmlElement subEle = (XmlElement)sNode;
                    list.Add(subEle.GetAttribute("value"), subEle.GetAttribute("text"));
                }
                _dic[ele.GetAttribute("name")] = list;
            }
        }
    }
}
