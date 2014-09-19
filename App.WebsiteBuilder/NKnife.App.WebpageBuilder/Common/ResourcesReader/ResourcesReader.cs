using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System;

namespace Jeelu
{
    /// <summary>
    /// 针对Jeelu.Win中的BaseForm，BaseControl，BaseUserControl几类控件所
    /// 携带的资源（控件.Text值，其他字符串，图标，图像，光标，文件）的读
    /// 取静态类。
    /// DesignBy: Lukan
    /// DateTime：2008年5月30日5时3分
    /// </summary>
    public static class ResourcesReader
    {
        /// <summary>
        /// 对象的资源文件存放目录，下一层将还会有语言路径。
        /// </summary>
        static string XmlDirectory;// = Application.StartupPath;
        /// <summary>
        /// 对象的资源文件语言类型字符串，同时也是路径名（目录）。
        /// </summary>
        static string LanguageDirectory;// = "chs";
        /// <summary>
        /// 对象的资源文件的最终完整路径（目录）
        /// </summary>
        static string FullDirectory = string.Empty;

        #region Res: 所有的Dictionary定义，私有

        static Dictionary<string, Dictionary<string, string>> _controlTextResources = new Dictionary<string, Dictionary<string, string>>();
        static Dictionary<string, Dictionary<string, Icon>> _controlIconResources = new Dictionary<string, Dictionary<string, Icon>>();
        static Dictionary<string, Dictionary<string, Image>> _controlImageResources = new Dictionary<string, Dictionary<string, Image>>();

        static Dictionary<string, Dictionary<string, string>> _textResources = new Dictionary<string, Dictionary<string, string>>();
        static Dictionary<string, Dictionary<string, Icon>> _iconResources = new Dictionary<string, Dictionary<string, Icon>>();
        static Dictionary<string, Dictionary<string, Image>> _imageResources = new Dictionary<string, Dictionary<string, Image>>();
        static Dictionary<string, Dictionary<string, Cursor>> _cursorResources = new Dictionary<string, Dictionary<string, Cursor>>();
        static Dictionary<string, Dictionary<string, byte[]>> _fileStreamResources = new Dictionary<string, Dictionary<string, byte[]>>();

        /// 应用程序的公共资源
        static Dictionary<string, string> _textPublicResources = new Dictionary<string, string>();
        static Dictionary<string, Icon> _iconPublicResources = new Dictionary<string, Icon>();
        static Dictionary<string, Image> _imagePublicResources = new Dictionary<string, Image>();
        static Dictionary<string, Cursor> _cursorPublicResources = new Dictionary<string, Cursor>();
        static Dictionary<string, byte[]> _byteArrayPublicResources = new Dictionary<string, byte[]>();

        #endregion

        /// <summary>
        /// 初始化资源。一般在应用程序启动时调用。
        /// </summary>
        /// <param name="resourcesPath">资源目录。</param>
        /// <param name="languageDirectory">语言代码：chs,ths,en...。</param>
        /// <param name="type">类型名</param>
        static public void InitializeResources(string resourcesPath, string languageDirectory, Type type)
        {
            XmlDirectory = Path.Combine(Application.StartupPath, resourcesPath);
            LanguageDirectory = languageDirectory;

            string filepath = Path.Combine(XmlDirectory, LanguageDirectory);

            InitializeResources(filepath, type);
        }
        /// <summary>
        /// 初始化资源。一般在应用程序启动时调用。
        /// </summary>
        /// <param name="fullDirectory">资源目录完整名。</param>
        /// <param name="languageDirectory">语言代码：chs,ths,en...。</param>
        /// <param name="type">类型名</param>
        static public void InitializeResources(string fullDirectory, Type type)
        {
            FullDirectory = fullDirectory;

            if (type == null)
            {
                type = typeof(ResourcesReader);
            }

#if DEBUG       /// 当Debug状态时，如果配置文件不存在，则自动生成一个配置文件
            string filePath = Path.Combine(fullDirectory, type.FullName + ".formXml");
            if (!File.Exists(filePath))
            {
                ResourceXmlFile.PublicXmlCreator(type, filePath);
            }
#endif
            /// 初始化应用程序的公共资源
            _textPublicResources = TextReader(type);
            _iconPublicResources = IconReader(type);
            _imagePublicResources = ImageReader(type);
            _cursorPublicResources = CursorReader(type);
            _byteArrayPublicResources = ByteArrayReader(type);
        }

