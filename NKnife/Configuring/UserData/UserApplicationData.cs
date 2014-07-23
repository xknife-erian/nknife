using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using NKnife.Configuring.Interfaces;
using NKnife.Utility.File;

namespace NKnife.Configuring.UserData
{
    /// <summary>这是一个保存用户目录下的选项文件，该文件可以不存在，当使用时发现该文件不存在时，将创建，并创建默认值 
    /// </summary>
    public class UserApplicationData : IUserApplicationData
    {
        private string _FileName;
        private string _UserApplicationDataPath;

        protected UserApplicationData()
        {
            Load();
        }

        /// <summary>本选项面向的持久化文件
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_FileName))
                    _FileName = UserApplicationDataPath + "\\" + GetType().Name + ".UserApplicationData";
                return _FileName;
            }
        }

        /// <summary>用作当前非漫游用户使用的应用程序特定数据的公共储存库路径。
        /// </summary>
        /// <value>The user application data path.</value>
        public string UserApplicationDataPath
        {
            get
            {
                if (string.IsNullOrEmpty(_UserApplicationDataPath))
                {
                    const Environment.SpecialFolder FOLDER = Environment.SpecialFolder.ApplicationData;
                    string path = Environment.GetFolderPath(FOLDER);
                    string namespaceStr = Assembly.GetEntryAssembly().GetName().FullName;
                    if (string.IsNullOrWhiteSpace(namespaceStr))
                        namespaceStr = "NKnife.UserData";
                    string subpath = namespaceStr.Replace('.', '\\').Insert(0, "\\");
                    _UserApplicationDataPath = path + subpath;
                    if (!Directory.Exists(_UserApplicationDataPath))
                        UtilityFile.CreateDirectory(_UserApplicationDataPath);
                }
                return _UserApplicationDataPath;
            }
        }

        /// <summary>将本选项文件所对应的XML文件
        /// </summary>
        /// <value>The document.</value>
        protected virtual XmlDocument Document { get; private set; }

        /// <summary>按指定的名称获取选项值，如果该值无法获取，将保存指定的默认值
        /// </summary>
        /// <param name="localname">The localname.</param>
        /// <param name="defalutValue">The defalut value.</param>
        /// <returns></returns>
        public string GetValue(string localname, object defalutValue)
        {
            XmlElement ele = GetElement(localname);
            string value = ele.InnerText;
            if (string.IsNullOrEmpty(value))
            {
                value = defalutValue.ToString();
                ele.InnerText = value;
                Save();
            }
            return value;
        }

        /// <summary>按指定的名称设置值
        /// </summary>
        /// <param name="localname">The localname.</param>
        /// <param name="value">The value.</param>
        public void SetValue(string localname, object value)
        {
            XmlElement ele = GetElement(localname);
            ele.InnerText = value.ToString();
            Save();
        }

        /// <summary>获取指定名称的XmlElement，如果不存在，将创建
        /// </summary>
        /// <param name="localname">The localname.</param>
        /// <returns></returns>
        public XmlElement GetElement(string localname)
        {
            if (Document.DocumentElement != null)
            {
                XmlNode node = Document.DocumentElement.SelectSingleNode(localname);
                if (node == null)
                {
                    node = Document.CreateElement(localname);
                    if (Document.DocumentElement != null) 
                        Document.DocumentElement.AppendChild(node);
                    Save();
                }
                return (XmlElement) node;
            }
            return null;
        }

        /// <summary>加载选项文件
        /// </summary>
        public void Load()
        {
            if (!File.Exists(FileName))
                Document = XmlHelper.CreatNewDoucmnet(FileName);
            else
            {
                Document = new XmlDocument();
                Document.Load(FileName);
            }
        }

        /// <summary>持久化选项文件
        /// </summary>
        public void Save()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                Debug.Fail("this.FilePath is Null!");
                return;
            }
            var fileAtts = FileAttributes.Normal;
            if (File.Exists(FileName))
            {
                fileAtts = File.GetAttributes(FileName); //先获取此文件的属性
                File.SetAttributes(FileName, FileAttributes.Normal); //将文件属性设置为普通（即没有只读和隐藏等）
            }
            Document.Save(FileName); //在文件属性为普通的情况下保存。（不然有可能会“访问被拒绝”）
            File.SetAttributes(FileName, fileAtts); //恢复文件属性
        }
    }
}