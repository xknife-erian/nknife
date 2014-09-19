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
    /// ҳ��
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
        /// ҳ���������ģ���Id
        /// </summary>
        public string TmpltId
        {
            get { return SdsiteElement.TmpltId; }
        }

        #region ���ԣ����ݣ�

        /// <summary>
        /// ��ȡ���������±������
        /// </summary>
        [SnipPart("pageTitleByName", "pageTitleByName", "pageTitleByName", "pageTitleByName", 0, 80)]
        public string PageTitleAlias
        {
            get { return this.DocumentElement.GetAttribute("pageTitleAlias"); }
            set { this.DocumentElement.SetAttribute("pageTitleAlias", value); }
        }

        /// <summary>
        /// ��ȡ��������������
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
        /// ���úͻ����������
        /// </summary>
        [SnipPart("pageAuthor", "pageAuthor", "pageAuthor", "pageAuthor", 0, 80)]
        public string Author
        {
            get { return this.DocumentElement.GetAttribute("author"); }
            set { this.DocumentElement.SetAttribute("author", value); }
        }

        /// <summary>
        /// ��ȡ����������ժҪ
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
        /// ��ȡ���ڵ�Ƶ��
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
        ///���´���ʱ��
        /// </summary>
        public string PageCreateTime
        {
            get { return this.DocumentElement.GetAttribute("pageCreateTime"); }
            set { this.DocumentElement.SetAttribute("pageCreateTime", value); }
        }

        /// <summary>
        ///�����߱���
        /// </summary>
        public string AuthorAlias
        {
            get { return this.DocumentElement.GetAttribute("authorAlias"); }
            set { this.DocumentElement.SetAttribute("authorAlias", value); }
        }

        /// <summary>
        ///�޸��߱���
        /// </summary>
        public string ModifyAlias
        {
            get { return this.DocumentElement.GetAttribute("modifyAlias"); }
            set { this.DocumentElement.SetAttribute("modifyAlias", value); }
        }

        /// <summary>
        ///�޸��߱���
        /// </summary>
        public string DesignSummary
        {
            get { return this.DocumentElement.GetAttribute("designSummary"); }
            set { this.DocumentElement.SetAttribute("designSummary", value); }
        }

        /// <summary>
        ///�޸��߱���
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
        /// ����һ��PageXmlDocument����(type�����������ĸ������ഴ��)
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
                    Debug.Assert(false, "�����ڴ���δ֪��PageType:" + type);
                    break;
            }

            return doc;
        }

        #region ����DataGridColumn�ķ���
        //TODO: StringFormating û��д��Ӧ
        //TODO: ShowImage����Դδȷ��
        public static DataGridViewColumn[] ToDataGridViewColumns()
        {
            List<DataGridViewColumn> columns = new List<DataGridViewColumn>();

            PropertyInfo[] properties;
            //TODO:�ϲ��������ҳ�� ����� Lisuye
            properties = typeof(CorpDataXmlDocument).GetProperties(
                   BindingFlags.Instance |
                   BindingFlags.Public);

            ///���������ҵ�������Property
            foreach (PropertyInfo info in properties)
            {
                object[] gridUsages = info.GetCustomAttributes(typeof(GridAttribute), false);
                if (gridUsages.Length <= 0)
                {
                    continue;//�޶�������
                }
                #region �������ԵĶ��������趨�е���ʽ
                else
                {
                    DataGridViewColumn column = null;
                    foreach (GridAttribute u in gridUsages)
                    {
                        DataGridViewCell cell = null;

                        #region �������Եķ������Ͳ�ͬʹ�ò�ͬ��������

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

                        #region �н����ɣ��Ƿ���ʾ
                        if (u.IsDisplayInGrid)
                        {
                            column.Visible = u.IsDisplayInGrid;
                        }
                        else
                        {
                            column.Visible = u.IsDisplayInGrid;
                        }
                        #endregion

                        #region �����Ƿ��ǹ̶���С
                        if (!u.IsFixColumn)
                        {
                            column.Resizable = DataGridViewTriState.False;
                        }
                        column.Resizable = DataGridViewTriState.True;
                        #endregion

                        #region �����Ƿ��������
                        if (!u.IsOrderColumn)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                        #endregion

                        #region �б��⸳ֵ
                        if (!string.IsNullOrEmpty(u.HeaderText))
                        {
                            column.HeaderText = u.HeaderText;
                        }
                        else
                        {
                            column.HeaderText = "";
                        }
                        #endregion

                        #region �����в˵��е���ʾ����
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

                        #region �еĿ��
                        if (u.ColumnWidth <= 0)
                        {
                            column.Width = 50;
                        }
                        column.Width = u.ColumnWidth;
                        #endregion

                        #region �е��Զ����
                        if (u.IsAutoColumn)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                        #endregion

                        #region �е�Cell��Ҫ��ʾ��ͼƬ
                        if (string.IsNullOrEmpty(u.ShowImage))
                        {
                            //((DataGridViewImageColumn)column).Image = Image.FromFile(u.ShowImage);
                        }
                        #endregion

                        #region �е�Cell����ʾ���֣���ʾ���ֵĸ�ʽ��...
                        if (string.IsNullOrEmpty(u.StringFormating))
                        {
                        }
                        #endregion

                        #region �е���ʾ����

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

        #region ����DataGridRow�ķ���

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

        #region ISearch ��Ա

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

    #region ���Ҳ���
    /// <summary>
    /// ���ҽ���Ĵ��� by lisuye on 2008��6��18��
    /// </summary>
    public class GetDataResourceValue
    {
        #region �ֲ�����
        private XmlDocument _doc = null;
        private Dictionary<int, PropertyInfo> _canSearchEleDic = null;
        private string _findContent = string.Empty;
        private WantToDoType _type;
        private int dicKey = 0;
        private List<Position> _positionList = null;
        private Position _position = null;
        private Position _positionForSingle = null;
        #endregion

        #region ���캯��
        public GetDataResourceValue(XmlDocument doc)
        {
            _doc = doc;
            _findContent = FindOptions.Singler.FindContent;
            _canSearchEleDic = GetPropertyDic();
            _positionList = FindOptions.Singler.Positions;
        }
        #endregion

        #region �ĵ�Property�ļ���
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

        #region ��������Position
        public void GetLastPosition()
        {
            PropertyInfo property = _canSearchEleDic[_canSearchEleDic.Count - 1];
            string value = property.GetValue(_doc, null).ToString();
            int index = value.Length - 1;
            Position position = new Position(index, 0, property, _doc);
            FindOptions.Singler.LastPosition = position;
        }
        #endregion

        #region ������һ��Property�ļӹ���
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

        #region ���һ��滻��һ��
        //���һ��滻��һ��
        public Position SaveFindProperty(WantToDoType type)
        {
            this._type = type;
            int index = -1;
            //��������Position
            GetLastPosition();

            if (_canSearchEleDic != null)
            {
                //�ĵ���ǰ������
                int tag = SreachPropertyFactory();
                if (FindOptions.Singler.UpWard)
                {
                    for (int tagindex = tag; tagindex >= 0; tagindex--)
                    {
                        object value = _canSearchEleDic[tagindex].GetValue(_doc, null);
                        string strValue = value.ToString();
                        //TODO:��������

                        //��Сдƥ��
                        strValue = MatchCase(strValue);
                        if (!string.IsNullOrEmpty(strValue) && !string.IsNullOrEmpty(this._findContent))
                        {
                            while (true)
                            {
                                //��������
                                if (FindOptions.Singler.IsUsingFindType)
                                {
                                    MessageBox.Show("������ʹ�������ͨ�����������Ϊ����λ����PageXMlDocument��583��");
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
                                //���򲿷�
                                if (FindOptions.Singler.IsUsingFindType)
                                {
                                    MessageBox.Show("������ʹ�������ͨ�����������Ϊ����λ����PageXMlDocument��631��");
                                    break;
                                }
                                else
                                {
                                    //��Сдƥ��
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

        #region ����ȫ��Position�ı���
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
                //���ҡ��滻 �Ķ�λ
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

        #region ���һ��滻ȫ��
        /// <summary>
        /// ���һ��滻ȫ��
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
                            //���򲿷�
                            if (FindOptions.Singler.IsUsingFindType)
                            {
                                string matchContent=Rule(strValue);
                                if (!string.IsNullOrEmpty(matchContent))
                                {
                                    if (FindOptions.Singler.MatchCase)
                                    {
                                       
                                    }
                                }
                                MessageBox.Show("������ʹ�������ͨ�����������Ϊ����λ����PageXMlDocument��753��");
                                break;
                            }
                            else
                            {
                                //��Сдƥ��
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

        #region ����ȫ��Position�ı���
        public int SavePosition(string strValue, PropertyInfo canFindProperty)
        {
            int index = 0;
            //���ҡ��滻 ȫ���Ķ�λ
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

        #region �滻�ı���
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

        #region ���������ļ�
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

        #region ��Сдƥ�䲿��
        public string MatchCase(string value)
        {
            string _findContent = string.Empty;
            string strValue = value;
            //��Сдƥ��
            if (!FindOptions.Singler.MatchCase)
            {
                _findContent = FindOptions.Singler.FindContent.ToLower();
                FindOptions.Singler.FindContent = _findContent;
                strValue = value.ToLower();
            }
            return strValue;
        }
        #endregion

        #region ȫ��ƥ��
        /// <summary>
        /// ȫ��ƥ��(�ж��Ƿ��������Ĳ���)
        /// </summary>
        /// <param name="positionValue"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool MatchWhole(string positionValue, int index, int length, string findContent)
        {
            string leftStr, frontLeft, rightStr, afterRight;
            //�Ȳ鿴���ҵ��ַ��Ƿ�ȫ��������
            Regex regex = new Regex("[\u4e00-\u9fa5]");
            string matchString = regex.Replace(findContent, "");
            if (string.IsNullOrEmpty(matchString))
            {
                return true;
            }
            //�жϺ��е�Ӣ�Ļ������Ƿ������ԣ����������Ƿ������ֻ���ĸ
            //���ֵ
            leftStr = positionValue.Substring(index, 1);
            if (IsMatchWhole(leftStr))
            {
                //��ߵ��Ա�ֵ
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
            //�ұ�ֵ
            rightStr = positionValue.Substring(index + length - 1, 1);
            if (IsMatchWhole(rightStr))
            {
                //�ұߵ��ұ�ֵ
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

        #region ���ֺ���ĸ���ж�
        public bool IsMatchWhole(string str)
        {
            Regex numberRegex = new Regex("[A-Za-z0-9]+");
            return numberRegex.IsMatch(str);
        }
        #endregion

        #region ����ͨ�����ƥ��ֵ
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
