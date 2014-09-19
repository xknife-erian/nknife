using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class StyleXmlElement : IndexXmlElement
    {
        internal StyleXmlElement(XmlDocument doc)
            : base("style", doc)
        {
            //Width = "500px";
            //Height = "400px";
        }
        #region 内部变量

        #endregion

        #region 公共属性

        /// <summary>
        /// Css
        /// </summary>
        public string Css { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public string Width
        {
            get { return GetAttribute("width"); }
            set { SetAttribute("width", value); }
        }

        /// <summary>
        ///  高度
        /// </summary>
        public string Height
        {
            get { return GetAttribute("height"); }
            set { SetAttribute("height", value); }
        }

        #endregion

        #region 公共方法

        public SnipPartXmlElement CreatePart()
        {
            SnipPartXmlElement _partEle = new SnipPartXmlElement(OwnerDocument);
            return _partEle;
        }

        #endregion

        #region 内部方法

        #endregion
        
        #region 事件响应

        #endregion

        #region 自定义事件

        #endregion

        /// <summary>
        /// 获得名为parts的节点
        /// </summary>
        /// <returns></returns>
        public SnipPartsXmlElement GetPartsElement()
        {
            return (SnipPartsXmlElement)SelectSingleNode("parts");
        }
    }
}
