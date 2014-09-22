using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 根频道。即网站频道，0级频道。
    /// </summary>
    public class RootChannelXmlElement : ChannelSimpleExXmlElement
    {
        public RootChannelXmlElement(XmlDocument doc)
            :base(doc)
        {
        }

        /// <summary>
        /// 已重写。获取此节点所对应的数据文件的相对路径。
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
        /// 已重写。获取此节点所对应的数据文件的绝对路径。
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