        #region GetHelper

        /// <summary>
        /// 调用文本资源
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="ctr">控件类型，如果为null，将调用应用程序公共资源</param>
        public static string GetText(string key, object obj)
        {
            if (obj == null)
            {
                if (_textPublicResources.ContainsKey(key))
                {
                    return _textPublicResources[key];
                }
                else
                {
                    return key;
                }
            }
            Dictionary<string, string> dic = TextReader(obj);
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return key;
            }
        }
        /// <summary>
        /// 调用图标资源
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="ctr">控件类型，如果为null，将调用应用程序公共资源</param>
        public static Icon GetIcon(string key, object obj)
        {
            if (obj == null)
            {
                return _iconPublicResources[key];
            }
            Dictionary<string, Icon> dic = IconReader(obj);
            return dic[key];
        }
        /// <summary>
        /// 调用图像资源
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="ctr">控件类型，如果为null，将调用应用程序公共资源</param>
        public static Image GetImage(string key, object obj)
        {
            if (obj == null)
            {
                return _imagePublicResources[key];
            }
            Dictionary<string, Image> dic = ImageReader(obj);
            return dic[key];
        }
        /// <summary>
        /// 调用光标资源
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="ctr">控件类型，如果为null，将调用应用程序公共资源</param>
        public static Cursor GetCursor(string key, object obj)
        {
            if (obj == null)
            {
                return _cursorPublicResources[key];
            }
            Dictionary<string, Cursor> dic = CursorReader(obj);
            return dic[key];
        }
        /// <summary>
        /// 调用byte[]资源，存入资源文件时可能是任何类型的文件
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="ctr">控件类型，如果为null，将调用应用程序公共资源</param>
        public static byte[] GetBytes(string key, object obj)
        {
            if (obj == null)
            {
                return _byteArrayPublicResources[key];
            }
            Dictionary<string, byte[]> dic = ByteArrayReader(obj);
            return dic[key];
        }

        #endregion

