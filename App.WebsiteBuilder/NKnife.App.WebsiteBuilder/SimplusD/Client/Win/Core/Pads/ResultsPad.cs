using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using System.Reflection;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{
    /*******************  本form尚缺工具栏中的按钮的图标  共4个 **************/
    /*******************         添加完成后请删除这两行文字     **************/


    public class ResultsPad : PadContent
    {
        /// <summary>
        /// 当前的主窗体
        /// </summary>
        static Form _mainFindForm = null;
        ListView _resultsListView = new ListView();
        ToolStrip _toolStrip = new ToolStrip();
        ToolStripButton _markButton = new ToolStripButton();
        ToolStripButton _moveUpButton = new ToolStripButton();
        ToolStripButton _moveDownButton = new ToolStripButton();
        ToolStripButton _deleteAllButton = new ToolStripButton();
        ToolStripSeparator _seParator1 = new ToolStripSeparator();
        ToolStripSeparator _seParator2 = new ToolStripSeparator();
        ColumnHeader _header = new ColumnHeader();

        ListViewItem _item;
        Dictionary<int, Position> _dictionry = new Dictionary<int, Position>();
        bool ResultsIsEmpty = true;

        public ResultsPad()
        {

            this.Text = StringParserService.Parse("${res:Pad.Results.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("main.newproject.smallicon").GetHicon());

            this.Load += new EventHandler(ResultsPadLoad);
            _markButton.Click += new EventHandler(MarkButtonClick);
            _moveDownButton.Click += new EventHandler(MoveDownButtonClick);
            _moveUpButton.Click += new EventHandler(MoveUpButtonClick);
            _deleteAllButton.Click += new EventHandler(DeleteAllButtonClick);
            _resultsListView.SelectedIndexChanged += new EventHandler(ResultsListViewSelectedIndexChanged);
            _resultsListView.MouseDoubleClick += new MouseEventHandler(_resultsListView_MouseDoubleClick);

            // _toolStrip
            _toolStrip.Dock = DockStyle.Top;
            _toolStrip.Items.AddRange(new ToolStripItem[] {_markButton,
                _seParator1,
                _moveUpButton,
                _moveDownButton,
                _seParator2,
                _deleteAllButton });
            _toolStrip.Visible = true;
            _toolStrip.Enabled = false;

            // _markButton
            _markButton.Text = "转到当前行的位置";
            _markButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            //_markButton.Image = Image.FromFile(Path.Combine(PathService.SoftwarePath, ""));
            _markButton.Image = null;
            _markButton.AutoSize = true;
            _markButton.Visible = true;
            _markButton.Enabled = true;


            // _seParator1 & _seParator2
            //_seParator1.AutoSize = true;
            _seParator1.Visible = true;
            // _seParator2.AutoSize = true;
            _seParator2.Visible = true;

            // _moveUpButton
            _moveUpButton.Text = "转到列表中的上一个位置";
            _moveUpButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _moveUpButton.AutoSize = true;
            _moveUpButton.Visible = true;
            _moveUpButton.Enabled = true;

            // _moveDownButton
            _moveDownButton.Text = "转到列表中的下一个位置";
            _moveDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _moveDownButton.AutoSize = true;
            _moveDownButton.Visible = true;
            _moveDownButton.Enabled = true;

            // _deleteAllButton
            _deleteAllButton.Text = "全部清除";
            _deleteAllButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            _deleteAllButton.AutoSize = true;
            _deleteAllButton.Visible = true;
            _deleteAllButton.Enabled = true;

            // _header
            _header.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            // _resultsListView
            _resultsListView.Visible = true;
            _resultsListView.MultiSelect = false;
            _resultsListView.View = View.Details;
            _resultsListView.Dock = DockStyle.Fill;
            _resultsListView.BorderStyle = BorderStyle.Fixed3D;
            _resultsListView.HeaderStyle = ColumnHeaderStyle.None;
            _resultsListView.Columns.Add(_header);
            _header.Width = 700;
            _resultsListView.Groups.Clear();

            this.Controls.Add(_resultsListView);
            this.Controls.Add(_toolStrip);

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mainForm"> 主窗体</param>
        static public void Initialize(Form mainForm)
        {
            _mainFindForm = mainForm;
        }
        //结果窗口的定位 by lisuye on 2008年6月13日
        void _resultsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = _resultsListView.SelectedItems[0].Index;
            if (index != 0)
            {
                Position position = _dictionry[index];
                XmlDocument xmlDocument = (XmlDocument)position.GetDocument();
                Type type = xmlDocument.GetType();
                switch (type.Name)
                {
                    case "HrXmlDocument":
                        HrXmlDocument hrDoc = (HrXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm hrForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, hrDoc.Id) as MdiBaseEditViewForm;
                        hrForm.MarkPosition(position);
                        FindOptions.Singler.CurrentPosition = position;
                        //if (index > 0)
                        //    FindOptions.Singler.StartPosition = _dictionry[index];
                        //else
                        //    FindOptions.Singler.StartPosition = null;
                        break;
                    case "InviteBiddingXmlDocument":
                        InviteBiddingXmlDocument bidDoc = (InviteBiddingXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm bidForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, bidDoc.Id) as MdiBaseEditViewForm;
                        bidForm.MarkPosition(position);
                        FindOptions.Singler.CurrentPosition = position;
                        FindOptions.Singler.CurrentPosition = position;
                        //if (index > 0)
                        //    FindOptions.Singler.StartPosition = _dictionry[index];
                        //else
                        //    FindOptions.Singler.StartPosition = null;
                        break;
                    case "KnowledgeXmlDocument":
                        KnowledgeXmlDocument knowDoc = (KnowledgeXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm knowForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, knowDoc.Id) as MdiBaseEditViewForm;
                        knowForm.MarkPosition(position);
                        FindOptions.Singler.CurrentPosition = position;
                        FindOptions.Singler.CurrentPosition = position;
                        //if (index > 0)
                        //    FindOptions.Singler.StartPosition = _dictionry[index];
                        //else
                        //    FindOptions.Singler.StartPosition = null;
                        break;
                    case "ProductXmlDocument":
                        ProductXmlDocument prodcutDoc = (ProductXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm productForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, prodcutDoc.Id) as MdiBaseEditViewForm;
                        productForm.MarkPosition(position);
                        FindOptions.Singler.CurrentPosition = position;
                        FindOptions.Singler.CurrentPosition = position;
                        //if (index > 0)
                        //    FindOptions.Singler.StartPosition = _dictionry[index];
                        //else
                        //    FindOptions.Singler.StartPosition = null;
                        break;
                    case "ProjectXmlDocument":
                        ProjectXmlDocument projectDoc = (ProjectXmlDocument)position.GetDocument();
                        MdiBaseEditViewForm projectForm = Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, projectDoc.Id) as MdiBaseEditViewForm;
                        projectForm.MarkPosition(position);
                        FindOptions.Singler.CurrentPosition = position;
                        FindOptions.Singler.CurrentPosition = position;
                        //if (index > 0)
                        //    FindOptions.Singler.StartPosition = _dictionry[index];
                        //else
                        //    FindOptions.Singler.StartPosition = null;
                        break;
                    default:
                        break;
                }

            }
        }

        /// <summary>
        /// 启动面板时发生
        /// </summary>
        void ResultsPadLoad(object sender, EventArgs e)
        {
            _dictionry.Clear();
        }

        /// <summary>
        /// 当选择的部分发生改变时。。。
        /// </summary>
        void ResultsListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ResultsIsEmpty)
                _item = null;
            else
                _item = _resultsListView.FocusedItem == _resultsListView.Items[0] ? _resultsListView.Items[1] : _resultsListView.FocusedItem;
        }

        void MarkButtonClick(object sender, EventArgs e)
        {
            if (ResultsIsEmpty)
                return;
            _item = _resultsListView.FocusedItem == _resultsListView.Items[0] ? _resultsListView.Items[1] : _resultsListView.FocusedItem;
            MarkingResult();
        }

        void MoveDownButtonClick(object sender, EventArgs e)
        {
            if (ResultsIsEmpty)
                return;
            _item = _resultsListView.FocusedItem == _resultsListView.Items[0] ? _resultsListView.Items[1] : _resultsListView.FocusedItem;
            int index = _resultsListView.Items.IndexOf(_item);
            if (index == _resultsListView.Items.Count - 1)
                MarkingResult();
            else if (index <= 0 && index > _resultsListView.Items.Count - 1)
                return;
            else
            {
                _item = _resultsListView.Items[index + 1];
                MarkingResult();
            }


        }

        void MoveUpButtonClick(object sender, EventArgs e)
        {
            if (ResultsIsEmpty)
                return;
            _item = _resultsListView.FocusedItem == _resultsListView.Items[0] ? _resultsListView.Items[1] : _resultsListView.FocusedItem;
            int index = _resultsListView.Items.IndexOf(_item);
            if (index == 1)
                MarkingResult();
            else if (index <= 0 && index > _resultsListView.Items.Count - 1)
                return;
            else
            {
                _item = _resultsListView.Items[index - 1];
                MarkingResult();
            }

        }

        void DeleteAllButtonClick(object Sender, EventArgs e)
        {
            FindOptions.Singler.Positions.Clear();
            _resultsListView.Items.Clear();
            ResultsIsEmpty = true;
        }
        public void ForOtherSearchForm()
        {
            SetContent();
        }
        /// <summary>
        /// 根据查找的结果设置内容
        /// </summary>
        void SetContent()
        {
            _resultsListView.Items.Clear();
            if (FindOptions.Singler.Positions == null || FindOptions.Singler.Positions.Count <= 0)
            {
                ResultsIsEmpty = true;
                _resultsListView.Items.Add(new ListViewItem("查找结果：0个匹配项"));
                _toolStrip.Enabled = false;
            }
            else
            {
                ResultsIsEmpty = false;
                _resultsListView.Items.Add(new ListViewItem("查找结果：" + FindOptions.Singler.Positions.Count + "个匹配项"));
                foreach (Position position in FindOptions.Singler.Positions)
                {
                    ListViewItem item = new ListViewItem();
                  
                    item.Text = position.Property.GetValue(position.GetDocument(), null).ToString();
                    _resultsListView.Items[0].Selected = true;
                    _resultsListView.Items.Add(item);
                    _dictionry.Add(_dictionry.Count + 1, position);
                }
                _toolStrip.Enabled = true;
            }
        }

        void MarkingResult()
        {
            Position position = _dictionry[_item.Index];
            //WorkbenchForm.MainForm.ActivingForm(position.Path);
            ((IMarkPosition)WorkbenchForm.MainForm.ActiveMdiChild).MarkPosition(position);
            _item.Checked = true;
        }
    }

}
