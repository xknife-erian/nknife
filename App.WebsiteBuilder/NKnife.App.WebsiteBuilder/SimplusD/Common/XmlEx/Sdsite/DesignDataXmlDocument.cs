using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    public class DesignDataXmlDocument : AnyXmlDocument
    {
        public DesignDataXmlDocument(string absoluteFilePath)
            : base(absoluteFilePath)
        {
            //_relativeFilePath = relativeFilePath;
        }

        //private string _relativeFilePath;
        //public override string RelativeFilePath
        //{
        //    get { return _relativeFilePath; }
        //}

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            XmlElement returnEle = null;

            switch (localName)
            {
                case "hrSample":
                    returnEle = new HrSampleXmlElement(this);
                    break;
                case "productPropertyGroup":
                    returnEle = new ProductPropertyGroupXmlElement(this);
                    break;
                case "guestSearch":
                    returnEle = new GuestSearchElement(this);
                    break;
                case "searchContent":
                    returnEle = new SearchContentElement(this);
                    break;
                default:
                    returnEle = base.CreateElement(prefix, localName, namespaceURI);
                    break;
            }

            return returnEle;
        }
        public ProductPropertyGroupXmlElement ProductPropGroupEle
        {
            get
            {
                ProductPropertyGroupXmlElement element = this.DocumentElement.SelectSingleNode("productPropertyGroup") as ProductPropertyGroupXmlElement;

                if (element == null)
                {
                    element = (ProductPropertyGroupXmlElement)this.CreateElement("productPropertyGroup");
                    this.DocumentElement.AppendChild(element);
                }

                return element;
            }
        }
        public XmlElement WorkbenchDocuments
        {
            get
            {
                XmlElement element = this.DocumentElement.SelectSingleNode("workbenchDocuments") as XmlElement;

                if (element == null)
                {
                    element = this.CreateElement("workbenchDocuments");
                    this.DocumentElement.AppendChild(element);
                }

                return element;
            }
        }

        public string[] GetTreeOpenItems()
        {
            XmlElement treeOpenItems = (XmlElement)this.DocumentElement.SelectSingleNode("treeOpenItems");

            if (treeOpenItems == null)
            {
                treeOpenItems = this.CreateElement("treeOpenItems");
                this.DocumentElement.AppendChild(treeOpenItems);
            }

            return XmlUtilService.GetGroupItems(treeOpenItems, "item", ItemsDataMode.CData);
        }

        public void SetTreeOpenItems(string[] values)
        {
            XmlElement treeOpenItems = (XmlElement)this.DocumentElement.SelectSingleNode("treeOpenItems");

            if (treeOpenItems == null)
            {
                treeOpenItems = this.CreateElement("treeOpenItems");
                this.DocumentElement.AppendChild(treeOpenItems);
            }

            XmlUtilService.SetGroupItems(treeOpenItems, "item", ItemsDataMode.CData, values);
            this.Save();
        }

        public int HTMLDesignerDesignPanelHeight
        {
            get
            {
                XmlElement designPanelHeight = (XmlElement)this.DocumentElement.SelectSingleNode("designPanelHeight");

                if (designPanelHeight == null)
                {
                    designPanelHeight = this.CreateElement("designPanelHeight");
                    this.DocumentElement.AppendChild(designPanelHeight);
                }
                string heightStr = designPanelHeight.GetAttribute("height");
                if (string.IsNullOrEmpty(heightStr))
                    return -1;
                else
                    return int.Parse(heightStr);
            }
            set
            {
                XmlElement designPanelHeight = (XmlElement)this.DocumentElement.SelectSingleNode("designPanelHeight");

                if (designPanelHeight == null)
                {
                    designPanelHeight = this.CreateElement("designPanelHeight");
                    this.DocumentElement.AppendChild(designPanelHeight);
                }

                designPanelHeight.SetAttribute("height", value.ToString());
                this.Save();
            }
        }

        public InsertTableData HTMLDesignerInsertTableData
        {
            get
            {
                XmlElement insertTableData = (XmlElement)this.DocumentElement.SelectSingleNode("insertTableData");

                if (insertTableData == null)
                {
                    insertTableData = this.CreateElement("insertTableData");
                    this.DocumentElement.AppendChild(insertTableData);
                    return null;
                }
                InsertTableData insertTable = new InsertTableData();
                try
                {
                    insertTable.RowNum = int.Parse(insertTableData.GetAttribute("rowNum"));
                    insertTable.ColNum = int.Parse(insertTableData.GetAttribute("colNum"));
                    insertTable.TableWidth = insertTableData.GetAttribute("tableWidth");
                    insertTable.BorderWidth = int.Parse(insertTableData.GetAttribute("borderWidth"));
                    insertTable.CellSpacing = int.Parse(insertTableData.GetAttribute("cellSpacing"));
                    insertTable.Cellpadding = int.Parse(insertTableData.GetAttribute("cellpadding"));
                    insertTable.Align = insertTableData.GetAttribute("align");
                }
                catch
                { 
                }
                return insertTable;
            }
            set
            {
                XmlElement insertTableData = (XmlElement)this.DocumentElement.SelectSingleNode("insertTableData");

                if (insertTableData == null)
                {
                    insertTableData = this.CreateElement("insertTableData");
                    this.DocumentElement.AppendChild(insertTableData);
                }
                insertTableData.SetAttribute("rowNum", value.RowNum.ToString());
                insertTableData.SetAttribute("colNum", value.ColNum.ToString());
                insertTableData.SetAttribute("borderWidth", value.BorderWidth.ToString());
                insertTableData.SetAttribute("tableWidth", value.TableWidth.ToString());
                insertTableData.SetAttribute("cellSpacing", value.CellSpacing.ToString());
                insertTableData.SetAttribute("cellpadding", value.Cellpadding.ToString());
                insertTableData.SetAttribute("align", value.Align);
                this.Save();
            }
        }

        public InsertPicData HTMLDesignerInsertPicData
        {
            get
            {
                XmlElement picDataEle = (XmlElement)this.DocumentElement.SelectSingleNode("insertPicData");
                if (picDataEle == null)
                    return null;

                InsertPicData picData = new InsertPicData();
                picData.PicAlign = int.Parse(picDataEle.GetAttribute("align"));
                picData.PicHSpace = decimal.Parse(picDataEle.GetAttribute("hSpace"));
                picData.PicVSpace = decimal.Parse(picDataEle.GetAttribute("vSpace"));
                picData.PicFrameWidth = decimal.Parse(picDataEle.GetAttribute("frameWidth"));
                return picData;
            }
            set
            {
                XmlElement picDataEle = (XmlElement)this.DocumentElement.SelectSingleNode("insertPicData");
                if (picDataEle == null)
                {
                    picDataEle = this.CreateElement("insertPicData");
                    this.DocumentElement.AppendChild(picDataEle);
                }

                picDataEle.SetAttribute("align", value.PicAlign.ToString());
                picDataEle.SetAttribute("hSpace", value.PicHSpace.ToString());
                picDataEle.SetAttribute("vSpace", value.PicVSpace.ToString());
                picDataEle.SetAttribute("frameWidth", value.PicFrameWidth.ToString());
                this.Save();
            }
        }

        #region flash / audio / video

        public InsertMediaData GetHTMLDesignerInsertMediaData(MediaFileType type)
        {
            XmlElement element = GetMediaElement(type);

            //如果没有属性,则表示是刚创建的元素
            if (!element.HasAttributes)
                return null;

            InsertMediaData mediaData = new InsertMediaData();
            mediaData.MediaAlign = int.Parse(element.GetAttribute("align"));
            mediaData.MediaHMargin = decimal.Parse(element.GetAttribute("hMargin"));
            mediaData.MediaQuality = int.Parse(element.GetAttribute("quality"));
            mediaData.MediaScale = int.Parse(element.GetAttribute("scale"));
            mediaData.MediaVMargin = decimal.Parse(element.GetAttribute("vMargin"));
            return mediaData;
        }

        public void SetHTMLDesignerInsertMediaData(MediaFileType type, InsertMediaData data)
        {
            XmlElement element = GetMediaElement(type);
            element.SetAttribute("vMargin", data.MediaVMargin.ToString());
            element.SetAttribute("scale", data.MediaScale.ToString());
            element.SetAttribute("quality", data.MediaQuality.ToString());
            element.SetAttribute("hMargin", data.MediaHMargin.ToString());
            element.SetAttribute("align", data.MediaAlign.ToString());

            this.Save();
        }

        private XmlElement GetMediaElement(MediaFileType type)
        {
            switch (type)
            {
                case MediaFileType.Flash:
                    {
                        XmlElement mediaEle = (XmlElement)this.DocumentElement.SelectSingleNode("flashOfMedia");
                        if (mediaEle == null)
                        {
                            mediaEle = this.CreateElement("flashOfMedia");
                            this.DocumentElement.AppendChild(mediaEle);
                        }
                        return mediaEle;
                    }
                case MediaFileType.Audio:
                    {
                        XmlElement mediaEle = (XmlElement)this.DocumentElement.SelectSingleNode("audioOfMedia");
                        if (mediaEle == null)
                        {
                            mediaEle = this.CreateElement("audioOfMedia");
                            this.DocumentElement.AppendChild(mediaEle);
                        }
                        return mediaEle;
                    }
                case MediaFileType.Video:
                    {
                        XmlElement mediaEle = (XmlElement)this.DocumentElement.SelectSingleNode("videoOfMedia");
                        if (mediaEle == null)
                        {
                            mediaEle = this.CreateElement("videoOfMedia");
                            this.DocumentElement.AppendChild(mediaEle);
                        }
                        return mediaEle;
                    }
                default:
                    throw new Exception("MediaFileType is not exist!");
            }
        }

        #endregion

        public class InsertTableData
        {
            public int RowNum { get; set; }
            public int ColNum { get; set; }
            public int BorderWidth { get; set; }
            public string TableWidth { get; set; }
            public int CellSpacing { get; set; }
            public int Cellpadding { get; set; }
            public string Align { get; set; }
        }

        public class InsertPicData
        {
            /// <summary>
            /// 对齐
            /// </summary>
            public int PicAlign { get; set; }

            /// <summary>
            /// 水平间距
            /// </summary>
            public decimal PicHSpace { get; set; }

            /// <summary>
            /// 垂直间距
            /// </summary>
            public decimal PicVSpace { get; set; }

            /// <summary>
            /// 框架粗细
            /// </summary>
            public decimal PicFrameWidth { get; set; }

        }

        public class InsertMediaData
        {
            /// <summary>
            /// 水平边距
            /// </summary>
            public decimal MediaHMargin { get; set; }
            /// <summary>
            /// 垂直边距
            /// </summary>
            public decimal MediaVMargin { get; set; }
            /// <summary>
            /// 对齐
            /// </summary>
            public int MediaAlign { get; set; }
            /// <summary>
            /// 品质
            /// </summary>
            public int MediaQuality { get; set; }
            /// <summary>
            /// 比例
            /// </summary>
            public int MediaScale { get; set; }
        }
    }
}
