using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Jeelu.Win;
using System.Drawing;
using System.Reflection;

namespace Jeelu.SimplusD
{
    [PageCustom(true)]
    public class HrXmlDocument : CorpDataXmlDocument,ISearch
    {
        public HrXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {
        }
        #region HR元素特别属性

        /// <summary>
        /// 职位名称(ComboBox)
        /// </summary>
        [SnipPart("jobName", "jobName", "jobName", "jobName", 0, 80)]
        [PropertyPad(0, 0, "jobName", MainControlWidth = 120, GroupBoxMainImage = @"Image\hr.png", LabelRight = "BaseLabel2", GroupBoxDockTop = false, IsRed = true, TextMaxLength = 20)]
        [Editor(1, 0, "jobName", MainControlWidth = 180, LabelRight = "BaseLabel2", GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "jobHr", IsRed = true, TextMaxLength = 20,IsCanFind=true)]
        public string JobName
        {
            get
            {
                return this.DocumentElement.GetAttribute("jobName");
            }
            set
            {
                this.DocumentElement.SetAttribute("jobName", value);
            }
        }

        /// <summary>
        /// 职位性质(ComboBox)
        /// </summary>
        [SnipPart("WorkProperty", "WorkProperty", "WorkProperty", "WorkProperty", 0, 80)]
        [PropertyPad(1, 0, "WorkProperty", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
          MainControlBindingFile = "WorkProperty.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        [Editor(1, 1, "WorkProperty", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
       MainControlBindingFile = "WorkProperty.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string WorkProperty
        {
            get
            {
                return this.DocumentElement.GetAttribute("workProperty");
            }
            set
            {
                this.DocumentElement.SetAttribute("workProperty", value);
            }
        }

        /// <summary>
        /// 招聘人数(ComboBox)
        /// </summary>
        [SnipPart("PhireNum", "PhireNum", "PhireNum", "PhireNum", 0, 80)]
        [PropertyPad(2, 0, "PhireNum", MainControlType = MainControlType.NumericUpDown, MainControlWidth = 50, NumericUpDownMin = 1, NumericUpDownMax = 100, NumericUpDownStep = 1)]
        [Editor(1, 3, "PhireNum", MainControlType = MainControlType.NumericUpDown, MainControlWidth = 80, NumericUpDownMin = 1, NumericUpDownMax = 100, NumericUpDownStep = 1)]
        public string HireNum
        {
            get
            {
                string num = Utility.Convert.StringToString(this.DocumentElement.GetAttribute("hireNum"));
                if (string.IsNullOrEmpty(num))
                    return "5";
                else
                    return num;
            }
            set
            {
                this.DocumentElement.SetAttribute("hireNum", Convert.ToString(value));
            }
        }
        /// <summary>
        /// 招聘部门(ComboBox)
        /// </summary>
        [SnipPart("Deparement", "Deparement", "Deparement", "Deparement", 0, 80)]
        [PropertyPad(0, 1, "Deparement", MainControlType = MainControlType.DepartmentNameControl)]
        [Editor(1, 5, "Deparement", MainControlType = MainControlType.DepartmentNameControl)]
        public string DeparementName
        {
            get
            {
                return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("deparementName"));
            }
            set
            {
                this.DocumentElement.SetAttribute("deparementName", Convert.ToString(value));
            }
        }
        /// <summary>
        /// 是否急聘(SelectGroup,CheckBox 急聘)
        /// </summary>
        [SnipPart("PisHot", "PisHot", "PisHot", "PisHot", 0, 80)]
        [PropertyPad(4, 0, "", MainControlType = MainControlType.SimpleCheckBox,
        LabelHelpWidth = -1, SimpleCheckBoxText = "PisHot")]
        [Editor(1, 2, "More", MainControlType = MainControlType.SimpleCheckBox,
       LabelHelpWidth = -1, SimpleCheckBoxText = "PisHot", LabelLeft = "NoTableTextTxt")]
        public bool IsHot
        {
            get
            {
                return Utility.Convert.StringToBool(this.DocumentElement.GetAttribute("isHot"));
            }
            set
            {
                this.DocumentElement.SetAttribute("isHot", Convert.ToString(value));
            }
        }

        /// <summary>
        /// 若干(SelectGroup,CheckBox  若干)
        /// </summary>
        [PropertyPad(3, 0, "", MainControlType = MainControlType.SimpleCheckBox,
        LabelHelpWidth = -1, SimpleCheckBoxText = "Pmany")]
        [SnipPart("Pmany", "Pmany", "Pmany", "Pmany", 0, 80)]
        [Editor(1, 4, "More", MainControlType = MainControlType.SimpleCheckBox,
       LabelHelpWidth = -1, SimpleCheckBoxText = "Pmany", LabelLeft = "NoTableTextTxt")]
        public bool IsSomeOne
        {
            get
            {
                return Utility.Convert.StringToBool(this.DocumentElement.GetAttribute("isSomeOne"));
            }
            set
            {
                this.DocumentElement.SetAttribute("isSomeOne", Convert.ToString(value));
            }
        }
        /// <summary>
        /// 截止时间(DateTimePicker)
        /// </summary>
        /// 
        [SnipPart("EndTime", "EndTime", "EndTime", "EndTime", 0, 80)]
        [Editor(1, 8, "EndTime", MainControlType = MainControlType.DateTimePicker, MainControlWidth = 120, DateTimePickerChecked = true)]
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
        /// 职位类别(DateTimePicker)
        /// </summary>
        [SnipPart("jobType", "jobType", "jobType", "jobType", 0, 80)]
        [Editor(1, 9, "jobType", MainControlType = MainControlType.ComboBoxGroupControl,
        MainControlBindingFile = "JobType.xml", MainControlWidth = 120, SpaceMark = "job")]
        public string[] JobType
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//jobType");
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
                XmlNode node = this.SelectSingleNode("//jobType");
                if (node == null)
                {
                    XmlElement ele = this.CreateElement("jobType");
                    this.DocumentElement.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                node.InnerText = "";
                foreach (var val in value)
                {
                    XmlElement newEle = this.CreateElement("typeItem");
                    newEle.SetAttribute("value", val.ToString());
                    node.AppendChild(newEle);
                }
            }
        }
        /// <summary>
        /// 工作地点(省)(ComboBox)
        /// </summary>
        [SnipPart("WorkPlace", "WorkPlace", "WorkPlace", "WorkPlace", 0, 80)]
        [Editor(1, 10, "PInlocaus", MainControlType = MainControlType.ComboBoxGroupControl,
        MainControlBindingFile = "WorkPlace.xml", MainControlWidth = 120)]
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
        /// 月薪范围(ComboBox)
        /// </summary>
        [SnipPart("priceRequale", "priceRequale", "priceRequale", "priceRequale", 0, 80)]
        [Editor(1, 11, "priceRequale", MainControlType = MainControlType.ComboBox,
MainControlBindingFile = "SalaryDesire.xml", MainControlWidth = 120, ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string PriceLocation
        {
            get
            {
                return this.DocumentElement.GetAttribute("priceLocation");
            }
            set
            {
                this.DocumentElement.SetAttribute("priceLocation", value);
            }
        }
        /// <summary>
        /// 其它待遇(DateTimePicker)
        /// </summary>
        [SnipPart("other", "other", "other", "other", 0, 80)]
        [Editor(1, 12, "other", MainControlWidth = 450, LabelRight = "BaseLabel5", IsRed = true, TextMaxLength = 50,IsCanFind=true )]
        public string Other
        {
            get
            {
                return Utility.Convert.StringToString(this.DocumentElement.GetAttribute("other"));
            }
            set
            {
                this.DocumentElement.SetAttribute("other", value);
            }
        }
        /// <summary>
        /// 职位描述
        /// </summary>
        [SnipPart("jobDescription", "jobDescription", "jobDescription", "jobDescription", 0, 80)]
        [Editor(2, 0, "", MainControlType = MainControlType.HTMLDesignControl, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "jobDescription", LabelFooter = "BaseLabel4", IsRed = true, TextMaxLength = 5000)]
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
        /// 要求学历(ComboBox)
        /// </summary>
        [SnipPart("EduLevel", "EduLevel", "EduLevel", "EduLevel", 0, 80)]
        [Editor(9, 0, "EduLevel", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
  MainControlBindingFile = "EduLevel.xml", ComboBoxStyle = ComboBoxStyle.DropDownList, GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "HrRequare")]
        public string EduLevel
        {
            get
            {
                return this.DocumentElement.GetAttribute("eduLevel");
            }
            set
            {
                this.DocumentElement.SetAttribute("eduLevel", value);
            }
        }
        /// <summary>
        /// 工作经验(ComboBox)
        /// </summary>
        [SnipPart("WorkExperience", "WorkExperience", "WorkExperience", "WorkExperience", 0, 80)]
        [PropertyPad(1, 1, "WorkExperience", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
     MainControlBindingFile = "WorkExperience.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        [Editor(9, 1, "WorkExperience", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
   MainControlBindingFile = "WorkExperience.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string WorkExperience
        {
            get
            {
                return this.DocumentElement.GetAttribute("workExperience");
            }
            set
            {
                this.DocumentElement.SetAttribute("workExperience", value);
            }
        }


        /// <summary>
        /// 性别(SelectGroup RadioButton男，女)
        /// </summary>
        [SnipPart("Sex", "Sex", "Sex", "Sex", 0, 80)]
        [Editor(9, 2, "Sex", MainControlType = MainControlType.ComboBox, MainControlBindingFile = "sex.xml", ComboBoxStyle = ComboBoxStyle.DropDownList, MainControlWidth = 120)]
        public string Sex
        {
            get
            {
                return this.DocumentElement.GetAttribute("sex");
            }
            set
            {
                this.DocumentElement.SetAttribute("sex", value);
            }
        }
        /// <summary>
        /// 年龄(ComboBox)
        /// </summary>
        [SnipPart("Age", "Age", "Age", "Age", 0, 80)]
        [Editor(9, 3, "Age", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
MainControlBindingFile = "Age.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string Age
        {
            get
            {
                return this.DocumentElement.GetAttribute("age");
            }
            set
            {
                this.DocumentElement.SetAttribute("age", value);
            }
        }

        /// <summary>
        /// 语言要求(ComboBox)
        /// </summary>
        [SnipPart("LanguageDesire", "LanguageDesire", "LanguageDesire", "LanguageDesire", 0, 80)]
        [Editor(9, 4, "LanguageDesire", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
  MainControlBindingFile = "LanguageDesire.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string LanguageDesire
        {
            get
            {
                return this.DocumentElement.GetAttribute("LanguageDesire");
            }
            set
            {
                this.DocumentElement.SetAttribute("LanguageDesire", value);
            }
        }

        /// <summary>
        /// 其它语言要求(TextBox)
        /// </summary>
        [SnipPart("LanguageOtherDesire", "LanguageOtherDesire", "LanguageOtherDesire", "LanguageOtherDesire", 0, 80)]
        [Editor(9, 5, "LanguageOtherDesire", MainControlType = MainControlType.ComboBox, MainControlBindingFile = "LanguageDesire.xml", ComboBoxStyle = ComboBoxStyle.DropDownList, MainControlWidth = 120)]
        public string LanguageOtherDesire
        {
            get
            {
                return this.DocumentElement.GetAttribute("languageOtherDesire");
            }
            set
            {
                this.DocumentElement.SetAttribute("languageOtherDesire", value);
            }
        }

        /// <summary>
        /// 出差情况(ComboBox)
        /// </summary>
        [SnipPart("Evection", "Evection", "Evection", "Evection", 0, 80)]
        [Editor(9, 6, "Evection", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
MainControlBindingFile = "OutGo.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string Evection
        {
            get
            {
                return this.DocumentElement.GetAttribute("evection");
            }
            set
            {
                this.DocumentElement.SetAttribute("evection", value);
            }
        }
        /// <summary>
        /// 驾驶执照(ComboBox)
        /// </summary>
        [SnipPart("DriverLicence", "DriverLicence", "DriverLicence", "DriverLicence", 0, 80)]
        [Editor(9, 7, "DriverLicence", MainControlWidth = 120, MainControlType = MainControlType.ComboBox,
MainControlBindingFile = "DriverLicence.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string DriverLicence
        {
            get
            {
                return this.DocumentElement.GetAttribute("driverLicence");
            }
            set
            {
                this.DocumentElement.SetAttribute("driverLicence", value);
            }
        }

        /// <summary>
        /// 其它证件说明(TextBox)
        /// </summary>
        /// 
        [SnipPart("OtherCertificate", "OtherCertificate", "OtherCertificate", "OtherCertificate", 0, 80)]
        [Editor(9, 8, "OtherCertificate", MainControlWidth = 330, LabelRight = "BaseLabel3", IsRed = true, TextMaxLength = 50,IsCanFind=true)]
        public string OtherCertificate
        {
            get
            {
                return this.DocumentElement.GetAttribute("otherCertificate");
            }
            set
            {
                this.DocumentElement.SetAttribute("otherCertificate", value);
            }
        }
        #endregion

        /// <summary>
        /// 获得招聘简历样式
        /// </summary>
        [SnipPart("getResumeMode", "getResumeMode", "getResumeMode", "getResumeMode", 0, 80)]
        [Editor(19, 0, "getResumeMode", MainControlType = MainControlType.SelectGroup, SelectGroupMultiModel = true,
             SelectGroupHorizontalCount = 2, MainControlBindingFile = "ResumeInclude.xml", GroupBoxDockTop = true, GroupBoxUseWinStyle = true, GroupBoxUseWinStyleText = "hrResume")]
        public string[] GroupItemsValue
        {
            get
            {
                XmlNode node = this.SelectSingleNode("//group");
                if (node == null)
                    return new string[0];
                List<string> values = new List<string>();
                if (node.HasChildNodes)
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement ele = (XmlElement)subNode;  ///椤哄簭鐩稿叧
                            values.Add(ele.GetAttribute("value"));
                        }
                    }

                }
                return values.ToArray();
            }
            set
            {
                XmlNode node = this.SelectSingleNode("//group");
                if (node == null)
                {
                    XmlElement ele = this.CreateElement("group");
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
        /// 简历形式(TextBox)
        /// </summary>
        [SnipPart("resumeMode", "resumeMode", "resumeMode", "resumeMode", 0, 80)]
        [Editor(19, 1, "resumeMode", MainControlType = MainControlType.ComboBox, MainControlBindingFile = "ThrowForm.xml", ComboBoxStyle = ComboBoxStyle.DropDownList)]
        public string ResumeMode
        {
            get
            {
                return this.DocumentElement.GetAttribute("resumeMode");
            }
            set
            {
                this.DocumentElement.SetAttribute("resumeMode", value);
            }
        }

        //public override string ToHtml()
        //{
        //    throw new NotImplementedException();
        //}


        #region ISearch 成员

        Position ISearch.SearchNext(WantToDoType type)
        {
            Position position = null;
            GetDataResourceValue getDataResourceValue = new GetDataResourceValue(this);
            switch(type)
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