        /// <summary>
        /// 获取根据控件类型获取资源Dictionary。
        /// </summary>
        /// <param name="ctr">控件对象</param>
        /// <param name="textRes">文本资源Dictionary</param>
        /// <param name="iconRes">图标资源Dictionary</param>
        /// <param name="imageRes">图像资源Dictionary</param>
        static void ReaderForControl(Control ctr,
                                        out Dictionary<string, string> textRes,
                                        out Dictionary<string, Icon> iconRes,
                                        out Dictionary<string, Image> imageRes)
        {
            //键值：即为控件的类型的全名
            string keyForType = ctr.GetType().FullName;
            //资源文件存储的路径
            string filePath = Path.Combine(FullDirectory, keyForType + ".formXml");

            //从整个应用程序的总的Dictionary中获取该类型的Dictionary，如果不存在，则读取资源
            //文件生成该Dictionary并存入总的Dictionary
            if (!_controlTextResources.TryGetValue(keyForType, out textRes))
            {
                textRes = new Dictionary<string, string>();
                iconRes = new Dictionary<string, Icon>();
                imageRes = new Dictionary<string, Image>();
#if DEBUG       /// 当Debug状态时，如果配置文件不存在，则自动生成一个配置文件
                if (!File.Exists(filePath))
                {
                    ResourceXmlFile.XmlCreator(ctr, filePath);
                }
#endif
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                #region 遍历XmlNode，将关于Control的配置存储在静态Dictionary中
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    if (node.Name == "Control" || node.Name == "ToolStripItem")
                    {
                        XmlNode textNode = node.SelectSingleNode("Text");
                        if (textNode != null)
                        {
                            string str = textNode.InnerText;
                            if (!string.IsNullOrEmpty(str))
                            {
                                textRes.Add(node.Attributes["Name"].Value, str);
                            }
                        }
                        XmlNode iconNode = node.SelectSingleNode("Icon");
                        if (iconNode != null)
                        {
                            string str = iconNode.InnerText;
                            if (!string.IsNullOrEmpty(str))
                            {
                                Icon icon = Jeelu.Utility.Convert.Base64ToIcon(str);
                                iconRes.Add(node.Attributes["Name"].Value, icon);
                            }
                        }
                        XmlNode imageNode = node.SelectSingleNode("Image");
                        if (imageNode != null)
                        {
                            string str = imageNode.InnerText;
                            if (!string.IsNullOrEmpty(str))
                            {
                                Image image = Jeelu.Utility.Convert.Base64ToImage(str);
                                imageRes.Add(node.Attributes["Name"].Value, image);
                            }
                        }
                    }
                }
                #endregion

                _controlTextResources.Add(keyForType, textRes);
                _controlImageResources.Add(keyForType, imageRes);
                _controlIconResources.Add(keyForType, iconRes);
            }
            else
            {
                iconRes = _controlIconResources[keyForType];
                imageRes = _controlImageResources[keyForType];
            }
        }

        /// <summary>
        /// 从应用程序总Dictionary中读取控件类型的非.Text类型的文本资源
        /// </summary>
        /// <param name="ctr">控件类型</param>
        static Dictionary<string, string> TextReader(object obj)
        {
            string keyType;
            string filePath;
            GetNameAndPath(obj, out keyType, out filePath);

            Dictionary<string, string> textRes;
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, string>();
            }
            if (!_textResources.TryGetValue(keyType, out textRes))
            {
                textRes = new Dictionary<string, string>();
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("Text"))
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement element = (XmlElement)node;
                    string keyStr = element.GetAttribute("Key");
                    string value = element.InnerText;
                    textRes.Add(keyStr, value);
                }
                _textResources.Add(keyType, textRes);
            }
            return textRes;
        }

        /// <summary>
        /// 从应用程序总Dictionary中读取控件类型的非.Icon类型的图标资源
        /// </summary>
        /// <param name="ctr">控件类型</param>
        static Dictionary<string, Icon> IconReader(object obj)
        {
            string keyType;
            string filePath;
            GetNameAndPath(obj, out keyType, out filePath);

            Dictionary<string, Icon> iconRes;
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, Icon>();
            }
            if (!_iconResources.TryGetValue(keyType, out iconRes))
            {
                iconRes = new Dictionary<string, Icon>();
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("Icon"))
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement element = (XmlElement)node;
                    string keyStr = element.GetAttribute("Key");
                    Icon value = Jeelu.Utility.Convert.Base64ToIcon(element.InnerText);
                    iconRes.Add(keyStr, value);
                }
                _iconResources.Add(keyType, iconRes);
            }
            return iconRes;
        }

        /// <summary>
        /// 从应用程序总Dictionary中读取控件类型的非.Image类型的图像资源
        /// </summary>
        /// <param name="ctr">控件类型</param>
        static public Dictionary<string, Image> ImageReader(object obj)
        {
            string keyType;
            string filePath;
            GetNameAndPath(obj, out keyType, out filePath);

            Dictionary<string, Image> imageRes;
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, Image>();
            }
            if (!_imageResources.TryGetValue(keyType, out imageRes))
            {
                imageRes = new Dictionary<string, Image>();
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("Image"))
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement element = (XmlElement)node;
                    string keyStr = element.GetAttribute("Key");
                    Image value = Jeelu.Utility.Convert.Base64ToImage(element.InnerText);
                    imageRes.Add(keyStr, value);
                }
                _imageResources.Add(keyType, imageRes);
            }
            return imageRes;
        }

        /// <summary>
        /// 从应用程序总Dictionary中读取控件类型的光标资源
        /// </summary>
        /// <param name="ctr">控件类型</param>
        static Dictionary<string, Cursor> CursorReader(object obj)
        {
            string keyType;
            string filePath;
            GetNameAndPath(obj, out keyType, out filePath);

            Dictionary<string, Cursor> cursorRes;
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, Cursor>();
            }
            if (!_cursorResources.TryGetValue(keyType, out cursorRes))
            {
                cursorRes = new Dictionary<string, Cursor>();
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("Cursor"))
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement element = (XmlElement)node;
                    string keyStr = element.GetAttribute("Key");
                    Cursor value = Jeelu.Utility.Convert.Base64ToCursor(element.InnerText);
                    cursorRes.Add(keyStr, value);
                }
                _cursorResources.Add(keyType, cursorRes);
            }
            return cursorRes;
        }

        /// <summary>
        /// 从应用程序总Dictionary中读取控件类型的Stream资源
        /// </summary>
        /// <param name="ctr">控件类型</param>
        static Dictionary<string, byte[]> ByteArrayReader(object obj)
        {
            string keyType;
            string filePath;
            GetNameAndPath(obj, out keyType, out filePath);

            Dictionary<string, byte[]> byteArrayRes;
            if (!File.Exists(filePath))
            {
                return new Dictionary<string, byte[]>();
            }
            if (!_fileStreamResources.TryGetValue(keyType, out byteArrayRes))
            {
                byteArrayRes = new Dictionary<string, byte[]>();
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("FileStream"))
                {
                    if (node.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    XmlElement element = (XmlElement)node;
                    string keyStr = element.GetAttribute("Key");
                    byte[] value = Jeelu.Utility.Convert.Base64ToByteArray(element.InnerText);
                    byteArrayRes.Add(keyStr, value);
                }
                _fileStreamResources.Add(keyType, byteArrayRes);
            }
            return byteArrayRes;
        }

        private static void GetNameAndPath(object obj, out string keyType, out string filePath)
        {
            if (obj is Type)
            {
                keyType = ((Type)obj).FullName;
            }
            else
            {
                keyType = obj.GetType().FullName;
            }
            filePath = Path.Combine(FullDirectory, keyType + ".formXml");
        }

        /// <summary>
        /// 自动根据资源文件设置控件的“.Text,.Images,.Icon”属性
        /// </summary>
        static public void SetControlPropertyHelper(Control ctr)
        {
            Dictionary<string, string> textRes;  // = new Dictionary<string, string>();
            Dictionary<string, Icon> iconRes;    // = new Dictionary<string, Icon>();
            Dictionary<string, Image> imageRes;  // = new Dictionary<string, Image>();

            ReaderForControl(ctr, out textRes, out iconRes, out imageRes);

            SubSetControlPropertyHelper(ctr, textRes, iconRes, imageRes);
        }

        #region SetControlText方法用到的递归子方法(内部调用)

        static void SubSetControlPropertyHelper(Control ctr,
                            Dictionary<string, string> textRes,
                            Dictionary<string, Icon> iconRes,
                            Dictionary<string, Image> imageRes)
        {
            SubSetValue(ctr, textRes, iconRes, imageRes);
            foreach (Control subCtr in ctr.Controls)
            {
                if (subCtr is ToolStrip)
                {
                    SubSetToolStripPropertyHelper((ToolStrip)subCtr, textRes, iconRes, imageRes);
                    continue;
                }
                /// 当控件（含窗体）中的子控件有了ResReaderAttribute定制特性，且IsGetResource为False时，
                /// 不再为其中的进行下一步的操作。
                object[] objs = subCtr.GetType().GetCustomAttributes(typeof(ResReaderAttribute), false);
                if (objs.Length > 0 && !((ResReaderAttribute)objs[0]).IsGetResource)
                {
                    continue;
                }
                SubSetControlPropertyHelper((Control)subCtr, textRes, iconRes, imageRes);
            }
        }

        #region ToolStripProperty
        static void SubSetToolStripPropertyHelper(ToolStrip toolStrip, Dictionary<string, string> textRes, Dictionary<string, Icon> iconRes, Dictionary<string, Image> imageRes)
        {
            SubSetValue(toolStrip, textRes, iconRes, imageRes);
            foreach (ToolStripItem item in toolStrip.Items)
            {
                SubSetToolStripItemPropertyHelper(item, textRes, iconRes, imageRes);
            }
        }

        static void SubSetToolStripItemPropertyHelper(ToolStripItem item, Dictionary<string, string> textRes, Dictionary<string, Icon> iconRes, Dictionary<string, Image> imageRes)
        {
            SubSetValue(item, textRes, iconRes, imageRes);
            if (item is ToolStripDropDownItem)
            {
                foreach (ToolStripItem subItem in ((ToolStripDropDownItem)item).DropDownItems)
                {
                    SubSetToolStripItemPropertyHelper(subItem, textRes, iconRes, imageRes);
                }
            }
        }
        #endregion

        /// <summary>
        /// 为对象设置相应的资源
        /// </summary>
        static void SubSetValue(object obj, 
            Dictionary<string, string> textRes, 
            Dictionary<string, Icon> iconRes, 
            Dictionary<string, Image> imageRes)
        {
            string name = string.Empty;
            if (obj is Control)
            {
                name = ((Control)obj).Name;
            }
            else if (obj is ToolStripItem)//ToolStripItem没有继承自Control，单独处理
            {
                name = ((ToolStripItem)obj).Name;
            }
            else
            {
                Debug.Fail(obj.GetType().FullName + " is Error Type! - SimplusD,Designtime.");
            }

            if (textRes.ContainsKey(name))
            {
                if (obj is Control)
                {
                    /// 2008年6月19日17时6分，modify by lukan
                    /// 如果控件是TextBox,ComoBox,WebBrowser的话，不做他的Text值的改变
                    if (!(obj is TextBox || obj is ComboBox || obj is WebBrowser))
                    {
                        ((Control)obj).Text = textRes[name];
                    }
                }
                else if (obj is ToolStripItem)
                {
                    ((ToolStripItem)obj).Text = textRes[name];
                }
            }
            if (iconRes.ContainsKey(name))
            {
                PropertyInfo pinfo = obj.GetType().GetProperty("Icon");
                Debug.Assert(pinfo != null, obj.GetType().ToString() + " hasn't Icon property! - SimplusD,Designtime.");
                if (pinfo != null)
                {
                    pinfo.SetValue(obj, iconRes[name], null);
                }
            }
            if (imageRes.ContainsKey(name))
            {
                PropertyInfo pinfo = obj.GetType().GetProperty("Image");
                Debug.Assert(pinfo != null, obj.GetType().ToString() + " hasn't Image property! - SimplusD,Designtime.");
                if (pinfo != null)
                {
                    pinfo.SetValue(obj, imageRes[name], null);
                }
            }
        }

        #endregion

        /// <summary>
        /// 设置对象所对应的资源文件
        /// </summary>
        /// <param name="obj">将要设置的对象</param>
        static public void SetObjectResourcesHelper(object obj)
        {
            //键值：即为对象的类型的全名
            string keyForObject = obj.GetType().FullName;
            //资源文件存储的路径
            string filePath = Path.Combine(FullDirectory, keyForObject + ".formXml");
#if DEBUG   /// 当Debug状态时，如果配置文件不存在，则自动生成一个配置文件
            if (!File.Exists(filePath))
            {
                ResourceXmlFile.ObjectXmlCreator(obj, filePath);
            }
#endif
        }

    }


