using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Jeelu.Win;
using System.Windows.Forms;
using System.Drawing;
namespace Jeelu.SimplusD
{
    [PageCustom(true)]
    public class InviteBiddingXmlDocument : CorpDataXmlDocument, ISearch
    {
        public InviteBiddingXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {
        }
        #region bidding信息的属性
        /// <summary>
        /// 招标标题(TextBox)
        /// </summary> 
        [Editor(1, 0, "BiddingName", MainControlWidth = 180, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "baseBidding", LabelRight = "BaseLabel2", IsRed = true, TextMaxLength = 20, IsCanFind = true)]
        [SnipPart("BiddingName", "BiddingName", "BiddingName", "BiddingName", 0, 80)]
        [PropertyPad(0, 0, "BiddingName", MainControlWidth = 120, GroupBoxMainImage = @"Image\bidd.png", LabelRight = "BaseLabel2", GroupBoxDockTop = false, IsRed = true, TextMaxLength = 20)]
        public string BiddingName
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("biddingName")); }
            set { this.DocumentElement.SetAttribute("biddingName", value); }
        }
        /// <summary>
        /// 招标编号(TextBox)
        /// </summary>
        /// 

        [Editor(1, 1, "Biddingnumber", MainControlType = MainControlType.BuildNumberControl, MainControlWidth = 180, PageName = "ZB")]
        [SnipPart("Biddingnumber", "Biddingnumber", "Biddingnumber", "Biddingnumber", 0, 80)]
        public string Number
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("number")); }
            set { this.DocumentElement.SetAttribute("number", value); }
        }
        /// <summary>
        /// 招标类型(ComboBox)
        /// </summary>
        /// 
        [SnipPart("Biddingtype", "Biddingtype", "Biddingtype", "Biddingtype", 0, 80)]
        [Editor(1, 2, "Biddingtype", MainControlWidth = 120, MainControlType = MainControlType.ComboBoxGroupControl,
        MainControlBindingFile = "BiddingType.xml")]
        public string[] Type
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//biddingType");
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
                XmlNode node = this.SelectSingleNode("//biddingType");
                if (node == null)
                {
                    XmlElement ele = this.CreateElement("biddingType");
                    this.DocumentElement.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                node.InnerText = "";
                foreach (var val in value)
                {
                    XmlElement newEle = this.CreateElement("biddingTypeItem");
                    newEle.SetAttribute("value", val.ToString());
                    node.AppendChild(newEle);
                }
            }
        }
        ///<summary>
        ///招标范围(TextBox)***********************************************
        ///</summary>
        [SnipPart("biddingLocation", "biddingLocation", "biddingLocation", "biddingLocation", 0, 80)]
        [Editor(1, 5, "biddingLocation", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
        MainControlBindingFile = "BiddingLocation.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        [PropertyPad(0, 2, "biddingLocation", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
        MainControlBindingFile = "BiddingLocation.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string BiddingLocation
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("biddingLocation")); }
            set { this.DocumentElement.SetAttribute("biddingLocation", value); }
        }
        /// <summary>
        /// 资金来源(TextBox)
        /// </summary>
        /// 
        [SnipPart("MoneySource", "MoneySource", "MoneySource", "MoneySource", 0, 80)]
        [Editor(1, 7, "MoneySource", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
        MainControlBindingFile = "MoneySource.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string MoneySource
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("moneySource")); }
            set { this.DocumentElement.SetAttribute("moneySource", value); }
        }

        /// <summary>
        /// 项目实施省(ComboBox)
        /// </summary>
        /// 
        [SnipPart("Plocaus", "Plocaus", "Plocaus", "Plocaus", 0, 80)]
        [Editor(1, 6, "Plocaus", MainControlWidth = 200, MainControlType = MainControlType.ComboBoxGroupControl,
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
        /// 资质要求(TextBox)
        /// </summary>
        /// 
        [SnipPart("Condition", "Condition", "Condition", "Condition", 0, 80)]
        [Editor(1, 8, "Condition", MainControlWidth = 350, LabelFooter = "BaseLabel", IsRed = true, TextMaxLength = 100, IsCanFind = true)]
        public string Condition
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("condition")); }
            set { this.DocumentElement.SetAttribute("condition", value); }
        }
        /// <summary>
        ///发布时间(DateTimePicker)
        /// </summary>
        /// 
        [SnipPart("PublicTime", "PublicTime", "PublicTime", "PublicTime", 0, 80)]
        [Editor(8, 0, "PublicTime", MainControlType = MainControlType.DateTimePicker, DateTimePickerChecked = true, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "PublicMessage", MainControlWidth = 120)]
        public DateTime PublicTime
        {
            get
            {
                return Utility.Convert.StringToDateTime(this.DocumentElement.GetAttribute("publicTime"));
            }
            set
            {
                this.DocumentElement.SetAttribute("publicTime", Convert.ToString(value));
            }
        }
        /// <summary>
        /// 发布方式(ComboBox)
        /// </summary>
        /// 
        [SnipPart("PublicMode", "PublicMode", "PublicMode", "PublicMode", 0, 80)]
        [PropertyPad(1, 1, "PublicMode", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
      MainControlBindingFile = "PublicMode.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        [Editor(8, 1, "PublicMode", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
        MainControlBindingFile = "PublicMode.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string PublicMode
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("publicMode")); }
            set { this.DocumentElement.SetAttribute("publicMode", value); }
        }
        /// <summary>
        ///开标时间(DateTimePicker)
        /// </summary>
        /// 
        [SnipPart("StartTime", "StartTime", "StartTime", "StartTime", 0, 80)]
        [Editor(8, 2, "StartTime", MainControlType = MainControlType.DateTimePicker, DateTimePickerChecked = true, MainControlWidth = 120)]
        public DateTime StartTime
        {
            get
            {
                return Utility.Convert.StringToDateTime(this.DocumentElement.GetAttribute("startTime"));
            }
            set
            {
                this.DocumentElement.SetAttribute("startTime", Convert.ToString(value));
            }
        }
        /// <summary>
        /// 截止时间(DateTimePicker)
        /// </summary>
        /// 
        [SnipPart("EndTime", "EndTime", "EndTime", "EndTime", 0, 80)]
        [Editor(8, 3, "EndTime", MainControlType = MainControlType.DateTimePicker, DateTimePickerChecked = true, MainControlWidth = 120)]
        public DateTime EndTime
        {
            get
            {
                return Utility.Convert.StringToDateTime(this.DocumentElement.GetAttribute("endTime"));
            }
            set
            {
                this.DocumentElement.SetAttribute("endTime", Convert.ToString(value));
            }
        }
        /// <summary>
        /// 报名开始时间(DateTimePicker)
        /// </summary>
        /// 
        [SnipPart("ReportStartTime", "ReportStartTime", "ReportStartTime", "ReportStartTime", 0, 80)]
        [Editor(8, 4, "ReportStartTime", MainControlType = MainControlType.DateTimePicker, DateTimePickerChecked = true, MainControlWidth = 120)]
        public DateTime ReportStartTime
        {
            get
            {
                return Utility.Convert.StringToDateTime(this.DocumentElement.GetAttribute("reportStartTime"));
            }
            set
            {
                this.DocumentElement.SetAttribute("reportStartTime", Convert.ToString(value));
            }
        }
        /// <summary>
        /// 报名截止时间(DateTimePicker)
        /// </summary>
        /// 
        [SnipPart("ReportEndTime", "ReportEndTime", "ReportEndTime", "ReportEndTime", 0, 80)]
        [Editor(8, 5, "ReportEndTime", MainControlType = MainControlType.DateTimePicker, DateTimePickerChecked = true, MainControlWidth = 120)]
        public DateTime ReportEndTime
        {
            get
            {
                return Utility.Convert.StringToDateTime(this.DocumentElement.GetAttribute("reportEndTime"));
            }
            set
            {
                this.DocumentElement.SetAttribute("reportEndTime", Convert.ToString(value));
            }
        }
        /// <summary>
        ///开标地点(TextBox)
        /// </summary>
        /// 
        [SnipPart("BiddingAddress", "BiddingAddress", "BiddingAddress", "BiddingAddress", 0, 80)]
        [Editor(8, 10, "BiddingAddress", MainControlWidth = 330, LabelRight = "BaseLabel3", IsRed = true, TextMaxLength = 50, IsCanFind = true)]
        public string BiddingAddress
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("biddingAddress")); }
            set { this.DocumentElement.SetAttribute("biddingAddress", value); }
        }
        /// <summary>
        /// 标书价格(ValidateTextBox RegexText:^\d*(\.\d{1,2})?$ RegexTextRuntime: ^\d*(\.\d{0,2})?$ )
        /// </summary>
        /// 
        [SnipPart("BiddingPrice", "BiddingPrice", "BiddingPrice", "BiddingPrice", 0, 80)]
        [PropertyPad(1, 0, "BiddingPrice", MainControlWidth = 120, MainControlType = MainControlType.ValidateTextBox, ValidateTextBoxRegexText = @"^\d*(\.\d{1,2})?$",
       ValidateTextBoxRegexTextRuntime = @"^\d*(\.\d{0,2})?$", LabelRight = "priceTag")]
        [Editor(8, 6, "BiddingPrice", MainControlWidth = 120, MainControlType = MainControlType.ValidateTextBox, ValidateTextBoxRegexText = @"^\d*(\.\d{1,2})?$",
        ValidateTextBoxRegexTextRuntime = @"^\d*(\.\d{0,2})?$", LabelRight = "priceTag")]
        public float BiddingPrice
        {
            get { return Utility.Convert.StringToFloat(this.DocumentElement.GetAttribute("biddingPrice")); }
            set { this.DocumentElement.SetAttribute("biddingPrice", value.ToString()); }
        }

        /// <summary>
        /// 标书领取方式(ComboBox)
        /// </summary>
        /// 
        [SnipPart("BiddingGetMode", "BiddingGetMode", "BiddingGetMode", "BiddingGetMode", 0, 80)]
        [Editor(8, 7, "BiddingGetMode", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
    MainControlBindingFile = "BiddingGetMode.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string BiddingGetMode
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("biddingGetMode")); }
            set { this.DocumentElement.SetAttribute("biddingGetMode", value); }
        }


        /// <summary>
        /// //有无代理及其相关联系
        /// </summary>
        [SnipPart("AgentMessage", "AgentMessage", "AgentMessage", "AgentMessage", 0, 80)]
        [Editor(9, 0, "", MainControlType = MainControlType.BiddingAgent, MainControlBindingFile = "Agent.xml", GroupBoxUseWinStyle = true, GroupBoxDockTop = true, GroupBoxUseWinStyleText = "AgentMessage")]
        public AgentInfo Agent
        {
            get
            {
                AgentInfo agentInfo = new AgentInfo();
                XmlNode node = this.SelectSingleNode("//agentMessage");
                if (node is XmlElement && node != null)
                {
                    XmlElement ele = (XmlElement)node;
                    if (!string.IsNullOrEmpty(ele.GetAttribute("isAgent")))
                        agentInfo.IsAgent = ele.GetAttribute("isAgent");
                    agentInfo.Agent = ele.GetAttribute("agent");
                    agentInfo.Phone = ele.GetAttribute("agentPhone");
                    agentInfo.AgentUnit = ele.GetAttribute("agentUnit");
                }
                return agentInfo;

            }
            set
            {
                XmlNode node = this.SelectSingleNode("//agentMessage");
                if (node == null)
                {
                    XmlElement newEle = this.CreateElement("agentMessage");
                    this.DocumentElement.AppendChild(newEle);
                    node = (XmlNode)newEle;
                }
                node.RemoveAll();
                if (node is XmlElement)
                {
                    XmlElement ele = (XmlElement)node;
                    ele.SetAttribute("isAgent", value.IsAgent);
                    ele.SetAttribute("agent", value.Agent);
                    ele.SetAttribute("agentPhone", value.Phone);
                    ele.SetAttribute("agentUnit", value.AgentUnit);
                }
            }
        }

        /// <summary>
        /// 标书上传(FileSelecterControl)
        /// </summary>
        /// 
        [SnipPart("BiddingFile", "BiddingFile", "BiddingFile", "BiddingFile", 0, 80)]
        [Editor(10, 0, "BiddingFile", MainControlType = MainControlType.FileSelecterControl, MainControlWidth = 330, GroupBoxUseWinStyle = true, GroupBoxDockTop = true, GroupBoxUseWinStyleText = "BiddingFile")]
        public string BiddingFile
        {
            get { return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("biddingFile")); }
            set { this.DocumentElement.SetAttribute("biddingFile", value); }
        }
        /// <summary>
        /// 招标内容(TextBox 多行的)
        /// </summary>
        ///  
        [SnipPart("biddingContent", "biddingContent", "biddingContent", "biddingContent", 0, 80)]
        [Editor(19, 0, "", MainControlType = MainControlType.HTMLDesignControl, GroupBoxUseWinStyle = true, GroupBoxDockTop = true, GroupBoxUseWinStyleText = "biddingContent", LabelFooter = "BaseLabel4", IsRed = true, TextMaxLength = 5000)]
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