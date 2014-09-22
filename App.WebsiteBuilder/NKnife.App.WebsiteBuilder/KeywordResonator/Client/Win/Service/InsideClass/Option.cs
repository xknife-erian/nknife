using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Jeelu.KeywordResonator.Client
{
    internal static partial class Service
    {
        /// <summary>
        /// 应用程序的Option静态类
        /// </summary>
        internal static partial class Option
        {
            private static XmlDocument _OptionDocument;
            private static Dictionary<string, string> _OptionDictionary;
            private static string _OptionFile;

            /// <summary>
            /// 应用程序的Option的初始化。一般由应用程序在启动时初始化。
            /// </summary>
            internal static void InitializeOption(string file)
            {
                _OptionFile = file;
                _OptionDictionary = new Dictionary<string, string>();
                if (!File.Exists(file))
                {
                    ReOptionFile();
                }
                _OptionDocument = new XmlDocument();
            LoadValueToDic:
                try
                {
                    LoadValueToDic();
                }
                catch (XmlException ex)
                {
                    Debug.Fail(ex.Message);
                    ReOptionFile();
                    goto LoadValueToDic;
                }
            }

            /// <summary>
            /// 从Option的Xml文件中读取值置入Dictionary中
            /// </summary>
            private static void LoadValueToDic()
            {
                _OptionDocument.Load(_OptionFile);
                XmlElement docELe = _OptionDocument.DocumentElement;
                foreach (XmlNode node in docELe.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement optionEle = (XmlElement)node;
                    string name = optionEle.GetAttribute("name");
                    string value = optionEle.InnerText;
                    _OptionDictionary.Add(name, value);
                }
            }

            /// <summary>
            /// 根据Option的Name获取Option值。
            /// 使用其值时请判断它是否值为空。
            /// </summary>
            /// <param name="optionName">Option的Name</param>
            internal static string GetValue(string optionName)
            {
                optionName = optionName.ToLower();
                string value = null;
                if (!_OptionDictionary.TryGetValue(optionName, out value))
                {
                    ///如果不能得到值，一般来讲是Option的Xml文件中可能没有这个节点
                    XmlElement ele = _OptionDocument.DocumentElement;
                    XmlElement newEle = _OptionDocument.CreateElement("option");
                    newEle.SetAttribute("name", optionName);
                    ele.AppendChild(newEle);
                    _OptionDocument.Save(_OptionFile);

                    _OptionDictionary.Add(optionName, "");///将该选项加入到选项的Dictionary中去
                }
                return value;
            }

            /// <summary>
            /// 根据Option的Name设置Option值
            /// </summary>
            /// <param name="optionName">Option的名字</param>
            /// <param name="value">正在设置的Option的值</param>
            internal static void SetValue(string optionName, string value)
            {
                optionName = optionName.ToLower();
                if (_OptionDictionary.ContainsKey(optionName))
                {
                    _OptionDictionary[optionName] = value;
                    string xpath = string.Format(@".//option[@name='{0}']", optionName);
                    XmlNode node = _OptionDocument.DocumentElement.SelectSingleNode(xpath);
                    node.InnerText = value;
                }
                else///当Option的字典中没有这个Key时，
                {
                    ///如果不能得到值，一般来讲是Option的Xml文件中可能没有这个节点
                    XmlElement ele = _OptionDocument.CreateElement("option");
                    ele.SetAttribute("name", optionName);
                    ele.InnerText = value;
                    _OptionDocument.DocumentElement.AppendChild(ele);

                    _OptionDictionary.Add(optionName, value);///将该选项加入到选项的Dictionary中去
                }
                _OptionDocument.Save(_OptionFile);

            }

            /// <summary>
            /// OptionFile可能损坏。创建一份新的文件。
            /// </summary>
            private static void ReOptionFile()
            {
                if (File.Exists(_OptionFile))
                {
                    File.Delete(_OptionFile);///因虽文件存在，但xml读取出错，故将其删除。
                }
                FileStream fs = File.Open(_OptionFile, FileMode.Create);
                XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);
                xtw.Formatting = Formatting.Indented;
                xtw.WriteStartDocument();
                xtw.WriteElementString("options", "");
                xtw.Flush();
                xtw.Close();
                fs.Close();
            }

        }
    }
}