using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Threading;
using System.Xml;
using Jeelu.Win;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
     public partial class MdiBaseListViewForm : MdiBaseViewForm, IGetPropertiesForPanelable
    {
        #region 控件定义
         private bool tagIsCut;
        protected DataGridViewEx DataGrid { get; set; }
        private Label _tipLabel;
        private Dictionary<string, DataGridViewRow> _idc;
        private List<PageSimpleExXmlElement> _list=null;
        #endregion

        #region 构造函数及变量定义
        /// <summary>
        /// 构造函数
        /// </summary>
        public MdiBaseListViewForm(string channelId)
        {
            this._channelId = channelId;
            _idc = new Dictionary<string, DataGridViewRow>();
            StateSave.RowList = new List<int>();
            InitControl();
            _list = ChannelSelect();
            tagIsCut = false;
            Application.Idle += new EventHandler(Application_Idle);
        }
         //频道筛选之后，将筛选的页面保存 by lisuye on 2008年5月27日
        private List<PageSimpleExXmlElement> ChannelSelect()
        {
            List<PageSimpleExXmlElement> PageList = null;
            if (State.PageTypeListAll != null && !string.IsNullOrEmpty(State.ChannelId))
            {
                PageList = new List<PageSimpleExXmlElement>();
                FolderXmlElement folderEle = Service.Sdsite.CurrentDocument.GetFolderElementById(State.ChannelId);
                foreach (PageSimpleExXmlElement pageEle in folderEle.SelectNodes("//channel[@id='" + State.ChannelId + "']//page"))
                {
                    foreach (PageType pagetype in State.PageTypeListAll)
                    {
                        if (pagetype == pageEle.PageType)
                        {
                            PageList.Add(pageEle);
                            break;
                        }
                    }
                }

            }
            return PageList;
        }
        //判断工具栏里面的推广和发布按钮的状态 by lisuye on 2008年5月27日
        void Application_Idle(object sender, EventArgs e)
        {
            if (Service.Sdsite.IsOpened)
            {
                if (this.DataGrid.SelectedRows.Count != 0)
                {
                    ToolAdAndPublish();
                }
            }
        }
        #endregion

        #region 显示bool
        protected ViewBoolMethod ParseChecked(string isText, string isNotText)
        {
            ToolStripMenuItem itemIs = ((ToolStripMenuItem)this.MenuForGrid.Items["filterItem"]).DropDownItems[isText] as ToolStripMenuItem;
            ToolStripMenuItem itemIsNot = ((ToolStripMenuItem)this.MenuForGrid.Items["filterItem"]).DropDownItems[isNotText] as ToolStripMenuItem;
            if (itemIs.Checked)
            {
                if (itemIsNot.Checked)
                {
                    return ViewBoolMethod.All;
                }
                else
                {
                    return ViewBoolMethod.True;
                }
            }
            else
            {
                if (itemIsNot.Checked)
                {
                    return ViewBoolMethod.Flase;
                }
                else
                {
                    return ViewBoolMethod.Nothing;
                }
            }
        }

        #endregion

        #region 控件设置

        private IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitControl()
        {
            this._tipLabel = new Label();

            GridSetting();

            this.components = new System.ComponentModel.Container();

            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Tahoma", 8.25F);
            this.Name = "MdiBaseListViewForm";
            this.Controls.Add(this.DataGrid);
            this.Controls.Add(this._tipLabel);
            this._tipLabel.Hide();
            this.DataGrid.Dock = DockStyle.Fill;
            this.DataGrid.BringToFront();

            this.BuildMenuAndToolStrip();

            DefindEvent();
        }

        /// <summary>
        /// 定义窗体配置文件路径  by lisuye on 2008年5月27日
        /// </summary>
        private string ThisConfigPath
        {
            get
            {
                string path = Path.Combine(PathService.CL_Dialog_Folder, "MdiBaseListViewForm.Xml");
                if (File.Exists(path))
                {
                    return path;
                }
                throw new Exception(this.GetTextResource("error") + path + this.GetTextResource("tagerror"));
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ///移除关联的事件  by lisuye on 2008年5月27日
            UnloadEvent();

            base.OnFormClosed(e);
        }

        #region 控件设置:DataGridView设置

        /// <summary>
        /// DataGridView的一系列设置  by lisuye on 2008年5月27日
        /// </summary>
        private void GridSetting()
        {
            this.DataGrid = new DataGridViewEx();
            this.MenuForGrid = new ContextMenuStrip();

            /// 奇数行的设置  by lisuye on 2008年5月27日
            DataGridViewCellStyle alternatingStyle = new DataGridViewCellStyle();
            alternatingStyle.BackColor = Color.FromArgb(255, 255, 245);

            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.BorderStyle = BorderStyle.None;
            this.DataGrid.RowHeadersWidth = 22;
            this.DataGrid.BackgroundColor = Color.White;
            this.DataGrid.ColumnHeadersHeight = 30;
            this.DataGrid.ReadOnly = true;
            this.DataGrid.RowTemplate.Height = 23;
            this.DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGrid.ContextMenuStrip = this.MenuForGrid;
            this.DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGrid.AlternatingRowsDefaultCellStyle = alternatingStyle;

            this.DataGrid.GridColor = SystemColors.ActiveBorder;
            this.DataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 200, 255);

        }

        #endregion

        #region 数据为空时的界面提示

        private int _tipWidth;
        private int _headBottom;
        /// <summary>
        /// 当数据为空时显示提示信息  by lisuye on 2008年5月27日
        /// </summary>
        private void WhileDataIsEmptied()
        {
            Graphics g = this.CreateGraphics();

            this._tipLabel.Text = DataIsEmptyTipText;
            this._tipLabel.AutoSize = true;
            this._tipLabel.BackColor = Color.White;

            _tipWidth = Convert.ToInt32(
                g.MeasureString(this.DataIsEmptyTipText, this.Font).Width);
            _headBottom = this.DataGrid.Location.Y + this.DataGrid.ColumnHeadersHeight;
            g.Dispose();

            this._tipLabel.Location = new Point(
                this.DataGrid.Width / 2 - _tipWidth / 2,
                _headBottom + 20);
            this._tipLabel.Show();
            this._tipLabel.BringToFront();
        }
        #endregion

        #endregion

        #region 抛出的属性

        /// <summary>
        /// 被选择的行所关联的项目的ID   by lisuye on 2008年5月27日
        /// </summary>
        public string SelectedItemId
        {
            get
            {
                if (this.DataGrid.SelectedRows.Count > 0)
                {
                    return this.DataGrid.SelectedRows[0].Tag.ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// 被选择的行所关联的项目的ID集合  by lisuye on 2008年5月27日
        /// </summary>
        public List<string> SelectedItemIds
        {
            get
            {
                List<string> strList = new List<string>();
                foreach (DataGridViewRow row in this.DataGrid.SelectedRows)
                {
                    strList.Add(row.Tag.ToString());
                }
                return strList;
            }
        }

        private string _channelId;
        public string ChannelId
        {
            get { return _channelId; }
            set { _channelId = value; }
        }

        string _channelIdForNewPage;
        public string ChannelIdForNewPage
        {
            get { return _channelIdForNewPage; }
            set { _channelIdForNewPage = value; }
        }

        private ToolStrip _toolStrip;
        protected ToolStrip ToolStrip
        {
            get { return _toolStrip; }
            set { _toolStrip = value; }
        }

        private ContextMenuStrip _menuForGridHeader = null;
        protected ContextMenuStrip MenuForGridHeader
        {
            get { return _menuForGridHeader; }
            set { _menuForGridHeader = value; }
        }

        private ContextMenuStrip _menuForGrid;
        protected ContextMenuStrip MenuForGrid
        {
            get { return _menuForGrid; }
            set { _menuForGrid = value; }
        }

        private string _dataIsEmptyTip = string.Empty;
        /// <summary>
        /// 数据为空时提示用户的文字  by lisuye on 2008年5月27日
        /// </summary>
        protected string DataIsEmptyTipText
        {
            get { return _dataIsEmptyTip; }
            set { _dataIsEmptyTip = value; }
        }
        #endregion

        #region OnLoad  OnClosed  OnResize  OnShown

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //工具栏的初始化  by lisuye on 2008年5月27日
            this.ToolStripAdico();
            this.MarkColumns();
            if (_list != null)
            {
                this.MarkRows(SimpleState.All, _list);
            }
            else
            {
                this.MarkRows(SimpleState.All, null);
            }
            
            this.TitleText();
            _dataIsEmptyTip = this.GetTextResource("tag");
            //把用户自定义搜索的ToolStripItem填充上值  by lisuye on 2008年5月27日
            this.AdUGS();
            //初始化DataGridViewRow的值  by lisuye on 2008年5月27日
            this.GetDataGridViewRowValue();
            
        }
        public override string Id
        {
            get { return Service.SiteDataManager.StartupPageId; }
        }
        protected override void OnClosed(EventArgs e)
        {
            //整个管理form状态的保存  by lisuye on 2008年5月27日
            SaveState();
            base.OnClosed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this._tipLabel.Location = new Point(
                this.DataGrid.Width / 2 - _tipWidth / 2,
                _headBottom + 20);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (this.DataGrid.Rows.Count <= 0)
            {
                WhileDataIsEmptied();
            }
            this._isToolStripCreated = true;
            NewOpenState();
        }
        #endregion
        #region SaveState
         /// <summary>
        /// 保存管理信息的状态  by lisuye on 2008年5月27日
         /// </summary>
        protected void SaveState()
        {

           ToolStripComboBox comboBoxfv = ((ToolStripComboBox)this.ToolStrip.Items["findValue"]);
           ToolStripComboBox comboBoxTime = ((ToolStripComboBox)this.ToolStrip.Items["findTimeType"]);
           ToolStripComboBox comboBoxType = ((ToolStripComboBox)this.ToolStrip.Items["findTimeConfig"]);
           ToolStripDropDownButton comboBoxFilter = ((ToolStripDropDownButton)this.ToolStrip.Items["filterItem"]);

           StateSave.Search=comboBoxfv.Text;
           StateSave.Filter = comboBoxFilter.Text;
           StateSave.EditTime = comboBoxTime.Text;
           StateSave.DateNumber = comboBoxType.Text;
           List<string> list = new List<string>(); 
           foreach (DataGridViewColumn column in this.DataGrid.Columns)
           {
               if (column.Visible)
               {
                   list.Add(column.Name);
               }
           }
           StateSave.ColumnName = list.ToArray();
           StateSave.PageEleList = _list;
        }
        #endregion
        #region NewOpenState
         /// <summary>
        /// 新打开时的状态  by lisuye on 2008年5月27日
         /// </summary>
        protected void NewOpenState()
        {
                ToolStripComboBox comboBoxfv = ((ToolStripComboBox)this.ToolStrip.Items["findValue"]);
                ToolStripComboBox comboBoxTime = ((ToolStripComboBox)this.ToolStrip.Items["findTimeType"]);
                ToolStripComboBox comboBoxType = ((ToolStripComboBox)this.ToolStrip.Items["findTimeConfig"]);
                ToolStripDropDownButton comboBoxFilter = ((ToolStripDropDownButton)this.ToolStrip.Items["filterItem"]);
                if (!string.IsNullOrEmpty(StateSave.Filter))
                comboBoxFilter.Text = StateSave.Filter;
                if (!string.IsNullOrEmpty(StateSave.DateNumber))
                comboBoxType.Text = StateSave.DateNumber;
                if (!string.IsNullOrEmpty(StateSave.EditTime))
                comboBoxTime.Text = StateSave.EditTime;
                if (!string.IsNullOrEmpty(StateSave.Search))
                comboBoxfv.Text = StateSave.Search;
                FindValue_SelectedIndexChanged();
                //设置列的状态  by lisuye on 2008年5月27日
                if (StateSave.ColumnName != null)
                {
                    string[] names = StateSave.ColumnName;
                    List<string> list = new List<string>();
                    foreach (DataGridViewColumn column in this.DataGrid.Columns)
                    {
                        column.Visible = false;
                        foreach (string name in names)
                        {
                            if (column.Name.Equals(name))
                            {
                                column.Visible = true;
                                BuildGridHeaderCellContentMenuStrip();
                                ToolStripMenuItem item = new ToolStripMenuItem(((string[])column.HeaderCell.Tag)[0]);
                                item.Checked = !item.Checked;
                            }
                        }
                    }
                }
        }
        #endregion
        #region 工具栏图标的加载  by lisuye on 2008年5月27日
        private void ToolStripAdico()
        {
            this.ToolStrip.ImageList = ResourceService.MainImageList;
            this.ToolStrip.Items[0].ImageKey = "page.img.new";
            this.ToolStrip.Items[1].ImageKey = "page.img.edit";
            this.ToolStrip.Items[2].ImageKey = "page.img.deleted";
            this.ToolStrip.Items[4].ImageKey = "page.img.publish";
            this.ToolStrip.Items[5].ImageKey = "page.img.zpublish";
            this.ToolStrip.Items[6].ImageKey = "page.img.ad";
            this.ToolStrip.Items[7].ImageKey = "page.img.zad";
            this.ToolStrip.Items[9].ImageKey = "page.img.refurbish";
            this.ToolStrip.Items[11].ImageKey = "";
        }
        #endregion

        #region column row serach

        private DataGridViewColumn[] _columns;

        public DataGridViewColumn[] Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
        private void TitleText()
        {
            if (this.ChannelId == "00000000")
            {
                this.Text = string.Format(this.GetTextResource("titleFormat"), this.GetTextResource("showAll"));
            }
            //else
            //{
            //    string tagTitle = Service.Sdsite.CurrentDocument.GetChannelElementById(this.ChannelId).Title;
            //    this.Text = string.Format(this.GetTextResource("titleFormat"), tagTitle);

            //}
        }
        /// <summary>
        /// 调用Document的静态方法创建DataGridView列。 by lisuye on 2008年5月27日
        /// </summary>
        public virtual void MarkColumns()
        {
            DataGridViewColumn[] columns = PageXmlDocument.ToDataGridViewColumns();//Service.ListView.Columns;
            foreach (DataGridViewColumn column in columns)
            {
                if (((Type)column.Tag).FullName == typeof(DateTime).FullName)
                {
                    ToolStripComboBox comboBox = ((ToolStripComboBox)this.ToolStrip.Items["findTimeType"]);
                    string columnText = this.GetTextResource(column.HeaderText);
                    if (string.IsNullOrEmpty(columnText))
                    {
                        columnText = column.HeaderText;
                    }
                    comboBox.Items.Add(columnText);
                    int index = comboBox.Items.IndexOf(columnText);
                    this._findTimeTypeDic.Add(index, column);
                    if (columnText == this.GetTextResource("ModifiedTime"))
                    {
                        comboBox.SelectedItem = columnText;
                    }
                }
            }

            if (columns != null)
            {
                this._columns = columns;
                foreach (DataGridViewColumn column in columns)
                {
                    if (!string.IsNullOrEmpty(column.Name))
                    {
                        column.HeaderCell.ToolTipText = this.GetTextResource(column.Name);
                    }
                    if (!string.IsNullOrEmpty(column.HeaderText))
                    {
                        column.HeaderText = this.GetTextResource(column.HeaderText);
                    }
                    string[] headerCell = (string[])column.HeaderCell.Tag;
                    headerCell[0] = this.GetTextResource(headerCell[0]);
                    if (headerCell[1].Contains(","))
                    {
                        int tagindex = headerCell[1].IndexOf(",");
                        string tempString = this.GetTextResource(headerCell[1].Substring(0, tagindex));
                        headerCell[1] = tempString + "," + headerCell[1].Substring(tagindex);
                    }
                    column.HeaderCell.Tag = headerCell;
                }
                this.DataGrid.Columns.AddRange(columns);
            }

        }

        Dictionary<int, DataGridViewColumn> _findTimeTypeDic = new Dictionary<int, DataGridViewColumn>();

        public PageSimpleState Serach(SimpleState state)
        {
            switch (state)
            {
                case SimpleState.All:
                    return PageSimpleState.Unknown;
                case SimpleState.New:
                    return PageSimpleState.New;
                case SimpleState.PublishAndIsModify:
                    return PageSimpleState.Modified;
                case SimpleState.PublishAndNotIsModify:
                    return PageSimpleState.NotModified;
                default:
                    return PageSimpleState.Unknown;
            }
        }

        /// <summary>
        /// 调用CorpDataXmlDocument的静态方法根据列的特性创建Row  by lisuye on 2008年5月27日
        /// </summary>
        /// <summary>
        /// 根据列的特性创建Row  by lisuye on 2008年5月27日
        /// </summary>
        public virtual void MarkRows(SimpleState state,List<PageSimpleExXmlElement>eles)
        {
            int totleCount = 0;
            int partCount = 0;
            BaseScrollbarForm scrollForm = new BaseScrollbarForm();
            this.Cursor = Cursors.WaitCursor; 
            if (state == SimpleState.All)
            {
                ToolStripComboBox comboBoxfv = ((ToolStripComboBox)this.ToolStrip.Items["findValue"]);
                comboBoxfv.Text = string.Empty;
                ToolStripComboBox comboBoxType = ((ToolStripComboBox)this.ToolStrip.Items["findTimeConfig"]);
                comboBoxType.Text = comboBoxType.Items[comboBoxType.Items.Count - 1].ToString();
            }
            if (eles == null)
            {
                eles = new List<PageSimpleExXmlElement>();
               XmlNodeList elestag = Service.Sdsite.CurrentDocument.SelectNodes("//page");
               foreach (PageSimpleExXmlElement node in elestag)
               {
                   eles.Add(node);
               }
            }
            totleCount = eles.Count+this.DataGrid.Rows.Count;
            if (totleCount > 300)
            {
                scrollForm.Show(this);
                Thread.Sleep(1000);
            }
            int i = 0;
            foreach (PageSimpleExXmlElement pageEle in eles)
            {
                i++;
                PageXmlDocument doc = (PageXmlDocument)pageEle.GetIndexXmlDocument();
                RowEx row = RowEx.GetDataGridViewRow(this.Columns,doc);
                row.ContextMenuStrip = this.MenuForGrid;
                if (Serach(state) == PageSimpleState.Unknown && doc.IsDeleted == false)
                {
                    this.DataGrid.Rows.Add(row);
                    _idc.Add(doc.Id, row);

                }
                if (doc.SimpleState == Serach(state) && doc.IsDeleted == false)
                {
                    this.DataGrid.Rows.Add(row);
                    _idc.Add(doc.Id, row);
                }
                partCount = row.Index + 1;
                if (totleCount > 0 && partCount > 0)
                {
                    scrollForm.ProgressBar.Value = (int)((float)(partCount / totleCount) * 100);
                }
            }
            scrollForm.Close();
            this.Cursor = Cursors.Default;
            
        }

        public virtual void GetDataGridViewRowValue()
        {
            RowList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in this.DataGrid.Rows)
            {
                RowList.Add(row);
            } 
        }
        //用户自定义搜索  by lisuye on 2008年5月27日 
        public virtual void AdUGS()
        {
            string SerachEleName ="childSearchContent";
            XmlElement SearchEle = Service.Sdsite.DesignDataDocument.GetElementByName(SerachEleName);
            ToolStripComboBox comboBoxfv = ((ToolStripComboBox)this.ToolStrip.Items["findValue"]);
            if (SearchEle != null)
            {
                foreach (SearchContentElement childEle in SearchEle.ChildNodes)
                {
                    comboBoxfv.Items.Add(childEle.SearchContent.ToString());
                }
                SearchContentElement ele = SearchEle.FirstChild as SearchContentElement;
            }
            comboBoxfv.Text = string.Empty;
            
        }
        #endregion

        #region 事件

        private void DefindEvent()
        {
            #region DataGrid事件

            this.DataGrid.KeyDown += new KeyEventHandler(this.DataGrid_KeyDown);
            this.DataGrid.ColumnAdded += new DataGridViewColumnEventHandler(this.DataGrid_ColumnAdded);
            this.DataGrid.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.DataGrid_CellMouseDoubleClick);
            this.DataGrid.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(DataGrid_ColumnDisplayIndexChanged);
            this.DataGrid.RowsAdded += new DataGridViewRowsAddedEventHandler(DataGrid_RowsAdded);
            this.DataGrid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(DataGrid_RowsRemoved);
            this.DataGrid.SelectionChanged += new EventHandler(DataGrid_SelectionChanged);
            this.MenuForGrid.Opening += new System.ComponentModel.CancelEventHandler(MenuForGrid_Opening);
            #endregion

            #region Click事件

            EventHandler eh_Click = new EventHandler(EventArgs_DefindClickMethod);
            EventHandler eh_TextChanged = new EventHandler(EventArgs_DefindTextChangedMethod);
            EventHandler eh_Leave = new EventHandler(EventArgs_DefindLeaveMethod);
            EventHandler eh_SelectedIndexChanged = new EventHandler(EventArgs_DefindSelectedIndexChangedMethod);

            foreach (ToolStripItem toolstripItem in ToolStrip.Items)
            {
                toolstripItem.Click += eh_Click;
                if (toolstripItem.Name == "filterItem" || toolstripItem.Name == "newItem")
                {
                    foreach (ToolStripItem subitem in ((ToolStripDropDownItem)toolstripItem).DropDownItems)
                    {
                        subitem.Click += eh_Click;
                    }
                }
                if (toolstripItem is ToolStripComboBox)
                {
                    ((ToolStripComboBox)toolstripItem).TextChanged += eh_TextChanged;
                    ((ToolStripComboBox)toolstripItem).Leave += eh_Leave;
                    ((ToolStripComboBox)toolstripItem).SelectedIndexChanged += eh_SelectedIndexChanged;
                    ((ToolStripComboBox)toolstripItem).KeyDown += new KeyEventHandler(BaseListForm_KeyDown);
                }
            }

            foreach (ToolStripItem toolstripItem in MenuForGrid.Items)
            {
                toolstripItem.Click += eh_Click;
                if (toolstripItem.Name == "filterItem" || toolstripItem.Name == "newItem")
                {
                    foreach (ToolStripItem subitem in ((ToolStripMenuItem)toolstripItem).DropDownItems)
                    {
                        subitem.Click += eh_Click;
                    }
                }

                if (toolstripItem is ToolStripComboBox)
                {
                    ((ToolStripComboBox)toolstripItem).TextChanged += eh_TextChanged;
                    ((ToolStripComboBox)toolstripItem).Leave += eh_Leave;
                    ((ToolStripComboBox)toolstripItem).SelectedIndexChanged += eh_SelectedIndexChanged;
                    ((ToolStripComboBox)toolstripItem).KeyDown += new KeyEventHandler(BaseListForm_KeyDown);
                }
            }

            #endregion

            #region XmlDocument事件

            Service.Sdsite.CurrentDocument.ElementAdded += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementAdded);
            Service.Sdsite.CurrentDocument.ElementDeleted += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementDeleted);
            Service.Sdsite.CurrentDocument.ElementAddedFavorite += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementAddedFavorite);
            Service.Sdsite.CurrentDocument.ElementRemovedFavorite += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementRemovedFavorite);
            Service.Sdsite.CurrentDocument.ElementTitleChanged += new EventHandler<ChangeTitleEventArgs>(CurrentDocument_ElementTitleChanged);
            Service.Sdsite.CurrentDocument.ElementMoved += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementMoved);
            #endregion
        }

        private void UnloadEvent()
        {
            Service.Sdsite.CurrentDocument.ElementAdded -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementAdded);
            Service.Sdsite.CurrentDocument.ElementDeleted -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementDeleted);
            Service.Sdsite.CurrentDocument.ElementAddedFavorite -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementAddedFavorite);
            Service.Sdsite.CurrentDocument.ElementRemovedFavorite -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementRemovedFavorite);
            Service.Sdsite.CurrentDocument.ElementTitleChanged -= new EventHandler<ChangeTitleEventArgs>(CurrentDocument_ElementTitleChanged);
            Service.Sdsite.CurrentDocument.ElementMoved -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(CurrentDocument_ElementMoved);
        }

        #region 事件绑定到方法

        private void EventArgs_DefindSelectedIndexChangedMethod(object sender, EventArgs e)
        {
            ToolStripComboBox item = sender as ToolStripComboBox;
            switch (item.Name)
            {
                case "findValue":
                    #region
                    {
                        this.FindValue_SelectedIndexChanged();
                        break;
                    }
                    #endregion
                case "findTimeType":
                    #region
                    {
                        this.FindTimeType_SelectedIndexChanged();
                        break;
                    }
                    #endregion
                case "findTimeConfig":
                    #region
                    {
                        this.FindTimeConfig_SelectedIndexChanged();
                        break;
                    }
                    #endregion
                default:
                    break;
            }
        }
        private void EventArgs_DefindTextChangedMethod(object sender, EventArgs e)
        {
            ToolStripComboBox item = sender as ToolStripComboBox;
            switch (item.Name)
            {
                case "findValue":
                    #region
                    {
                        this.FindValue_TextChanged();
                        break;
                    }
                    #endregion
                case "findTimeType":
                    #region
                    {
                        this.FindTimeType_TextChanged();
                        break;
                    }
                    #endregion
                case "findTimeConfig":
                    #region
                    {
                        this.FindTimeConfig_TextChanged();
                        break;
                    }
                    #endregion
                default:
                    break;
            }
        }
        private void EventArgs_DefindLeaveMethod(object sender, EventArgs e)
        {
            ToolStripComboBox item = sender as ToolStripComboBox;
            switch (item.Name)
            {
                case "findValue":
                    #region
                    {
                        this.FindValue_Leave();
                        break;
                    }
                    #endregion
                case "findTimeType":
                    #region
                    {
                        this.FindTimeType_Leave();
                        break;
                    }
                    #endregion
                case "findTimeConfig":
                    #region
                    {
                        this.FindTimeConfig_Leave();
                        break;
                    }
                    #endregion
                default:
                    break;
            }
        }
        private void BaseListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter||e.KeyCode==Keys.Space)
            {
                ToolStripComboBox item = sender as ToolStripComboBox;
                switch (item.Name)
                {
                    case "findValue":
                        #region
                        {
                            this.FindValue_KeyDown();
                            break;
                        }
                        #endregion
                    case "findTimeType":
                        #region
                        {
                            this.FindTimeType_KeyDown();
                            break;
                        }
                        #endregion
                    case "findTimeConfig":
                        #region
                        {
                            this.FindTimeConfig_KeyDown();
                            break;
                        }
                        #endregion
                    default:
                        break;
                }
            }
        }
        private void EventArgs_DefindClickMethod(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            switch (item.Name)
            {
                case "editItem":
                    #region
                    {
                        this.EditItem_Click();
                        break;
                    }
                    #endregion
                case "publishItem":
                    #region
                    {
                        this.PublishItem_Click(true);
                        break;
                    }
                    #endregion
                case "advertisementItem":
                    #region
                    {
                        this.AdvertisementItem_Click(true);
                        break;
                    }
                    #endregion
                case "publishZItem":
                    #region
                    {
                        this.PublishItem_Click(false);
                        break;
                    }
                    #endregion
                case "advertisementZItem":
                    #region
                    {
                        this.AdvertisementItem_Click(false);
                        break;
                    }
                    #endregion
                case "cutItem":
                    #region
                    {
                        this.CutItem_Click();
                        break;
                    }
                    #endregion
                case "copyItem": 
                    #region
                    {
                        this.CopyItem_Click();
                        break;
                    }
                    #endregion
                case "pasteItem":
                    #region
                    {
                        this.PasteItem_Click();
                        break;
                    }
                    #endregion
                case "deleteItem":
                    #region
                    {
                        this.DeleteItem_Click();
                        break;
                    }
                    #endregion
                case "allSelectionItem":
                    #region
                    {
                        this.AllSelectionItem_Click();
                        break;
                    }
                    #endregion
                case "refurbish":
                    #region
                    {
                        this.Refurbish_Click(SimpleState.All);
                        if (DataGridRowState())
                            NewOpenState();
                        break;
                    }
                    #endregion
                      case "selectedChannel":
                    #region
                    {
                        this.SelectedChannel_Click();
                        break;
                    }
                    #endregion
                case "upItem":
                    #region
                    {
                        this.UpItem_Click();
                        break;
                    }
                    #endregion
                case "upTopItem":
                    #region
                    {
                        this.UpTopItem_Click();
                        break;
                    }
                    #endregion
                case "downItem":
                    #region
                    {
                        this.DownItem_Click();
                        break;
                    }
                    #endregion
                case "downFooterItem":
                    #region
                    {
                        this.DownFooterItem_Click();
                        break;
                    }
                    #endregion
                case "showAll":
                    #region
                    {
                        this.ShowAll_Click(sender);
                        break;
                    }
                    #endregion
                case "new":
                    #region
                    {
                        this.New_Click(sender);
                        break;
                    }
                    #endregion
                case "publishAndIsModify":
                    #region
                    {
                        this.PublishAndIsModify_Click(sender);
                        break;
                    }
                    #endregion
                case "publishAndIsNotModify":
                    #region
                    {
                        this.PublishAndIsNotModify_Click(sender);
                        break;
                    }
                    #endregion
                case "itemProperty":
                    #region
                    {
                        this.ItemProperty_Click();
                        break;
                    }
                    #endregion
                case "generalPage":
                    #region
                    {
                        this.GeneralPageNew_Click(sender);
                        break;
                    }
                    #endregion
                case "productPage":
                    #region
                    {
                        this.ProductPageNew_Click(sender);
                        break;
                    }
                    #endregion
                case "knowledgePage":
                    #region
                    {
                        this.KnowledgePageNew_Click(sender);
                        break;
                    }
                    #endregion
                case "projectPage":
                    #region
                    {
                        this.ProjectPageNew_Click(sender);
                        break;
                    }
                    #endregion
                case "biddPage":
                    #region
                    {
                        this.BiddPageNew_Click(sender);
                        break;
                    }
                    #endregion
                case "hrPage":
                    #region
                    {
                        this.HrPageNew_Click(sender);
                        break;
                    }
                    #endregion
                default:
                    break;
            }
        }

        #endregion

        #region Find_SelectedIndexChanged

        #region 根据时间的选项筛选

        protected virtual void FindTimeConfig_SelectedIndexChanged()
        {
            if (this._isToolStripCreated)
            {
                FindFormTimeConfig();
            }
        }
        protected virtual void FindTimeType_SelectedIndexChanged()
        {
            if (this._isToolStripCreated)
            {
                this.DataGrid.Rows.Clear();
                _idc.Clear();
                if (_list != null)
                    this.MarkRows(SimpleState.All, _list);
                else
                    this.MarkRows(SimpleState.All, null);
                FindFormTimeConfig();
            }
        }

        protected virtual void FindFormTimeConfig()
        {
            int totleCount = this.DataGrid.Rows.Count;
            int partCount = 0;
            BaseScrollbarForm scrollForm = new BaseScrollbarForm();
            ToolStripComboBox comboBoxCfg = ((ToolStripComboBox)this.ToolStrip.Items["findTimeConfig"]);
            ToolStripComboBox comboBoxType = ((ToolStripComboBox)this.ToolStrip.Items["findTimeType"]);
            DataGridViewColumn column = this._findTimeTypeDic[comboBoxType.SelectedIndex];
            if (totleCount > 300)
            {
                scrollForm.Show(this);
                Thread.Sleep(1000);
            }
            foreach (RowEx row in this.DataGrid.Rows)
            {
                if (row.Visible)
                {
                    partCount = row.Index + 1;
                    switch ((FindTimeType)(comboBoxCfg.SelectedIndex))
                    {
                        case FindTimeType.Day:
                            FindTime(DateTime.Now.AddDays(-1), row, column);
                            break;
                        case FindTimeType.Week:
                            FindTime(DateTime.Now.AddDays(-7), row, column);
                            break;
                        case FindTimeType.Month:
                            FindTime(DateTime.Now.AddMonths(-1), row, column);
                            break;
                        case FindTimeType.Quarter:
                            FindTime(DateTime.Now.AddMonths(-3), row, column);
                            break;
                        case FindTimeType.Year:
                            FindTime(DateTime.Now.AddYears(-1), row, column);
                            break;
                        case FindTimeType.All:
                            FindTime(DateTime.Now.AddYears(-1000), row, column);
                            break;
                        default:
                            break;
                    }
                    if (totleCount > 0 && partCount > 0)
                    {
                        scrollForm.ProgressBar.Value = (int)((float)(partCount / totleCount) * 100);
                    }
                }
            }
            scrollForm.Close();

        }
        private void FindTime(DateTime findTime, RowEx row, DataGridViewColumn column)
        {
            int tagIndex = column.Index;
            int partCount = row.Index + 1;
            row.Visible = false;
            if (row.Cells[tagIndex].Value!=null)
            row.Visible = ((DateTime)row.Cells[tagIndex].Value > findTime);

        }
        #endregion
        #region
        /// <summary>
        /// 用户自定义搜索的处理部分  by lisuye on 2008年5月27日
        /// </summary>
        protected virtual void FindValue_SelectedIndexChanged()
        {
            if (this._isToolStripCreated)
            {
                ToolStripComboBox comboBoxfv = ((ToolStripComboBox)this.ToolStrip.Items["findValue"]);
                FindFormValueConfig();
            }
        }
        private string IsAddComboBoxItem()
        {
            string findValue = string.Empty;
            ToolStripComboBox comboBoxfv = ((ToolStripComboBox)this.ToolStrip.Items["findValue"]);
            findValue = comboBoxfv.Text;
            if (comboBoxfv.SelectedIndex == -1)
            {
                GuestSearchElement ChildElement = Service.Sdsite.DesignDataDocument.GetElementByName("guestSearch") as GuestSearchElement;
                if (ChildElement == null)
                {
                    XmlNode SearchElemant = Service.Sdsite.DesignDataDocument.CreateElement("guestSearch");
                    Service.Sdsite.DesignDataDocument.DocumentElement.AppendChild(SearchElemant);
                    ChildElement = SearchElemant as GuestSearchElement;
                }
                XmlElement ChildEle = ChildElement.CreateNode(findValue);
                comboBoxfv.Items.Clear();
                foreach (SearchContentElement ChildNode in ChildEle.ChildNodes)
                {
                    comboBoxfv.Items.Add(ChildNode.SearchContent.ToString());
                }
            }
            return findValue;
        }
        protected virtual void FindFormValueConfig()
        {
            BaseScrollbarForm scrollForm = new BaseScrollbarForm();
            int trCount = this.DataGrid.Rows.Count;
            int tdCount = this.DataGrid.Columns.Count;
            int totleCount = trCount * tdCount;
            int partCount = 0;
            string findValue = string.Empty;
            if (trCount > 300)
            {
                scrollForm.Show(this);
                Thread.Sleep(1000);
            }

            findValue = IsAddComboBoxItem();
            DataGridViewRowCollection rows = this.DataGrid.Rows;
            foreach (RowEx row in rows)
            {
                string cellText=string.Empty;
                row.Visible = false;
                partCount = (row.Index + 1);
                foreach (DataGridViewColumn column in this.DataGrid.Columns)
                {
                    int tagIndex = column.Index;
                    if (column is DataGridViewTextBoxColumn)
                    {
                        if(row.Cells[tagIndex].Value!=null)
                        cellText = row.Cells[tagIndex].Value.ToString();
                        if (cellText.Contains(findValue))
                        {
                            row.Visible = true;
                        }
                    }

                }
                if (totleCount > 0 && partCount > 0)
                {
                    scrollForm.ProgressBar.Value = (int)((float)(partCount / totleCount) * 100);
                }
            }
            scrollForm.Close();
        }
        #endregion
        #endregion
        #region Find_TextChanged

        protected virtual void FindTimeConfig_TextChanged(){ }
        protected virtual void FindTimeType_TextChanged() {}
        protected virtual void FindValue_TextChanged() { }
        #endregion

        #region Find_Leave
        protected virtual void FindTimeConfig_Leave() { }
        protected virtual void FindTimeType_Leave() { }
        protected virtual void FindValue_Leave()
        {
            if (this._isToolStripCreated)
            {
                FindFormValueConfig();
            }
        }

        #endregion

        #region Find_Enter
        protected virtual void FindTimeConfig_KeyDown()
        {
            if (this._isToolStripCreated)
            {
                FindFormTimeConfig();
            }
        }
        protected virtual void FindTimeType_KeyDown()
        {
            if (this._isToolStripCreated)
            {
                this.DataGrid.Rows.Clear();
                _idc.Clear();
                if (_list != null)
                    this.MarkRows(SimpleState.All, _list);
                else
                    this.MarkRows(SimpleState.All, null);
                FindFormTimeConfig();
            }
        }
        //KeyDown事件的触发执行的内容  by lisuye on 2008年5月27日
        protected virtual void FindValue_KeyDown()
        {
            if (this._isToolStripCreated)
            {
                FindFormValueConfig();
            }
        }
        #endregion
        #region Downitem And Upitem
        //TODO:上下移功能 暂时功能未开发
        protected virtual void DownFooterItem_Click() { }
        protected virtual void DownItem_Click() { }
        protected virtual void UpTopItem_Click() { }
        protected virtual void UpItem_Click() { }
        #endregion
        #region copy paste cut
        //TODO:copy,paste,cut Lisuye
        //粘贴
        protected virtual void PasteItem_Click()
        {
            if (TagCutOrCopy.PageEleList != null)
            {
                    if (TagCutOrCopy.TagCut)
                    {
                        foreach (PageSimpleExXmlElement pageChildEle in TagCutOrCopy.PageEleList)
                        {
                            Service.Sdsite.CurrentDocument.CutNode(pageChildEle.Id, this.ChannelId);
                        }
                        TagCutOrCopy.TagCut = false;
                    }
                    else
                    {
                        foreach (PageSimpleExXmlElement pageChildEle in TagCutOrCopy.PageEleList)
                        {
                            Service.Sdsite.CurrentDocument.CopyNode(pageChildEle.Id, this.ChannelId);
                        }
                    }
            }
            else
            {
                MessageBox.Show(this.GetTextResource("notContent"));
            }

        }
        //复制
        protected virtual void CopyItem_Click()
        {
            CutAndCopy();
        }
        //剪切
        protected virtual void CutItem_Click()
        {
            TagCutOrCopy.TagCut = true;
            TagCutOrCopy.CutChannelId = this.ChannelId;
            CutAndCopy();
            tagIsCut = true;
        }
        //复制剪切的操作
        private void CutAndCopy()
        {
            
            DataGridViewSelectedRowCollection rows = this.DataGrid.SelectedRows;
            if (this.DataGrid.SelectedRows.Count != 0)
            {
                TagCutOrCopy.PageEleList = new List<PageSimpleExXmlElement>();
                if (TagCutOrCopy.PageEleList != null)
                {
                    TagCutOrCopy.PageEleList.Clear(); 
                }
                if (TagCutOrCopy.TagCut)
                {
                    TagCutOrCopy.CutPageIndex = new List<int>();
                    if (TagCutOrCopy.CutPageIndex != null)
                    {
                        TagCutOrCopy.CutPageIndex.Clear();
                    }
                    foreach (DataGridViewRow row in rows)
                    {
                        string pageId = (string)row.Tag;
                        PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById(pageId);
                        TagCutOrCopy.PageEleList.Add(pageEle);
                        TagCutOrCopy.CutPageIndex.Add(row.Index);
                    }
                    TagCutOrCopy.TagPageIndexCut= TagCutOrCopy.CutPageIndex.Count - 1;
                }
                else
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        string pageId = (string)row.Tag;
                        PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById(pageId);
                        TagCutOrCopy.PageEleList.Add(pageEle);
                    }

                }
            }
        }
        #endregion

        #region 右键菜单中属性Item点击事件

        protected virtual void ItemProperty_Click()
        {
            EditItem_Click();
        }

        #endregion
        #region 新建
         protected virtual void GeneralPageNew_Click(object sender)
         {
             NewItem_Click(PageType.General);
         }
         protected virtual void ProductPageNew_Click(object sender)
         {
             NewItem_Click(PageType.Product);
         }
         protected virtual void KnowledgePageNew_Click(object sender)
         {
             NewItem_Click(PageType.Knowledge);
         }
         protected virtual void ProjectPageNew_Click(object sender)
         {
             NewItem_Click(PageType.Project);
         }
         protected virtual void BiddPageNew_Click(object sender)
         {
             NewItem_Click(PageType.InviteBidding);
         }
         protected virtual void HrPageNew_Click(object sender)
         {
             NewItem_Click(PageType.Hr);
         }
        #endregion
        #region 筛选
        //筛选
        protected virtual void ShowAll_Click(object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripDropDownButton comboBox = ((ToolStripDropDownButton)this.ToolStrip.Items["filterItem"]);
            comboBox.Text = item.Text;
            this.DataGrid.Rows.Clear();
            _idc.Clear();
            if (_list != null)
                this.MarkRows(SimpleState.All, _list);
            else
                this.MarkRows(SimpleState.All, null);
        }
        protected virtual void New_Click(object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripDropDownButton comboBox = ((ToolStripDropDownButton)this.ToolStrip.Items["filterItem"]);
            comboBox.Text = item.Text;
            this.DataGrid.Rows.Clear();
            _idc.Clear();
            if (_list != null)
                this.MarkRows(SimpleState.New, _list);
            else
                this.MarkRows(SimpleState.New, null);
        }
        protected virtual void PublishAndIsModify_Click(object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripDropDownButton comboBox = ((ToolStripDropDownButton)this.ToolStrip.Items["filterItem"]);
            comboBox.Text = item.Text;
            this.DataGrid.Rows.Clear();
            _idc.Clear();
            if (_list != null)
                this.MarkRows(SimpleState.PublishAndIsModify, _list);
            else
                this.MarkRows(SimpleState.PublishAndIsModify, null);
        }
        protected virtual void PublishAndIsNotModify_Click(object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripDropDownButton comboBox = ((ToolStripDropDownButton)this.ToolStrip.Items["filterItem"]);
            comboBox.Text = item.Text;
            this.DataGrid.Rows.Clear();
            _idc.Clear();
            if (_list != null)
                this.MarkRows(SimpleState.PublishAndNotIsModify, _list);
            else
                this.MarkRows(SimpleState.PublishAndNotIsModify, null);
        }
        #endregion
        public enum SimpleState
        {
            All = 0,
            New = 1,
            PublishAndIsModify = 2,
            PublishAndNotIsModify = 3,
        }

        //删除,修改后记录的重新显示
        private void ShowNew(DataGridViewRow row, PageSimpleExXmlElement pageEle)
        {
            int tagIndex = row.Index;
            PageXmlDocument doc = (PageXmlDocument)pageEle.GetIndexXmlDocument();
            RowEx rowex = RowEx.GetDataGridViewRow(this.Columns, doc);
            rowex.ContextMenuStrip = this.MenuForGrid;
            this.DataGrid.Rows.Insert(row.Index, rowex);
            this.DataGrid.Rows[tagIndex].Selected = true;
            this.DataGrid.Rows.RemoveAt(row.Index);
            _idc.Remove(doc.Id);
        }
        #region
        protected bool DataGridRowState()
        {
            bool IsRow=false;
            foreach (DataGridViewRow row in this.DataGrid.Rows)
            {
                if (row.Visible)
                    IsRow = true;
            }
            return IsRow;
        }
        #endregion

        #region Refurbish AllSelectionItem DeleteItem PublishItem AdvertisementItem EditItem NewItem

        protected virtual void Refurbish_Click(SimpleState state)
        {
            SaveState();
            if (DataGridRowState())
            {
                this.DataGrid.Rows.Clear();
                _idc.Clear();
                this.MarkRows(state, _list);
            }
        }
        protected virtual void AllSelectionItem_Click()
        {
            DataGridViewRowCollection rows = this.DataGrid.Rows;
            foreach (DataGridViewRow row in rows)
            {
                row.Selected = true;
            }
        }
        protected virtual void DeleteItem_Click()
        {  
            DataGridViewSelectedRowCollection rows = this.DataGrid.SelectedRows;
            int i = rows.Count;
            if (MessageService.Show("${res:Tree.msg.delete}", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (DataGridViewRow row in rows)
                {
                    string pageId = (string)row.Tag;
                    Service.Sdsite.CurrentDocument.DeleteItem(pageId);
                    Service.Sdsite.CurrentDocument.Save();
                }
            }
        }
        protected virtual void PublishItem_Click(bool isPublish)
        {
            DataGridViewSelectedRowCollection rows = this.DataGrid.SelectedRows;
            StateSave.RowList.Clear();
            foreach (DataGridViewRow row in rows)
            {
                string pageId = (string)row.Tag;
                PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById(pageId);
                pageEle.IsPublish = isPublish;
                if (isPublish)
                    pageEle.PublishedTime = DateTime.Now;
                Service.Sdsite.CurrentDocument.MarkModified(pageId);
                Service.Sdsite.CurrentDocument.Save();
                StateSave.RowList.Add(row.Index);
                ShowNew(row, pageEle);
            }
            this.Refurbish_Click(SimpleState.All);
            if (DataGridRowState())
                NewOpenState();
            foreach (DataGridViewRow row in this.DataGrid.Rows)
            {
                row.Selected = false;
            }
            foreach (int i in StateSave.RowList)
            {
                this.DataGrid.Rows[i].Selected = true;
            }
        }
        protected virtual void AdvertisementItem_Click(bool isAd)
        {

            DataGridViewSelectedRowCollection rows = this.DataGrid.SelectedRows;
            StateSave.RowList.Clear();
            foreach (DataGridViewRow row in rows)
            {
                string pageId = (string)row.Tag;
                PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById(pageId);
                pageEle.IsAd = isAd;
                if (isAd)
                    pageEle.AdTime = DateTime.Now;
                Service.Sdsite.CurrentDocument.MarkModified(pageId);
                Service.Sdsite.CurrentDocument.Save();
                StateSave.RowList.Add(row.Index);
                ShowNew(row, pageEle);
            }
            this.Refurbish_Click(SimpleState.All);
            if (DataGridRowState())
                NewOpenState();
            foreach (DataGridViewRow row in this.DataGrid.Rows)
            {
                row.Selected = false;
            }
            foreach (int i in StateSave.RowList)
            {
                this.DataGrid.Rows[i].Selected = true;
            }
        }
        protected virtual void EditItem_Click()
        {
            //如果多选的话将打开多个编辑窗体
            DataGridViewSelectedRowCollection rows = this.DataGrid.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                string pageId = (string)row.Tag;
                PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById(pageId);
                PageXmlDocument pageDoc = Service.Sdsite.CurrentDocument.GetPageDocumentById(pageId);
                switch (pageEle.PageType)
                {
                    case PageType.General: Service.Workbench.OpenWorkDocument(WorkDocumentType.HtmlDesigner, pageId); break;
                    case PageType.Product: Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, pageId); break;
                    case PageType.Project: Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, pageId); break;
                    case PageType.InviteBidding: Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, pageId); break;
                    case PageType.Knowledge: Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, pageId); break;
                    case PageType.Hr: Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, pageId); break;
                    default: break;
                }
                ShowNew(row, pageEle);
            }
        }
        
       /// <summary>
       /// 频道的选择
       /// </summary>
       //TDDO:频道选择 Lisuye
        protected virtual void SelectedChannel_Click()
        {
            SelectChannelShowForm form = new SelectChannelShowForm();
            List<DataGridViewRow> DataGridViewRowList = new List<DataGridViewRow>();
            form.SelectesChannelIDList = new List<string>();  
            DataGridViewRowList = RowList;
            foreach (DataGridViewRow row in DataGridViewRowList)
            {
               string pageEleID = row.Tag as string;
               XmlElement ele= Service.Sdsite.CurrentDocument.GetPageElementById(pageEleID);
               if (ele != null)
               {
                   string channelID = ((FolderXmlElement)(Service.Sdsite.CurrentDocument.GetPageElementById(pageEleID).ParentNode)).Id;
                   form.SelectesChannelIDList.Add(channelID);
               }
            }
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                this.DataGrid.Rows.Clear();
                _idc.Clear();
                this.MarkRows(SimpleState.All,form.PageList);
                _list = form.pageList;
            }
            this.TitleText();
        }
        protected virtual void NewItem_Click(PageType pagetype)
        {
            ToolStripDropDownButton comboBoxNew = ((ToolStripDropDownButton)this.ToolStrip.Items["newItem"]);
            if (this.ChannelId == "00000000")
            {
                NewPageForm newPage = new NewPageForm(null, pagetype);
                if (newPage.ShowDialog() == DialogResult.OK)
                {
                    ///建完页面后打开页面
                    WorkDocumentType workDocumentType = WorkDocumentType.None;
                    if (pagetype == PageType.General)
                    {
                        workDocumentType = WorkDocumentType.HtmlDesigner;
                    }
                    else
                    {
                        workDocumentType = WorkDocumentType.Edit;
                    }
                    Service.Workbench.OpenWorkDocument(workDocumentType, newPage.NewPageId);
                }
            }
            else
            {
                NewPageForm newPage = new NewPageForm(Service.Sdsite.CurrentDocument.GetFolderElementById(ChannelId), pagetype);
                if (newPage.ShowDialog() == DialogResult.OK)
                {
                    ///建完页面后打开页面
                    WorkDocumentType workDocumentType = WorkDocumentType.None;
                    if (pagetype == PageType.General)
                    {
                        workDocumentType = WorkDocumentType.HtmlDesigner;
                    }
                    else
                    {
                        workDocumentType = WorkDocumentType.Edit;
                    }
                    Service.Workbench.OpenWorkDocument(workDocumentType, newPage.NewPageId);
                }
            }

        }
        #endregion
        enum ItemState
        {
            Unknown = 0,
            AllTrue = 1,
            AllFalse = 2,
            BothHave = 3,
        }
        #region 主右键菜单事件
        //主右键菜单事件
        void MenuForGrid_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ContextMenuStrip strip = ((ContextMenuStrip)sender);
            ToolStripMenuItem itemDeleted = strip.Items["deleteItem"] as ToolStripMenuItem;
            ToolStripMenuItem itemPublish = strip.Items["publishItem"] as ToolStripMenuItem;
            ToolStripMenuItem itemAd = strip.Items["advertisementItem"] as ToolStripMenuItem;
            ToolStripMenuItem itemNotDeleted = strip.Items["revertItem"] as ToolStripMenuItem;
            ToolStripMenuItem itemNotPublish = strip.Items["publishZItem"] as ToolStripMenuItem;
            ToolStripMenuItem itemNotAd = strip.Items["advertisementZItem"] as ToolStripMenuItem;
            ItemState publishState = ItemState.Unknown;
            ItemState advState = ItemState.Unknown;
            DataGridViewSelectedRowCollection rows = this.DataGrid.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById((string)row.Tag);
                ///判断发布的状态
                if (pageEle.IsPublish)
                {
                    if (publishState == ItemState.Unknown)
                    {
                        publishState = ItemState.AllTrue;
                    }
                    else if (publishState == ItemState.AllFalse)
                    {
                        publishState = ItemState.BothHave;
                    }
                }
                else
                {
                    if (publishState == ItemState.Unknown)
                    {
                        publishState = ItemState.AllFalse;
                    }
                    else if (publishState == ItemState.AllTrue)
                    {
                        publishState = ItemState.BothHave;
                    }
                }
                ///判断推广的状态
                if (pageEle.IsAd)
                {
                    if (advState == ItemState.Unknown)
                    {
                        advState = ItemState.AllTrue;
                    }
                    else if (advState == ItemState.AllFalse)
                    {
                        advState = ItemState.BothHave;
                    }
                }
                else
                {
                    if (advState == ItemState.Unknown)
                    {
                        advState = ItemState.AllFalse;
                    }
                    else if (advState == ItemState.AllTrue)
                    {
                        advState = ItemState.BothHave;
                    }
                }

                if (publishState == ItemState.BothHave
                    && advState == ItemState.BothHave)
                {
                    break;
                }
            }
            //设置IsPublished的状态
            switch (publishState)
            {
                case ItemState.Unknown:
                    {
                        itemPublish.Enabled = false;
                        itemNotPublish.Enabled = false;
                        break;
                    }
                case ItemState.AllTrue:
                    {
                        itemPublish.Enabled = false;
                        itemNotPublish.Enabled = true;
                        break;
                    }
                case ItemState.AllFalse:
                    {
                        itemPublish.Enabled = true;
                        itemNotPublish.Enabled = false;
                        break;
                    }
                case ItemState.BothHave:
                    {
                        itemPublish.Enabled = true;
                        itemNotPublish.Enabled = true;
                        break;
                    }
                default:
                    break;
            }
            //设置advState的状态
            switch (advState)
            {
                case ItemState.Unknown:
                    {
                        itemAd.Enabled = false;
                        itemNotAd.Enabled = false;
                        break;
                    }
                case ItemState.AllTrue:
                    {
                        itemAd.Enabled = false;
                        itemNotAd.Enabled = true;
                        break;
                    }
                case ItemState.AllFalse:
                    {
                        itemAd.Enabled = true;
                        itemNotAd.Enabled = false;
                        break;
                    }
                case ItemState.BothHave:
                    {
                        itemAd.Enabled = true;
                        itemNotAd.Enabled = true;
                        break;
                    }
                default:
                    break;
            }
        }
        //判断工具栏的状态
        private void ToolAdAndPublish()
        {
            ItemState publishState = ItemState.Unknown;
            ItemState advState = ItemState.Unknown;
            DataGridViewSelectedRowCollection rows = this.DataGrid.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById((string)row.Tag);
                    ///判断发布的状态
                    if (pageEle.IsPublish)
                    {
                        if (publishState == ItemState.Unknown)
                        {
                            publishState = ItemState.AllTrue;
                        }
                        else if (publishState == ItemState.AllFalse)
                        {
                            publishState = ItemState.BothHave;
                        }
                    }
                    else
                    {
                        if (publishState == ItemState.Unknown)
                        {
                            publishState = ItemState.AllFalse;
                        }
                        else if (publishState == ItemState.AllTrue)
                        {
                            publishState = ItemState.BothHave;
                        }
                    }
                    ///判断推广的状态
                    if (pageEle.IsAd)
                    {
                        if (advState == ItemState.Unknown)
                        {
                            advState = ItemState.AllTrue;
                        }
                        else if (advState == ItemState.AllFalse)
                        {
                            advState = ItemState.BothHave;
                        }
                    }
                    else
                    {
                        if (advState == ItemState.Unknown)
                        {
                            advState = ItemState.AllFalse;
                        }
                        else if (advState == ItemState.AllTrue)
                        {
                            advState = ItemState.BothHave;
                        }
                    }

                    if (publishState == ItemState.BothHave
                        && advState == ItemState.BothHave)
                    {
                        break;
                    }

            }
            //设置IsPublished的状态
            switch (publishState)
            {
                case ItemState.Unknown:
                    {
                        this.ToolStrip.Items[4].Enabled = false;
                        this.ToolStrip.Items[5].Enabled = false;
                        break;
                    }
                case ItemState.AllTrue:
                    {
                        this.ToolStrip.Items[4].Enabled = false;
                        this.ToolStrip.Items[5].Enabled = true;
                        break;
                    }
                case ItemState.AllFalse:
                    {
                        this.ToolStrip.Items[4].Enabled = true;
                        this.ToolStrip.Items[5].Enabled = false;
                        break;
                    }
                case ItemState.BothHave:
                    {
                        this.ToolStrip.Items[4].Enabled = true;
                        this.ToolStrip.Items[5].Enabled = true;
                        break;
                    }
                default:
                    break;
            }
            //设置advState的状态
            switch (advState)
            {
                case ItemState.Unknown:
                    {
                        this.ToolStrip.Items[6].Enabled = false;
                        this.ToolStrip.Items[7].Enabled = false;
                        break;
                    }
                case ItemState.AllTrue:
                    {
                        this.ToolStrip.Items[6].Enabled = false;
                        this.ToolStrip.Items[7].Enabled = true;
                        break;
                    }
                case ItemState.AllFalse:
                    {
                        this.ToolStrip.Items[6].Enabled = true;
                        this.ToolStrip.Items[7].Enabled = false;
                        break;
                    }
                case ItemState.BothHave:
                    {
                        this.ToolStrip.Items[6].Enabled = true;
                        this.ToolStrip.Items[7].Enabled = true;
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion

        #region XmlDocument事件

        void CurrentDocument_ElementTitleChanged(object sender, ChangeTitleEventArgs e)
        {
            PageSimpleExXmlElement pageEle = e.Item as PageSimpleExXmlElement;
            if (pageEle != null)
            {
                DataGridViewRow row = _idc[pageEle.Id];
                row.Cells["title"].Value = pageEle.Title;
            }
        }

        void CurrentDocument_ElementRemovedFavorite(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            PageSimpleExXmlElement pageEle = e.Item as PageSimpleExXmlElement;
            if (pageEle != null)
            {
                DataGridViewRow row =  _idc[pageEle.Id];
                    row.Cells["IsFavorite"].Value = GetImage(@"Image\save.png");
            }
        }

        void CurrentDocument_ElementAddedFavorite(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            PageSimpleExXmlElement pageEle = e.Item as PageSimpleExXmlElement;
            if (pageEle != null)
            {
                DataGridViewRow row =  _idc[pageEle.Id];
                    row.Cells["IsFavorite"].Value = GetImage(@"Image\save.png");
            }
        }

        void CurrentDocument_ElementDeleted(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            PageSimpleExXmlElement pageEle = e.Item as PageSimpleExXmlElement;
            if (pageEle != null)
            {
                DataGridViewRow row =  _idc[pageEle.Id];
                this.DataGrid.Rows.Remove(row);
             //   this.DataGrid.Rows.RemoveAt(row.Index);
                 _idc.Remove(pageEle.Id);
            }
        }

        void CurrentDocument_ElementAdded(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            PageSimpleExXmlElement pageEle = e.Item as PageSimpleExXmlElement;
            if (pageEle != null)
            {
                DataGridViewRow row = RowEx.GetDataGridViewRow(this.Columns, pageEle.GetIndexXmlDocument());

                this.DataGrid.Rows.Add(row);
                if (! _idc.ContainsValue(row))
                     _idc.Add(pageEle.Id, row);
                GetDataGridViewRowValue();
            }
        }
        //剪切保存后所触发的事件
        void CurrentDocument_ElementMoved(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            if (tagIsCut)
            {
                TagCutOrCopy.TagCut = true;
                TagCutOrCopy.CutChannelId = this.ChannelId;
                CutAndCopy();
                tagIsCut = false;
            }
            PageSimpleExXmlElement pageEle = e.Item as PageSimpleExXmlElement;
            //对粘贴的form的DataGridView的添加操作
            if (pageEle != null && ((ChannelSimpleExXmlElement)pageEle.ParentNode).Id.ToString().Equals(this.ChannelId.ToString()))
            {
                DataGridViewRow row = RowEx.GetDataGridViewRow(this.Columns, pageEle.GetIndexXmlDocument());
                this.DataGrid.Rows.Add(row);
                if (! _idc.ContainsKey(pageEle.Id))
                     _idc.Add(pageEle.Id, row);
                GetDataGridViewRowValue();
                this.DataGrid.Rows[0].Selected = false;
                row.Selected = true;
            }
            //对剪切的form的DataGridView的删除操作
            if (TagCutOrCopy.CutChannelId != null)
            {
                if (pageEle != null && this.ChannelId.ToString().Equals(TagCutOrCopy.CutChannelId.ToString()))
                {
                    DataGridViewRow row = RowEx.GetDataGridViewRow(this.Columns, pageEle.GetIndexXmlDocument());
                    int pageIndextDel = TagCutOrCopy.CutPageIndex[TagCutOrCopy.TagPageIndexCut];
                    this.DataGrid.Rows.RemoveAt(pageIndextDel);
                    GetDataGridViewRowValue();
                    TagCutOrCopy.TagPageIndexCut--;
                }
            }
        }
        #endregion

        Dictionary<string, Image> _dicImagesCache = new Dictionary<string, Image>();
        Image GetImage(string fileName)
        {
            Image image = null;
            if (!_dicImagesCache.TryGetValue(fileName, out image))
            {
                //todo:
                image = Image.FromFile(Path.Combine(Application.StartupPath, fileName));
                _dicImagesCache.Add(fileName, image);
            }
            return image;
        } 
         #region DataGrid事件
        virtual protected void DataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            BuildGridHeaderCellContentMenuStrip();
            for (int i = 0; i < this.DataGrid.Columns.Count; i++)
            {
                this.DataGrid.Columns[i].HeaderCell.ContextMenuStrip = this.MenuForGridHeader;
            }
        }

        virtual protected void DataGrid_ColumnDisplayIndexChanged
            (object sender, DataGridViewColumnEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        virtual protected void DataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.RowIndex >= 0)
                    {
                        EditItem_Click();
                    }
                }
        }

        virtual protected void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem_Click();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                EditItem_Click();
                e.Handled = true;
            }
        }

        virtual protected void DataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.DataGrid.Rows.Count > 0)
            {
                this._tipLabel.Visible = false;
            }
        }

        virtual protected void DataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (this.DataGrid.Rows.Count <= 0)
            {
                WhileDataIsEmptied();
            }
        }

        #endregion

        #endregion

        #region ContextMenu, Toolstrip

        #region Grid主菜单和工具栏的建立

        bool _isToolStripCreated = false;
        /// <summary>
        /// 生成右键Grid主菜单和工具栏
        /// </summary>
        private void BuildMenuAndToolStrip()
        {
            ToolStripXmlDocument doc = new ToolStripXmlDocument();
            doc.Load(this.ThisConfigPath);

            this.MenuForGrid = (ContextMenuStrip)doc.BuildToolStrip(StripType.ContextMenu);

            this.ToolStrip = doc.BuildToolStrip(StripType.ToolBar);
            this.Controls.Add(this.ToolStrip);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
        }

        #endregion

        #region Grid的列的头部菜单的建立

        private void BuildGridHeaderCellContentMenuStrip()
        {
            if (this.MenuForGridHeader == null)
            {
                this.MenuForGridHeader = new ContextMenuStrip();
            }
            else
            {
                this.MenuForGridHeader.Items.Clear();
            }

            ToolStripMenuItem item = null;
            List<ToolStripMenuItem> multiMenu = new List<ToolStripMenuItem>();
            foreach (DataGridViewColumn column in this.DataGrid.Columns)
            {
                string[] strArr = (string[])column.HeaderCell.Tag;
                string headerText = strArr[0];
                string[] menuLevel = strArr[1].Split(',');


                if (menuLevel.Length > 1)//分级显示
                {
                    ToolStripMenuItem fristMenu = null;
                    foreach (ToolStripMenuItem mainItem in multiMenu)
                    {
                        if (mainItem.Name == menuLevel[0])
                        {
                            fristMenu = mainItem;
                            break;
                        }
                    }
                    if (fristMenu == null)
                    {
                        fristMenu = new ToolStripMenuItem(menuLevel[0]);
                        fristMenu.Name = menuLevel[0];
                        multiMenu.Add(fristMenu);
                        this.MenuForGridHeader.Items.Add(fristMenu);
                    }
                    item = new ToolStripMenuItem(strArr[0]);
                    if (column.Visible)
                    {
                        item.Checked = true;
                    }

                    item.Tag = column;
                    item.Click += new EventHandler(HeaderMenu_Click);
                    fristMenu.DropDownItems.Add(item);

                }
                else//不分级显示
                {
                    item = new ToolStripMenuItem(strArr[0]);
                    if (column.Visible)
                    {
                        item.Checked = true;
                    }

                    item.Tag = column;
                    item.Click += new EventHandler(HeaderMenu_Click);
                    this.MenuForGridHeader.Items.Add(item);
                }
            }
            //ToolStripSeparator tss = new ToolStripSeparator();
            //this.MenuForGridHeader.Items.Add(tss);
            //TODO:菜单中自定义项未开发
            // item = new ToolStripMenuItem(this.GetTextResource("Custom")+"(&C)...");
            //item.Name = "customHeaderMenu";
            //this.MenuForGridHeader.Items.Add(item);
            //this.MenuForGridHeader.Items["customHeaderMenu"].Click += new EventHandler(CustomHeaderMenu_Click);
        }

        void HeaderMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ((DataGridViewColumn)item.Tag).Visible = !((DataGridViewColumn)item.Tag).Visible;
            item.Checked = !item.Checked;
        }

        void CustomHeaderMenu_Click(object sender, EventArgs e)
        {
            //TODO:列自定义窗口未做，如无时间，暂不要该功能
            Form form = new Form();
            form.Size = new Size(480, 360);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        #endregion

        #endregion

        #region IGetPropertiesForPanelable 成员

        public object[] GetPropertiesForPanel()
        {
            if (!Service.Sdsite.IsOpened)
            {
                return new object[0];
            }
            List<PageXmlDocument> docList = new List<PageXmlDocument>();
            foreach (RowEx row in this.DataGrid.SelectedRows)
            {
                docList.Add(row.PageDoc);
            }
            return docList.ToArray();
        }

        public event EventHandler PropertiesChanged;

        protected void OnPropertiesChanged(EventArgs e)
        {
            if (PropertiesChanged != null)
            {
                PropertiesChanged(this, e);
            }
        }

        protected virtual void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            OnPropertiesChanged(EventArgs.Empty);
        }

        #endregion

        #region 五个频道存储DataGridViewRowList的属性
        private static List<DataGridViewRow> rowlist;
        public static List<DataGridViewRow> RowList
        {
            get { return rowlist; }
            set { rowlist = value; }
        }
        #endregion
    }
    #region Enum

    public enum ViewBoolMethod { All, Flase, True, Nothing }
    public enum FindTimeType
    {
        //近一天
        Day = 0,
        //近一周
        Week = 1,
        //近一月
        Month = 2,
        //近一季度
        Quarter = 3,
        //近一年
        Year = 4,
        //所有
        All = 5
    }

    #endregion

    #region TagCutOrCopy 为五个form提供一个共有的平台
    public static class TagCutOrCopy
    {
        private static string cutChannelId;
        public static string CutChannelId
        {
            get {return cutChannelId;}
            set {cutChannelId = value;}
        }
        private static List<int> cutPageIndex;
        public static List<int> CutPageIndex
        {
            get {return cutPageIndex; }
            set { cutPageIndex = value; }
        }
        private static int tagPageIndexCut;
        public static int TagPageIndexCut
        {
            get { return tagPageIndexCut; }
            set { tagPageIndexCut = value; }
        }
        private static bool tagCut;
        public static bool TagCut
        {
            get { return tagCut; }
            set { tagCut = value; }
        }
        private static List<PageSimpleExXmlElement> pageEleList;
        public static List<PageSimpleExXmlElement> PageEleList
        {
            get { return pageEleList; }
            set { pageEleList = value; }
        }
    }
    #endregion
    #region 窗口的状态保存
    public static class StateSave
    {
        private static string search;
        public static string Search
        {
            get { return search; }
            set { search = value; }
        }
        private static string editTime;
        public static string EditTime
        {
            get { return editTime; }
            set { editTime = value; }
        }
        private static string dateNumber;
        public static string DateNumber
        {
            get { return dateNumber; }
            set { dateNumber = value; }
        }
        private static string filter;
        public static string Filter
        {
            get { return filter; }
            set { filter = value; }
        }
        private static string[] columnName;
        public static string[] ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }
        private static List<PageSimpleExXmlElement> pageEleList;
        public static List<PageSimpleExXmlElement> PageEleList
        {
            get { return pageEleList; }
            set { pageEleList = value; }
        }
        private static List<int> rowsList;
        public static List<int> RowList
        {
            get { return rowsList; }
            set { rowsList = value; }
        }
    }

    #endregion
}
