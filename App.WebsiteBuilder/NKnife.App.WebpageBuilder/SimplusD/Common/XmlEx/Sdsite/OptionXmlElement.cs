using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 项目中的选项型数据保存的位置。
    /// （选项型数据，如：Hr模板数据、记录的产品属性组、产品属性组等）
    /// </summary>
    public class OptionXmlElement : GeneralXmlElement
    {
        internal OptionXmlElement(XmlDocument doc)
            :base("option",doc)
        {
        }

        //public ProductPropertyGroupXmlElement productPropGroupEle
        //{
        //    get
        //    {
        //        ProductPropertyGroupXmlElement element = this.SelectSingleNode("productPropertyGroup") as ProductPropertyGroupXmlElement;
        //        return element;
        //    }
        //}

        //public string[] GetTreeOpenItems()
        //{
        //    XmlElement treeOpenItems = (XmlElement)this.SelectSingleNode("treeOpenItems");
        //    return XmlUtilService.GetGroupItems(treeOpenItems, "item", ItemsDataMode.CData);
        //}

        //public void SetTreeOpenItems(string[] values)
        //{
        //    XmlElement treeOpenItems = (XmlElement)this.SelectSingleNode("treeOpenItems");
        //    XmlUtilService.SetGroupItems(treeOpenItems, "item", ItemsDataMode.CData,values);
        //    Service.Sdsite.CurrentDocument.Save();
        //}
    }
}
