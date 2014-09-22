using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 页面
    /// </summary>
    abstract public partial class PageXmlDocument : IndexXmlDocument, ISearch
    {
        public PageXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(sdsiteElement) { }

        public PageType PageType
        {
            get
            {
                string strType = string.Empty;
                string pageId = this.DocumentElement.GetAttribute("id");
                XmlElement pageElement = OwnerSdsiteDocument.GetElementById(pageId);
                if (pageElement != null)
                {
                    strType = pageElement.GetAttribute("type");
                }
                return (PageType)Enum.Parse(typeof(PageType), strType);
            }
        }

        /// <summary>
        /// 页面关联到的模板的Id
        /// </summary>
        public string TmpltId
        {
            get { return SdsiteElement.TmpltId; }
        }

        #region 属性（数据）

        /// <summary>
        /// 获取与设置文章标题别名
        /// </summary>
        [SnipPart("pageTitleByName", "pageTitleByName", "pageTitleByName", "pageTitleByName", 0, 80)]
        public string PageTitleAlias
        {
            get { return this.DocumentElement.GetAttribute("pageTitleAlias"); }
            set { this.DocumentElement.SetAttribute("pageTitleAlias", value); }
        }

        /// <summary>
        /// 获取与设置文章正文
        /// </summary>
        [SnipPart("Content", "Content", "Content", "Content", 0, 80)]
        public string PageText
        {
            get
            {
                foreach (XmlNode node in this.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "pageText")
                    {
                        return ((AnyXmlElement)node).CDataValue;
                    }
                }
                return string.Empty;
            }
            set
            {
                foreach (XmlNode node in this.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "pageText")
                    {
                        ((AnyXmlElement)node).CDataValue = value;
                    }
                }
            }
        }

        /// <summary>
        /// 设置和获得文章作者
        /// </summary>
        [SnipPart("pageAuthor", "pageAuthor", "pageAuthor", "pageAuthor", 0, 80)]
        public string Author
        {
            get { return this.DocumentElement.GetAttribute("author"); }
            set { this.DocumentElement.SetAttribute("author", value); }
        }

        /// <summary>
        /// 获取与设置文章摘要
        /// </summary>
        [SnipPart("pageSummary", "pageSummary", "pageSummary", "pageSummary", 0, 80)]
        public string PageSummary
        {
            get
            {
                foreach (XmlNode node in this.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "pageSummary")
                    {
                        return ((AnyXmlElement)node).CDataValue;
                    }
                }
                return string.Empty;
            }
            set
            {
                foreach (XmlNode node in this.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "pageSummary")
                    {
                        ((AnyXmlElement)node).CDataValue = value;
                    }
                }
            }
        }
        /// <summary>
        /// 获取所在的频道
        /// edit by zhenghao at 2008-06-18 14:53
        /// ??????
        /// </summary>
        [SnipPart("inchannel", "inchannel", "inchannel", "inchannel", 0, 80)]
        public string Channel
        {
            get { return this.DocumentElement.GetAttribute("inchannel"); }
            set { this.DocumentElement.SetAttribute("inchannel", value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [SnipPart("keyWords", "keyWords", "keyWords", "keyWords", 0, 80)]
        public string[] PageKeywords
        {
            get
            {
                foreach (XmlNode node in this.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "pageKeywords")
                    {
                        string[] keyword = new string[node.ChildNodes.Count];
                        for (int i = 0; i < keyword.Length; i++)
                        {
                            keyword[i] = ((XmlElement)node.ChildNodes[i]).GetAttribute("text");
                        }
                        return keyword;
                    }
                }
                return null;
            }
            set
            {
                XmlNodeList pageKeywordsEle = this.DocumentElement.SelectNodes(@"pageKeywords");//.GetChildsByName("pageKeywords");
                if (pageKeywordsEle.Count == 0)
                {
                    XmlElement newKeywordsEle = this.CreateElement("pageKeywords");
                    this.DocumentElement.AppendChild(newKeywordsEle);
                    for (int i = 0; i < value.Length; i++)
                    {
                        XmlElement newEle = this.CreateElement("pageKeyword");
                        newEle.SetAttribute("text", value[i]);
                        newKeywordsEle.AppendChild(newEle);
                    }
                }
                foreach (XmlNode node in this.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "pageKeywords")
                    {
                        node.RemoveAll();
                        for (int i = 0; i < value.Length; i++)
                        {
                            XmlElement newEle = this.CreateElement("pageKeyword");
                            newEle.SetAttribute("text", value[i]);
                            node.AppendChild(newEle);
                        }
                    }
                }
                this.Save();
            }
        }

        /// <summary>
        ///文章创建时间
        /// </summary>
        public string PageCreateTime
        {
            get { return this.DocumentElement.GetAttribute("pageCreateTime"); }
            set { this.DocumentElement.SetAttribute("pageCreateTime", value); }
        }

        /// <summary>
        ///创建者别名
        /// </summary>
        public string AuthorAlias
        {
            get { return this.DocumentElement.GetAttribute("authorAlias"); }
            set { this.DocumentElement.SetAttribute("authorAlias", value); }
        }

        /// <summary>
        ///修改者别名
        /// </summary>
        public string ModifyAlias
        {
            get { return this.DocumentElement.GetAttribute("modifyAlias"); }
            set { this.DocumentElement.SetAttribute("modifyAlias", value); }
        }

        /// <summary>
        ///修改者别名
        /// </summary>
        public string DesignSummary
        {
            get { return this.DocumentElement.GetAttribute("designSummary"); }
            set { this.DocumentElement.SetAttribute("designSummary", value); }
        }

        /// <summary>
        ///修改者别名
        /// </summary>
        public string ContentSource
        {
            get { return this.DocumentElement.GetAttribute("contentSource"); }
            set { this.DocumentElement.SetAttribute("contentSource", value); }
        }
        #endregion

        new public PageSimpleExXmlElement SdsiteElement
        {
            get
            {
                return base.SdsiteElement as PageSimpleExXmlElement;
            }
        }

        /// <summary>
        /// 创建一个PageXmlDocument对象。(type参数决定从哪个派生类创建)
        /// </summary>
        static public PageXmlDocument CreateInstance(string relativeFilePath, string pageId, PageType type, SimpleExIndexXmlElement sdsiteElement)
        {
            Debug.Assert(!string.IsNullOrEmpty(relativeFilePath));
            Debug.Assert(!string.IsNullOrEmpty(pageId));

            PageXmlDocument doc = null;
            switch (type)
            {
                case PageType.General:
                    doc = new GeneralPageXmlDocument(relativeFilePath, sdsiteElement);
                    break;
                case PageType.Product:
                    doc = new ProductXmlDocument(relativeFilePath, sdsiteElement);
                    break;
                case PageType.Project:
                    doc = new ProjectXmlDocument(relativeFilePath, sdsiteElement);
                    break;
                case PageType.InviteBidding:
                    doc = new InviteBiddingXmlDocument(relativeFilePath, sdsiteElement);
                    break;
                case PageType.Knowledge:
                    doc = new KnowledgeXmlDocument(relativeFilePath, sdsiteElement);
                    break;
                case PageType.Hr:
                    doc = new HrXmlDocument(relativeFilePath, sdsiteElement);
                    break;
                case PageType.Home:
                    doc = new HomeXmlDocument(relativeFilePath, sdsiteElement);
                    break;
                default:
                    Debug.Assert(false, "开发期错误。未知的PageType:" + type);
                    break;
            }

            return doc;
        }

        #region 生成DataGridColumn的方法
        //TODO: StringFormating 没有写响应
        //TODO: ShowImage的来源未确定
        public static DataGridViewColumn[] ToDataGridViewColumns()
        {
            List<DataGridViewColumn> columns = new List<DataGridViewColumn>();

            PropertyInfo[] properties;
            //TODO:合并五个管理页面 添加列 Lisuye
            properties = typeof(CorpDataXmlDocument).GetProperties(
                   BindingFlags.Instance |
                   BindingFlags.Public);

            ///遍历处理找到的所有Property
            foreach (PropertyInfo info in properties)
            {
                object[] gridUsages = info.GetCustomAttributes(typeof(GridAttribute), false);
                if (gridUsages.Length <= 0)
                {
                    continue;//无定制属性
                }
                #region 根据属性的定制特性设定列的样式
                else
                {
                    DataGridViewColumn column = null;
                    foreach (GridAttribute u in gridUsages)
                    {
                        DataGridViewCell cell = null;

                        #region 根据属性的返回类型不同使用不同的列类型

                        Type propertyType = info.GetGetMethod().ReturnType;

                        if (propertyType == typeof(bool))
                        {
                            column = new DataGridViewImageColumn();
                            cell = new DataGridViewImageCell();
                        }
                        else
                        {
                            column = new DataGridViewTextBoxColumn();
                            cell = new DataGridViewTextBoxCell();
                        }

                        column.Tag = propertyType;

                        #endregion

                        #region 列将生成，是否显示
                        if (u.IsDisplayInGrid)
                        {
                            column.Visible = u.IsDisplayInGrid;
                        }
                        else
                        {
                            column.Visible = u.IsDisplayInGrid;
                        }
                        #endregion

                        #region 该列是否是固定大小
                        if (!u.IsFixColumn)
                        {
                            column.Resizable = DataGridViewTriState.False;
                        }
                        column.Resizable = DataGridViewTriState.True;
                        #endregion

                        #region 该列是否可以排序
                        if (!u.IsOrderColumn)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                        #endregion

                        #region 列标题赋值
                        if (!string.IsNullOrEmpty(u.HeaderText))
                        {
                            column.HeaderText = u.HeaderText;
                        }
                        else
                        {
                            column.HeaderText = "";
                        }
                        #endregion

                        #region 列在列菜单中的显示名字
                        if (string.IsNullOrEmpty(u.ColumnInMenuText))
                        {
                            throw new Exception("DesignTime: HeaderMenu's text cann't empty in column!");
                        }
                        string[] strArr = new string[2];
                        strArr[0] = u.ColumnInMenuText;
                        strArr[1] = u.MenuLevel;
                        column.HeaderCell.ToolTipText = strArr[0];
                        column.HeaderCell.Tag = strArr;
                        #endregion

                        #region 列的宽度
                        if (u.ColumnWidth <= 0)
                        {
                            column.Width = 50;
                        }
                        column.Width = u.ColumnWidth;
                        #endregion

                        #region 列的自动宽度
                        if (u.IsAutoColumn)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                        #endregion

                        #region 列的Cell所要显示的图片
                        if (string.IsNullOrEmpty(u.ShowImage))
                        {
                            //((DataGridViewImageColumn)column).Image = Image.FromFile(u.ShowImage);
                        }
                        #endregion

                        #region 列的Cell如显示文字，显示文字的格式是...
                        if (string.IsNullOrEmpty(u.StringFormating))
                        {
                        }
                        #endregion

                        #region 列的显示索引

                        column.DisplayIndex = u.ColumnDisplayIndex;

                        #endregion

                        if (column != null)
                        {
                            column.Name = info.Name;
                            column.CellTemplate = cell;
                            columns.Add(column);
                        }
                    }
                }
                #endregion

            }
            return columns.ToArray();
        }

        #endregion

        #region 生成DataGridRow的方法

        //Dictionary<string, Image> _dicImagesCache = new Dictionary<string, Image>();
        //Image GetImage(string fileName)
        //{
        //    Image image = null;
        //    if (!_dicImagesCache.TryGetValue(fileName, out image))
        //    {
        //        //todo:
        //        image = Image.FromFile(Path.Combine(Application.StartupPath, fileName));
        //        _dicImagesCache.Add(fileName, image);
        //    }
        //    return image;
        //}
        #endregion

        #region ISearch 成员

        public Position SearchNext(WantToDoType type)
        {
            return null;
        }
        public void Replace(Position position)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public enum PageSimpleState
    {
        Unknown = 0,
        New = 1,
        Modified = 2,
        NotModified = 3,
    }

    #region 查找部分
    /// <summary>
    /// 查找结果的处理 by lisuye on 2008年6月18日
    /// </summary>
    public class GetDataResourceValue
    {
        #region 局部变量
        private XmlDocument _doc = null;
        private Dictionary<int, PropertyInfo> _canSearchEleDic = null;
        private string _findContent = string.Empty;
        private WantToDoType _type;
        private int dicKey = 0;
        private List<Position> _positionList = null;
        private Position _position = null;
        private Position _positionForSingle = null;
        #endregion

        #region 构造函数
        public GetDataResourceValue(XmlDocument doc)
        {
            _doc = doc;
            _findContent = FindOptions.Singler.FindContent;
            _canSearchEleDic = GetPropertyDic();
            _positionList = FindOptions.Singler.Positions;
        }
        #endregion

        #region 的到Property的集合
        public Dictionary<int, PropertyInfo> GetPropertyDic()
        {
            Dictionary<int, PropertyInfo> canSearchEleDic = new Dictionary<int, PropertyInfo>();
            Type type = _doc.GetType();
            PropertyInfo[] propertys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (propertys != null)
            {
                foreach (PropertyInfo property in propertys)
                {
                    object[] attributes = property.GetCustomAttributes(typeof(EditorAttribute), false);
                    if (attributes.Length > 0)
                    {
                        EditorAttribute ediAttribute = (EditorAttribute)attributes[0];
                        if (ediAttribute.IsCanFind)
                        {
                            canSearchEleDic.Add(dicKey, property);
                            dicKey++;
                        }
                    }
                }
            }
            return canSearchEleDic;
        }
        #endregion

        #region 设置最后的Position
        public void GetLastPosition()
        {
            PropertyInfo property = _canSearchEleDic[_canSearchEleDic.Count - 1];
            string value = property.GetValue(_doc, null).ToString();
            int index = value.Length - 1;
            Position position = new Position(index, 0, property, _doc);
            FindOptions.Singler.LastPosition = position;
        }
        #endregion

        #region 查找下一个Property的加工厂
        private int SreachPropertyFactory()
        {
            int index = 0;
            if (FindOptions.Singler.CurrentPosition != null)
            {
                PropertyInfo property = FindOptions.Singler.CurrentPosition.Property;
                if (_canSearchEleDic.ContainsValue(property))
                {
                    foreach (int key in _canSearchEleDic.Keys)
                    {
                        if (_canSearchEleDic[key] == property)
                        {
                            index = key;
                            return index;
                        }
                    }
                }
            }
            return index;
        }
        #endregion

        #region 查找或替换下一个
        //查找或替换下一个
        public Position SaveFindProperty(WantToDoType type)
        {
            this._type = type;
            int index = -1;
            //设置最后的Position
            GetLastPosition();

            if (_canSearchEleDic != null)
            {
                //的到当前的属性
                int tag = SreachPropertyFactory();
                if (FindOptions.Singler.UpWard)
                {
                    for (int tagindex = tag; tagindex >= 0; tagindex--)
                    {
                        object value = _canSearchEleDic[tagindex].GetValue(_doc, null);
                        string strValue = value.ToString();
                        //TODO:正则问题

                        //大小写匹配
                        strValue = MatchCase(strValue);
                        if (!string.IsNullOrEmpty(strValue) && !string.IsNullOrEmpty(this._findContent))
                        {
                            while (true)
                            {
                                //正则问题
                                if (FindOptions.Singler.IsUsingFindType)
                                {
                                    MessageBox.Show("启动了使用正则和通配符，本部分为完善位置在PageXMlDocument的583行");
                                    break;
                                }
                                else
                                {
                                    index = SaveSinglePosition(strValue, _canSearchEleDic[tagindex]);
                                    if (this._type == WantToDoType.ReplaceNext)
                                    {
                                        ReplaceFindContent();
                                    }
                                    if (index >= 0)
                                    {
                                        if (this._positionForSingle != null)
                                        {
                                            FindOptions.Singler.CurrentPosition = _positionForSingle;
                                            return this._positionForSingle;
                                        }
                                        else
                                            return FindOptions.Singler.CurrentPosition;
                                    }
                                    else
                                        break;
                                }

                            }
                        }
                        if (tagindex == 0)
                            break;
                        else
                            continue;
                    }
                }
                else
                {
                    for (int tagindex = tag; tagindex < _canSearchEleDic.Count; tagindex++)
                    {
                        object value = _canSearchEleDic[tagindex].GetValue(_doc, null);
                        string strValue = value.ToString();


             
                        if (!string.IsNullOrEmpty(strValue) && !string.IsNullOrEmpty(this._findContent))
                        {
                            while (true)
                            {
                                //正则部分
                                if (FindOptions.Singler.IsUsingFindType)
                                {
                                    MessageBox.Show("启动了使用正则和通配符，本部分为完善位置在PageXMlDocument的631行");
                                    break;
                                }
                                else
                                {
                                    //大小写匹配
                                    strValue = MatchCase(strValue);
                                    index = SaveSinglePosition(strValue, _canSearchEleDic[tagindex]);
                                    if (this._type == WantToDoType.ReplaceNext)
                                    {
                                        ReplaceFindContent();
                                    }
                                    if (index >= 0)
                                    {
                                        if (this._positionForSingle != null)
                                        {
                                            FindOptions.Singler.CurrentPosition = _positionForSingle;
                                            return this._positionForSingle;
                                        }
                                        else
                                            return FindOptions.Singler.CurrentPosition;
                                    }
                                    else
                                        break;
                                }

                            }
                        }
                        else if (tagindex == _canSearchEleDic.Count - 1)
                            break;
                        else
                            continue;
                    }
                }
            }
            return null;
        }
        #endregion

        #region 查找全部Position的保存
        public int SaveSinglePosition(string strValue, PropertyInfo canFindProperty)
        {
            int index = 0;
            if (FindOptions.Singler.UpWard)
            {
                if (FindOptions.Singler.CurrentPosition != null)
                {
                    Position position = FindOptions.Singler.CurrentPosition;
                    if (position.Property.Equals(canFindProperty))
                    {
                        index = position.Index;
                    }
                    else
                    {
                        index=canFindProperty.GetValue(_doc, null).ToString().Length;
                    }
                }
                string newStr = strValue.Substring(0, index);
                index = newStr.LastIndexOf(this._findContent);

            }
            else
            {
                //查找、替换 的定位
                if (FindOptions.Singler.CurrentPosition != null)
                {
                    Position position = FindOptions.Singler.CurrentPosition;
                    if (position.Property.Equals(canFindProperty))
                    {
                        index = position.Index + position.Length;
                    }
                }
                index = strValue.IndexOf(this._findContent, index);
            }



            if (index >= 0)
            {
                if (FindOptions.Singler.MatchWholeWord)
                {
                    if (!MatchWhole(strValue, index, this._findContent.Length, this._findContent))
                    {
                        _position = FindOptions.Singler.CurrentPosition;
                        FindOptions.Singler.CurrentPosition = new Position(index, 0, canFindProperty, _doc);
                        return -1;
                    }
                }
                Position position = new Position(index, this._findContent.Length, canFindProperty, _doc);
                FindOptions.Singler.CurrentPosition = position;
            }
            return index;
        }
        #endregion

        #region 查找或替换全部
        /// <summary>
        /// 查找或替换全部
        /// </summary>
        /// <param name="findContent"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Position SearchOrReplaceAll(WantToDoType type)
        {
            this._type = type;
            int index = 0;
            if (_canSearchEleDic != null)
            {
                foreach (PropertyInfo canFindProperty in _canSearchEleDic.Values)
                {

                    while (true)
                    {
                        object value = canFindProperty.GetValue(_doc, null);
                        string strValue = value.ToString();
                       

                        if (!string.IsNullOrEmpty(strValue) && !string.IsNullOrEmpty(this._findContent))
                        {  
                            //正则部分
                            if (FindOptions.Singler.IsUsingFindType)
                            {
                                string matchContent=Rule(strValue);
                                if (!string.IsNullOrEmpty(matchContent))
                                {
                                    if (FindOptions.Singler.MatchCase)
                                    {
                                       
                                    }
                                }
                                MessageBox.Show("启动了使用正则和通配符，本部分为完善位置在PageXMlDocument的753行");
                                break;
                            }
                            else
                            {
                                //大小写匹配
                                strValue = MatchCase(strValue);
                                index = SavePosition(strValue, canFindProperty);
                                if (this._type == WantToDoType.ReplaceAll)
                                {
                                    ReplaceFindContent();
                                }
                                if (index < 0)
                                {
                                    break;
                                }
                            }

                        }
                        else
                            break;

                    }
                }
            }
            return null;
        }
        #endregion

        #region 查找全部Position的保存
        public int SavePosition(string strValue, PropertyInfo canFindProperty)
        {
            int index = 0;
            //查找、替换 全部的定位
            if (this._positionList.Count != 0)
            {
                Position position = this._positionList[this._positionList.Count - 1];
                if (position.Property.Equals(canFindProperty))
                {
                    index = position.Index + position.Length;
                }
                if (this._position != null)
                {
                    this._positionList[this._positionList.Count - 1] = this._position;
                    this._position = null;
                }
            }
            index = strValue.IndexOf(this._findContent, index);


            if (index >= 0)
            {
                if (FindOptions.Singler.MatchWholeWord)
                {
                    if (!MatchWhole(strValue, index, this._findContent.Length, this._findContent))
                    {
                        _position = this._positionList[this._positionList.Count - 1];
                        this._positionList[this._positionList.Count - 1] = new Position(index, 0, canFindProperty, _doc);
                        return -1;
                    }
                }
                Position position = new Position(index, this._findContent.Length, canFindProperty, _doc);
                FindOptions.Singler.Positions.Add(position);
            }
            return index;
        }
        #endregion

        #region 替换的保存
        public void ReplaceFindContent()
        {
            Position position = null;
            if (this._type == WantToDoType.ReplaceAll)
                position = FindOptions.Singler.Positions[FindOptions.Singler.Positions.Count - 1];
            else if (FindOptions.Singler.CurrentPosition != null)
            {
                position = FindOptions.Singler.CurrentPosition;
            }
            if (position != null)
            {
                PropertyInfo property = position.Property;
                string str = property.GetValue(position.GetDocument(), null).ToString();

                str = str.Remove(position.Index, position.Length);
                string NewStr = str.Insert(position.Index, FindOptions.Singler.ReplaceContent);
                Position newPosition = new Position(position.Index, FindOptions.Singler.ReplaceContent.Length, property, _doc);
                if (this._type == WantToDoType.ReplaceAll)
                    FindOptions.Singler.Positions[FindOptions.Singler.Positions.Count - 1] = newPosition;
                else
                {
                    _positionForSingle = FindOptions.Singler.CurrentPosition;
                    FindOptions.Singler.CurrentPosition = newPosition;
                }
                SaveDocument(newPosition, NewStr);
            }
        }
        #endregion

        #region 保存数据文件
        public void SaveDocument(Position position, string NewStr)
        {
            XmlDocument xmlDocument = (XmlDocument)position.GetDocument();
            Type type = xmlDocument.GetType();
            switch (type.Name)
            {
                case "HrXmlDocument":
                    HrXmlDocument hrDoc = (HrXmlDocument)position.GetDocument();
                    position.Property.SetValue(hrDoc, NewStr, null);
                    hrDoc.Save();
                    _doc = hrDoc;
                    break;
                case "InviteBiddingXmlDocument":
                    InviteBiddingXmlDocument bidDoc = (InviteBiddingXmlDocument)position.GetDocument();
                    position.Property.SetValue(bidDoc, NewStr, null);
                    bidDoc.Save();
                    _doc = bidDoc;
                    break;
                case "KnowledgeXmlDocument":
                    KnowledgeXmlDocument knowDoc = (KnowledgeXmlDocument)position.GetDocument();
                    position.Property.SetValue(knowDoc, NewStr, null);
                    knowDoc.Save();
                    _doc = knowDoc;
                    break;
                case "ProductXmlDocument":
                    ProductXmlDocument prodcutDoc = (ProductXmlDocument)position.GetDocument();
                    position.Property.SetValue(prodcutDoc, NewStr, null);
                    prodcutDoc.Save();
                    _doc = prodcutDoc;
                    break;
                case "ProjectXmlDocument":
                    ProjectXmlDocument projectDoc = (ProjectXmlDocument)position.GetDocument();
                    position.Property.SetValue(projectDoc, NewStr, null);
                    projectDoc.Save();
                    _doc = projectDoc;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 大小写匹配部分
        public string MatchCase(string value)
        {
            string _findContent = string.Empty;
            string strValue = value;
            //大小写匹配
            if (!FindOptions.Singler.MatchCase)
            {
                _findContent = FindOptions.Singler.FindContent.ToLower();
                FindOptions.Singler.FindContent = _findContent;
                strValue = value.ToLower();
            }
            return strValue;
        }
        #endregion

        #region 全字匹配
        /// <summary>
        /// 全字匹配(判断是否能正常的查找)
        /// </summary>
        /// <param name="positionValue"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool MatchWhole(string positionValue, int index, int length, string findContent)
        {
            string leftStr, frontLeft, rightStr, afterRight;
            //先查看查找的字符是否全部是中文
            Regex regex = new Regex("[\u4e00-\u9fa5]");
            string matchString = regex.Replace(findContent, "");
            if (string.IsNullOrEmpty(matchString))
            {
                return true;
            }
            //判断含有的英文或数字是否在两旁，并且两旁是否是数字或字母
            //左边值
            leftStr = positionValue.Substring(index, 1);
            if (IsMatchWhole(leftStr))
            {
                //左边的旁边值
                if (index > 0)
                {
                    frontLeft = string.Empty;
                    if (index - 1 > 0)
                        frontLeft = positionValue.Substring(index - 1, 1);
                    if (IsMatchWhole(frontLeft))
                    {
                        return false;
                    }
                }
            }
            //右边值
            rightStr = positionValue.Substring(index + length - 1, 1);
            if (IsMatchWhole(rightStr))
            {
                //右边的右边值
                if (index < positionValue.Length - 1)
                {
                    afterRight = string.Empty;
                    if (index + length < positionValue.Length - 1)
                    {
                        afterRight = positionValue.Substring(index + length, 1);
                        if (IsMatchWhole(afterRight))
                        {
                            return false;
                        }
                    }
                }
            }
            else
                return true;
            return true;

        }
        #endregion

        #region 数字和字母的判断
        public bool IsMatchWhole(string str)
        {
            Regex numberRegex = new Regex("[A-Za-z0-9]+");
            return numberRegex.IsMatch(str);
        }
        #endregion

        #region 正则通配符的匹配值
        public string Rule(string needFindContent)
        {
            int matchIndex=FindOptions.Singler.ruleIndex;
            string strValue = string.Empty;
            if (FindOptions.Singler.IsUsingFindType)
            {
                Regex regex = new Regex(FindOptions.Singler.FindContent);
                Match match=regex.Match(needFindContent, matchIndex);
                if (match == null)
                {
                    FindOptions.Singler.ruleIndex = -1;
                    return null;
                }
                strValue = match.Value;
                FindOptions.Singler.FindContent = strValue;
                if ((match.Index + match.Length) >= needFindContent.Length)
                {
                    FindOptions.Singler.ruleIndex = -1;
                }
                else
                    FindOptions.Singler.ruleIndex = match.Index + match.Length;
            }
            return strValue;
        }
        #endregion
    }
    #endregion
}
