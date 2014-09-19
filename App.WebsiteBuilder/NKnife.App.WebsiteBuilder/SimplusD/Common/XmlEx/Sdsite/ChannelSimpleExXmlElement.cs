using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// Ƶ��
    /// </summary>
    public class ChannelSimpleExXmlElement : FolderXmlElement
    {
        public ChannelSimpleExXmlElement(XmlDocument doc)
            :base("channel",doc)
        {
        }

        public override DataType DataType
        {
            get
            {
                return DataType.Channel;
            }
        }

        /// <summary>
        /// ��ȡ�Ƿ��Ƶ��
        /// </summary>
        public bool IsRootChannel
        {
            get { return this.Id == Utility.Const.ChannelRootId; }
        }

        public SiteShowItemsXmlElement SiteShowItem
        {
            get { return (SiteShowItemsXmlElement)this.GetElementByName("siteShowItem"); }
        }

        /// <summary>
        /// ����ChannelElement��������typeҪ���ɲ�ͬ������,���Բ�����AnyXmlElement����߼�.
        /// </summary>
        public override XmlNode Clone()
        {
            ChannelSimpleExXmlElement channelEle = Activator.CreateInstance(this.GetType(), this.OwnerDocument)
                as ChannelSimpleExXmlElement;

            XmlUtilService.CopyXmlElement(this, channelEle);
            return channelEle;
        }

        /// <summary>
        /// ��ȡ��Ƶ���ļ���
        /// </summary>
        public ChannelSimpleExXmlElement[] GetChildChannels()
        {
            XmlNodeList nodes = this.SelectNodes("channel");
            List<ChannelSimpleExXmlElement> list = new List<ChannelSimpleExXmlElement>();

            foreach (XmlNode node in nodes)
            {
                list.Add((ChannelSimpleExXmlElement)node);
            }
            return list.ToArray();
        }

        /// <summary>
        /// ��ȡ��һ���ļ��еļ���
        /// </summary>
        public FolderXmlElement[] GetChildFolders()
        {
            XmlNodeList nodes = this.SelectNodes("folder");
            List<FolderXmlElement> list = new List<FolderXmlElement>();

            foreach (XmlNode node in nodes)
            {
                list.Add((FolderXmlElement)node);
            }
            return list.ToArray();
        }

        /// <summary>
        /// ��ȡҳ��ļ���
        /// </summary>
        public PageSimpleExXmlElement[] GetPages()
        {
            XmlNodeList nodes = this.SelectNodes(".//page");
            List<PageSimpleExXmlElement> list = new List<PageSimpleExXmlElement>();

            foreach (XmlNode node in nodes)
            {
                list.Add((PageSimpleExXmlElement)node);
            }
            return list.ToArray();
        }

        /// <summary>
        /// ��ȡҳ��ļ���
        /// </summary>
        public PageSimpleExXmlElement[] GetPages(PageType type)
        {
            XmlNodeList nodes = this.SelectNodes(string.Format(".//page[@type='{0}']",type));
            List<PageSimpleExXmlElement> list = new List<PageSimpleExXmlElement>();

            foreach (XmlNode node in nodes)
            {
                list.Add((PageSimpleExXmlElement)node);
            }
            return list.ToArray();
        }

        /// <summary>
        /// ��ȡģ��ļ���
        /// </summary>
        public TmpltSimpleExXmlElement[] GetTmplts()
        {
            XmlNodeList nodes = this.SelectNodes("tmplt");
            List<TmpltSimpleExXmlElement> list = new List<TmpltSimpleExXmlElement>();

            foreach (XmlNode node in nodes)
            {
                list.Add((TmpltSimpleExXmlElement)node);
            }
            return list.ToArray();
        }

    }
}
