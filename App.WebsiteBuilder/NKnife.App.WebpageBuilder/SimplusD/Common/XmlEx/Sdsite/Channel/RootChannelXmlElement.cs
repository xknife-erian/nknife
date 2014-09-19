using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��Ƶ��������վƵ����0��Ƶ����
    /// </summary>
    public class RootChannelXmlElement : ChannelSimpleExXmlElement
    {
        public RootChannelXmlElement(XmlDocument doc)
            :base(doc)
        {
        }

        /// <summary>
        /// ����д����ȡ�˽ڵ�����Ӧ�������ļ������·����
        /// </summary>
        public override string RelativeFilePath
        {
            get
            {
                return @"Root\";
            }
        }

        public override string OldRelativeFilePath
        {
            get
            {
                return RelativeFilePath;
            }
        }

        /// <summary>
        /// ����д����ȡ�˽ڵ�����Ӧ�������ļ��ľ���·����
        /// </summary>
        public override string RelativeUrl
        {
            get
            {
                return @"/";
            }
        }

        public override string OldRelativeUrl
        {
            get
            {
                return RelativeUrl;
            }
        }
    }
}
