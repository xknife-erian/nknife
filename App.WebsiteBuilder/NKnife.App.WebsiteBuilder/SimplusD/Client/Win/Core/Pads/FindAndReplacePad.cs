using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public enum FindMode
    {
        Find,
        Replace
    }

    public partial class FindAndReplacePad : Form
    {
        #region 声明所有控件

        ToolStripButton findButton = new ToolStripButton();
        ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
        ToolStripButton replaceButton = new ToolStripButton();
        CheckBox matchWholeWordCheckBox = new CheckBox();
        CheckBox findStrategyTypeCheckBox = new CheckBox();
        CheckBox matchCaseCheckBox = new CheckBox();
        CheckBox upWardCheckBox = new CheckBox();
        ComboBox findAreaComboBox = new ComboBox();
        ComboBox findStrategyTypeComboBox = new ComboBox();
        Button findNextButton = new Button();
        Button replaceNextButton = new Button();
        Button replaceAllButton = new Button();
        TextBox findPatternTextBox = new TextBox();
        TextBox replacePatternTextBox = new TextBox();
        GroupBox groupBox = new GroupBox();
        ToolStrip toolStrip = new ToolStrip();
        Label findPattrenLabel = new Label();
        Label replacePatternLabel = new Label();
        Label findAreaLabel = new Label();

        #endregion

        #region 声明全局变量

        static Form _mainForm = null;

        FindMode findAndReplaceMode = FindMode.Find;

        ISearch _currentFind = null;
        List<ISearch> _finds = new List<ISearch>();

        IMarkPosition _currentMarkPosition = null;

        List<Position> _positions = new List<Position>();

        bool changFind = false;

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置当前查找的对象
        /// </summary>
        public ISearch CurrentFind
        {
            get
            {
                return _currentFind;
            }
            set
            {
                _currentFind = value;
            }
        }

        public List<ISearch> Finds
        {
            get
            {
                return _finds;
            }
            set
            {
                _finds = value;
            }
        }

        /// <summary>
        /// 获取或设置当前的定位方式
        /// </summary>
        public IMarkPosition MarkPosition
        {
            get
            {
                return _currentMarkPosition;
            }
            set
            {
                _currentMarkPosition = value;
            }
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mainForm"> 主窗体</param>
        static public void Initialize(Form mainForm)
        {
            _mainForm = mainForm;
        }

        public FindAndReplacePad(FindMode mode)
        {
            this.Owner = _mainForm;
            _mainForm.MdiChildActivate += new EventHandler(MainFormMdiChildActivate);
            // this.Activated += new EventHandler(FindFormActivated);
            this.Load += new EventHandler(FindFormLoad);
            this.TopMost = true;
            FindOptions.Singler.Positions.Clear();

            findAndReplaceMode = mode;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.TopMost = false;
            this.Text = "查找和替换";
            //groupBox
            groupBox.Location = mode == FindMode.Replace ? new Point(13, 161) : new Point(13, 111);
            groupBox.Size = new Size(220, 139);
            groupBox.Enabled = true;
            groupBox.Visible = true;
            groupBox.Text = "查找选项";
            //matchCaseCheckBox
            matchCaseCheckBox.Enabled = true;
            matchCaseCheckBox.Visible = true;
            matchCaseCheckBox.Checked = FindOptions.Singler.MatchCase;
            matchCaseCheckBox.Text = "大小写匹配";
            matchCaseCheckBox.Location = new Point(18, 20);
            matchCaseCheckBox.AutoSize = true;
            matchCaseCheckBox.UseVisualStyleBackColor = true;
            matchCaseCheckBox.CheckedChanged += new EventHandler(MatchCaseCheckBoxCheckedChanged);
            groupBox.Controls.Add(matchCaseCheckBox);
            //matchWholeWordCheckBox
            matchWholeWordCheckBox.Enabled = true;
            matchWholeWordCheckBox.Visible = true;
            matchWholeWordCheckBox.Checked = FindOptions.Singler.MatchWholeWord;
            matchWholeWordCheckBox.Text = "全字匹配";
            matchWholeWordCheckBox.Location = new Point(18, 40);
            matchWholeWordCheckBox.AutoSize = true;
            matchWholeWordCheckBox.UseVisualStyleBackColor = true;
            matchWholeWordCheckBox.CheckedChanged += new EventHandler(MatchWholeWordCheckBoxCheckedChanged);
            groupBox.Controls.Add(matchWholeWordCheckBox);
            //upWardCheckBox
            upWardCheckBox.Enabled = true;
            upWardCheckBox.Visible = true;
            upWardCheckBox.Checked = FindOptions.Singler.UpWard;
            upWardCheckBox.Text = "向上查找";
            upWardCheckBox.Location = new Point(18, 60);
            upWardCheckBox.AutoSize = true;
            upWardCheckBox.UseVisualStyleBackColor = true;
            upWardCheckBox.CheckStateChanged += new EventHandler(UpWardCheckBoxCheckStateChanged);
            groupBox.Controls.Add(upWardCheckBox);
            //findStrategyTypeCheckBox
            findStrategyTypeCheckBox.Enabled = true;
            findStrategyTypeCheckBox.Visible = true;
            findStrategyTypeCheckBox.AutoSize = true;
            findStrategyTypeCheckBox.Text = "使用";
            findStrategyTypeCheckBox.Location = new Point(18, 80);
            findStrategyTypeCheckBox.UseVisualStyleBackColor = true;
            //if (FindOptions.Singler.FindStrategyType == FindStrategyType.Normal)
            //    findStrategyTypeCheckBox.Checked = false;
            //else
            //    findStrategyTypeCheckBox.Checked = true;
            findStrategyTypeCheckBox.CheckedChanged += new EventHandler(FindStrategyTypeCheckBoxCheckedChanged);
            groupBox.Controls.Add(findStrategyTypeCheckBox);
            //findStrategyTypeComboBox
            findStrategyTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            findStrategyTypeComboBox.Items.AddRange(new object[] { "正则表达式", "通配符" });
            findStrategyTypeComboBox.Enabled = findStrategyTypeCheckBox.Checked;
            findStrategyTypeComboBox.Visible = true;
            findStrategyTypeComboBox.FormattingEnabled = true;
            findStrategyTypeComboBox.Location = new Point(28, 100);
            findStrategyTypeComboBox.Size = new Size(175, 20);
            findStrategyTypeComboBox.SelectedIndex = 0;
            findStrategyTypeComboBox.SelectedIndexChanged += new EventHandler(FindStrategyTypeComboBoxSelectedIndexChanged);
            groupBox.Controls.Add(findStrategyTypeComboBox);
            //

            //toolStrip
            toolStrip.Location = new Point(0, 0);
            toolStrip.Dock = DockStyle.Top;
            toolStrip.Stretch = true;
            toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip.Visible = true;
            //findButton
            findButton.Text = "快速查找(&F)";
            findButton.Checked = findAndReplaceMode == FindMode.Find;
            findButton.Click += new EventHandler(FindButtonClick);
            toolStrip.Items.Add(findButton);
            //toolStripSeparator
            toolStrip.Items.Add(toolStripSeparator);
            //replaceButton
            replaceButton.Text = "快速替换(&H)";
            replaceButton.Checked = findAndReplaceMode == FindMode.Replace;
            replaceButton.Click += new EventHandler(ReplaceButtonClick);
            toolStrip.Items.Add(replaceButton);
            //

            //findPatternLabel
            findPattrenLabel.Text = "查找内容：";
            findPattrenLabel.TextAlign = ContentAlignment.MiddleCenter;
            findPattrenLabel.Location = new Point(10, 25);
            findPattrenLabel.Size = new Size(77, 14);
            findPattrenLabel.Visible = true;

            //findPatternTextBox
            findPatternTextBox.Enabled = true;
            findPatternTextBox.Visible = true;
            findPatternTextBox.Location = new Point(15, 41);
            findPatternTextBox.Size = new Size(215, 21);
            findPatternTextBox.Text = FindOptions.Singler.FindContent;
            findPatternTextBox.TextChanged += new EventHandler(FindPatternTextBoxTextChanged);

            //replacePatternLabel
            replacePatternLabel.Location = new Point(10, 65);
            replacePatternLabel.Text = "替换为：";
            replacePatternLabel.TextAlign = ContentAlignment.MiddleCenter;
            replacePatternLabel.Size = new Size(63, 14);

            //replacePatternTextBox
            replacePatternTextBox.Text = FindOptions.Singler.ReplaceContent;
            replacePatternTextBox.Location = new Point(15, 82);
            replacePatternTextBox.Size = new Size(215, 20);
            replacePatternTextBox.Enabled = mode == FindMode.Replace;
            replacePatternTextBox.Visible = mode == FindMode.Replace;
            replacePatternTextBox.TextChanged += new EventHandler(ReplacePatternTextBoxTextChanged);

            //findNextButton
            findNextButton.Enabled = false;
            findNextButton.Visible = true;
            findNextButton.Text = "查找下一个";
            findNextButton.Size = new Size(85, 23);
            findNextButton.Location = mode == FindMode.Find ? new Point(148, 236) : new Point(148, 286);
            findNextButton.Click += new EventHandler(FindNextButtonClick);
            findNextButton.EnabledChanged += new EventHandler(FindNextButtonEnabledChanged);

            //replaceNextButton
            replaceNextButton.Size = new Size(56, 23);
            replaceNextButton.Location = new Point(177, 306);
            replaceNextButton.Text = "替换";
            replaceNextButton.Visible = mode == FindMode.Replace;
            replaceNextButton.Click += new EventHandler(ReplaceNextButtonClick);

            //replaceAllButton
            replaceAllButton.Size = new Size(85, 23);
            replaceAllButton.Text = "全部替换";
            replaceAllButton.Visible = mode == FindMode.Replace;
            replaceAllButton.Click += new EventHandler(ReplaceAllButtonClick);

            //findAreaLabel
            findAreaLabel.Size = new Size(77, 14);
            findAreaLabel.Text = "查找范围：";
            findAreaLabel.TextAlign = ContentAlignment.MiddleCenter;

            //findAreaComboBox
            findAreaComboBox.Location = mode == FindMode.Replace ? new Point(15, 128) : new Point(15, 82);
            findAreaComboBox.Size = new Size(215, 20);
            findAreaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            findAreaComboBox.SelectedIndexChanged += new EventHandler(FindAreaComboBoxSelectedIndexChanged);
            //
            AddControls();
            SetFindMode();

        }

        void FindFormLoad(object sender, EventArgs e)
        {
            SetFindArea();
        }

        void FindFormActivated(object sender, EventArgs e)
        {
            SetFindArea();
        }

        void MainFormMdiChildActivate(object sender, EventArgs e)
        {
            SetFindArea();
        }

        void AddControls()
        {
            Controls.Add(findPattrenLabel);
            Controls.Add(replacePatternLabel);
            Controls.Add(findAreaLabel);
            Controls.Add(toolStrip);
            Controls.Add(findPatternTextBox);
            Controls.Add(replacePatternTextBox);
            Controls.Add(findAreaComboBox);
            Controls.Add(groupBox);
            Controls.Add(findNextButton);
            Controls.Add(replaceNextButton);
            Controls.Add(replaceAllButton);
        }

        void FindButtonClick(object sender, EventArgs e)
        {
            if (!findButton.Checked)
            {
                EnableFindMode(true);
            }
        }

        void ReplaceButtonClick(object sender, EventArgs e)
        {
            if (!replaceButton.Checked)
            {
                EnableFindMode(false);
            }
        }

        void EnableFindMode(bool enable)
        {
            findButton.Checked = enable;
            replaceButton.Checked = !enable;
            SetFindMode();
            Focus();
        }

        void SetFindMode()
        {
            if (findButton.Checked)
            {
                findAndReplaceMode = FindMode.Find;
                groupBox.Location = new Point(13, 111);
                findAreaLabel.Location = new Point(10, 65);
                findAreaComboBox.Location = new Point(15, 82);
                findNextButton.Location = new Point(148, 256);
                replaceAllButton.Visible = false;
                replacePatternLabel.Visible = false;
                replaceNextButton.Visible = false;
                replaceNextButton.Enabled = findNextButton.Enabled; ;
                replacePatternTextBox.Enabled = false;
                replacePatternTextBox.Visible = false;
                this.ClientSize = new Size(250, 285);
            }
            else
            {
                findAndReplaceMode = FindMode.Replace;
                groupBox.Location = new Point(13, 161);
                findAreaLabel.Location = new Point(10, 111);
                findAreaComboBox.Location = new Point(15, 128);
                findNextButton.Location = new Point(87, 306);
                replaceAllButton.Enabled = findNextButton.Enabled;
                replaceAllButton.Visible = true;
                replaceAllButton.Location = new Point(148, 336);
                replacePatternLabel.Visible = true;
                replaceNextButton.Visible = true;
                replaceNextButton.Enabled = findNextButton.Enabled; ;
                replacePatternTextBox.Enabled = true;
                replacePatternTextBox.Visible = true;
                this.ClientSize = new Size(250, 365);
            }
        }

        void FindPatternTextBoxTextChanged(object sender, EventArgs e)
        {
            if (findPatternTextBox.Text != "" && findPatternTextBox.Text != null)
            {
                FindOptions.Singler.FindContent = findPatternTextBox.Text;
                findNextButton.Enabled = true;
            }
            else
                findNextButton.Enabled = false;
        }

        void ReplacePatternTextBoxTextChanged(object sender, EventArgs e)
        {
            if (replacePatternTextBox.Text != null)
                FindOptions.Singler.ReplaceContent = replacePatternTextBox.Text;
        }

        void FindNextButtonClick(object sender, EventArgs e)
        {
            if (_currentFind == null)
            {
                MessageBox.Show("查找范围设置有误！");
                return;
            }
            Finding();
            if (FindOptions.Singler.CurrentPosition == null)
            {
                Reset();
                MessageBox.Show("查找结束！");
            }
            else
                _currentMarkPosition.MarkPosition(FindOptions.Singler.CurrentPosition);
        }

        void FindNextButtonEnabledChanged(object sender, EventArgs e)
        {
            replaceNextButton.Enabled = findNextButton.Enabled;
            replaceAllButton.Enabled = findNextButton.Enabled;
        }

        void ReplaceNextButtonClick(object sender, EventArgs e)
        {
            Finding();
            if (FindOptions.Singler.CurrentPosition == null)
            {
                Reset();
                MessageBox.Show("查找结束！");
            }
            else
            {
                _currentFind.Replace(FindOptions.Singler.CurrentPosition);
                FindOptions.Singler.Positions.Add(FindOptions.Singler.CurrentPosition);
                _currentMarkPosition.MarkPosition(FindOptions.Singler.CurrentPosition);
            }
        }

        void ReplaceAllButtonClick(object sender, EventArgs e)
        {
            foreach (ISearch find in _finds)
            {
                FindOptions.Singler.CurrentPosition = _currentFind.SearchNext();
                _currentFind.Replace(FindOptions.Singler.CurrentPosition);
                FindOptions.Singler.Positions.Add(FindOptions.Singler.CurrentPosition);
            }
        }

        void FindAreaComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.Reset();
            if (findAreaComboBox.Items.Count <= 1)
            {
                //FindOptions.Singler.FindScope = FindScope.CurrentChannel;
                SetFinds();
            }
            else
            {
                FindOptions.Singler.FindScope = (FindScope)findAreaComboBox.SelectedIndex;
                SetFinds();
            }
        }

        void MatchCaseCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.Reset();
            FindOptions.Singler.MatchCase = matchCaseCheckBox.Checked;
        }

        void MatchWholeWordCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.Reset();
            FindOptions.Singler.MatchWholeWord = matchWholeWordCheckBox.Checked;
        }

        void UpWardCheckBoxCheckStateChanged(object sender, EventArgs e)
        {
            FindOptions.Singler.Reset();
            FindOptions.Singler.UpWard = upWardCheckBox.Checked;
        }

        void FindStrategyTypeCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            //FindOptions.Singler.Reset();
            //FindOptions.Singler.FindStrategyType = findStrategyTypeCheckBox.Checked ? (FindStrategyType)findStrategyTypeComboBox.SelectedIndex : FindStrategyType.Normal;
            //findStrategyTypeComboBox.Enabled = findStrategyTypeCheckBox.Checked;
        }

        void FindStrategyTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            //FindOptions.Singler.Reset();
            //if (findStrategyTypeCheckBox.Checked)
            //    FindOptions.Singler.FindStrategyType = (FindStrategyType)findStrategyTypeComboBox.SelectedIndex;
            //else
            //    FindOptions.Singler.FindStrategyType = FindStrategyType.Normal;
        }

        void SetFindArea()
        {
            /* 确定findAreaComBox的内容*/
            findAreaComboBox.Items.Clear();
            if (!Service.Project.IsProjectOpened)
            {
                return;
            }
            else if (_mainForm.MdiChildren.Length <= 0)
            {
                findAreaComboBox.Items.AddRange(new object[] { "当前项目" });
                findAreaComboBox.SelectedIndex = 0;
                //FindOptions.Singler.FindScope = FindScope.CurrentChannel;
            }
            else
            {
                //foreach (IMarkPosition markPosition in _mainForm.MdiChildren)
                //{
                //    if (markPosition != null)
                //    {
                //        findAreaComboBox.Items.AddRange(new object[] { "当前文档", "选定部分", "所有打开的文档", "当前块(项目)", "解决方案" });
                //        findAreaComboBox.SelectedIndex = 0;
                //        FindOptions.Singler.FindAreaType = FindAreaType.CurrentForm;
                //    }
                //    else
                //    {
                //        findAreaComboBox.Items.AddRange(new object[] { "当前项目" });
                //        findAreaComboBox.SelectedIndex = 0;
                //        FindOptions.Singler.FindAreaType = FindAreaType.CurrentChannel;
                //    }
                //}
            }
            SetFinds();
        }

        void SetFinds()
        {
            _finds.Clear();
            if (!Service.Project.IsProjectOpened)
                return;
            if (_mainForm.MdiChildren.Length <= 0)
                return;
            try
            {
                switch (FindOptions.Singler.FindScope)
                {
                    case FindScope.CurrentForm:
                        IMarkPosition currentDocMark = _mainForm.ActiveMdiChild as IMarkPosition;
                        _currentFind = currentDocMark.Search;
                        _finds.Add(currentDocMark.Search);
                        break;
                    //case FindScope.CurrentSelection:
                    //    foreach (IMarkPosition markPosition in _mainForm.MdiChildren)
                    //    {
                    //        if (markPosition != null && markPosition.SelectedPositions != null)
                    //        {
                    //            _finds.Add(markPosition.Search);
                    //        }
                    //    }
                    //    _currentFind = ((IMarkPosition)_mainForm.ActiveMdiChild).Search;
                    //    break;
                    case FindScope.AllOpenForm:
                        foreach (IMarkPosition allOpenMark in _mainForm.MdiChildren)
                        {
                            if (allOpenMark != null)
                                _finds.Add(allOpenMark.Search);
                        }
                        _currentFind = ((IMarkPosition)_mainForm.ActiveMdiChild).Search;
                        break;
                    
                    case FindScope.WholeChannels:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Reset()
        {
            FindOptions.Singler.Reset();
        }

        void Finding()
        {
            if (_finds.Count <= 0)
            {
                MessageBox.Show("查找范围设置有误！");
                return;
            }
            if (_mainForm.MdiChildren.Length > 0)
            {
                FindOptions.Singler.StartPosition = ((IMarkPosition)(_mainForm.ActiveMdiChild)).CurrentPosition;
                _currentFind = ((IMarkPosition)(_mainForm.ActiveMdiChild)).Search;
            }
            try
            {
                foreach (ISearch find in _finds)
                {
                    if (find == _currentFind && !changFind)
                    {
                        FindOptions.Singler.CurrentPosition = _currentFind.SearchNext();
                        if (FindOptions.Singler.CurrentPosition == null)
                        {
                            changFind = true;
                            continue;
                        }
                    }
                    else if (find != _currentFind && changFind)
                    {
                        changFind = false;
                        _currentFind = find;
                        FindOptions.Singler.CurrentPosition = _currentFind.SearchNext();
                        if (FindOptions.Singler.CurrentPosition == null)
                        {
                            
                            changFind = true;
                            continue;
                        }
                    }
                    else
                        continue;
                }
                MessageBox.Show("查找完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}