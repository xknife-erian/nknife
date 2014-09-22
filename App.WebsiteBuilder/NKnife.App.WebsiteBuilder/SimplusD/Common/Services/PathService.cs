using System.Xml;
using System.IO;
using System.Windows.Forms;

///此文件是用生成器自动生成，请勿手动更改其代码
namespace Jeelu.SimplusD
{
    static public class PathService
    {
        static string _softwarePath;
        static public string SoftwarePath { get { return _softwarePath; } }
        static public string ProjectPath { get; set; }

        static public void Initialize(string softwarePath)
        {
            _softwarePath = softwarePath;
            using (XmlReader reader = XmlReader.Create(Path.Combine(softwarePath, "Path.xml")))
            {
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    switch (reader.Name)
                    {
                        case "directory":
                        case "file":
                            {
                                switch (reader.GetAttribute("name"))
                                {
                                    case "Xslt_Channelimg":
                                        _xslt_Channelimg = reader.GetAttribute("path");
                                        break;
                                    case "Xslt_ContentSort":
                                        _xslt_ContentSort = reader.GetAttribute("path");
                                        break;
                                    case "Xslt_Pub2Svr":
                                        _xslt_Pub2Svr = reader.GetAttribute("path");
                                        break;
                                    case "Xslt_PageID":
                                        _xslt_PageID = reader.GetAttribute("path");
                                        break;
                                    case "Xslt_ContentTitleList":
                                        _xslt_ContentTitleList = reader.GetAttribute("path");
                                        break;
                                    case "Xslt_Text":
                                        _xslt_Text = reader.GetAttribute("path");
                                        break;
                                    case "Xslt_Folder":
                                        _xslt_Folder = reader.GetAttribute("path");
                                        break;
                                    case "USER_Folder":
                                        _uSER_Folder = reader.GetAttribute("path");
                                        break;
                                    case "TempForUpdate_Folder":
                                        _tempForUpdate_Folder = reader.GetAttribute("path");
                                        break;
                                    case "JsPlugIns_Folder":
                                        _jsPlugIns_Folder = reader.GetAttribute("path");
                                        break;
                                    case "Icon_Folder":
                                        _icon_Folder = reader.GetAttribute("path");
                                        break;
                                    case "HtmlPlugIns_Folder":
                                        _htmlPlugIns_Folder = reader.GetAttribute("path");
                                        break;
                                    case "Help_Folder":
                                        _help_Folder = reader.GetAttribute("path");
                                        break;
                                    case "DtdData_Folder":
                                        _dtdData_Folder = reader.GetAttribute("path");
                                        break;
                                    case "Config_FontList":
                                        _config_FontList = reader.GetAttribute("path");
                                        break;
                                    case "Config_GlobalSetting":
                                        _config_GlobalSetting = reader.GetAttribute("path");
                                        break;
                                    case "Config_Config":
                                        _config_Config = reader.GetAttribute("path");
                                        break;
                                    case "Config_LayoutConfig":
                                        _config_LayoutConfig = reader.GetAttribute("path");
                                        break;
                                    case "Config_PadLayout":
                                        _config_PadLayout = reader.GetAttribute("path");
                                        break;
                                    case "Config_RecentFiles":
                                        _config_RecentFiles = reader.GetAttribute("path");
                                        break;
                                    case "Config_RecordInfo":
                                        _config_RecordInfo = reader.GetAttribute("path");
                                        break;
                                    case "Config_HistoryInputRecord":
                                        _config_HistoryInputRecord = reader.GetAttribute("path");
                                        break;
                                    case "Config_Folder":
                                        _config_Folder = reader.GetAttribute("path");
                                        break;
                                    case "CL_StartupPage_Folder":
                                        _cL_StartupPage_Folder = reader.GetAttribute("path");
                                        break;
                                    case "CL_Resources_Folder":
                                        _cL_Resources_Folder = reader.GetAttribute("path");
                                        break;
                                    case "CL_Dialog_Folder":
                                        _cL_Dialog_Folder = reader.GetAttribute("path");
                                        break;
                                    case "CL_DataSources_Folder":
                                        _cL_DataSources_Folder = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_Age":
                                        _cL_DS_Age = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_Agent":
                                        _cL_DS_Agent = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_AvailaDate":
                                        _cL_DS_AvailaDate = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BiddingGetMode":
                                        _cL_DS_BiddingGetMode = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BiddingType":
                                        _cL_DS_BiddingType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BiddProjectIsIndustry":
                                        _cL_DS_BiddProjectIsIndustry = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BiddPubType":
                                        _cL_DS_BiddPubType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BiddStyle":
                                        _cL_DS_BiddStyle = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BidType":
                                        _cL_DS_BidType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BorderPart":
                                        _cL_DS_BorderPart = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BorderStyle":
                                        _cL_DS_BorderStyle = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BorderWidth":
                                        _cL_DS_BorderWidth = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_BorderWidthUnit":
                                        _cL_DS_BorderWidthUnit = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ChannelType":
                                        _cL_DS_ChannelType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_City":
                                        _cL_DS_City = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ContentSortItem":
                                        _cL_DS_ContentSortItem = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_CredentialType":
                                        _cL_DS_CredentialType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_Department":
                                        _cL_DS_Department = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_DepartmentFunction":
                                        _cL_DS_DepartmentFunction = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_DriverLicence":
                                        _cL_DS_DriverLicence = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_DetailDate":
                                        _cL_DS_DetailDate = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_EduLevel":
                                        _cL_DS_EduLevel = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_FunctionPlace":
                                        _cL_DS_FunctionPlace = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_FundSourceType":
                                        _cL_DS_FundSourceType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_Industry":
                                        _cL_DS_Industry = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_IndustrySort":
                                        _cL_DS_IndustrySort = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ItemType":
                                        _cL_DS_ItemType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_KnowType":
                                        _cL_DS_KnowType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_Language":
                                        _cL_DS_Language = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_LanguageDesire":
                                        _cL_DS_LanguageDesire = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_LanguageLevel":
                                        _cL_DS_LanguageLevel = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ManageExperience":
                                        _cL_DS_ManageExperience = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_OffsetTime":
                                        _cL_DS_OffsetTime = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_OrganationType":
                                        _cL_DS_OrganationType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_OutGo":
                                        _cL_DS_OutGo = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_PasswordQuestion":
                                        _cL_DS_PasswordQuestion = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_Place":
                                        _cL_DS_Place = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ProductProperty":
                                        _cL_DS_ProductProperty = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ProductType":
                                        _cL_DS_ProductType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ProductUnitPostion":
                                        _cL_DS_ProductUnitPostion = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ProjectType":
                                        _cL_DS_ProjectType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_PublicMode":
                                        _cL_DS_PublicMode = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_SalaryDesire":
                                        _cL_DS_SalaryDesire = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ResumeInclude":
                                        _cL_DS_ResumeInclude = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_SoftwareOptionEnum":
                                        _cL_DS_SoftwareOptionEnum = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_TagFindType":
                                        _cL_DS_TagFindType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_ShowItem":
                                        _cL_DS_ShowItem = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_WorkExperience":
                                        _cL_DS_WorkExperience = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_WorkPlace":
                                        _cL_DS_WorkPlace = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_WorkProperty":
                                        _cL_DS_WorkProperty = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_TmpltType":
                                        _cL_DS_TmpltType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_SnipType":
                                        _cL_DS_SnipType = reader.GetAttribute("path");
                                        break;
                                    case "CL_DS_Sex":
                                        _cL_DS_Sex = reader.GetAttribute("path");
                                        break;
                                    case "CL_BlankProject_Folder":
                                        _cL_BlankProject_Folder = reader.GetAttribute("path");
                                        break;
                                    case "CL_MainFormResourcesConfig":
                                        _cL_MainFormResourcesConfig = reader.GetAttribute("path");
                                        break;
                                    case "CL_DrawToolsBox":
                                        _cL_DrawToolsBox = reader.GetAttribute("path");
                                        break;
                                    case "CL_Menu":
                                        _cL_Menu = reader.GetAttribute("path");
                                        break;
                                    case "CL_ResourceText":
                                        _cL_ResourceText = reader.GetAttribute("path");
                                        break;
                                    case "CL_SiteType":
                                        _cL_SiteType = reader.GetAttribute("path");
                                        break;
                                    case "CL_SoftOption":
                                        _cL_SoftOption = reader.GetAttribute("path");
                                        break;
                                    case "CL_swfobject":
                                        _cL_swfobject = reader.GetAttribute("path");
                                        break;
                                    case "CL_ToolbarList":
                                        _cL_ToolbarList = reader.GetAttribute("path");
                                        break;
                                    case "CL_ToolsBoxItems":
                                        _cL_ToolsBoxItems = reader.GetAttribute("path");
                                        break;
                                    case "CL_Validate":
                                        _cL_Validate = reader.GetAttribute("path");
                                        break;
                                    case "CHS_Folder":
                                        _cHS_Folder = reader.GetAttribute("path");
                                        break;
                                    case "Site_Temp_Folder":
                                        _site_Temp_Folder = reader.GetAttribute("path");
                                        break;
                                    case "Site_Root_Folder":
                                        _site_Root_Folder = reader.GetAttribute("path");
                                        break;
                                    case "Site_Root_Resources_Folder":
                                        _site_Root_Resources_Folder = reader.GetAttribute("path");
                                        break;
                                    case "Site_PrevPublishFilesBak_Folder":
                                        _site_PrevPublishFilesBak_Folder = reader.GetAttribute("path");
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

        }


        static private string _xslt_Channelimg;
        static public string Xslt_Channelimg { get { return Path.Combine(_softwarePath, _xslt_Channelimg); } }

        static private string _xslt_ContentSort;
        static public string Xslt_ContentSort { get { return Path.Combine(_softwarePath, _xslt_ContentSort); } }

        static private string _xslt_Pub2Svr;
        static public string Xslt_Pub2Svr { get { return Path.Combine(_softwarePath, _xslt_Pub2Svr); } }

        static private string _xslt_PageID;
        static public string Xslt_PageID { get { return Path.Combine(_softwarePath, _xslt_PageID); } }

        static private string _xslt_ContentTitleList;
        static public string Xslt_ContentTitleList { get { return Path.Combine(_softwarePath, _xslt_ContentTitleList); } }

        static private string _xslt_Text;
        static public string Xslt_Text { get { return Path.Combine(_softwarePath, _xslt_Text); } }

        static private string _xslt_Folder;
        static public string Xslt_Folder { get { return Path.Combine(_softwarePath, _xslt_Folder); } }

        static private string _uSER_Folder;
        static public string USER_Folder { get { return Path.Combine(_softwarePath, _uSER_Folder); } }

        static private string _tempForUpdate_Folder;
        static public string TempForUpdate_Folder { get { return Path.Combine(_softwarePath, _tempForUpdate_Folder); } }

        static private string _jsPlugIns_Folder;
        static public string JsPlugIns_Folder { get { return Path.Combine(_softwarePath, _jsPlugIns_Folder); } }

        static private string _icon_Folder;
        static public string Icon_Folder { get { return Path.Combine(_softwarePath, _icon_Folder); } }

        static private string _htmlPlugIns_Folder;
        static public string HtmlPlugIns_Folder { get { return Path.Combine(_softwarePath, _htmlPlugIns_Folder); } }

        static private string _help_Folder;
        static public string Help_Folder { get { return Path.Combine(_softwarePath, _help_Folder); } }

        static private string _dtdData_Folder;
        static public string DtdData_Folder { get { return Path.Combine(_softwarePath, _dtdData_Folder); } }

        static private string _config_FontList;
        static public string Config_FontList { get { return Path.Combine(_softwarePath, _config_FontList); } }

        static private string _config_GlobalSetting;
        static public string Config_GlobalSetting { get { return Path.Combine(_softwarePath, _config_GlobalSetting); } }

        static private string _config_Config;
        static public string Config_Config { get { return Path.Combine(_softwarePath, _config_Config); } }

        static private string _config_LayoutConfig;
        static public string Config_LayoutConfig { get { return Path.Combine(_softwarePath, _config_LayoutConfig); } }

        static private string _config_PadLayout;
        static public string Config_PadLayout { get { return Path.Combine(_softwarePath, _config_PadLayout); } }

        static private string _config_RecentFiles;
        static public string Config_RecentFiles { get { return Path.Combine(_softwarePath, _config_RecentFiles); } }

        static private string _config_RecordInfo;
        static public string Config_RecordInfo { get { return Path.Combine(_softwarePath, _config_RecordInfo); } }

        static private string _config_HistoryInputRecord;
        static public string Config_HistoryInputRecord { get { return Path.Combine(_softwarePath, _config_HistoryInputRecord); } }

        static private string _config_Folder;
        static public string Config_Folder { get { return Path.Combine(_softwarePath, _config_Folder); } }

        static private string _cL_StartupPage_Folder;
        static public string CL_StartupPage_Folder { get { return Path.Combine(_softwarePath, _cL_StartupPage_Folder); } }

        static private string _cL_Resources_Folder;
        static public string CL_Resources_Folder { get { return Path.Combine(_softwarePath, _cL_Resources_Folder); } }

        static private string _cL_Dialog_Folder;
        static public string CL_Dialog_Folder { get { return Path.Combine(_softwarePath, _cL_Dialog_Folder); } }

        static private string _cL_DataSources_Folder;
        static public string CL_DataSources_Folder { get { return Path.Combine(_softwarePath, _cL_DataSources_Folder); } }

        static private string _cL_DS_Age;
        static public string CL_DS_Age { get { return Path.Combine(_softwarePath, _cL_DS_Age); } }

        static private string _cL_DS_Agent;
        static public string CL_DS_Agent { get { return Path.Combine(_softwarePath, _cL_DS_Agent); } }

        static private string _cL_DS_AvailaDate;
        static public string CL_DS_AvailaDate { get { return Path.Combine(_softwarePath, _cL_DS_AvailaDate); } }

        static private string _cL_DS_BiddingGetMode;
        static public string CL_DS_BiddingGetMode { get { return Path.Combine(_softwarePath, _cL_DS_BiddingGetMode); } }

        static private string _cL_DS_BiddingType;
        static public string CL_DS_BiddingType { get { return Path.Combine(_softwarePath, _cL_DS_BiddingType); } }

        static private string _cL_DS_BiddProjectIsIndustry;
        static public string CL_DS_BiddProjectIsIndustry { get { return Path.Combine(_softwarePath, _cL_DS_BiddProjectIsIndustry); } }

        static private string _cL_DS_BiddPubType;
        static public string CL_DS_BiddPubType { get { return Path.Combine(_softwarePath, _cL_DS_BiddPubType); } }

        static private string _cL_DS_BiddStyle;
        static public string CL_DS_BiddStyle { get { return Path.Combine(_softwarePath, _cL_DS_BiddStyle); } }

        static private string _cL_DS_BidType;
        static public string CL_DS_BidType { get { return Path.Combine(_softwarePath, _cL_DS_BidType); } }

        static private string _cL_DS_BorderPart;
        static public string CL_DS_BorderPart { get { return Path.Combine(_softwarePath, _cL_DS_BorderPart); } }

        static private string _cL_DS_BorderStyle;
        static public string CL_DS_BorderStyle { get { return Path.Combine(_softwarePath, _cL_DS_BorderStyle); } }

        static private string _cL_DS_BorderWidth;
        static public string CL_DS_BorderWidth { get { return Path.Combine(_softwarePath, _cL_DS_BorderWidth); } }

        static private string _cL_DS_BorderWidthUnit;
        static public string CL_DS_BorderWidthUnit { get { return Path.Combine(_softwarePath, _cL_DS_BorderWidthUnit); } }

        static private string _cL_DS_ChannelType;
        static public string CL_DS_ChannelType { get { return Path.Combine(_softwarePath, _cL_DS_ChannelType); } }

        static private string _cL_DS_City;
        static public string CL_DS_City { get { return Path.Combine(_softwarePath, _cL_DS_City); } }

        static private string _cL_DS_ContentSortItem;
        static public string CL_DS_ContentSortItem { get { return Path.Combine(_softwarePath, _cL_DS_ContentSortItem); } }

        static private string _cL_DS_CredentialType;
        static public string CL_DS_CredentialType { get { return Path.Combine(_softwarePath, _cL_DS_CredentialType); } }

        static private string _cL_DS_Department;
        static public string CL_DS_Department { get { return Path.Combine(_softwarePath, _cL_DS_Department); } }

        static private string _cL_DS_DepartmentFunction;
        static public string CL_DS_DepartmentFunction { get { return Path.Combine(_softwarePath, _cL_DS_DepartmentFunction); } }

        static private string _cL_DS_DriverLicence;
        static public string CL_DS_DriverLicence { get { return Path.Combine(_softwarePath, _cL_DS_DriverLicence); } }

        static private string _cL_DS_DetailDate;
        static public string CL_DS_DetailDate { get { return Path.Combine(_softwarePath, _cL_DS_DetailDate); } }

        static private string _cL_DS_EduLevel;
        static public string CL_DS_EduLevel { get { return Path.Combine(_softwarePath, _cL_DS_EduLevel); } }

        static private string _cL_DS_FunctionPlace;
        static public string CL_DS_FunctionPlace { get { return Path.Combine(_softwarePath, _cL_DS_FunctionPlace); } }

        static private string _cL_DS_FundSourceType;
        static public string CL_DS_FundSourceType { get { return Path.Combine(_softwarePath, _cL_DS_FundSourceType); } }

        static private string _cL_DS_Industry;
        static public string CL_DS_Industry { get { return Path.Combine(_softwarePath, _cL_DS_Industry); } }

        static private string _cL_DS_IndustrySort;
        static public string CL_DS_IndustrySort { get { return Path.Combine(_softwarePath, _cL_DS_IndustrySort); } }

        static private string _cL_DS_ItemType;
        static public string CL_DS_ItemType { get { return Path.Combine(_softwarePath, _cL_DS_ItemType); } }

        static private string _cL_DS_KnowType;
        static public string CL_DS_KnowType { get { return Path.Combine(_softwarePath, _cL_DS_KnowType); } }

        static private string _cL_DS_Language;
        static public string CL_DS_Language { get { return Path.Combine(_softwarePath, _cL_DS_Language); } }

        static private string _cL_DS_LanguageDesire;
        static public string CL_DS_LanguageDesire { get { return Path.Combine(_softwarePath, _cL_DS_LanguageDesire); } }

        static private string _cL_DS_LanguageLevel;
        static public string CL_DS_LanguageLevel { get { return Path.Combine(_softwarePath, _cL_DS_LanguageLevel); } }

        static private string _cL_DS_ManageExperience;
        static public string CL_DS_ManageExperience { get { return Path.Combine(_softwarePath, _cL_DS_ManageExperience); } }

        static private string _cL_DS_OffsetTime;
        static public string CL_DS_OffsetTime { get { return Path.Combine(_softwarePath, _cL_DS_OffsetTime); } }

        static private string _cL_DS_OrganationType;
        static public string CL_DS_OrganationType { get { return Path.Combine(_softwarePath, _cL_DS_OrganationType); } }

        static private string _cL_DS_OutGo;
        static public string CL_DS_OutGo { get { return Path.Combine(_softwarePath, _cL_DS_OutGo); } }

        static private string _cL_DS_PasswordQuestion;
        static public string CL_DS_PasswordQuestion { get { return Path.Combine(_softwarePath, _cL_DS_PasswordQuestion); } }

        static private string _cL_DS_Place;
        static public string CL_DS_Place { get { return Path.Combine(_softwarePath, _cL_DS_Place); } }

        static private string _cL_DS_ProductProperty;
        static public string CL_DS_ProductProperty { get { return Path.Combine(_softwarePath, _cL_DS_ProductProperty); } }

        static private string _cL_DS_ProductType;
        static public string CL_DS_ProductType { get { return Path.Combine(_softwarePath, _cL_DS_ProductType); } }

        static private string _cL_DS_ProductUnitPostion;
        static public string CL_DS_ProductUnitPostion { get { return Path.Combine(_softwarePath, _cL_DS_ProductUnitPostion); } }

        static private string _cL_DS_ProjectType;
        static public string CL_DS_ProjectType { get { return Path.Combine(_softwarePath, _cL_DS_ProjectType); } }

        static private string _cL_DS_PublicMode;
        static public string CL_DS_PublicMode { get { return Path.Combine(_softwarePath, _cL_DS_PublicMode); } }

        static private string _cL_DS_SalaryDesire;
        static public string CL_DS_SalaryDesire { get { return Path.Combine(_softwarePath, _cL_DS_SalaryDesire); } }

        static private string _cL_DS_ResumeInclude;
        static public string CL_DS_ResumeInclude { get { return Path.Combine(_softwarePath, _cL_DS_ResumeInclude); } }

        static private string _cL_DS_SoftwareOptionEnum;
        static public string CL_DS_SoftwareOptionEnum { get { return Path.Combine(_softwarePath, _cL_DS_SoftwareOptionEnum); } }

        static private string _cL_DS_TagFindType;
        static public string CL_DS_TagFindType { get { return Path.Combine(_softwarePath, _cL_DS_TagFindType); } }

        static private string _cL_DS_ShowItem;
        static public string CL_DS_ShowItem { get { return Path.Combine(_softwarePath, _cL_DS_ShowItem); } }

        static private string _cL_DS_WorkExperience;
        static public string CL_DS_WorkExperience { get { return Path.Combine(_softwarePath, _cL_DS_WorkExperience); } }

        static private string _cL_DS_WorkPlace;
        static public string CL_DS_WorkPlace { get { return Path.Combine(_softwarePath, _cL_DS_WorkPlace); } }

        static private string _cL_DS_WorkProperty;
        static public string CL_DS_WorkProperty { get { return Path.Combine(_softwarePath, _cL_DS_WorkProperty); } }

        static private string _cL_DS_TmpltType;
        static public string CL_DS_TmpltType { get { return Path.Combine(_softwarePath, _cL_DS_TmpltType); } }

        static private string _cL_DS_SnipType;
        static public string CL_DS_SnipType { get { return Path.Combine(_softwarePath, _cL_DS_SnipType); } }

        static private string _cL_DS_Sex;
        static public string CL_DS_Sex { get { return Path.Combine(_softwarePath, _cL_DS_Sex); } }

        static private string _cL_BlankProject_Folder;
        static public string CL_BlankProject_Folder { get { return Path.Combine(_softwarePath, _cL_BlankProject_Folder); } }

        static private string _cL_MainFormResourcesConfig;
        static public string CL_MainFormResourcesConfig { get { return Path.Combine(_softwarePath, _cL_MainFormResourcesConfig); } }

        static private string _cL_DrawToolsBox;
        static public string CL_DrawToolsBox { get { return Path.Combine(_softwarePath, _cL_DrawToolsBox); } }

        static private string _cL_Menu;
        static public string CL_Menu { get { return Path.Combine(_softwarePath, _cL_Menu); } }

        static private string _cL_ResourceText;
        static public string CL_ResourceText { get { return Path.Combine(_softwarePath, _cL_ResourceText); } }

        static private string _cL_SiteType;
        static public string CL_SiteType { get { return Path.Combine(_softwarePath, _cL_SiteType); } }

        static private string _cL_SoftOption;
        static public string CL_SoftOption { get { return Path.Combine(_softwarePath, _cL_SoftOption); } }

        static private string _cL_swfobject;
        static public string CL_swfobject { get { return Path.Combine(_softwarePath, _cL_swfobject); } }

        static private string _cL_ToolbarList;
        static public string CL_ToolbarList { get { return Path.Combine(_softwarePath, _cL_ToolbarList); } }

        static private string _cL_ToolsBoxItems;
        static public string CL_ToolsBoxItems { get { return Path.Combine(_softwarePath, _cL_ToolsBoxItems); } }

        static private string _cL_Validate;
        static public string CL_Validate { get { return Path.Combine(_softwarePath, _cL_Validate); } }

        static private string _cHS_Folder;
        static public string CHS_Folder { get { return Path.Combine(_softwarePath, _cHS_Folder); } }

        static private string _site_Temp_Folder;
        static public string Site_Temp_Folder { get { return Path.Combine(ProjectPath, _site_Temp_Folder); } }

        static private string _site_Root_Folder;
        static public string Site_Root_Folder { get { return Path.Combine(ProjectPath, _site_Root_Folder); } }

        static private string _site_Root_Resources_Folder;
        static public string Site_Root_Resources_Folder { get { return Path.Combine(ProjectPath, _site_Root_Resources_Folder); } }

        static private string _site_PrevPublishFilesBak_Folder;
        static public string Site_PrevPublishFilesBak_Folder { get { return Path.Combine(ProjectPath, _site_PrevPublishFilesBak_Folder); } }

    }
}