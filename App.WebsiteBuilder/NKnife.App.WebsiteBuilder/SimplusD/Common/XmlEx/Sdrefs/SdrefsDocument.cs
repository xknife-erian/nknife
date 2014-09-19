using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD
{
    public class SdrefsDocument : BaseDocument
    {
        public ElementCollection<SrcFileElement,SdrefsDocument> SrcFiles { get; private set; }
        public SdrefsDocument(string absoluteFilePath)
            :base(absoluteFilePath)
        {
            SrcFiles = new ElementCollection<SrcFileElement, SdrefsDocument>(this);
            SrcFiles.Inserted += new EventHandler<EventArgs<SrcFileElement>>(SrcFiles_Inserted);
            SrcFiles.Removed += new EventHandler<EventArgs<SrcFileElement>>(SrcFiles_Removed);
        }
        void SrcFiles_Inserted(object sender, EventArgs<SrcFileElement> e)
        {
            _innerXmlDoc.DocumentElement.AppendChild(e.Item._innerXmlEle);
        }

        void SrcFiles_Removed(object sender, EventArgs<SrcFileElement> e)
        {
            _innerXmlDoc.DocumentElement.RemoveChild(e.Item._innerXmlEle);
        }

        public SrcFileElement GetSrcFileById(string id)
        {
            return this.SrcFiles[id];
        }

        /// <summary>
        /// 创建源节点
        /// </summary>
        /// <param name="localName"></param>
        /// <param name="srcEle"></param>
        /// <returns>创建源节点</returns>
        public SrcFileElement CreateSrcFile(string id, string customId, IdType type)
        {
            XmlElement ele = _innerXmlDoc.CreateElement("src");
            SrcFileElement srcEle = new SrcFileElement(ele,this);
            srcEle.Id = id;
            srcEle.CustomId = customId;
            srcEle.Type = type;
            return srcEle;
        }
        public override void Load()
        {
            base.Load();
        }
    }
}
