using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using Jeelu.Win;
using System.IO;
using System.Reflection;

namespace Jeelu.SimplusD
{
    [PageCustom(true)]
    public class ProductXmlDocument : CorpDataXmlDocument,ISearch
    {
        ItemCollection item = null;
        public ProductXmlDocument(string relativeFilePath,SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath,sdsiteElement)
        {
            item = new ItemCollection(this);
        }
        /// <summary>
        /// 产品名称(TextBox)
        /// </summary> 
        [Editor(1, 0, "ProductName", MainControlWidth = 180, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "baseProperty", LabelRight = "BaseLabel2", IsRed = true,TextMaxLength=20,IsCanFind=true)]
        [PropertyPad(0, 0, "ProductName", MainControlWidth = 120, GroupBoxMainImage = @"Image\product.png", LabelRight = "BaseLabel2", GroupBoxDockTop = false, IsRed = true,TextMaxLength=20)]
        [SnipPart("ProductName", "ProductName", "ProductName", "ProductName", 0, 80)]
        public string ProductName
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("productName")); }
            set { this.DocumentElement.SetAttribute("productName", value); }
        }
        /// <summary>
        /// 产品编号(TextBox)
        /// </summary> 
        [Editor(1, 2, "Productnumber", MainControlWidth = 150, MainControlType = MainControlType.BuildNumberControl, PageName = "PC")]
        [SnipPart("Productnumber", "Productnumber", "Productnumber", "Productnumber", 0, 80)]
        public string Number
        {
            get { return Utility.Convert.StringToString (this.DocumentElement.GetAttribute("number")); }
            set { this.DocumentElement.SetAttribute("number", value); }
        }

        /// <summary>
        /// 产品类型(ComboBox)
        /// </summary>
        /// 
        [Editor(1, 3, "Producttype", MainControlType = MainControlType.ComboBoxGroupControl,
            MainControlBindingFile = "ProductType.xml", MainControlWidth = 80)]
        [SnipPart("Producttype", "Producttype", "Producttype", "Producttype", 0, 80)]
        public string[] MainSort
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//productType");
                if (node == null)
                    return new string[0];
                List<string> values = new List<string>();
                if (node.HasChildNodes)
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement ele = (XmlElement)subNode;
                            values.Add(ele.GetAttribute("value"));
                        }
                    }
                }
                return values.ToArray();
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//productType");
                if (node == null)
                {
                    XmlElement ele = this.CreateElement("productType");
                    this.DocumentElement.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                node.InnerText = "";
                foreach (var val in value)
                {
                    XmlElement newEle = this.CreateElement("type");
                    newEle.SetAttribute("value", val.ToString());
                    node.AppendChild(newEle);
                }
            }
        }  
        /// <summary>
        /// 产品型号
        /// </summary>
        [Editor(1, 6, "TypeNumber", MainControlWidth = 180,IsCanFind=true)]
        [PropertyPad(0, 1, "TypeNumber", MainControlWidth = 120)]
        [SnipPart("TypeNumber", "TypeNumber", "TypeNumber", "TypeNumber", 0, 80)]
        public string TypeNumber
        {
            get { return this.DocumentElement.GetAttribute("typeNumber"); }
            set { this.DocumentElement.SetAttribute("typeNumber", value); }
        }
        /// <summary>
        /// 产品品牌(ComboBox)
        /// </summary>
        /// 
        [Editor(1, 7, "PublishName", MainControlWidth = 180,IsCanFind=true)]
        [PropertyPad(2, 0, "PublishName", MainControlWidth = 120)]
        [SnipPart("PublishName", "PublishName", "PublishName", "PublishName", 0, 80)]
        public string PublishName
        {
            get { return this.DocumentElement.GetAttribute("publishName"); }
            set { this.DocumentElement.SetAttribute("publishName", value); }
        }
        /// <summary>
        /// 产品价格(ComboBox)
        /// </summary>
        /// 
        [SnipPart("Price", "Price", "Price", "Price", 0, 80)]
        [Editor(1, 8, "Price", MainControlWidth = 180,MainControlType = MainControlType.ValidateTextBox,ValidateTextBoxRegexText = @"^\d*(\.\d{1,2})?$" ,
         ValidateTextBoxRegexTextRuntime = @"^\d*(\.\d{0,2})?$",LabelRight="priceTag")]
        public float Price
        {
            get { return Utility.Convert.StringToFloat(this.DocumentElement.GetAttribute("price")); }
            set { this.DocumentElement.SetAttribute("price", value.ToString()); }
        }
        /// <summary>
        ///判断是否允许购买(ComboBox)
        /// </summary>
        /// 
        [SnipPart("Isshop", "Isshop", "Isshop", "Isshop", 0, 80)]
        [Editor(1, 9, "Isshop", MainControlType = MainControlType.SelectGroup, MainControlBindingFile = "IsTrue.xml",
              LabelHelpWidth = -1, SelectGroupHorizontalCount = 2, SelectGroupHIndent = 5, SelectGroupVIndent = 2)]
        public string Isshop
        {
            get { return this.DocumentElement.GetAttribute("isshop"); }
            set { this.DocumentElement.SetAttribute("isshop", value); }
        }
        /// <summary>
        ///库存情况ComboBox)
        /// </summary>
        /// 
        [SnipPart("ram", "ram", "ram", "ram", 0, 80)]
        [PropertyPad(2, 1, "ram", MainControlWidth = 120, MainControlType = MainControlType.NumericUpDown, NumericUpDownMin = 0, NumericUpDownMax = 1000000, NumericUpDownStep = 1)]
        [Editor(1, 10, "ram", MainControlWidth = 80 ,MainControlType = MainControlType.NumericUpDown, NumericUpDownMin = 0,NumericUpDownMax = 1000000,NumericUpDownStep = 1)]
        public string Ram
        {
            get 
            {
                string ram = this.DocumentElement.GetAttribute("ram");
                if (string.IsNullOrEmpty(ram))
                    return "500";
                else
                    return ram;
            }
            set { this.DocumentElement.SetAttribute("ram", value); }
        }
        /// <summary>
        /// 产品小图片的Id存储
        /// </summary>
        /// 
        [SnipPart("ProductSmallImageData", "ProductSmallImageData", "ProductSmallImageData", "ProductSmallImageData", 0, 80)]
        [Editor(2, 0, "", MainControlType = MainControlType.ProductImage, IsBigImage = false, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, ColumnCountOfGroupControl = 2, GroupBoxUseWinStyleText = "productImage")]
        public ProductImageData ProductSmallImage
        {
            get
            {
                ProductImageData data = new ProductImageData();
                XmlNode node = this.SelectSingleNode("//productImageSmall");
                if (node != null && node is XmlElement)
                {
                    XmlElement ele = (XmlElement)node;
                    data.ImageId = ele.GetAttribute("imageId");
                }
                return data;
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//productImageSmall");
                if (node != null)
                    node.RemoveAll();
                else
                {
                    XmlElement ele = this.CreateElement("productImageSmall");
                    this.DocumentElement.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                XmlElement newEle = (XmlElement)node;
                newEle.SetAttribute("imageId", value.ImageId);
            }
        }
        /// <summary>
        /// 产品大图片的Id存储
        /// </summary>
        [SnipPart("ProductBigImageData", "ProductBigImageData", "ProductBigImageData", "ProductBigImageData", 0, 80)]
        [Editor(2, 1, "", MainControlType = MainControlType.ProductImage,IsBigImage=true)]
        public ProductImageData ProductBigImage
        {
            get
            {
                ProductImageData data = new ProductImageData();
                XmlNode node = this.SelectSingleNode("//productImageBig");
                if (node != null && node is XmlElement)
                {
                    XmlElement ele=(XmlElement)node;
                    data.ImageId = ele.GetAttribute("imageId");
                }
                return data;
            }
            set
            {
               XmlNode node= this.SelectSingleNode("//productImageBig");
               if (node != null)
                   node.RemoveAll();
               else
               {
                   XmlElement ele = this.CreateElement("productImageBig");
                   this.DocumentElement.AppendChild(ele);
                   node = (XmlNode)ele;
               }
               if (node is XmlElement)
               {
                   XmlElement newEle = (XmlElement)node;
                   newEle.SetAttribute("imageId", value.ImageId);
               }
            }
        }
        /// <summary>
        ///商品介绍
        /// </summary>
        [SnipPart("productDescription", "productDescription", "productDescription", "productDescription", 0, 80)]
        [Editor(17, 0, "", MainControlType = MainControlType.HTMLDesignControl, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "productDescription", LabelFooter = "BaseLabel4", IsRed = true,TextMaxLength=5000)]
        public string Description
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//description");
                if (node == null)
                    return string.Empty;
                else
                    return node.InnerText;
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//description");
                if (node == null)
                {
                    XmlElement newEle = this.CreateElement("description");
                    this.DocumentElement.AppendChild(newEle);
                    node = (XmlNode)newEle;
                }
                node.RemoveAll();
                XmlCDataSection data = this.CreateCDataSection(value);
                node.AppendChild(data);
            }
        }
        /// <summary>
        ///产品属性组
        /// </summary>
        [SnipPart("productProprety", "productProprety", "productProprety", "productProprety", 0, 80)]
        [Editor(16, 0, "", MainControlType = MainControlType.ProductProperty, GroupBoxUseWinStyle = true, GroupBoxDockTop = true, GroupBoxUseWinStyleText = "productProprety")]
        public ItemCollection ProductPropert
        {
            get 
            {
                item.Clear();
                XmlNode node=this.SelectSingleNode("//productProperty");
                if (node != null)
                {
                    XmlElement ele = (XmlElement)node;
                    item.GroupId = ele.GetAttribute("groupId");
                    item.GroupName = ele.GetAttribute("groupName");
                    XmlNodeList nodes = ele.SelectNodes("//item");
                    if (nodes != null)
                    {
                        foreach (XmlElement itemEle in nodes)
                        {
                            ProductPropertyData data = new ProductPropertyData();
                            data.Name = itemEle.GetAttribute("name");
                            data.Value = itemEle.GetAttribute("value");
                            item.Add(data);
                        }
                    }
                }
                return item;
            }
            set
            {
                XmlNode node =this.DocumentElement.SelectSingleNode("productProperty");
                if(node!=null)
                this.DocumentElement.RemoveChild(node);
                this.DocumentElement.AppendChild(value);
            }
        }


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

