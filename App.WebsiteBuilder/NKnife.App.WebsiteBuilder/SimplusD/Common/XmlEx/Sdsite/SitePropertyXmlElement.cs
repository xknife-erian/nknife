using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Jeelu.Win;
using System.Windows.Forms;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 网站属性数据。存放在.sdsite里。
    /// </summary>
    public class SitePropertyXmlElement : AnyXmlElement
    {
        public SitePropertyXmlElement(XmlDocument doc)
            : base("siteProperty", doc)
        {
        }

        public SiteBasicDataXmlElement SiteBasicData
        {
            get { return (SiteBasicDataXmlElement)this.GetElementByName("siteBasicData"); }
        }

        public SiteShowItemsXmlElement SiteShowItem
        {
            get { return ((SdsiteXmlDocument)OwnerDocument).RootChannel.SiteShowItem; }
        }

    }


    /// <summary>
    /// 网站组织的属性集合
    /// </summary>
    public class SiteBasicDataXmlElement : AnyXmlElement
    {
        public SiteBasicDataXmlElement(XmlDocument doc)
            : base("siteBasicData", doc)
        {
        }

        #region 网站基本信息
        /// <summary>
        /// 组织名称 中文
        /// </summary>
        [PropertyPad(0, 0, "NameOfChinaTxt",
            GroupBoxDockTop = true,
            GroupBoxUseWinStyle = true,
            GroupBoxUseWinStyleText = "SiteNameTxt",
            MainControlWidth = 250
            )]
        public string OrgaNameOfChina
        {
            get { return this.GetAttribute("orgaNameOfChina"); }
            set { this.SetAttribute("orgaNameOfChina", value); }
        }
        /// <summary>
        /// 组织名称 英文
        /// </summary>
        [PropertyPad(0, 1, "NameOfEnglishTxt",
            MainControlWidth = 250
            )]
        public string OrgaNameOfEnglish
        {
            get { return this.GetAttribute("orgaNameOfEnglish"); }
            set { this.SetAttribute("orgaNameOfEnglish", value); }
        }

        ///// <summary>
        ///// 隶属组织(TextBox)
        ///// </summary>
        //[PropertyPad(0, 4, "OfManagerTxt",
        //    MainControlWidth = 150
        //    )]
        //public string OfManager
        //{
        //    get { return this.GetAttribute("ofManager"); }
        //    set { this.SetAttribute("ofManager", value); }
        //}
        #endregion

        #region 关键字推广
        [PropertyPad(1, 0, "",
            GroupBoxDockTop = true,
            GroupBoxUseWinStyle = true,
            GroupBoxUseWinStyleText = "TagsExtendTxt",
            MainControlType = MainControlType.CheckBoxExControl,
            CheckBoxExLabelText = "SaleTagsTxt",
            CheckBoxExText = "ExtendStronglyTxt"
            )]
        public bool SaleExtend
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("saleExtend")); }
            set { this.SetAttribute("saleExtend", Convert.ToString(value)); }
        }
        [PropertyPad(1, 1, "",
         LabelRight = "MulityKeywordsTxt",
         MainControlType = MainControlType.TextBox,
         MainControlWidth = 450
         )]
        public string SaleTags
        {
            get { return this.GetAttribute("saleTags"); }
            set { this.SetAttribute("saleTags", value); }
        }
        [PropertyPad(1, 2, "",
        MainControlType = MainControlType.CheckBoxExControl,
        CheckBoxExLabelText = "StockTagsTxt",
        CheckBoxExText = "ExtendStronglyTxt"
        )]
        public bool StockExtend
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("stockExtend")); }
            set { this.SetAttribute("stockExtend", Convert.ToString(value)); }
        }
        [PropertyPad(1, 3, "",
         LabelRight = "MulityKeywordsTxt",
         MainControlType = MainControlType.TextBox,
         MainControlWidth = 450
         )]
        public string StockTags
        {
            get { return this.GetAttribute("stockTags"); }
            set { this.SetAttribute("stockTags", value); }
        }
        [PropertyPad(1, 4, "",
        MainControlType = MainControlType.CheckBoxExControl,
        CheckBoxExLabelText = "ServiceTagsTxt",
        CheckBoxExText = "ExtendStronglyTxt"
        )]
        public bool ServiceExtend
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("serviceExtend")); }
            set { this.SetAttribute("serviceExtend", Convert.ToString(value)); }
        }
        [PropertyPad(1, 5, "",
         LabelRight = "MulityKeywordsTxt",
         MainControlType = MainControlType.TextBox,
         MainControlWidth = 450
         )]
        public string ServiceTags
        {
            get { return this.GetAttribute("serviceTags"); }
            set { this.SetAttribute("serviceTags", value); }
        }
        #endregion

        #region 网站经营信息
        /// <summary>
        /// 经营模式
        /// </summary>
        [PropertyPad(2, 0, "",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "SiteManagerInfoTxt",
        LabelTop = "SiteManagerModelTxt",
        MainControlType = MainControlType.SelectGroup,
        MainControlBindingFile = "ManageModel.xml",
        SelectGroupMultiModel = true,
        SelectedItemCount = 2,        
        SelectGroupHorizontalCount = 3,
        
        MainControlWidth = 550
         )]
        public string[] OrgaManageInfo
        {
            get
            {
                XmlNode groupNode = this.SelectSingleNode("group");
                if (groupNode == null)
                    return new string[0];
                List<string> lt = new List<string>();
                foreach (XmlNode node in groupNode.ChildNodes)
                {
                    lt.Add(node.InnerText);
                }
                return lt.ToArray();
            }
            set
            {
                XmlNode groupNode = this.SelectSingleNode("group");
                if (groupNode == null)
                {
                    XmlElement groupEle = this.OwnerDocument.CreateElement("group");
                    groupEle.SetAttribute("type", "mode");
                    this.AppendChild(groupEle);
                    groupNode = (XmlElement)groupEle;
                }
                groupNode.InnerText = "";
                foreach (string item in value)
                {
                    XmlElement ele = this.OwnerDocument.CreateElement("item");
                    ele.InnerText = item;
                    groupNode.AppendChild(ele);
                }
            }
        }

        /// <summary>
        /// 网站行业类型
        /// </summary>
        [PropertyPad(2, 1, "IndustryStyleTxt",
            MainControlType = MainControlType.ComboBoxGroupControl,
            MainControlBindingFile = "IndustrySort.xml",
            MainControlWidth = 120
            )]
        public string[] SiteIndustryStyle
        {

            get
            {
                XmlNode node = this.SelectSingleNode("siteIndustryStyle");
                if (node == null)
                    return new string[0];
                List<string> values = new List<string>();
                if (node.HasChildNodes)
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement ele = (XmlElement)subNode;  ///顺序相关
                            values.Add(ele.GetAttribute("value"));
                        }
                    }
                }
                return values.ToArray();
            }
            set
            {
                XmlNode node = this.SelectSingleNode("siteIndustryStyle");
                if (node == null)
                {
                    XmlElement ele = this.OwnerDocument.CreateElement("siteIndustryStyle");
                    this.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                node.InnerText = "";
                foreach (var val in value)
                {
                    XmlElement newEle = this.OwnerDocument.CreateElement("style");
                    newEle.SetAttribute("value", val.ToString());
                    node.AppendChild(newEle);
                }
            }
        }

        /// <summary>
        /// 注册资金
        /// </summary>
        [PropertyPad(2, 2, "",
        LabelRight = "TenThousandTxt",
        LabelLeft = "RegisterFundTxt",
        MainControlType = MainControlType.NumericUpDown ,
        NumericUpDownMax = 10000000000000000,
        NumericUpDownMin = 0,
        NumericUpDownStep = 1,
        MainControlWidth = 100
         )]
        public string RegisterFund
        {
            get 
            {
                string fund = this.GetAttribute("registerFund");
                if (!string.IsNullOrEmpty(fund))
                {
                    return fund;
                }
                else
                    return "0";
            }
            set { this.SetAttribute("registerFund", value.ToString()); }
        }
        /// <summary>
        /// 成立时间
        /// </summary>
        [PropertyPad(2, 3, "BuildTimeTxt",
        MainControlType = MainControlType.DateTimePicker,
        DateTimePickerChecked = true ,
        MainControlWidth = 120
         )]
        public DateTime BuildTime
        {
            get { return Utility.Convert.StringToDateTime(this.GetAttribute("buildTime")); }
            set { this.SetAttribute("buildTime", value.ToString ()); }
        }
        /// <summary>
        /// 注册地点
        /// </summary>
        [PropertyPad(2, 4, "RegisterAddressTxt",
        MainControlType = MainControlType.ComboBoxGroupControl,
        MainControlBindingFile = "Place.xml",
        MainControlWidth = 500
         )]
        public string[] RegisterAddress
        {
            get
            {
                XmlNode node = this.SelectSingleNode("registerAddress");
                if (node == null)
                    return new string[0];
                List<string> values = new List<string>();
                if (node.HasChildNodes)
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement ele = (XmlElement)subNode;  ///顺序相关
                            values.Add(ele.GetAttribute("value"));
                        }
                    }
                }
                return values.ToArray();
            }
            set
            {
                XmlNode node = this.SelectSingleNode("registerAddress");
                if (node == null)
                {
                    XmlElement ele = this.OwnerDocument.CreateElement("registerAddress");
                    this.AppendChild(ele);
                    node = (XmlNode)ele;
                }
                node.InnerText = "";
                foreach (var val in value)
                {
                    XmlElement newEle = this.OwnerDocument.CreateElement("address");
                    newEle.SetAttribute("value", val.ToString());
                    node.AppendChild(newEle);
                }
            }
        }
        /// <summary>
        /// 经营地址
        /// </summary>
        [PropertyPad(2, 5, "ManagerAddressTxt",
        MainControlWidth = 370
        )]
        public string ManagerAddress
        {
            get { return this.GetAttribute("managerAddress"); }
            set { this.SetAttribute("managerAddress", value); }
        }
        #endregion

        #region 部门信息
        /// <summary>
        /// 自定义部门控件
        /// </summary>
        [PropertyPad(3, 0, "",
            GroupBoxDockTop = true ,
            GroupBoxUseWinStyle = true,
            GroupBoxUseWinStyleText = "OrgaDeptInfoTxt",
            MainControlType = MainControlType.DepartmentControl
            )]
        public DepartmentData[] Department
        {
            get
            {
                List<DepartmentData> list = new List<DepartmentData>();
                XmlNode deptsNode = this.SelectSingleNode("departments");
                ///读取xml文件里的所有部门
                foreach (var node in deptsNode.ChildNodes)
                {
                    SiteDepartmentXmlElement deptEle = (SiteDepartmentXmlElement)node;
                    DepartmentData deptData = new DepartmentData();
                    deptData.Id = deptEle.DeptID;
                    deptData.Name = deptEle.DeptName;
                    deptData.Duty = deptEle.DeptDuty;

                    deptData.LinkMan = deptEle.LinkMan;
                    deptData.LinkManSex = deptEle.LinkManSex;
                    deptData.Phone = deptEle.LinkPhone;
                    deptData.MobilePhone = deptEle.LinkMobelPhone;
                    deptData.Fax = deptEle.LinkFax;
                    deptData.Msn = deptEle.LinkMsn;
                    deptData.Email = deptEle.LinkEmail;
                    deptData.Address = deptEle.LinkAddress;
                    deptData.PostCode = deptEle.LinkPostCode;

                    list.Add(deptData);
                }
                return list.ToArray();
            }
            set
            {
                XmlNode deptsNode = this.SelectSingleNode("departments");
                XmlUtilService.RemoveAllChilds((XmlElement)deptsNode);
                foreach (var department in value)
                {
                    SiteDepartmentXmlElement element = new SiteDepartmentXmlElement(this.OwnerDocument);
                    element.DeptID = department.Id;
                    element.DeptName = department.Name;
                    element.DeptDuty = department.Duty;

                    element.LinkMan = department.LinkMan;
                    element.LinkManSex = department.LinkManSex;
                    element.LinkPhone = department.Phone;
                    element.LinkMobelPhone = department.MobilePhone;
                    element.LinkFax = department.Fax;
                    element.LinkMsn = department.Msn;
                    element.LinkEmail = department.Email;
                    element.LinkAddress = department.Address;
                    element.LinkPostCode = department.PostCode;

                    deptsNode.AppendChild(element);
                }
            }
        }

        #endregion

        #region 网站简介
        /// <summary>
        /// 网站简介
        /// </summary>
        [PropertyPad(4, 0, "",
            GroupBoxDockTop = true,
            GroupBoxUseWinStyle = true,
            GroupBoxUseWinStyleText = "SiteBriefTxt",
            MainControlType = MainControlType.HTMLDesignControl,
            MainControlWidth = 400
            )]
        public string SiteBrief
        {
            get
            {
                XmlNode node = this.SelectSingleNode("siteBrief");
                if (node == null)
                    return string.Empty;
                else
                    return node.InnerText;
            }
            set
            {
                XmlNode node = this.SelectSingleNode("siteBrief");
                if (node == null)
                {
                    XmlElement newEle = this.OwnerDocument.CreateElement("siteBrief");
                    this.AppendChild(newEle);
                    node = (XmlNode)newEle;
                }
                node.RemoveAll();
                XmlCDataSection data = this.OwnerDocument.CreateCDataSection(value);
                node.AppendChild(data);
            }
        }
        #endregion

    }


    /// <summary>
    /// 网站其它的显示项
    /// </summary>
    public class SiteShowItemsXmlElement : AnyXmlElement
    {
        public SiteShowItemsXmlElement(XmlDocument doc)
            : base("siteShowItem", doc)
        {
        }

        #region 文章基本功能
        /// <summary>
        /// 显示文章编辑姓名
        /// </summary>
        [PropertyPad(0, 0, "",
            GroupBoxDockTop = true,
            GroupBoxUseWinStyle = true,
            ColumnCountOfGroupControl = 3,
            GroupBoxUseWinStyleText = "ArticleBasicFunctionTxt",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsArticleEditorTxt"
            )]
        public bool IsArticleEditor
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isArticleEditor")); }
            set { this.SetAttribute("isArticleEditor", Convert.ToString(value)); }
        }
        /// <summary>
        /// 显示频道导航信息
        /// </summary>
        [PropertyPad(0, 1, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsShowChannelNaviTxt"
            )]
        public bool IsShowChannelNavi
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isShowChannelNavi")); }
            set { this.SetAttribute("isShowChannelNavi", Convert.ToString(value)); }
        }
        /// <summary>
        /// 显示频道导航信息
        /// </summary>
        [PropertyPad(0, 2, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "ShowClosePageTxt"
            )]
        public bool IsShowClosePage
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isShowClosePage")); }
            set { this.SetAttribute("isShowClosePage", Convert.ToString(value)); }
        }
        /// <summary>
        /// 显示文章日期
        /// </summary>
        [PropertyPad(0, 3, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsArticleDateTxt"
            )]
        public bool IsArticleDate
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isArticleDate")); }
            set { this.SetAttribute("isArticleDate", Convert.ToString(value)); }
        }
        /// <summary>
        /// 显示打印本篇文章功能
        /// </summary>
        [PropertyPad(0, 4, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsPrintTxt"
            )]
        public bool IsPrint
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isPrint")); }
            set { this.SetAttribute("isPrint", Convert.ToString(value)); }
        }
        /// <summary>
        /// 显示PDF功能
        /// </summary>
        [PropertyPad(0, 5, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsPDFTxt"
            )]
        public bool IsPDF
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isPDF")); }
            set { this.SetAttribute("isPDF", Convert.ToString(value)); }
        }
        /// <summary>
        /// 在文章页显示返回按钮
        /// </summary>
        [PropertyPad(0, 6, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsReturnTxt"
            )]
        public bool IsReturn
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isReturn")); }
            set { this.SetAttribute("isReturn", Convert.ToString(value)); }
        }
        /// <summary>
        /// 显示Email发送功能
        /// </summary>
        [PropertyPad(0, 7, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsEmailTxt"
            )]
        public bool IsEmail
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isEmail")); }
            set { this.SetAttribute("isEmail", Convert.ToString(value)); }
        }


        /// <summary>
        /// 显示文章来源
        /// </summary>
        [PropertyPad(0, 8, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsShowArticleSourceTxt"
            )]
        public bool IsShowArticleSource
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isShowArticleSource")); }
            set { this.SetAttribute("isShowArticleSource", Convert.ToString(value)); }
        }
        #endregion

        #region 文章高级功能
        /// <summary>
        /// 文章标题可链接
        /// </summary>
        [PropertyPad(1, 0, "",
            GroupBoxDockTop = true,
            GroupBoxUseWinStyle = true,
            ColumnCountOfGroupControl = 3,
            GroupBoxUseWinStyleText = "ArticleHighFunctionTxt",
            MainControlType = MainControlType.SimpleCheckBox,
            LabelHelpWidth = -1,
            SimpleCheckBoxText = "IsArticleTitleLinkTxt"
            )]
        public bool IsArticleTitleLink
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isArticleTitleLink")); }
            set { this.SetAttribute("isArticleTitleLink", Convert.ToString(value)); }
        }
        /// <summary>
        /// 新闻展示的文字大小
        /// </summary>
        [PropertyPad(1, 1, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "NewWordSizeTxt"
            )]
        public bool NewWordSize
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isNewWordSize")); }
            set { this.SetAttribute("isNewWordSize", Convert.ToString(value)); }
        }
        /// <summary>
        /// 显示浏览文章心情
        /// </summary>
        [PropertyPad(1, 2, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "faceOfLookArticleTxt"
            )]
        public bool FaceOfLookArticle
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("faceOfLookArticle")); }
            set { this.SetAttribute("faceOfLookArticle", Convert.ToString(value)); }
        }
        /// <summary>
        /// 文章大约分页字数
        /// </summary>
        [PropertyPad(1, 3, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "IsPaginationTxt"
            )]
        public bool IsPagination
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isPagination")); }
            set { this.SetAttribute("isPagination", Convert.ToString(value)); }
        }
        /// <summary>
        /// 是否支持文章观点
        /// </summary>
        [PropertyPad(1, 4, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "AgreeOpinionTxt"
            )]
        public bool AgreeOpinion
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isAgreeOpinion")); }
            set { this.SetAttribute("isAgreeOpinion", Convert.ToString(value)); }
        }

        /// <summary>
        ///网友意见留言板
        /// </summary>
        [PropertyPad(1, 5, "",
            MainControlType = MainControlType.SimpleCheckBox,
            SimpleCheckBoxText = "LeaveWordTxt"
            )]
        public bool LeaveWord
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isLeaveWord")); }
            set { this.SetAttribute("isLeaveWord", Convert.ToString(value)); }
        }
        ///// <summary>
        ///// 文章可评分评级
        ///// </summary>
        ////    <text name="IsArticleGrade" >文章可评分评级</text>
        //[PropertyPad(1, 2, "",
        //    MainControlType = MainControlType.SimpleCheckBox,
        //    SimpleCheckBoxText = "%文章可评分评级"
        //    )]
        //public bool IsArticleGrade
        //{
        //    get { return Utility.Convert.StringToBool(this.GetAttribute("isArticleGrade")); }
        //    set { this.SetAttribute("isArticleGrade", Convert.ToString(value)); }
        //}
        #endregion

        #region 时间格式、关键字间隔符
        /// <summary>
        /// 时间格式
        /// </summary>
        [PropertyPad(2, 0, "DateFormatTxt",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "ChannelDateFormatTxt",
        MainControlType = MainControlType.ComboBox,
        ComboBoxStyle = ComboBoxStyle.DropDownList,
        MainControlBindingFile = "DateFormat.xml"
        )]
        public string DateFormat
        {
            get { return this.GetAttribute("dateFormat"); }
            set { this.SetAttribute("dateFormat", value); }
        }
        /// <summary>
        /// 关键字间隔符
        /// </summary>
        [PropertyPad(3, 0, "SpaceSymbolTxt",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "PageTagsSpaceSymbolTxt",
        MainControlWidth = 300
        )]
        public string KeyWordSymbol
        {
            get { return this.GetAttribute("spaceMark"); }
            set { this.SetAttribute("spaceMark", value); }
        }
        /// <summary>
        /// 类型之间需连接符
        /// </summary>
        [PropertyPad(4, 0, "ConnectSymbolTxt",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "StyleSpaceSymbolTxt",
        LabelRight = "StyleSpaceSymbolBriefTxt",
        MainControlWidth = 200
        )]
        public string ProductTypeLinkSymbol
        {
            get { return this.GetAttribute("productTypeLinkSymbol"); }
            set { this.SetAttribute("productTypeLinkSymbol", value); }
        }
        /// <summary>
        /// 显示空字符
        /// </summary>
        [PropertyPad(5, 0, "NoTableTextTxt",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "ProductPropertyTxt",
        MainControlType = MainControlType.SimpleCheckBox,
        SimpleCheckBoxText = "ShowNothingTxt"
        )]
        public bool ShowEmptySymbol
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isEmptySymbol")); }
            set { this.SetAttribute("isEmptySymbol", Convert.ToString(value)); }
        }
        /// <summary>
        /// 商品属性名与其值的连接符号
        /// </summary>
        [PropertyPad(5, 1, "",
        LabelLeft = "ConnectSymbolTxt",
        LabelRight = "PropertyValueSpaceSymbolTxt",
        MainControlWidth = 200
        )]
        public string ProductLinkSymbol
        {
            get { return this.GetAttribute("linkSymbol"); }
            set { this.SetAttribute("linkSymbol", value); }
        }
        /// <summary>
        /// 商品图片的宽度(默认自动)
        /// </summary>
        [PropertyPad(5, 2, "ImageWidthTxt",
        LabelRight = "ImageAutosizeTxt",
        MainControlWidth = 80
        )]
        public string ProductImageWidth
        {
            get { return this.GetAttribute("productImageWidth"); }
            set { this.SetAttribute("productImageWidth", value); }
        }
        /// <summary>
        /// 商品图片的高度(默认自动)
        /// </summary>
        [PropertyPad(5, 3, "ImageHeightTxt",
        LabelRight = "ImageAutosizeTxt",
        MainControlWidth = 80
        )]
        public string ProductImageHeight
        {
            get { return this.GetAttribute("productImageHeight"); }
            set { this.SetAttribute("productImageHeight", value); }
        }
        /// <summary>
        ///显示空字符(针对有些没填写的值)
        /// </summary>
        [PropertyPad(6, 0, "NoTableTextTxt",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "DeptRecordTxt",
        MainControlType = MainControlType.SimpleCheckBox ,
        SimpleCheckBoxText = "ShowNothingTxt"
        )]
        public bool ShowDeptEmptySymbol
        {
            get { return Utility.Convert.StringToBool(this.GetAttribute("isDeptEmptySymbol")); }
            set { this.SetAttribute("isDeptEmptySymbol", Convert.ToString(value)); }
        }
        [PropertyPad(6, 1, "SpaceSymbolTxt",
        LabelRight = "DeptRecSpaceSymbolTxt",
        MainControlWidth = 200
        )]
        public string DeptRecordLinkSymbol
        {
            get { return this.GetAttribute("deptRecordLinkSymbol"); }
            set { this.SetAttribute("deptRecordLinkSymbol", value); }
        }
        /// <summary>
        ///货币符号
        /// </summary>
        [PropertyPad(7, 0, "",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "ProjectPartTxt",
        MainControlType = MainControlType.ComboBox,
        ComboBoxStyle = ComboBoxStyle.DropDownList,
        MainControlBindingFile = "MoneyUnit.xml",
        LabelLeft = "MoneySymbolTxt"
        )]
        public string MoneyFormat
        {
            get { return this.GetAttribute("moneyFormat"); }
            set { this.SetAttribute("moneyFormat", value); }
        }

        
        /// <summary>
        /// 星期格式
        /// </summary>
        [PropertyPad(8, 0, "",
        GroupBoxDockTop = true,
        GroupBoxUseWinStyle = true,
        GroupBoxUseWinStyleText = "FormatTxt",
        MainControlType = MainControlType.ComboBox,
        ComboBoxStyle = ComboBoxStyle.DropDownList,
        MainControlBindingFile = "WeekFormat.xml",
        LabelLeft = "WeekFormatTxt"
         )]
        public string WeekFormat
        {
            get { return this.GetAttribute("weekFormat"); }
            set { this.SetAttribute("weekFormat", value); }
        }
         /// <summary>
        /// 日期格式
        /// </summary>
        [PropertyPad(8, 1, "",
        MainControlType = MainControlType.ComboBox,
        ComboBoxStyle = ComboBoxStyle.DropDownList,
        MainControlBindingFile = "DateFormat.xml",
        LabelLeft = "DateFormatTxt"
        )]
        public string ProjectDateFormat
        {
            get { return this.GetAttribute("projectDateFormat"); }
            set { this.SetAttribute("projectDateFormat", value); }
        }
        /// <summary>
        /// 时间格式
        /// </summary>
        [PropertyPad(8, 2, "",
        MainControlType = MainControlType.ComboBox ,
        ComboBoxStyle = ComboBoxStyle.DropDownList,
        MainControlBindingFile = "TimeFormat.xml",
        LabelLeft = "TimeFormatTxt"
         )]
        public string TimeFormat
        {
            get { return this.GetAttribute("timeFormat"); }
            set { this.SetAttribute("timeFormat", value); }
        }
        #endregion

    }

}

