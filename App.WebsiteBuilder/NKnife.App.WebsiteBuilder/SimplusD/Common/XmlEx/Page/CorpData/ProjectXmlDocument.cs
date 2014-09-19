using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD
{
    [PageCustom(true)]
    public class ProjectXmlDocument : CorpDataXmlDocument,ISearch
    {
        public ProjectXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {

        }

        #region Project 元素的具体属性
        /// <summary>
        /// 项目名称(TextBox)
        /// </summary>
        /// 
        [SnipPart("projectName", "projectName", "projectName", "projectName", 0, 80)]
        [PropertyPad(0, 0, "projectName", MainControlWidth = 120, GroupBoxMainImage = @"Image\project.png", LabelRight = "BaseLabel2", GroupBoxDockTop = false, IsRed = true,TextMaxLength=20)]
        [Editor(1, 0, "projectName", MainControlWidth = 180, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "baseProject", LabelRight = "BaseLabel2", IsRed = true,TextMaxLength=20,IsCanFind=true)]    
        public string ProjectName
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("projectName")); }
            set { this.DocumentElement.SetAttribute("projectName", value); }
        }
        /// <summary>
        /// 项目编号(TextBox)
        /// </summary>
        /// 
        [SnipPart("projectNumber", "projectNumber", "projectNumber", "projectNumber", 0, 80)]
        [Editor(1, 2, "projectNumber", MainControlWidth = 180, MainControlType = MainControlType.BuildNumberControl,PageName="XM")]
        public string Number
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("number")); }
            set { this.DocumentElement.SetAttribute("number", value); }
        }
        /// <summary>
        /// 项目类型(ComboBox)
        /// </summary>
        [SnipPart("projectType", "projectType", "projectType", "projectType", 0, 80)]
        [Editor(1, 3, "projectType", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
        MainControlBindingFile = "ProjectType.xml",ComboBoxStyle=ComboBoxStyle.DropDownList)]
        [PropertyPad(0, 1, "projectType", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
        MainControlBindingFile = "ProjectType.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string Type
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("type")); }
            set { this.DocumentElement.SetAttribute("type", value); }
        }
        /// <summary>
        /// 项目成本预算(ValidateTextBox RegexText:^\d*(\.\d{1,2})?$ RegexTextRuntime: ^\d*(\.\d{0,2})?$ )
        /// </summary>
        /// 
        [SnipPart("Cost", "Cost", "Cost", "Cost", 0, 80)]
        [Editor(1, 4, "Cost", MainControlWidth = 100, MainControlType = MainControlType.ValidateTextBox, ValidateTextBoxRegexText = @"^\d*(\.\d{1,2})?$",
        ValidateTextBoxRegexTextRuntime = @"^\d*(\.\d{0,2})?$", LabelRight = "priceTag")]
        public float Cost
        {
            get { return Utility.Convert.StringToFloat(this.DocumentElement.GetAttribute("cost")); }
            set { this.DocumentElement.SetAttribute("cost", value.ToString()); }
        }
        ///<summary>
        /// 项目实施地区(ComboBox)
        /// </summary>
        ///  
        [SnipPart("Plocaus", "Plocaus", "Plocaus", "Plocaus", 0, 80)]
        [Editor(1, 5, "Plocaus",
            MainControlType = MainControlType.ComboBoxGroupControl,
            MainControlBindingFile = "WorkPlace.xml")]
        public string[] Area
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//area");
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
                XmlNode node = this.SelectSingleNode("//area");
                if (node == null)
                {
                    XmlElement ele = this.CreateElement("area");
                    this.DocumentElement.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                node.InnerText = "";
                foreach (var val in value)
                {
                    XmlElement newEle = this.CreateElement("item");
                    newEle.SetAttribute("value", val.ToString());
                    node.AppendChild(newEle);
                }
            }
        }
        /// <summary>
        /// 项目开始时间(DateTimePicker)
        /// </summary>
        [PropertyPad(1, 0, "PstartDate", MainControlType = MainControlType.DateTimePicker)]
        [SnipPart("PstartDate", "PstartDate", "PstartDate", "PstartDate", 0, 80)]
        [Editor(1, 6, "PstartDate", MainControlType = MainControlType.DateTimePicker, MainControlWidth = 120, DateTimePickerChecked = true)]
        public DateTime StartDate
        {
            get { return Utility.Convert.StringToDateTime(this.DocumentElement.GetAttribute("startDate")); }
            set { this.DocumentElement.SetAttribute("startDate", value.ToString(Utility.Const.TimeFormat)); }
        }
        /// <summary>
        /// 项目结束时间(DateTimePicker)
        /// </summary> 
        [PropertyPad(1, 1, "PendDate", MainControlType = MainControlType.DateTimePicker)]
        [SnipPart("PendDate", "PendDate", "PendDate", "PendDate", 0, 80)]
        [Editor(1, 7, "PendDate", MainControlType = MainControlType.DateTimePicker, MainControlWidth = 120, DateTimePickerChecked = true)]
        public DateTime EndDate
        {
            get { return Utility.Convert.StringToDateTime(this.DocumentElement.GetAttribute("endDate")); }
            set { this.DocumentElement.SetAttribute("endDate", value.ToString(Utility.Const.TimeFormat)); }
        }
        /// <summary>
        /// 项目介绍
        /// </summary>
        [SnipPart("projectDes", "projectDes", "projectDes", "projectDes", 0, 80)]
        [Editor(3, 0, "", MainControlType = MainControlType.HTMLDesignControl, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "projectDes", LabelFooter = "BaseLabel4", IsRed = true,TextMaxLength=5000)]
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
        /// 项目市场市场前景分析(TextBox 多行的)
        /// </summary>
        /// 
        [SnipPart("projectMarketAnalysis", "projectMarketAnalysis", "projectMarketAnalysis", "projectMarketAnalysis", 0, 80)]
        [Editor(4, 0, "", MainControlType = MainControlType.HTMLDesignControl, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "projectMarketAnalysis", LabelFooter = "BaseLabel4",IsRed=true,TextMaxLength=5000)]
        public string ProjectMarketAnalysis
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//projectMarketAnalysis");
                if (node == null)
                    return string.Empty;
                else
                    return node.InnerText;
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//projectMarketAnalysis");
                if (node == null)
                {
                    XmlElement newEle = this.CreateElement("projectMarketAnalysis");
                    this.DocumentElement.AppendChild(newEle);
                    node = (XmlNode)newEle;
                }
                node.RemoveAll();
                XmlCDataSection data = this.CreateCDataSection(value);
                node.AppendChild(data);
            }
        }
        /// <summary>
        /// 项目阶段
        /// </summary>
        [SnipPart("projectPart", "projectPart", "projectPart", "projectPart", 0, 80)]
        [Editor(6, 0, "", MainControlType = MainControlType.ProjectPartControl, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "projectPart")]
        public ProjectPart[] ProjectPartsElement
        {
            get
            {
                List<ProjectPart> partList = new List<ProjectPart>();
                XmlNode node = this.SelectSingleNode("//ProjectParts");
                if (node != null)
                {
                    foreach (XmlNode nodetag in node.ChildNodes)
                    {
                        ProjectPart part = new ProjectPart();
                        XmlElement ele = (XmlElement)nodetag;
                        if (!string.IsNullOrEmpty(ele.GetAttribute("isDoing")))
                        {
                            part.IsDoing = Utility.Convert.StringToBool(ele.GetAttribute("isDoing"));
                        }
                        part.PartName = ele.GetAttribute("name");
                        if (ele.GetAttribute("cost") != null)
                        {
                            part.partCost = Convert.ToDouble(ele.GetAttribute("cost"));
                        }
                        part.PartStartTime = Convert.ToDateTime(ele.GetAttribute("startTime"));
                        part.PartEndTime = Convert.ToDateTime(ele.GetAttribute("endTime"));
                        partList.Add(part);
                    }
                }
                return partList.ToArray();
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//ProjectParts");
                if (node != null)
                {
                    node.RemoveAll();
                }
                else
                {
                    XmlElement element = this.CreateElement("ProjectParts");
                    this.DocumentElement.AppendChild(element);
                    node = (XmlNode)element;
                }
                foreach (var item in value)
                {
                    XmlElement newEle = this.CreateElement("ProjectPart");
                    newEle.SetAttribute("isDoing", item.IsDoing.ToString());
                    newEle.SetAttribute("name", item.PartName);
                    newEle.SetAttribute("cost", item.partCost.ToString());
                    newEle.SetAttribute("startTime", item.PartStartTime.ToString());
                    newEle.SetAttribute("endTime", item.PartEndTime.ToString());
                    node.AppendChild(newEle);
                }
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