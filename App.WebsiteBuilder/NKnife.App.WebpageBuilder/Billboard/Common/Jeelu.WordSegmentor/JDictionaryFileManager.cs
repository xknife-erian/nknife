using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace Jeelu.Billboard
{
    /// <summary>
    /// 词库实体文件管理器。
    /// 词库实体文件由两大类组成：
    /// 1. 主词库：主词库由默认主词库"Dict.dct"（原开源组件定义), 及Jeelu自定义频道分类词库组成。
    /// 2. 阻拦词库：各网站的阻拦词都不一样，所以阻拦词库视网站不同均不同。
    /// 每類詞庫實體文件都有一個XML索引文件來表達其在目錄下的樹狀結構。
    /// </summary>
    public class JDictionaryFileManager
    {
        public JDictionaryFileManager() { }

        public void Load(JDictionaryTypeEnum type, string indexFile)
        {
            this.JDictionaryType = type;
            this.InnerDocument = new XmlDocument();
            this.DictFiles = new Dictionary<string, FileInfo>();
            if (!File.Exists(indexFile))
            {
                XmlTextWriter writer = new XmlTextWriter(indexFile, Encoding.UTF8);
                writer.WriteStartDocument();
                writer.WriteStartElement("dictfiles");
                writer.WriteAttributeString("createdtime", DateTime.Now.ToString());
                writer.WriteAttributeString("version", "0");
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            }
            File.SetAttributes(indexFile, FileAttributes.Normal);
            this.IndexFile = new FileInfo(indexFile);
            this.InnerDocument.Load(indexFile);
            this.SetDictFiles();
        }

        protected virtual JDictionaryTypeEnum JDictionaryType { get; set; }
        /// <summary>
        /// 每類詞庫實體文件都有一個XML索引文件來表達其在目錄下的樹狀結構。
        /// </summary>
        public virtual FileInfo IndexFile { get; set; }
        protected virtual string RootDir { get { return this.IndexFile.DirectoryName; } }
        protected virtual XmlDocument InnerDocument { get; set; }
        public Dictionary<string, FileInfo> DictFiles { get; protected set; }
        /// <summary>
        /// 一個目錄下的最大文件數量
        /// </summary>
        public int FileCountForDir
        {
            get { return this._FileCountForDir; }
            set { this._FileCountForDir = value; }
        }
        private int _FileCountForDir = 180;

        private void SetDictFiles()
        {
            XmlNodeList nodelist = this.InnerDocument.DocumentElement.SelectNodes(@".//dictfile");
            foreach (XmlNode node in nodelist)
            {
                XmlElement ele = (XmlElement)node;
                string channel = ele.GetAttribute("channel");
                string text = null;
                foreach (XmlNode textnode in ele.ChildNodes)
                {
                    if (textnode.NodeType == XmlNodeType.Text)
                    {
                        XmlText xmltext = (XmlText)textnode;
                        text = xmltext.Data.Trim();
                        continue;
                    }
                }
                string path = Path.Combine(this.RootDir, text);
                FileInfo info = new FileInfo(path);
                this.DictFiles.Add(channel, info);
            }
        }

        public void AddDictFile(string newChannel)
        {
            this.AddDictFile(newChannel, null);
        }
        public void AddDictFile(string newChannel, string parentChannel)
        {
            if (this.DictFiles.ContainsKey(newChannel))
            {
                Debug.Fail(newChannel + " is Exist!");
                return;
            }
            JDictionary jd = new JDictionary();
            string newdir;
            string newsubdir = Utility.File.GetLimitDirectory(this.RootDir, this.FileCountForDir, out newdir);
            string newFileName = newsubdir + newChannel + "." + JDictionaryType.ToString();
            string newFileFullName = Path.Combine(this.RootDir, newFileName);
            Directory.CreateDirectory(newdir);
            FileStream fs = File.Create(newFileFullName); fs.Close(); fs.Dispose();
            jd.FileInfo = new FileInfo(newFileFullName);

            XmlNode parentNode;
            if (string.IsNullOrEmpty(parentChannel))
            {
                parentNode = this.InnerDocument.DocumentElement;
            }
            else
            {
                parentNode = this.InnerDocument.DocumentElement.
                    SelectSingleNode(string.Format(".//dictfile[@channel='{0}']", parentChannel));
                if (parentNode == null)
                {
                    Debug.Fail(parentChannel + " isn't Exist!!!");
                    parentNode = this.InnerDocument.DocumentElement;
                }
            }

            XmlElement ele = this.InnerDocument.CreateElement("dictfile");
            ele.SetAttribute("channel", newChannel);
            ele.InnerText = newFileName;
            parentNode.AppendChild(ele);

            jd.Save();
            this.DictFiles.Add(newChannel, new FileInfo(newFileFullName));
        }

        public void Save()
        {
            this.InnerDocument.Save(this.IndexFile.FullName);
        }


    }
}
