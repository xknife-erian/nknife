using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD
{
    public class BaseDocument
    {
        /// <summary>
        /// XmlDocument的文件路径
        /// </summary>
        public string AbsoluteFilePath { get; private set; }

        /// <summary>
        /// 内部存储的真实的XmlDocument
        /// </summary>
        protected XmlDocument _innerXmlDoc = new XmlDocument();

        public BaseDocument(string absoluteFilePath)
        {
            this.AbsoluteFilePath = absoluteFilePath;
        }

        public virtual void Load()
        {
            _innerXmlDoc.Load(AbsoluteFilePath);
        }
        /// <summary>
        /// 保存当前XmlDocument
        /// </summary>
        virtual public void Save()
        {
            FileAttributes fileAtts = FileAttributes.Normal;

            if (File.Exists(AbsoluteFilePath))
            {
                ///先获取此文件的属性
                fileAtts = File.GetAttributes(AbsoluteFilePath);

                ///讲文件属性设置为普通（即没有只读和隐藏等）
                File.SetAttributes(AbsoluteFilePath, FileAttributes.Normal);
            }

            ///在文件属性为普通的情况下保存。（不然有可能会“访问被拒绝”）
            this._innerXmlDoc.Save(AbsoluteFilePath);

            ///恢复文件属性
            File.SetAttributes(AbsoluteFilePath, fileAtts);
        }
    }
}
