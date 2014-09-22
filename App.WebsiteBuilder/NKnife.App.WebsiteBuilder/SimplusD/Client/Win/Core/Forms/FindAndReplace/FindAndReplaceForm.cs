using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Jeelu.Win;
using System.Xml;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class FindAndReplaceForm : BaseForm
    {
        public FindAndReplaceForm(bool isFind)
        {
            InitializeComponent();
            _isFind = isFind;
            FindOptions.Singler.Reset();
            ResetFormSizeWhenIsFindChanged();
        }

        #region 内部变量

        /// <summary>
        /// 当前的主窗体
        /// </summary>
        static Form _mainFindForm = null;

        /// <summary>
        /// 当查找选项的隐藏状态变化时的窗体大小变化值
        /// </summary>       
        const int OffsetWhenOptionHidden = 106;

        /// <summary>
        /// 当查找状态切换到替换状态（反向相同）的内部子控件的位置变化值（窗体大小也变）
        /// </summary>
        const int OffsetWhenFindOrReplace = 40;

        /// <summary>
        /// 当查找选项的隐藏状态变化时的窗体大小变化值
        /// </summary>
        const int OffsetFormSize = 69;

        /// <summary>
        /// 当前状态是否查找状态
        /// </summary>
        private bool _isFind = true;

        /// <summary>
        /// 当前选项状态是否隐藏状态
        /// </summary>
        private bool _optionHiddened = false;

        /// <summary>
        /// 需要查找的文件集合
        /// </summary>
        private List<ISearch> _searchDocuments = new List<ISearch>();

        /// <summary>
        /// 当前需要查找的文件
        /// </summary>
        private ISearch _currentDocument;

        /// <summary>
        /// 当前的定位窗体
        /// </summary>
        private IMarkPosition _currentForm;
        /// <summary>
        /// 表示是否切换要查找的文档
        /// </summary>
        private bool _searchChanged = false;

        /// <summary>
        /// 查找的索引
        /// </summary>
        private int _searchingIndex = 0;

        /// <summary>
        /// 查找内容是否改变
        /// </summary>
        private bool _searchTextIsChanged = false;
        /// <summary>
        /// 查找范围改变时
        /// </summary>
        private bool _searchScopeChanged = false;
        /// <summary>
        /// 查找方式改变时
        /// </summary>
        private bool _searchTargetChanged = false;
        /// <summary>
        /// 大小写匹配发生改变
        /// </summary>
        private bool _matchCaseChanged = false;
        /// <summary>
        /// 全字匹配发生改变
        /// </summary>
        private bool _matchWholeWordChanged = false;
        /// <summary>
        /// 向上搜索改变时
        /// </summary>
        private bool _upWardChanged = false;

        

        #endregion

        #region 公共属性

        #endregion

        #region 公共方法

        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mainForm"> 主窗体</param>
        static public void Initialize(Form mainForm)
        {
            _mainFindForm = mainForm;
        }
        /// <summary>
        /// 查找下一个的Posintion集合变化
        /// </summary>
        private void SearchNextChange()
        {
            if (_searchTextIsChanged ||
                    _searchScopeChanged ||
                    _searchTargetChanged ||
                    _matchCaseChanged ||
                    _matchWholeWordChanged
                   )
            {
                FindOptions.Singler.CurrentPosition = null;
                FindOptions.Singler.StartPosition = null;
                _searchTextIsChanged = false;
                _searchScopeChanged = false;
                _searchTargetChanged = false;
                _matchCaseChanged = false;
                _matchWholeWordChanged = false;
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 : 初始化 
        /// </summary>
        private void Init()
        {
            InitComboBox();
            GetPosition();

            textBoxSearchContent.Text = FindOptions.Singler.FindContent;
            textBoxReplaceContent.Text = FindOptions.Singler.ReplaceContent;

            //查找选项部分的初始化
            checkBoxMatchCase.Checked = FindOptions.Singler.MatchCase;
            checkBoxMatchWholeWord.Checked = FindOptions.Singler.MatchWholeWord;
            checkBoxUpWard.Checked = FindOptions.Singler.UpWard;
            checkBoxUsing.Checked = FindOptions.Singler.IsUsingFindType;

            //填充现在的查找方式
            comboBoxSearchType.Text = ResourceService.GetEnumResourceText(typeof(FindType), FindOptions.Singler.FindType.ToString());

            //填充现在的查找目标类型
            comboBoxSearchTarget.Text = ResourceService.GetEnumResourceText(typeof(FindTargetType), FindOptions.Singler.FindTargetType.ToString());

            //填充现在的查找范围
            comboBoxSearchScope.Text = ResourceService.GetEnumResourceText(typeof(FindScope), FindOptions.Singler.FindScope.ToString());
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 : 初始化下拉文本框的内容
        /// </summary>
        private void InitComboBox()
        {
            comboBoxSearchScope.Items.Clear();
            comboBoxSearchTarget.Items.Clear();
            comboBoxSearchType.Items.Clear();
            string[] type = Enum.GetNames(typeof(FindType));
            for (int i = 0; i < type.Length; i++)
            {
                type[i] = ResourceService.GetEnumResourceText(typeof(FindType), type[i]);
            }
            comboBoxSearchType.Items.AddRange(type);

            type = Enum.GetNames(typeof(FindTargetType));
            for (int i = 0; i < type.Length; i++)
            {
                type[i] = ResourceService.GetEnumResourceText(typeof(FindTargetType), type[i]);
            }
            comboBoxSearchTarget.Items.AddRange(type);

            type = Enum.GetNames(typeof(FindScope));
            for (int i = 0; i < type.Length; i++)
            {
                type[i] = ResourceService.GetEnumResourceText(typeof(FindScope), type[i]);
            }
            comboBoxSearchScope.Items.AddRange(type);
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-29 : 当 查找状态 切换到 替换状态（或反向切换）时，窗体的布局刷新
        /// </summary>
        private void ResetFormSizeWhenIsFindChanged()
        {
            toolStripButtonSearch.Checked = _isFind;
            toolStripButtonReplace.Checked = !_isFind;
            labelReplaceContent.Visible = !_isFind;
            textBoxReplaceContent.Visible = !_isFind;
            foreach (Control control in Controls)
            {
                if (control.Name == "toolStripSearchAndReplace" ||
                    control.Name == "labelSearchContent" ||
                    control.Name == "textBoxSearchContent" ||
                    control.Name == "labelReplaceContent" ||
                    control.Name == "textBoxReplaceContent"||
                    control.Name == "buttenUsing")
                {
                    continue;
                }
                if (_isFind)
                    control.Location = new Point(control.Location.X, control.Location.Y - OffsetWhenFindOrReplace);
                else
                    control.Location = new Point(control.Location.X, control.Location.Y + OffsetWhenFindOrReplace);
            }
            if (_isFind)
            {
                this.Height -= OffsetFormSize;
                buttonSearchAll.Visible = true;
                buttonReplace.Visible = false;

                buttonReplaceAll.Visible = false;
            }
            else
            {
                this.Height += OffsetFormSize;
                buttonSearchAll.Visible = false;
                buttonReplace.Visible = true;
                buttonReplaceAll.Visible = true;
            }
        }



        /// <summary>
        /// 显示查找到的结果（位置）
        /// </summary>
        /// <param name="_currentPosition">查找到的位置</param>
        private void DisplayFindedResult(Position _currentPosition)
        {
            if (_currentPosition == null)
            {
                MessageService.Show("未找到结果，搜索回到起始点。");
            }
            else
            {

                Position position = _currentPosition;
                XmlDocument xmlDocument = (XmlDocument)position.GetDocument();
                Type type = xmlDocument.GetType();
                switch (type.Name)
                {
                    case "HrXmlDocument":
                        HrXmlDocument hrDoc = (HrXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm hrForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, hrDoc.Id) as MdiBaseEditViewForm;
                        hrForm.MarkPosition(position);
                        break;
                    case "InviteBiddingXmlDocument":
                        InviteBiddingXmlDocument bidDoc = (InviteBiddingXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm bidForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, bidDoc.Id) as MdiBaseEditViewForm;
                        bidForm.MarkPosition(position);
                        break;
                    case "KnowledgeXmlDocument":
                        KnowledgeXmlDocument knowDoc = (KnowledgeXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm knowForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, knowDoc.Id) as MdiBaseEditViewForm;
                        knowForm.MarkPosition(position);
                        break;
                    case "ProductXmlDocument":
                        ProductXmlDocument prodcutDoc = (ProductXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm productForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, prodcutDoc.Id) as MdiBaseEditViewForm;
                        productForm.MarkPosition(position);
                        break;
                    case "ProjectXmlDocument":
                        ProjectXmlDocument projectDoc = (ProjectXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm projectForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, projectDoc.Id) as MdiBaseEditViewForm;
                        projectForm.MarkPosition(position);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 设置需要搜索的文件集
        /// </summary>
        private void GetSearchDocuments()
        {
            _searchDocuments.Clear();
            KeyValuePair<ISearch, IMarkPosition> kv = new KeyValuePair<ISearch, IMarkPosition>();
            switch (FindOptions.Singler.FindScope)
            {
                case FindScope.CurrentForm://当前窗口 
                    #region
                    {
                        if (GetFormDocument((BaseViewForm)_mainFindForm.ActiveMdiChild, out kv))
                        {
                            _currentForm = kv.Value;
                            _currentDocument = kv.Key;
                            _searchDocuments.Add(kv.Key);
                        }
                        break;
                    }
                    #endregion
                case FindScope.AllOpenForm://所有打开的窗体
                    #region
                    {
                        if (GetFormDocument((BaseViewForm)_mainFindForm.ActiveMdiChild, out kv))
                        {
                            _currentForm = kv.Value;
                            _currentDocument = kv.Key;
                            _searchDocuments.Add(kv.Key);
                        }
                        foreach (BaseViewForm form in _mainFindForm.MdiChildren)
                        {
                            if (form == _mainFindForm.ActiveMdiChild)
                            {
                                continue;
                            }
                            if (GetFormDocument(form, out kv))
                            {
                                _searchDocuments.Add(kv.Key);
                            }
                        }
                        break;
                    }
                    #endregion
                case FindScope.WholeChannels://所有的频道
                    #region
                    {
                        if (GetFormDocument((BaseViewForm)_mainFindForm.ActiveMdiChild, out kv))
                        {
                            _currentForm = kv.Value;
                            _currentDocument = kv.Key;
                        }
                        // string[] tmpltIds = Service.Sdsite.CurrentDocument.GetAllTmpltId();
                        string[] pageIds = Service.Sdsite.CurrentDocument.GetAllPageId();
                        //foreach (string tmpletId in tmpltIds)
                        //{
                        //    TmpltXmlDocument tmpltDoc = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(tmpletId);
                        //    if ((tmpltDoc as ISearch) == kv.Key)
                        //    {
                        //        continue;
                        //    }
                        //    if (tmpltDoc != null)
                        //    {
                        //        _searchDocuments.Add(tmpltDoc);
                        //    }

                        //}
                        foreach (string pageId in pageIds)
                        {
                            PageXmlDocument pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById(pageId);
                            if ((pageDoc as ISearch) == kv.Key)
                            {
                                continue;
                            }
                            if (pageDoc != null)
                            {
                                _searchDocuments.Add(pageDoc);
                            }
                        }
                        break;
                    }
                    #endregion
                default:
                    break;
            }
        }

        /// <summary>
        /// 通过窗体得到ISearch和IMarkPosition
        /// </summary>
        /// <param name="form"></param>
        /// <param name="kv">Key是ISearch,Value是IMarkPosition</param>
        /// <returns>是否可以转换</returns>
        private bool GetFormDocument(BaseViewForm form, out KeyValuePair<ISearch, IMarkPosition> kv)
        {
            if (form == null)
            {
                kv = new KeyValuePair<ISearch, IMarkPosition>();
                return false;
            }
            ISearch _search;
            IMarkPosition _markPosition;
            switch (form.WorkDocumentType)
            {
                #region
                //case WorkDocumentType.TmpltDesigner:
                //    MdiTmpltDesignForm tmpltForm = (MdiTmpltDesignForm)form;
                //    _markPosition = tmpltForm;
                //    _search = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(tmpltForm.TmpltID);
                //    break;
                case WorkDocumentType.HtmlDesigner:
                    MdiHtmlDesignForm htmlForm = (MdiHtmlDesignForm)form;
                    _markPosition = htmlForm;
                    _search = Service.Sdsite.CurrentDocument.GetPageDocumentById(htmlForm.PageId);
                    break;
                //case WorkDocumentType.SnipDesigner:
                //    MdiSnipDesignerForm snipForm = (MdiSnipDesignerForm)form;
                //    _markPosition = snipForm;
                //    _search = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(snipForm.TmpltID);
                //    break;
                case WorkDocumentType.Edit:
                    MdiBaseEditViewForm editForm = (MdiBaseEditViewForm)form;
                    _markPosition = editForm;
                    _search = Service.Sdsite.CurrentDocument.GetPageDocumentById(editForm.PageId);
                    break;
                //case WorkDocumentType.HomePage:
                //    MdiHomePageDesignForm homeForm = (MdiHomePageDesignForm)form;
                //    _markPosition = homeForm;
                //    _search = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(homeForm.TmpltID);
                //    break;
                case WorkDocumentType.None:
                case WorkDocumentType.WebBrowser:
                case WorkDocumentType.Manager:
                case WorkDocumentType.StartupPage:
                case WorkDocumentType.SiteProperty:
                default:
                    kv = new KeyValuePair<ISearch, IMarkPosition>();
                    return false;
                #endregion
            }
            kv = new KeyValuePair<ISearch, IMarkPosition>(_search, _markPosition);
            return true;
        }

        #endregion

        #region 事件响应

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当点击查找选项的展开关闭按钮时......
        /// </summary>
        private void buttonSearchOption_Click(object sender, EventArgs e)
        {
            _optionHiddened = !_optionHiddened;
            groupBoxSearchOption.Visible = !_optionHiddened;
            labelSearchOption.Visible = _optionHiddened;
            LineSearchOption.Visible = _optionHiddened;
            if (_optionHiddened)
            {
                buttonSearchNext.Location =
                    new Point(buttonSearchNext.Location.X, buttonSearchNext.Location.Y - OffsetWhenOptionHidden);
                buttonReplace.Location =
                    new Point(buttonReplace.Location.X, buttonReplace.Location.Y - OffsetWhenOptionHidden);
                buttonReplaceAll.Location =
                    new Point(buttonReplaceAll.Location.X, buttonReplaceAll.Location.Y - OffsetWhenOptionHidden);
                buttonSearchAll.Location =
                    new Point(buttonSearchAll.Location.X, buttonSearchAll.Location.Y - OffsetWhenOptionHidden);
                this.Height -= OffsetWhenOptionHidden;
            }
            else
            {
                buttonSearchNext.Location =
                    new Point(buttonSearchNext.Location.X, buttonSearchNext.Location.Y + OffsetWhenOptionHidden);
                buttonReplace.Location =
                    new Point(buttonReplace.Location.X, buttonReplace.Location.Y + OffsetWhenOptionHidden);
                buttonReplaceAll.Location =
                    new Point(buttonReplaceAll.Location.X, buttonReplaceAll.Location.Y + OffsetWhenOptionHidden);
                buttonSearchAll.Location =
                    new Point(buttonSearchAll.Location.X, buttonSearchAll.Location.Y + OffsetWhenOptionHidden);
                this.Height += OffsetWhenOptionHidden;
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当选择或取消选择“大小写匹配”时......
        /// </summary>
        private void checkBoxMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.MatchCase = checkBoxMatchCase.Checked;
            _matchCaseChanged = true;
            SearchNextChange();
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当选择或取消选择“全字匹配”时......
        /// </summary>
        private void checkBoxMatchWholeWord_CheckedChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.MatchWholeWord = checkBoxMatchWholeWord.Checked;
            _matchWholeWordChanged = true;
            SearchNextChange();
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当选择或取消选择“向上搜索”时......
        /// </summary>
        private void checkBoxUpWard_CheckedChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.UpWard = checkBoxUpWard.Checked;
            _upWardChanged = true;
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当选择或取消选择“使用”时......
        /// </summary>
        private void checkBoxUsing_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSearchType.Enabled = checkBoxUsing.Checked;
            FindOptions.Singler.IsUsingFindType = checkBoxUsing.Checked;
            if (FindOptions.Singler.IsUsingFindType)
            {
                this.buttenUsing.Enabled = true;
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当点击菜单按钮的查找部分......
        /// </summary>
        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonSearch.Checked)
            {
                _isFind = true;
                ResetFormSizeWhenIsFindChanged();
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当点击菜单按钮的替换部分......
        /// </summary>
        private void toolStripButtonReplace_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonReplace.Checked)
            {
                _isFind = false;
                ResetFormSizeWhenIsFindChanged();
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-29 14:30
        /// 当窗体开始运行时......
        /// </summary>
        private void FindAndReplaceForm_Load(object sender, EventArgs e)
        {
            InitComboBox();
            Init();
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-29 16:30
        /// 当"查找内容"的文本框的内容发生变化时......
        /// </summary>
        private void textBoxSearchContent_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxSearchContent.Text))
            {
                FindOptions.Singler.FindContent = textBoxSearchContent.Text;
                _searchTextIsChanged = true;
                SearchNextChange();
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-29 16:30
        /// 当"替换为"的文本框的内容发生变化时......
        /// </summary>
        private void textBoxReplaceContent_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxReplaceContent.Text))
            {
                FindOptions.Singler.ReplaceContent = textBoxReplaceContent.Text;
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-29 16:30
        /// 当"查找范围"的下拉框选择内容发生变化时......
        /// </summary>
        private void comboBoxSearchScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.FindScope = (FindScope)comboBoxSearchScope.SelectedIndex;
            _searchScopeChanged = true;
            SearchNextChange();
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-29 16:30
        /// 当"查找目标类型"的下拉框选择内容发生变化时......
        /// </summary>
        private void comboBoxSearchTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.FindTargetType = (FindTargetType)comboBoxSearchTarget.SelectedIndex;
            _searchTargetChanged = true;
            SearchNextChange();
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-29 16:30
        /// 当"查找方式,使用"的下拉框选择内容发生变化时......
        /// </summary>
        private void comboBoxSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.FindType = (FindType)comboBoxSearchType.SelectedIndex;
        }

        /// <summary>
        /// edited by lisuye at 2008-06-17 10:00
        /// 当点击替换按钮时......
        /// </summary>
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            this.Searching(WantToDoType.ReplaceNext);
        }

        /// <summary>
        /// edited by lisuye at 2008-06-13 10:00
        /// 当点击查找下一个按钮时......
        /// </summary>
        private void buttonSearchNext_Click(object sender, EventArgs e)
        {
            GetPosition();
            this.Searching(WantToDoType.SearchNext);
        }
        /// <summary>
        /// 查找中.......
        /// </summary>
        /// <param name="wantToDoType"></param>
        private void Searching(WantToDoType wantToDoType)
        {
            GetSearchDocuments();
            foreach (ISearch _document in _searchDocuments)
            {
                while (true)
                {
                    //判断起始点
                    Position currentPosition = FindOptions.Singler.CurrentPosition;
                    if (currentPosition != null && FindOptions.Singler.StartPosition == null)
                    {
                        PropertyInfo property = currentPosition.Property;
                        string value = property.GetValue(currentPosition.GetDocument(), null).ToString();
                        string result = value.Substring(currentPosition.Index, currentPosition.Length);
                        if (result.Equals(FindOptions.Singler.FindContent))
                        {
                            FindOptions.Singler.StartPosition = currentPosition;
                        }
                    }
                    Position hasPosition = _document.SearchNext(wantToDoType);
                    if (hasPosition != null)
                    {
                        FindOptions.Singler.IsFindResult = true;
                        Position position = FindOptions.Singler.StartPosition;
                        if (position != null)
                        {
                            if (hasPosition.Index == position.Index &&
                                hasPosition.Length == position.Length &&
                                position.Property == hasPosition.Property)
                            {
                                DisplayFindedResult(hasPosition);
                                MessageService.Show("搜索回到起点");
                                break;
                            }
                            else
                            {
                                DisplayFindedResult(hasPosition);
                                break;
                            }
                        }
                        if (FindOptions.Singler.StartPosition == null)
                        {
                            FindOptions.Singler.StartPosition = hasPosition;

                        }
                        DisplayFindedResult(hasPosition);
                        break;
                    }
                    else
                    {
                        //标识未找到
                        if(!FindOptions.Singler.IsFindResult)
                        {
                            break;
                        }
                        if (FindOptions.Singler.UpWard)
                        {
                            FindOptions.Singler.CurrentPosition = FindOptions.Singler.LastPosition;
                        }
                        else
                        {
                            if (wantToDoType != WantToDoType.ReplaceNext)
                                FindOptions.Singler.CurrentPosition = null;
                        }
                        if (wantToDoType == WantToDoType.ReplaceNext)
                        {
                            MessageService.Show("搜索回到起点");
                            break;
                        }
                        else
                            continue;
                    }
                }
            }
            //未搜索到结果
            if (!FindOptions.Singler.IsFindResult)
            {
                DisplayFindedResult(null);
            }
        }

        /// <summary>
        /// 获得当前的Position
        /// </summary>
        public void GetPosition()
        {
            _searchDocuments.Clear();
            KeyValuePair<ISearch, IMarkPosition> kv = new KeyValuePair<ISearch, IMarkPosition>();
            if (GetFormDocument((BaseViewForm)_mainFindForm.ActiveMdiChild, out kv))
            {
                _currentForm = kv.Value;
                _currentDocument = kv.Key;
                _searchDocuments.Add(kv.Key);
                Position position = _currentForm.CurrentPosition;
            }
        }

        /// <summary>
        /// edited by lisuye at 2008-06-13 10:00
        /// 当点击查找全部按钮时......
        /// </summary>
        private void buttonSearchAll_Click(object sender, EventArgs e)
        {
            FindOptions.Singler.Positions.Clear();
            GetSearchDocuments();
            foreach (ISearch _document in _searchDocuments)
            {
                while (true)
                {
                    Position hasPosition = _document.SearchNext(WantToDoType.SearchAll);
                    if (hasPosition == null)
                    {
                        break;
                    }
                }
            }
            WorkbenchForm.MainForm.MainResultPad.Show(WorkbenchForm.MainForm.MainDockPanel, DockState.DockBottom);
            WorkbenchForm.MainForm.MainResultPad.ForOtherSearchForm();
        }

        /// <summary>
        /// edited by zhenghao at 2008-06-02 10:00
        /// 当点击全部替换按钮时......
        /// </summary>
        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            FindOptions.Singler.Positions.Clear();
            GetSearchDocuments();
            foreach (ISearch _document in _searchDocuments)
            {
                while (true)
                {
                    Position hasPosition = _document.SearchNext(WantToDoType.ReplaceAll);
                    if (hasPosition == null)
                    {
                        break;
                    }
                }
            }
        }

        #endregion

        #region 自定义事件

        #endregion
    }


}
