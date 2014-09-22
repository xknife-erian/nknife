using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 所有的关键词将显示在这个窗体，并进行管理，这个窗体的主要控件是CheckedListBox
    /// </summary>
    public class KeywordListView : AbstractView
    {
        #region 构造函数

        internal KeywordListView(WordsManager wordManager,KeywordFormType formType)
        {
            this.InitializeComponent();
            this.WordManager = wordManager;
            FormType = formType;
            Init();
            if (FormType == KeywordFormType.ExistWord)
            {
                _KeyWordCheckedListBox.Items.Add("1");
                _KeyWordCheckedListBox.Items.Add("2");
                _KeyWordCheckedListBox.Items.Add("3");
            }
        }

        #endregion

        #region 字段或属性

        private ToolStrip _ToolStrip;
        private StatusStrip _StatusStrip;
        private ContextMenuStrip _CleanMenuStrip;
        private System.ComponentModel.IContainer components;
        private ToolStripButton toolStrip_Qurey;

        private const int POWER = 1; //默认权重

        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStrip_UpPage;
        private ToolStripButton toolStrip_DownPage;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox toolStripComboBox_GotoPage;
        private ToolStripButton toolStrip_AddItem;

        private int _currentPageIndex; //当前显示PAGE的索引
        private int _totalPageIndex; //页数的总shuttle
        private int _pageItemCount = 280; //一页显示项的个数

        private WordsManager WordManager { get; set; }

        private KeywordFormType _formType;

        /// <summary>
        /// 被打开Form的类型 是新的还是已存在的
        /// </summary>
        public KeywordFormType FormType
        {
            get
            {
                return _formType;
            }
            set
            {
                _formType = value;
                switch (_formType)
                {
                    case KeywordFormType.NewWord:
                        this.TabText = "捕捉新出现关键词";
                        break;
                    case KeywordFormType.ExistWord:
                        this.TabText = "查看已存在库中的关键词";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 返回当前的CheckedListBox
        /// </summary>
        public CheckedListBox CheckedListBox
        {
            get
            {
                return _KeyWordCheckedListBox;
            }
        }

        /// <summary>
        /// 上一个选中索引
        /// </summary>
        private int _prevCheckIndex;

        /// <summary>
        /// 得到选中项索引
        /// </summary>
        private int CurrentCheckIndex
        {
            get
            {
                if (_KeyWordCheckedListBox.SelectedIndex < 0) return 0;

                if (_KeyWordCheckedListBox.GetItemCheckState(_KeyWordCheckedListBox.SelectedIndex) == CheckState.Checked)
                {
                    return _KeyWordCheckedListBox.SelectedIndex;
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// 获得当前词库
        /// </summary>
        private Dictionary<string, ulong> CurrentWordLib
        {
            get
            {
                if (FormType == KeywordFormType.NewWord)
                {
                    return WordManager.NewWords;
                }
                else if (FormType == KeywordFormType.ExistWord)
                {
                    return WordManager.ExistWords;
                }
                return new Dictionary<string,ulong>();
            }
        }
        #endregion

        #region 初始化

        private void Init()
        {
            if (FormType == KeywordFormType.ExistWord)
            {
                toolStrip_AddItem.Visible = false;
            }
            //if (this.CurrentWordLib.Count <= 0) return;

            if (this.CurrentWordLib.Count % _pageItemCount != 0)
            {
                _totalPageIndex = this.CurrentWordLib.Count / _pageItemCount + 1;
            }
            else
            {
                _totalPageIndex = this.CurrentWordLib.Count / _pageItemCount;
            }
            object[] array = new object[_totalPageIndex];
            for (int i = 0; i < _totalPageIndex; i++)
            {
                array[i] = i + 1;
            }
            toolStripComboBox_GotoPage.Items.AddRange(array);

            _currentPageIndex = 1;
            toolStripComboBox_GotoPage.Text = _currentPageIndex.ToString();

            ShowDesignatePageItem(_currentPageIndex);
        }

        #endregion

        #region 初始化控件

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeywordListView));
            this._KeyWordCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStrip_Qurey = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_UpPage = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_DownPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox_GotoPage = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip_AddItem = new System.Windows.Forms.ToolStripButton();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._CleanMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _KeyWordCheckedListBox
            // 
            this._KeyWordCheckedListBox.CheckOnClick = true;
            this._KeyWordCheckedListBox.ColumnWidth = 120;
            this._KeyWordCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._KeyWordCheckedListBox.FormattingEnabled = true;
            this._KeyWordCheckedListBox.Location = new System.Drawing.Point(0, 25);
            this._KeyWordCheckedListBox.MultiColumn = true;
            this._KeyWordCheckedListBox.Name = "_KeyWordCheckedListBox";
            this._KeyWordCheckedListBox.Size = new System.Drawing.Size(375, 244);
            this._KeyWordCheckedListBox.TabIndex = 1;
            this._KeyWordCheckedListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this._KeyWordCheckedListBox_MouseUp);
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Qurey,
            this.toolStripSeparator1,
            this.toolStrip_UpPage,
            this.toolStrip_DownPage,
            this.toolStripLabel1,
            this.toolStripComboBox_GotoPage,
            this.toolStrip_AddItem});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(375, 25);
            this._ToolStrip.TabIndex = 2;
            this._ToolStrip.Text = "toolStrip1";
            // 
            // toolStrip_Qurey
            // 
            this.toolStrip_Qurey.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStrip_Qurey.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_Qurey.Image")));
            this.toolStrip_Qurey.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Qurey.Name = "toolStrip_Qurey";
            this.toolStrip_Qurey.Size = new System.Drawing.Size(35, 22);
            this.toolStrip_Qurey.Text = "查询";
            this.toolStrip_Qurey.Click += new System.EventHandler(this.toolStrip_Qurey_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip_UpPage
            // 
            this.toolStrip_UpPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStrip_UpPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_UpPage.Image")));
            this.toolStrip_UpPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_UpPage.Name = "toolStrip_UpPage";
            this.toolStrip_UpPage.Size = new System.Drawing.Size(47, 22);
            this.toolStrip_UpPage.Text = "上一页";
            this.toolStrip_UpPage.Click += new System.EventHandler(this.toolStrip_UpPage_Click);
            // 
            // toolStrip_DownPage
            // 
            this.toolStrip_DownPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStrip_DownPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_DownPage.Image")));
            this.toolStrip_DownPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_DownPage.Name = "toolStrip_DownPage";
            this.toolStrip_DownPage.Size = new System.Drawing.Size(47, 22);
            this.toolStrip_DownPage.Text = "下一页";
            this.toolStrip_DownPage.Click += new System.EventHandler(this.toolStrip_DownPage_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel1.Text = "跳转到";
            // 
            // toolStripComboBox_GotoPage
            // 
            this.toolStripComboBox_GotoPage.Name = "toolStripComboBox_GotoPage";
            this.toolStripComboBox_GotoPage.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox_GotoPage.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_GotoPage_SelectedIndexChanged);
            this.toolStripComboBox_GotoPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripComboBox_GotoPage_KeyDown);
            // 
            // toolStrip_AddItem
            // 
            this.toolStrip_AddItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStrip_AddItem.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_AddItem.Image")));
            this.toolStrip_AddItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_AddItem.Name = "toolStrip_AddItem";
            this.toolStrip_AddItem.Size = new System.Drawing.Size(35, 22);
            this.toolStrip_AddItem.Text = "增加";
            this.toolStrip_AddItem.Click += new System.EventHandler(this.toolStrip_AddItem_Click);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Location = new System.Drawing.Point(0, 274);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(375, 22);
            this._StatusStrip.TabIndex = 3;
            this._StatusStrip.Text = "statusStrip1";
            // 
            // _CleanMenuStrip
            // 
            this._CleanMenuStrip.Name = "_CleanMenuStrip";
            this._CleanMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // KeywordListView
            // 
            this.ClientSize = new System.Drawing.Size(375, 296);
            this.Controls.Add(this._KeyWordCheckedListBox);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._ToolStrip);
            this.KeyPreview = true;
            this.Name = "KeywordListView";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeywordListView_KeyDown);
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private CheckedListBox _KeyWordCheckedListBox;

        #endregion

        #region 快捷菜单事件相应

        /// <summary>
        /// 删除选中的项
        /// </summary>
        public void DeleteItemFromCheckBox()
        {
            if(_KeyWordCheckedListBox.CheckedIndices.Count <= 0) return;

            DialogResult result = MessageBox.Show("是否删除？","提示",MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return;

            List<string> delItem = GetCheckedItem(_KeyWordCheckedListBox);
            foreach (var item in delItem)
            {
                _KeyWordCheckedListBox.Items.Remove(item);

                if (CurrentWordLib.ContainsKey(item.ToString()))
                {
                    CurrentWordLib.Remove(item.ToString()); //将合并前的词删除
                }
            }
        }

        /// <summary>
        /// 合并选中的项
        /// </summary>
        public void CombinCheckItem()
        {
            if (_KeyWordCheckedListBox.CheckedIndices.Count <= 1) return;

            string szSource = String.Empty;
            foreach (var obj in _KeyWordCheckedListBox.CheckedItems)
            {
                szSource += _KeyWordCheckedListBox.GetItemText(obj) + ",";
            }
            szSource = szSource.Remove(szSource.Length - 1);

            EditCheckedItem form = new EditCheckedItem(WordManager, EditFormType.CombinItem, szSource);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(form.ResultString)) return;

                List<string> checkItem = GetCheckedItem(_KeyWordCheckedListBox);

                int index = _KeyWordCheckedListBox.CheckedIndices[0]; //得到第一个选中的索引
                string szFirst = _KeyWordCheckedListBox.Items[index].ToString(); //老的词
                _KeyWordCheckedListBox.Items[index] = form.ResultString; //合并的结果给第一个赋值

                if (checkItem.Contains(szFirst)) //过滤掉老的词，其他的删除
                {
                    checkItem.Remove(szFirst);
                    if (CurrentWordLib.ContainsKey(szFirst))
                    {
                        CurrentWordLib.Remove(szFirst); //删除旧的词
                        CurrentWordLib.Add(form.ResultString, POWER); //将新词加上来
                    }
                }
                foreach (var item in checkItem)
                {
                    _KeyWordCheckedListBox.Items.Remove(item);

                    if (CurrentWordLib.ContainsKey(item.ToString()))
                    {
                        CurrentWordLib.Remove(item.ToString()); //将合并前的词删除
                    }
                }
            }
        }
        
        /// <summary>
        /// 拆分选中的项
        /// </summary>
        public void SplitCheckItem()
        {
            if (_KeyWordCheckedListBox.CheckedIndices.Count != 1) return;

            int index = _KeyWordCheckedListBox.CheckedIndices[0];
            string szSource = _KeyWordCheckedListBox.CheckedItems[0].ToString();

            EditCheckedItem form = new EditCheckedItem(WordManager, EditFormType.SplitItem, szSource);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(form.ResultString)) return;

                form.ResultString = form.ResultString.Replace("，", ",");
                string[] szInsertItem = form.ResultString.Split(',');

                //将老词删除掉
                if (CurrentWordLib.ContainsKey(szSource))
                {
                    CurrentWordLib.Remove(szSource);
                    _KeyWordCheckedListBox.Items.Remove(szSource);
                }   
  
                foreach (string str in szInsertItem)
                {
                    if (CurrentWordLib.ContainsKey(str)) continue;

                    _KeyWordCheckedListBox.Items.Insert(index++,str);

                    //增加到Dictionary中
                    CurrentWordLib.Add(str, POWER);
                    
                }
            }
        }

        /// <summary>
        /// 编辑选中的项
        /// </summary>
        public void EditCheckItem()
        {
            if (_KeyWordCheckedListBox.CheckedIndices.Count != 1) return;

            int index = _KeyWordCheckedListBox.CheckedIndices[0]; //得到选中的索引
            string szSource = _KeyWordCheckedListBox.CheckedItems[0].ToString();//得到选中的str
            EditCheckedItem form = new EditCheckedItem(WordManager, EditFormType.EditItem, szSource);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(form.ResultString)) return;

                _KeyWordCheckedListBox.Items[index] = form.ResultString;

                //操作Dictionary
                if (CurrentWordLib.ContainsKey(szSource))
                {
                    CurrentWordLib.Remove(szSource);
                    CurrentWordLib.Add(form.ResultString, POWER);
                }                
            }
        }

        /// <summary>
        /// 修改权重
        /// </summary>
        public void ModifyFrequency()
        {
            if (_KeyWordCheckedListBox.CheckedIndices.Count != 1) return;

            int index = _KeyWordCheckedListBox.CheckedIndices[0]; //得到选中的索引
            string szSource = _KeyWordCheckedListBox.CheckedItems[0].ToString();//得到选中的str
            string sztmp = szSource;
            if (CurrentWordLib.ContainsKey(sztmp))
            {
                ulong uFrequency = 0;
                CurrentWordLib.TryGetValue(sztmp,out uFrequency);
                sztmp += "," + uFrequency.ToString();
            }
            EditCheckedItem form = new EditCheckedItem(WordManager,EditFormType.EditFrequency, sztmp);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(form.ResultString)) return;

                if (CurrentWordLib.ContainsKey(szSource))
                {
                    ulong uUpate = 0;
                    ulong.TryParse(form.ResultString,out uUpate);
                    if(uUpate > 0)
                    {
                        CurrentWordLib[szSource] = uUpate;
                    }
                }
            }
        }

        /// <summary>
        /// 增加项
        /// </summary>
        public void AddItem()
        {
            int index = _KeyWordCheckedListBox.SelectedIndex; //得到选中的索引
            if (index < 0) index = 0;
            EditCheckedItem form = new EditCheckedItem(WordManager,EditFormType.AddItem, "");
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(form.ResultString)) return;

                //操作Dictionary
                if (!CurrentWordLib.ContainsKey(form.ResultString))
                {
                    if (_KeyWordCheckedListBox.Items.Count <= 0)
                    {
                        _KeyWordCheckedListBox.Items.Add(form.ResultString);
                    }
                    else
                    {
                        _KeyWordCheckedListBox.Items.Insert(++index, form.ResultString); //插入到LIST
                    }
                    CurrentWordLib.Add(form.ResultString, POWER); //像库中增加项
                }  
            }
        }

        /// <summary>
        /// 得到CheckedListBox 中选中项的字符串集合
        /// </summary>
        /// <param name="checkbox"></param>
        /// <returns></returns>
        private List<string> GetCheckedItem(CheckedListBox checkbox)
        {
            List<string> item = new List<string>();
            CheckedListBox.CheckedItemCollection checedColl = checkbox.CheckedItems;
            foreach (string i in checedColl)
            {
                item.Add(i);
            }
            return item;
        }

        /// <summary>
        /// 取消checkbox中 选中项状态
        /// </summary>
        public void SetCheckState(bool state)
        {
            if (_KeyWordCheckedListBox.CheckedIndices.Count <= 0) return;

            foreach (int i in _KeyWordCheckedListBox.CheckedIndices)
            {
                _KeyWordCheckedListBox.SetItemChecked(i, state);
            }
            if (!state)
            {
                _KeyWordCheckedListBox.ClearSelected();
            }
        }

        #endregion 

        #region CheckListBox 事件

        /// <summary>
        /// 处理 Shift多选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _KeyWordCheckedListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Shift)
            {
                int iCurrentIndex = CurrentCheckIndex;
                if (_prevCheckIndex > 0 && iCurrentIndex > 0)
                {
                    if (_prevCheckIndex > iCurrentIndex)
                    {
                        //_prevCheckIndex += iCurrentIndex;
                        //iCurrentIndex = _prevCheckIndex - iCurrentIndex;
                        //_prevCheckIndex = _prevCheckIndex - iCurrentIndex;
                        int temp = _prevCheckIndex;
                        _prevCheckIndex = iCurrentIndex;
                        iCurrentIndex = temp;
                    }
                    for (int i = _prevCheckIndex; i < iCurrentIndex; i++)
                    {
                        _KeyWordCheckedListBox.SetItemChecked(i, true);
                    }
                }
            }
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.None)
            {
                _prevCheckIndex = CurrentCheckIndex;
                //_currentPageItemIndex = _prevCheckIndex;
            }
        }

        /// <summary>
        /// 处理KeywordListView_KeyDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeywordListView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == (Keys.F | Keys.Control))
            {
                e.SuppressKeyPress = true;
                FindItemForm findForm = new FindItemForm(this);
                findForm.Show();
            }
            else if (e.KeyData == Keys.Delete)
            {
                DeleteItemFromCheckBox();
            }
            else if (e.KeyData == Keys.Insert)
            {
                AddItem();
            }
            else if (e.KeyData == Keys.Escape)
            {
                SetCheckState(false);
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="szDec"> 目标字符串</param>
        /// <param name="bAll">是否全字匹配</param>
        /// <param name="bCase">区分大小写</param>
        /// <param name="isDown">查找方向</param>
        public void FindCheckBoxItem(string szDec,bool bAll,bool bCase,bool isDown)
        {
            if (String.IsNullOrEmpty(szDec)) return;
            
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase; //默认不区分大小写
            if (bCase) stringComparison = StringComparison.CurrentCulture;

            int index = -1;
            if (bAll)
            {
                index = _KeyWordCheckedListBox.Items.IndexOf(szDec);
                if (index < 0)
                {
                    MessageBox.Show("未找到对应的项");
                    return;
                }

                _KeyWordCheckedListBox.SelectedIndex = index;
            }
            else
            {
                int iCurrenSelectIndex = _KeyWordCheckedListBox.SelectedIndex;
                if (iCurrenSelectIndex < 0) iCurrenSelectIndex = 0;

                if (isDown)
                {//下一个
                    for (int i = iCurrenSelectIndex; i <_KeyWordCheckedListBox.Items.Count -1; i++)
                    {
                       if( _KeyWordCheckedListBox.Items[i+1].ToString().IndexOf(szDec,stringComparison) >= 0)
                       {
                            _KeyWordCheckedListBox.SelectedIndex = ++i;
                            break;
                       }
                    }
                }
                else
                {//上一个
                    for (int i = iCurrenSelectIndex; i > 0; i--)
                    {
                        if (_KeyWordCheckedListBox.Items[i - 1].ToString().IndexOf(szDec, stringComparison) >= 0)
                        {
                            _KeyWordCheckedListBox.SelectedIndex = --i;
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region toolStrip工具栏事件相应

        /// <summary>
        /// 显示指定页数的ITEM
        /// </summary>
        /// <param name="ipage"></param>
        private void ShowDesignatePageItem(int ipage)
        {
            _KeyWordCheckedListBox.Items.Clear();

            ipage--;

            Dictionary<string, ulong>.KeyCollection keyColl = CurrentWordLib.Keys;
            long i = 0;
            long iBeginIndex = ipage * _pageItemCount;
            int icout = 0;
            foreach (string str in keyColl)
            {
                if (i < iBeginIndex)
                {
                    i++;
                    continue;
                }
                this._KeyWordCheckedListBox.Items.Add(str);
                icout++;
                if (icout >= _pageItemCount)
                {
                    break;
                }
            }

        }

        /// <summary>
        /// 查询定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip_Qurey_Click(object sender, EventArgs e)
        {
            KeywordListView_KeyDown(null,new KeyEventArgs(Keys.Control | Keys.F));
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip_UpPage_Click(object sender, EventArgs e)
        {
            if (_currentPageIndex <= 1) return;

            //if ( > 0)

            --_currentPageIndex;
            toolStripComboBox_GotoPage.Text = _currentPageIndex.ToString();
            toolStripComboBox_GotoPage_SelectedIndexChanged(sender, EventArgs.Empty);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip_DownPage_Click(object sender, EventArgs e)
        {
            if (_currentPageIndex >= _totalPageIndex) return;

            ++_currentPageIndex;
            toolStripComboBox_GotoPage.Text = _currentPageIndex.ToString();
            toolStripComboBox_GotoPage_SelectedIndexChanged(sender, EventArgs.Empty);
        }

        /// <summary>
        /// 跳转到指定的页数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox_GotoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iGotoPage = 0;
            int.TryParse(toolStripComboBox_GotoPage.Text,out iGotoPage);
            if (iGotoPage <= 0 || iGotoPage > _totalPageIndex) return;

            ShowDesignatePageItem(iGotoPage);
            _currentPageIndex = iGotoPage;

            _KeyWordCheckedListBox.Focus();
        }

        /// <summary>
        /// 跳转到指定页数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox_GotoPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                toolStripComboBox_GotoPage_SelectedIndexChanged(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 增加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip_AddItem_Click(object sender, EventArgs e)
        {
            if (FormType == KeywordFormType.ExistWord) return;
            AddItem();

        }

        #endregion       
    }
}