#if DEBUG

    /// <summary>
    /// 当BaseForm,BaseControl,BaseUserControl第一次Debug运行的时候，创建与其对应的资源文件。
    /// 当Debug时该类有效，Release时该类无效
    /// </summary>
    internal static class ResourceXmlFile
    {
        /// <summary>
        /// 创建应用程序公共的资源文件
        /// </summary>
        internal static void PublicXmlCreator(Type type, string filePath)
        {
            #region XmlTextWriter
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            FileStream fs = File.Open(filePath, FileMode.Create, FileAccess.Write); 
            XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);// (filePath, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Resources");
            xtw.WriteAttributeString("Type", type.FullName);
            xtw.WriteAttributeString("ObjectName", type.FullName);

            xtw.WriteEndElement();

            xtw.Close();
            fs.Close();
            fs.Dispose();
            #endregion
        }

        /// <summary>
        /// 创建Object的资源文件
        /// </summary>
        internal static void ObjectXmlCreator(object obj, string filePath)
        {
            #region XmlTextWriter
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            FileStream fs = File.Open(filePath, FileMode.Create, FileAccess.Write);
            XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);// (filePath, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Resources");
            xtw.WriteAttributeString("Type", obj.GetType().FullName);
            xtw.WriteAttributeString("ObjectName", obj.GetType().FullName);

            xtw.WriteEndElement();

            xtw.Close();
            fs.Close();
            fs.Dispose();
            #endregion
        }


        /// <summary>
        /// 创建相应的资源文件
        /// </summary>
        internal static void XmlCreator(Control ctr, string filePath)
        {
            string name = ctr.GetType().FullName;
            string path = filePath.Substring(0, filePath.LastIndexOf(@"\"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            #region XmlTextWriter
            FileStream fs = File.Create(filePath);
            XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);// (filePath, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Resources");
            xtw.WriteAttributeString("Type", ctr.GetType().FullName);
            xtw.WriteAttributeString("ObjectName", name);
            //xtw.WriteAttributeString("CreatTime", DateTime.Now.ToString());
            xtw.WriteStartElement("Text");
            xtw.WriteAttributeString("Key", "");
            xtw.WriteEndElement();
            xtw.WriteStartElement("Image");
            xtw.WriteAttributeString("Key", "");
            xtw.WriteEndElement();
            xtw.WriteStartElement("Icon");
            xtw.WriteAttributeString("Key", "");
            xtw.WriteEndElement();
            xtw.WriteStartElement("Cursor");
            xtw.WriteAttributeString("Key", "");
            xtw.WriteEndElement();
            xtw.WriteStartElement("ByteArray");
            xtw.WriteAttributeString("Key", "");
            xtw.WriteEndElement();
            xtw.WriteEndElement();

            xtw.Close();
            fs.Close();
            fs.Dispose();
            #endregion

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // 数据Element的数据节点根据控件包含关系递归生成
            ControlDataElementCreator(ctr, doc);

            doc.Save(filePath);
        }

        /// <summary>
        /// 数据Element的数据节点根据控件包含关系递归生成
        /// </summary>
        static void ControlDataElementCreator(Control ctr, XmlDocument doc)
        {
            SubControlDataElementCreator(ctr, doc);

            foreach (Control subctr in ctr.Controls)
            {
                /// 如果该控件类有定制特性(ResReaderAttribute.IsGetResource)且为真。
                /// 则已经为该控件实现了资源的载入，无需从使用该控件的上一层类型重新定义资源
                object[] objs = subctr.GetType().GetCustomAttributes(typeof(ResReaderAttribute), false);
                if (objs.Length > 0 && !((ResReaderAttribute)objs[0]).IsGetResource)
                {
                    continue;
                }
                else
                {
                    if (subctr is ToolStrip)
                    {
                        ToolStripDataElementCreator(subctr, doc);
                    }
                    else
                    {
                        ControlDataElementCreator(subctr, doc);//递归
                    }
                }

            }
        }

        /// <summary>
        /// 根据单个Control生成这个Control的Element(递归子方法)
        /// </summary>
        static void SubControlDataElementCreator(Control ctr, XmlDocument doc)
        {
            if (!string.IsNullOrEmpty(ctr.Name))
            {
                XmlElement ele = doc.CreateElement("Control");
                ele.SetAttribute("Type", ctr.GetType().FullName);
                ele.SetAttribute("Name", ctr.Name);
                SetControlElement(ctr, ele, doc);
                doc.DocumentElement.AppendChild(ele);
            }
        }

        /// <summary>
        /// 根据单个ToolStrip生成这个Control的Element
        /// </summary>
        static void ToolStripDataElementCreator(Control ctr, XmlDocument doc)
        {
            XmlElement ele = doc.CreateElement("Control");
            ele.SetAttribute("Type", ctr.GetType().FullName);
            ele.SetAttribute("Name", ctr.Name);
            SetControlElement(ctr, ele, doc);
            doc.DocumentElement.AppendChild(ele);

            ToolStrip strip = (ToolStrip)ctr;
            foreach (ToolStripItem item in strip.Items)
            {
                ToolStripItemDataElementCreator(item, doc);
            }
        }

        static void ToolStripItemDataElementCreator(ToolStripItem item, XmlDocument doc)
        {
            XmlElement ele = doc.CreateElement("ToolStripItem");
            ele.SetAttribute("Type", item.GetType().FullName);
            ele.SetAttribute("Name", item.Name);
            SetControlElement(item, ele, doc);
            doc.DocumentElement.AppendChild(ele);
            if (item is ToolStripDropDownItem)
            {
                ToolStripDropDownItem dditem = (ToolStripDropDownItem)item;
                foreach (ToolStripItem subItem in dditem.DropDownItems)
                {
                    ToolStripItemDataElementCreator(subItem, doc);
                }
            }
        }

        static void SetControlElement(ToolStripItem item, XmlElement ele, XmlDocument doc)
        {
            if (!string.IsNullOrEmpty(item.Text))
            {
                XmlElement textEle = doc.CreateElement("Text");
                textEle.InnerText = item.Text;
                ele.AppendChild(textEle);
            }
        }

        static void SetControlElement(Control ctr, XmlElement ele, XmlDocument doc)
        {
            if (!string.IsNullOrEmpty(ctr.Text))
            {
                XmlElement textEle = doc.CreateElement("Text");
                textEle.InnerText = ctr.Text;
                ele.AppendChild(textEle);
            }
        }

        /// <summary>
        /// 设置节点的CDATA节点值
        /// </summary>
        static void SetCDataValue(XmlNode node, string value)
        {
            foreach (XmlNode subNode in node.ChildNodes)
            {
                if (subNode.NodeType == XmlNodeType.CDATA)
                {
                    subNode.Value = value;
                    return;
                }
            }
            XmlCDataSection cdataText = node.OwnerDocument.CreateCDataSection(value);
            node.AppendChild(cdataText);
        }

    }

    internal static class EnumXmlFile
    {
        /// <summary>
        /// 创建Enum对应的资源文件
        /// </summary>
        internal static void PublicXmlCreator(Enum enumtype, string filePath)
        {
            #region XmlTextWriter
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            FileStream fs = File.Open(filePath, FileMode.Create, FileAccess.Write);
            XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);// (filePath, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Resources");
            xtw.WriteAttributeString("Type", enumtype.GetType().FullName);
            xtw.WriteAttributeString("ObjectName", enumtype.GetType().FullName);

            xtw.WriteEndElement();

            xtw.Close();
            fs.Close();
            fs.Dispose();
            #endregion

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // 数据Element的数据节点根据控件包含关系递归生成
            ElementCreator(enumtype, doc);

            doc.Save(filePath);
        }

        private static void ElementCreator(Enum enumtype, XmlDocument doc)
        {
            throw new NotImplementedException();
        }


    }

#endif

}
