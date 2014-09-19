using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    static public partial class SoftwareOption
    {
        static XmlDocument _softOptionXml;

        /// <summary>
        /// 加载选项的数据文件
        /// </summary>
        static public void Load()
        {
            if (_softOptionXml == null)
            {
                _softOptionXml = new XmlDocument();
                CheckAndLoadFile();
            }
        }
        /// <summary>
        /// 重新载入选项配置文件
        /// </summary>
        public static void ReLoad()
        {
            CheckAndLoadFile();
        }

        private static void CheckAndLoadFile()
        {
            string fileName = PathService.Config_GlobalSetting;
            bool isFirst = false;
            if (!File.Exists(fileName))//如果用户的配置文件不存在，将默认的配置文件拷入
            {
                File.Copy(PathService.CL_SoftOption, fileName, true);
                File.SetAttributes(fileName, FileAttributes.Normal);
                isFirst = true;
            }
            _softOptionXml.Load(fileName);
            if (isFirst)//保存配置文件重置后的当前时间
            {
                XmlElement ele = _softOptionXml.DocumentElement;
                ele.SetAttribute("saveTime", DateTime.Now.ToString());
                _softOptionXml.Save(fileName);
            }
        }

        /// <summary>
        /// 返回选项数据文件里Items元素
        /// </summary>
        /// <param name="itemName">items元素的名称</param>
        /// <returns></returns>
        static XmlElement GetItemElement(string itemsName)
        {
            XmlElement ele = (XmlElement)_softOptionXml.SelectSingleNode(string.Format(@"options/option/items[@name='{0}']", itemsName));
            Debug.Assert(ele != null, itemsName + ": Node isn't Find!");
            return ele;
        }

        /// <summary>
        /// 取得节点的值
        /// </summary>
        /// <param name="itemsEle">当前元素的父级元素</param>
        /// <param name="name">当前元素名称</param>
        /// <returns></returns>
        static public string GetNodeValue(XmlElement itemsEle, string name)
        {
            string nodeName = name.Trim();
            XmlNode element = itemsEle.SelectSingleNode(string.Format("subItem[@name='{0}']",nodeName ));

#if DEBUG
            if (element == null)
            {
                XmlElement ele = (XmlElement)element;
                ele = _softOptionXml.CreateElement("subItem");
                ele.SetAttribute("name", nodeName);
                itemsEle.AppendChild(ele);
                _softOptionXml.Save(PathService.Config_GlobalSetting);
            }
            element = itemsEle.SelectSingleNode(string.Format("subItem[@name='{0}']", nodeName));
#endif

            Debug.Assert(element != null, nodeName + " : SoftOptionService取得节点的值时没有找到节点！");
            return element.InnerText;
        }

        /// <summary>
        /// 设置节点的值
        /// </summary>
        /// <param name="itemsEle">当前元素的父级元素</param>
        /// <param name="name">当前元素名称</param>
        /// <param name="value">元素的值</param>
        static void SetNodeValue(XmlElement itemsEle, string name, string value)
        {
            string nodeName = name.Trim();
            XmlNode node = itemsEle.SelectSingleNode(string.Format("subItem[@name='{0}']", nodeName));
            if (node == null)
            {
                XmlElement newElement = _softOptionXml.CreateElement("subItem");
                newElement.SetAttribute("name", nodeName);
                itemsEle.AppendChild(newElement);
                node = (XmlNode)newElement;
            }
            node.InnerText = "";
            XmlCDataSection data = _softOptionXml.CreateCDataSection(value);
            node.AppendChild(data);
        }

        /// <summary>
        /// 获取元素值
        /// </summary>
        /// <param name="itemsEle">当前元素的父级</param>
        /// <param name="nodeName">当前元素的节点名</param>
        /// <param name="returnType">当前元素值的类型</param>
        /// <returns></returns>
        static Object GetValue(XmlElement itemsEle, string nodeName, Type returnType)
        {
            string nodeValue = GetNodeValue(itemsEle, nodeName);

          //  Debug.Assert(nodeValue != null);

            if (returnType == typeof(Color))
            {
                Debug.Assert(nodeValue != "","返回值不能为空");
                return Color.FromArgb(int.Parse(nodeValue));
            }
            if (returnType == typeof(Boolean))
            {
                return bool.Parse(nodeValue);
            }
            if (returnType == typeof(int))
            {
                return Utility.Convert.StringToInt(nodeValue);
            }
            if (returnType == typeof(float))
            {
                return float.Parse(nodeValue);
            }
            if (returnType == typeof(long))
            {
                return long.Parse(nodeValue);
            }
            if (returnType == typeof(Decimal))
            {
                return decimal.Parse(nodeValue);
            }
            if (returnType == typeof(DateTime))
            {
                return DateTime.Parse(nodeValue);
            }
            if (returnType == typeof(String))
            {
                return nodeValue;
            }
            if (returnType == typeof(EnumRulerScaleStyle))
            {
                return Enum.Parse(typeof(EnumRulerScaleStyle), nodeValue);
            }
            if (returnType == typeof (EnumDrawType ))
            {
                return Enum.Parse (typeof (EnumDrawType ),nodeValue );
            }
            if (returnType == typeof(DesignerOpenType))
            {
                return Enum.Parse (typeof (DesignerOpenType),nodeValue );
            }
            if (returnType == typeof (BorderStyle))
            {
                return Enum.Parse(typeof (BorderStyle),nodeValue);
            }
            Debug.Fail(returnType + "类型不存在!");
            return null;
        }

        /// <summary>
        /// 设置元素值
        /// </summary>
        /// <param name="itemsEle">当前元素的父级</param>
        /// <param name="nodeName">当前元素名称</param>
        /// <param name="nodeValue">重新要赋给元素的值</param>
        static void SetValue(XmlElement itemsEle, string nodeName, Object nodeValue)
        {
            string nodeVal = "";
            Type valueType = nodeValue.GetType();

            if (valueType == typeof(Color))
            {
                Color c = (Color)nodeValue;
                nodeVal = c.ToArgb().ToString();
            }
            else
            {
                nodeVal = nodeValue.ToString();
            }

            SetNodeValue(itemsEle, nodeName, nodeVal);
        }

        /// <summary>
        /// 外部可直接使用的选项配置文件XmlDocument的引用对象
        /// </summary>
        public static XmlDocument SoftOptionXmlDocument
        {
            get { return _softOptionXml; }
        }

        public static void Save()
        {
            SoftOptionXmlDocument.Save(PathService.Config_GlobalSetting);

            if (Saved != null)
            {
                Saved(null, EventArgs.Empty);
            }
        }

        public static event EventHandler Saved;
    }
}