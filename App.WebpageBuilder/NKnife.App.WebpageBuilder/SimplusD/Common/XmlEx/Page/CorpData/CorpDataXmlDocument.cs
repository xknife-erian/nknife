using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Xml;
using Jeelu.Win;
using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 企业数据页面的基类。招聘、招标、知识、产品、项目都属于企业数据
    /// </summary>
    abstract public partial class CorpDataXmlDocument : PageXmlDocument,ISearch
    {
        public CorpDataXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {
        }

        #region 联系人相关的属性
        
        [SnipPart("link", "link", "link", "link", 0, 80)]
        [Editor(21, 0, "", MainControlType = MainControlType.Department, GroupBoxUseWinStyle = true, GroupBoxDockTop = true, GroupBoxUseWinStyleText = "link", LabelTop = "help", IsRed = true,IsCanFind=false)]
        public DepartmentData pageDepartmentData
        {
            get
            {
                DepartmentData PageValue = new DepartmentData();
                XmlNode node = this.SelectSingleNode("//Department");
                if (node != null)
                {
                    XmlElement ele = (XmlElement)node;
                    PageValue.Name = ele.GetAttribute("name");
                    PageValue.LinkMan = ele.GetAttribute("linkMan");
                    PageValue.Phone = ele.GetAttribute("phone");
                    PageValue.MobilePhone = ele.GetAttribute("moblle");
                    PageValue.Address = ele.GetAttribute("address");
                    PageValue.PostCode = ele.GetAttribute("postCode");
                    PageValue.Fax = ele.GetAttribute("fax");
                    PageValue.Email = ele.GetAttribute("email");
                }
                return PageValue;
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//Department");
                if (node != null)
                {
                    node.RemoveAll();
                }
                else
                {
                    XmlElement element = this.CreateElement("Department");
                    node = (XmlNode)element;
                }
                DepartmentData PageValue = value;
                XmlElement newEle = node as XmlElement;
                newEle.SetAttribute("name", PageValue.Name);
                newEle.SetAttribute("linkMan", PageValue.LinkMan);
                newEle.SetAttribute("phone", PageValue.Phone);
                newEle.SetAttribute("moblle", PageValue.MobilePhone);
                newEle.SetAttribute("address", PageValue.Address);
                newEle.SetAttribute("postCode", PageValue.PostCode);
                newEle.SetAttribute("fax", PageValue.Fax);
                newEle.SetAttribute("email", PageValue.Email);
                this.DocumentElement.AppendChild(newEle);
            }
        }
        #endregion

        #region ISearch 成员
        Position ISearch.SearchNext(WantToDoType type)
        {
            Position position = null;
            GetDataResourceValue getDataResourceValue = new GetDataResourceValue(this);
            switch (type)
            {
                case WantToDoType.SearchNext:
                    position = getDataResourceValue.SaveFindProperty(type);
                    break;
                case WantToDoType.SearchAll:
                    position = getDataResourceValue.SearchOrReplaceAll(type);
                    break;
                case WantToDoType.ReplaceNext:
                    position = getDataResourceValue.SaveFindProperty(type);
                    break;
                case WantToDoType.ReplaceAll:
                    position = getDataResourceValue.SearchOrReplaceAll(type);
                    break;
            }
            return position;
        }
        void ISearch.Replace(Position position)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}