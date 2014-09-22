using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class StyleXmlDocument : AnyXmlDocument
    {
        public StyleXmlDocument()
            :base(null)//todo:
        {
        }
        
        #region 内部变量

        #endregion

        #region 公共属性

        /// <summary>
        /// 样式类型
        /// </summary>
        public StyleType StyleType
        { 
            get 
            { 
                string _typeStr =  DocumentElement.GetAttribute("type");
                if (string.IsNullOrEmpty(_typeStr))
                {
                    DocumentElement.SetAttribute("type", StyleType.GeneralPageListPart.ToString());
                    return StyleType.GeneralPageListPart;
                }
                else
                {
                    try
                    {
                        StyleType _tempType = (StyleType)Enum.Parse(typeof(StyleType), _typeStr);
                        return _tempType;
                    }
                    catch (Exception)
                    {
                        DocumentElement.SetAttribute("type", StyleType.GeneralPageListPart.ToString());
                        return StyleType.GeneralPageListPart;
                    }

                }

            }
            set { DocumentElement.SetAttribute("type", value.ToString()); }
        }        

        /// <summary>
        /// 样式节点
        /// </summary>
        public StyleXmlElement StyleElement 
        {
            get
            {
                StyleXmlElement ele = (StyleXmlElement)DocumentElement.SelectSingleNode("style");
                if (ele == null)
                {
                    ele = new StyleXmlElement(this);
                    XmlElement partsEle = this.CreateElement("parts");
                    ele.AppendChild(partsEle);
                    DocumentElement.AppendChild(ele);
                }
                return ele;
            }
            set 
            {
                if(value != null)
                    DocumentElement.ReplaceChild(value,DocumentElement.SelectSingleNode("style"));                
            }
        }

        /// <summary>
        /// 样式文件的路径
        /// </summary>
        public string StyleFilePath { get; private set; }

        /// <summary>
        /// 获取样式标签文本
        /// </summary>
        public string StyleLableText 
        {
            get 
            {
                if (StyleElement == null)
                {
                    return "";
                }
                XmlElement partsEle = StyleElement.GetPartsElement();
                if (partsEle == null || partsEle.ChildNodes.Count < 1)
                {
                    return "";
                }

                XmlNodeList nodes = partsEle.SelectNodes("//part");
                string value = "";
                foreach (XmlNode node in nodes)
                {
                    AnyXmlElement ele = node as AnyXmlElement;
                    string type = ele.GetAttribute("type");
                    switch (type.ToLower())
                    {
                        case "static":
                            value = value + "静态  ";
                            break;
                        case "attribute":
                            value = value + ele.CDataValue + "  ";
                            break;
                        default:
                            break;
                    }
                }
                return value;
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 静态方法：获取样式文件
        /// </summary>
        public static StyleXmlDocument GetStyleDocument(StyleType type, string fileName)
        {
            StyleXmlDocument doc = new StyleXmlDocument();
            string path = GetPath(type, fileName);
            if (!File.Exists(path))
            {
                return CreatNewStyleDocument(type, fileName);
            }
            doc.Load(path);
            doc.StyleFilePath = path;
            return doc;
        }

        /// <summary>
        /// 静态方法：创建新的样式文件
        /// </summary>
        public static StyleXmlDocument CreatNewStyleDocument(StyleType type,string fileName)
        {
            StyleXmlDocument doc = new StyleXmlDocument();
            string strXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?><root></root>";
            doc.LoadXml(strXml);
            doc.StyleType = type;
            doc.StyleFilePath = GetPath(type,fileName);
            doc.StyleElement.Width = "500px";
            doc.StyleElement.Height = "400px";
            doc.Save(doc.StyleFilePath);
            return doc;           
        }

        /// <summary>
        /// 静态方法：获得样式文件路径
        /// </summary>
        public static string GetPath(StyleType type, string fileName)
        {
            string _stylePath = GetDirectoryPath(type);            
            string filePath = Path.Combine(_stylePath, fileName + ".sdStyle");
            return filePath;
        }

        /// <summary>
        /// 静态方法：获得样式文件夹路径
        /// </summary>
        public static string GetDirectoryPath(StyleType type)
        {
            string _stylePath = "";
            switch (type)
            {
                #region
                case StyleType.GeneralPageListPart:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "GeneralPageListPart");
                    break;
                case StyleType.ProductPageListPart:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "ProductPageListPart");
                    break;
                case StyleType.ProjectPageListPart:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "ProjectPageListPart");
                    break;
                case StyleType.InviteBiddingPageListPart:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "InviteBiddingPageListPart");
                    break;
                case StyleType.KnowledgePageListPart:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "KnowledgePageListPart");
                    break;
                case StyleType.HrPageListPart:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "HrPageListPart");
                    break;
                case StyleType.HomePageListPart:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "HomePageListPart");
                    break;
                case StyleType.GeneralPageContent:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "GeneralPageContent");
                    break;
                case StyleType.ProductPageContent:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "ProductPageContent");
                    break;
                case StyleType.ProjectPageContent:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "ProjectPageContent");
                    break;
                case StyleType.InviteBiddingPageContent:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "InviteBiddingPageContent");
                    break;
                case StyleType.KnowledgePageContent:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "KnowledgePageContent");
                    break;
                case StyleType.HrPageContent:
                    _stylePath = Path.Combine(PathService.SoftwarePath, "HrPageContent");
                    break;
                default:
                    break;
                #endregion
            }
            return _stylePath;
        }

        public override System.Xml.XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            switch (localName.ToLower())
            {
                case "style":
                    return new StyleXmlElement(this);
                case "part":
                    return new SnipPartXmlElement(this);
                case "rect":
                    return new SnipRectXmlElement(this);
                case "parts":
                    return new SnipPartsXmlElement(this);                
                default:
                    return new AnyXmlElement(localName, this);
            }
        }

        #endregion

        #region 内部方法

        #endregion
        
        #region 事件响应

        #endregion

        #region 自定义事件

        #endregion
    }
}
