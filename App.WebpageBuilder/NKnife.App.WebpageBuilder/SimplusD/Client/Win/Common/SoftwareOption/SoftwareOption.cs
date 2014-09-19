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
        /// ����ѡ��������ļ�
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
        /// ��������ѡ�������ļ�
        /// </summary>
        public static void ReLoad()
        {
            CheckAndLoadFile();
        }

        private static void CheckAndLoadFile()
        {
            string fileName = PathService.Config_GlobalSetting;
            bool isFirst = false;
            if (!File.Exists(fileName))//����û��������ļ������ڣ���Ĭ�ϵ������ļ�����
            {
                File.Copy(PathService.CL_SoftOption, fileName, true);
                File.SetAttributes(fileName, FileAttributes.Normal);
                isFirst = true;
            }
            _softOptionXml.Load(fileName);
            if (isFirst)//���������ļ����ú�ĵ�ǰʱ��
            {
                XmlElement ele = _softOptionXml.DocumentElement;
                ele.SetAttribute("saveTime", DateTime.Now.ToString());
                _softOptionXml.Save(fileName);
            }
        }

        /// <summary>
        /// ����ѡ�������ļ���ItemsԪ��
        /// </summary>
        /// <param name="itemName">itemsԪ�ص�����</param>
        /// <returns></returns>
        static XmlElement GetItemElement(string itemsName)
        {
            XmlElement ele = (XmlElement)_softOptionXml.SelectSingleNode(string.Format(@"options/option/items[@name='{0}']", itemsName));
            Debug.Assert(ele != null, itemsName + ": Node isn't Find!");
            return ele;
        }

        /// <summary>
        /// ȡ�ýڵ��ֵ
        /// </summary>
        /// <param name="itemsEle">��ǰԪ�صĸ���Ԫ��</param>
        /// <param name="name">��ǰԪ������</param>
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

            Debug.Assert(element != null, nodeName + " : SoftOptionServiceȡ�ýڵ��ֵʱû���ҵ��ڵ㣡");
            return element.InnerText;
        }

        /// <summary>
        /// ���ýڵ��ֵ
        /// </summary>
        /// <param name="itemsEle">��ǰԪ�صĸ���Ԫ��</param>
        /// <param name="name">��ǰԪ������</param>
        /// <param name="value">Ԫ�ص�ֵ</param>
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
        /// ��ȡԪ��ֵ
        /// </summary>
        /// <param name="itemsEle">��ǰԪ�صĸ���</param>
        /// <param name="nodeName">��ǰԪ�صĽڵ���</param>
        /// <param name="returnType">��ǰԪ��ֵ������</param>
        /// <returns></returns>
        static Object GetValue(XmlElement itemsEle, string nodeName, Type returnType)
        {
            string nodeValue = GetNodeValue(itemsEle, nodeName);

          //  Debug.Assert(nodeValue != null);

            if (returnType == typeof(Color))
            {
                Debug.Assert(nodeValue != "","����ֵ����Ϊ��");
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
            Debug.Fail(returnType + "���Ͳ�����!");
            return null;
        }

        /// <summary>
        /// ����Ԫ��ֵ
        /// </summary>
        /// <param name="itemsEle">��ǰԪ�صĸ���</param>
        /// <param name="nodeName">��ǰԪ������</param>
        /// <param name="nodeValue">����Ҫ����Ԫ�ص�ֵ</param>
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
        /// �ⲿ��ֱ��ʹ�õ�ѡ�������ļ�XmlDocument�����ö���
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