using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��Ŀ�е�ѡ�������ݱ����λ�á�
    /// ��ѡ�������ݣ��磺Hrģ�����ݡ���¼�Ĳ�Ʒ�����顢��Ʒ������ȣ�
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
