using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace Gean.Xml.Linq
{
    /// <summary>
    /// 对XDocument的类的封装
    /// </summary>
    public abstract class AbstractXmlFile
    {
        /// <summary>
        /// 基础的XmlDocument扩展(组合)类
        /// </summary>
        /// <param name="filePath">XML文件的物理绝对路径</param>
        public AbstractXmlFile(string filePath)
        {
            this.FilePath = filePath;
            if (!File.Exists(this.FilePath))
            {
#if DEBUG
                throw new FileNotFoundException("Xml File isn't Exists!");
#else
                this.Document = new XDocument(
                    new XComment("Auto Created."),
                    new XElement("Root")
                );
#endif
            }
            else
            {
                this.Document = XDocument.Load(this.FilePath);
            }
            this.InitializeComponent();
        }

        protected virtual void InitializeComponent() { }
        protected XDocument Document { get; set; }
        public XElement Root { get { return this.Document.Root; } }

        /// <summary>
        /// 获取文档的绝对路径
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// 保存当前XmlDocument
        /// </summary>
        public virtual void Save()
        {
            if (string.IsNullOrEmpty(this.FilePath))
            {
                Debug.Fail("this.FilePath is Null!");
                return;
            }
            FileAttributes fileAtts = FileAttributes.Normal;
            if (File.Exists(FilePath))
            {
                fileAtts = File.GetAttributes(FilePath);//先获取此文件的属性
                File.SetAttributes(FilePath, FileAttributes.Normal);//将文件属性设置为普通（即没有只读和隐藏等）
            }
            ((XDocument)this.Document).Save(FilePath);//在文件属性为普通的情况下保存。（不然有可能会“访问被拒绝”）
            File.SetAttributes(FilePath, fileAtts);//恢复文件属性
        }
    }
}
