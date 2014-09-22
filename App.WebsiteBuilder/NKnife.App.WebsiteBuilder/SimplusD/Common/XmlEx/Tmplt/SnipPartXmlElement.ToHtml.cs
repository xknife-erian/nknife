using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Jeelu.SimplusD
{
    public partial class SnipPartXmlElement : ToHtmlXmlElement
    {
        internal override bool SaveXhtml(string fileFullName)
        {
            ///当Part是List型，排序方式用户设置为“自动关键词排序”时，该Part允许生成独立文件
            if (this.SnipPartType == SnipPartType.List && this.SequenceType == SequenceType.AutoKeyWord)
            {
                return base.SaveXhtml(fileFullName);
            }
            return false;
        }

        protected override void TagCreator()
        {
            XhtmlElement tag = null;
            if (this.HasLinkForPart)///当Part本身有连接时的处理
            {
                tag = this.ParentXhtmlElement.OwnerPage.CreateXhtmlA();
                XhtmlTags.A tagA = (XhtmlTags.A)tag;
                tagA.Builder("", "", Xhtml.Target._blank, 0, 'a');
                this.ParentXhtmlElement.AppendChild(tag);
            }
            if (tag == null)///当为Null时，即为该Part没有设置链接
            {
                tag = this.ParentXhtmlElement;
            }
            switch (this.SnipPartType)
            {
                case SnipPartType.Static:///静态Part的生成，比较简单
                case SnipPartType.Navigation:///导航Part的生成实际上与静态Part没有区别
                    #region 针对静态Part的值生成部份Html代码
                    {
                        if (!string.IsNullOrEmpty(this.CDataValue))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(TempCDataTag.CDataTag).Append(this.CDataValue).Append(TempCDataTag.CDataTag);
                            XhtmlElement htmlele = tag.OwnerPage.CreateXhtmlCData(sb.ToString());
                            tag.AppendChild(htmlele);
                        }
                        break;
                    }
                    #endregion
                case SnipPartType.List:
                    base.TagCreator();
                    break;
                case SnipPartType.ListBox:
                    base.TagCreator();
                    break;
                case SnipPartType.Box:
                    base.TagCreator();
                    break;
                case SnipPartType.Path:
                    base.TagCreator();
                    break;
                case SnipPartType.Attribute:///定制特性的SnipPart
                    {
                        this.AttributeTagCreator(tag);
                        break;
                    }
                case SnipPartType.None:
                default:
                    break;
            }//switch
            this.XhtmlElement = tag;
        }

        private void AttributeTagCreator(XhtmlElement tag)
        {
            if (string.IsNullOrEmpty(this.AttributeName))
            {
                return;
            }
            PropertyInfo pi = PageAttributeService.GetPropertyInfo(this.AttributeName, this.PageXmlDocument.GetType());
            object outvalue = pi.GetValue(this.PageXmlDocument, null);
            if (outvalue == null)///当通过定制特性取出值为空时
            {
                return;
            }
            string outObjType = outvalue.GetType().FullName;

            switch (outObjType)
            {
                case "System.String":
                    {
                        XhtmlTags.P ptag = tag.OwnerPage.CreateXhtmlP();
                        ptag.AppendText((string)outvalue);
                        tag.AppendChild(ptag);
                        break;
                    }
                case "System.String[]":
                    {
                        XhtmlTags.Ul ultag = tag.OwnerPage.CreateXhtmlUl();
                        foreach (string str in (Array)outvalue)
                        {
                            XhtmlTags.Li liTag = tag.OwnerPage.CreateXhtmlLi();
                            liTag.AppendText(str);
                            ultag.AppendChild(liTag);
                        }
                        tag.AppendChild(ultag);
                        break;
                    }
                case "Jeelu.DepartmentData":
                    {
                        DepartmentData ddata = (DepartmentData)outvalue;
                        
                        
                        















                        ///Todo:lukan,2008-7-3 12:39:30,
                        ///严重的漏设计项目，联系人众多的属性用户不能设计显示方式
                        XhtmlTags.P ptag = tag.OwnerPage.CreateXhtmlP();
                        ptag.AppendText("(DepartmentData)outvalue!");
                        tag.AppendChild(ptag);
                        break;
                    }
                case "System.Single":
                    {
                        XhtmlTags.P ptag = tag.OwnerPage.CreateXhtmlP();
                        ptag.AppendText(outvalue.ToString());
                        tag.AppendChild(ptag);
                        break;
                    }
                case "Jeelu.ProductImageData":
                    {
                        
                        break;
                    }
                case "Jeelu.SimplusD.ItemCollection":
                    {
                        //Jeelu.SimplusD.ItemCollection
                        break;
                    }
                default:
                    Debug.Fail(outObjType + " is Error Dispose Type!");
                    break;
            }//switch (outObjType)
        }//private void AttributeTagCreator
    }//class
}