using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Resources;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Jeelu.SimplusD
{
    public static class ResourceService
    {
        static Dictionary<string, string> _resourceTextDic = new Dictionary<string, string>();
        static Dictionary<string, string> _enumResourceTextDic = new Dictionary<string, string>();
        static Dictionary<string, object> _dicResources = new Dictionary<string, object>();
        static List<string> _processFile = new List<string>();
        static private ImageList _mainImageList;
        static public ImageList MainImageList
        {
            get { return _mainImageList; }
        }

        static private bool _isInited = false;
        static public bool IsInited
        {
            get { return _isInited; }
        }
        static public void Initialize()
        {
            if (_isInited)
            {
                return;
            }
            _isInited = true;

            _mainImageList = new ImageList();
            _mainImageList.ColorDepth = ColorDepth.Depth32Bit;

            XmlDocument doc = new XmlDocument();
            doc.Load(PathService.CL_ResourceText);
            ParseResourceText(doc.DocumentElement.ChildNodes);

            XmlDocument doc2 = new XmlDocument();
            doc2.Load(PathService.CL_MainFormResourcesConfig);
            foreach (XmlNode node in doc2.DocumentElement.ChildNodes)
            {
                string file = node.Attributes["file"].Value;
                _processFile.Add(file);
                using (IResourceReader reader = new ResourceReader(Path.Combine(PathService.CL_Resources_Folder, file)))
                {
                    /// 枚举资源文件所有对象
                    IDictionaryEnumerator iden = reader.GetEnumerator();
                    while (iden.MoveNext())
                    {
                        if (iden.Value is Image)
                        {
                            MainImageList.Images.Add((string)iden.Key, (Image)iden.Value);
                        }
                        else if (iden.Value is System.Drawing.Icon)
                        {
                            MainImageList.Images.Add((string)iden.Key, ((System.Drawing.Icon)iden.Value).ToBitmap());
                        }
                        else
                        {
                            _dicResources.Add((string)iden.Key, iden.Value);
                        }
                    }
                }
            }

            //TODO: 文件路径！！ lukan, 2008年2月26日10时31分
            string enumXmlFile = Path.Combine(Path.Combine(Application.StartupPath, "CHS"), "EnumsResources.xml");
            ParseResourceText(enumXmlFile);
        }

        /// <summary>
        /// 获取资源里的文本
        /// </summary>
        public static string GetResourceText(string key)
         {
            return _resourceTextDic[key];
        }
        public static string GetEnumResourceText(Type enumType, string name)
        {
            string value;
            string key = enumType.Name + "." + name;
            if (_enumResourceTextDic.TryGetValue(key, out value))
            {
                return value;
            }
            Debug.Fail("未找到枚举对应的资源文件:" + key);
            return name;
        }

        public static Bitmap GetResourceImage(string key)
        {
            return (Bitmap)MainImageList.Images[key];
        }

        /// <summary>
        /// 初始化枚举的对应的资源文件。
        /// </summary>
        /// <param name="enumXmlFile"></param>
        static void ParseResourceText(string enumXmlFile)
        {
            Debug.Assert(File.Exists(enumXmlFile), enumXmlFile + " : File isn't Exists");
            XmlTextReader reader = new XmlTextReader(enumXmlFile);
            string enumName = null;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "enum")
                    {
                        enumName = reader.GetAttribute("name");
                    }
                    else if (reader.Name == "text")
                    {
                        string key = enumName + "." + reader.GetAttribute("name");
                        _enumResourceTextDic.Add(key, reader.ReadString());
                    }
                }
            }
            reader.Close();
        }
        static void ParseResourceText(XmlNodeList nodeList)
        {
            ParseResourceText(nodeList, null);
        }
        static void ParseResourceText(XmlNodeList nodeList, string prefixString)
        {
            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    //将其属性添加进容器中
                    foreach (XmlAttribute att in node.Attributes)
                    {
                        string key = prefixString + node.Name + "." + att.Name;
                        _resourceTextDic.Add(key, att.Value.Replace(@"\r\n", "\r\n"));
                    }

                    //继续添加其子节点
                    if (node.HasChildNodes)
                    {
                        ParseResourceText(node.ChildNodes, prefixString + node.Name + ".");
                    }
                }
            }
        }
        //static public object GetResource(string key)
        //{
        //    return _dicResources[key];
        //}
        //static public string GetResource(string dataSourcesFileName, string value)
        //{
        //    FileStream stream = new FileStream(Path.Combine(PathService.CL_DataSources_Folder, dataSourcesFileName),
        //        FileMode.Open, FileAccess.Read);
        //    XmlReader reader = XmlReader.Create(stream);
        //    try
        //    {
        //        while (reader.Read())
        //        {
        //            if (reader.NodeType == XmlNodeType.Element)
        //            {
        //                if (reader.GetAttribute("value") == value)
        //                {
        //                    return reader.GetAttribute("text");
                            
        //                }
        //            }
        //        }

        //        ///没找到，返回null
        //        return null;
        //    }
        //    finally
        //    {
        //        reader.Close();
        //        stream.Close();
        //    }
        //}
        static public bool HasProcessFile(string file)
        {
            return _processFile.Contains(file);
        }
    }
}