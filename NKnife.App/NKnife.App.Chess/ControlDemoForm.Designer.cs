namespace Gean.Gui.ChessControl.Demo
{
    partial class DemoMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._mainSpliter = new System.Windows.Forms.SplitContainer();
            this._splitContainer3 = new System.Windows.Forms.SplitContainer();
            this._fenTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._recordTree = new System.Windows.Forms.TreeView();
            this._rightSplit = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._stepsTabPage = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._recordListView = new Gean.Gui.ChessControl.RecordListView();
            this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this._openingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pgnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pgnConventToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._stripContainer = new System.Windows.Forms.ToolStripContainer();
            this._mainStatus = new System.Windows.Forms.StatusStrip();
            this._label = new System.Windows.Forms.ToolStripStatusLabel();
            this._lableFenstring = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this._mainSpliter.Panel1.SuspendLayout();
            this._mainSpliter.Panel2.SuspendLayout();
            this._mainSpliter.SuspendLayout();
            this._splitContainer3.Panel2.SuspendLayout();
            this._splitContainer3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._rightSplit.Panel1.SuspendLayout();
            this._rightSplit.Panel2.SuspendLayout();
            this._rightSplit.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this._mainMenuStrip.SuspendLayout();
            this._stripContainer.ContentPanel.SuspendLayout();
            this._stripContainer.SuspendLayout();
            this._mainStatus.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainSpliter
            // 
            this._mainSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainSpliter.Location = new System.Drawing.Point(0, 0);
            this._mainSpliter.Name = "_mainSpliter";
            // 
            // _mainSpliter.Panel1
            // 
            this._mainSpliter.Panel1.Controls.Add(this._splitContainer3);
            // 
            // _mainSpliter.Panel2
            // 
            this._mainSpliter.Panel2.Controls.Add(this.splitContainer1);
            this._mainSpliter.Size = new System.Drawing.Size(632, 373);
            this._mainSpliter.SplitterDistance = 382;
            this._mainSpliter.SplitterWidth = 3;
            this._mainSpliter.TabIndex = 0;
            // 
            // _splitContainer3
            // 
            this._splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer3.Location = new System.Drawing.Point(0, 0);
            this._splitContainer3.Name = "_splitContainer3";
            this._splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer3.Panel2
            // 
            this._splitContainer3.Panel2.Controls.Add(this._fenTextBox);
            this._splitContainer3.Size = new System.Drawing.Size(382, 373);
            this._splitContainer3.SplitterDistance = 302;
            this._splitContainer3.TabIndex = 0;
            // 
            // _fenTextBox
            // 
            this._fenTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fenTextBox.Font = new System.Drawing.Font("Consolas", 8F);
            this._fenTextBox.Location = new System.Drawing.Point(0, 0);
            this._fenTextBox.Multiline = true;
            this._fenTextBox.Name = "_fenTextBox";
            this._fenTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._fenTextBox.Size = new System.Drawing.Size(382, 67);
            this._fenTextBox.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._recordTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._rightSplit);
            this.splitContainer1.Size = new System.Drawing.Size(247, 373);
            this.splitContainer1.SplitterDistance = 58;
            this.splitContainer1.TabIndex = 1;
            // 
            // _recordTree
            // 
            this._recordTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._recordTree.Location = new System.Drawing.Point(0, 0);
            this._recordTree.Name = "_recordTree";
            this._recordTree.Size = new System.Drawing.Size(58, 373);
            this._recordTree.TabIndex = 0;
            // 
            // _rightSplit
            // 
            this._rightSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rightSplit.Location = new System.Drawing.Point(0, 0);
            this._rightSplit.Name = "_rightSplit";
            this._rightSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _rightSplit.Panel1
            // 
            this._rightSplit.Panel1.Controls.Add(this.tabControl1);
            // 
            // _rightSplit.Panel2
            // 
            this._rightSplit.Panel2.Controls.Add(this._recordListView);
            this._rightSplit.Size = new System.Drawing.Size(185, 373);
            this._rightSplit.SplitterDistance = 200;
            this._rightSplit.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this._stepsTabPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(185, 200);
            this.tabControl1.TabIndex = 0;
            // 
            // _stepsTabPage
            // 
            this._stepsTabPage.Location = new System.Drawing.Point(4, 22);
            this._stepsTabPage.Name = "_stepsTabPage";
            this._stepsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._stepsTabPage.Size = new System.Drawing.Size(177, 174);
            this._stepsTabPage.TabIndex = 0;
            this._stepsTabPage.Text = "Record";
            this._stepsTabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(177, 175);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Options";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _recordListView
            // 
            this._recordListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._recordListView.FullRowSelect = true;
            this._recordListView.GridLines = true;
            this._recordListView.Location = new System.Drawing.Point(0, 0);
            this._recordListView.Name = "_recordListView";
            this._recordListView.Number = 1;
            this._recordListView.Size = new System.Drawing.Size(185, 169);
            this._recordListView.TabIndex = 0;
            this._recordListView.UseCompatibleStateImageBehavior = false;
            this._recordListView.View = System.Windows.Forms.View.Details;
            // 
            // _mainMenuStrip
            // 
            this._mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openingsToolStripMenuItem,
            this._pgnToolStripMenuItem});
            this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._mainMenuStrip.Name = "_mainMenuStrip";
            this._mainMenuStrip.Size = new System.Drawing.Size(632, 24);
            this._mainMenuStrip.TabIndex = 1;
            this._mainMenuStrip.Text = "menuStrip";
            // 
            // _openingsToolStripMenuItem
            // 
            this._openingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newGameToolStripMenuItem});
            this._openingsToolStripMenuItem.Name = "_openingsToolStripMenuItem";
            this._openingsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this._openingsToolStripMenuItem.Text = "新开局";
            // 
            // _newGameToolStripMenuItem
            // 
            this._newGameToolStripMenuItem.Name = "_newGameToolStripMenuItem";
            this._newGameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this._newGameToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this._newGameToolStripMenuItem.Text = "新开棋局";
            this._newGameToolStripMenuItem.Click += new System.EventHandler(this.NewGame);
            // 
            // _pgnToolStripMenuItem
            // 
            this._pgnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._pgnConventToolStripMenuItem1});
            this._pgnToolStripMenuItem.Name = "_pgnToolStripMenuItem";
            this._pgnToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this._pgnToolStripMenuItem.Text = "已有棋谱";
            // 
            // _pgnConventToolStripMenuItem1
            // 
            this._pgnConventToolStripMenuItem1.Name = "_pgnConventToolStripMenuItem1";
            this._pgnConventToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this._pgnConventToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this._pgnConventToolStripMenuItem1.Text = "转换棋谱";
            this._pgnConventToolStripMenuItem1.Click += new System.EventHandler(this.PGNConvent);
            // 
            // _stripContainer
            // 
            this._stripContainer.BottomToolStripPanelVisible = false;
            // 
            // _stripContainer.ContentPanel
            // 
            this._stripContainer.ContentPanel.AutoScroll = true;
            this._stripContainer.ContentPanel.Controls.Add(this._mainSpliter);
            this._stripContainer.ContentPanel.Size = new System.Drawing.Size(632, 373);
            this._stripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stripContainer.LeftToolStripPanelVisible = false;
            this._stripContainer.Location = new System.Drawing.Point(0, 0);
            this._stripContainer.Name = "_stripContainer";
            this._stripContainer.RightToolStripPanelVisible = false;
            this._stripContainer.Size = new System.Drawing.Size(632, 398);
            this._stripContainer.TabIndex = 3;
            this._stripContainer.Text = "toolStripContainer1";
            // 
            // _mainStatus
            // 
            this._mainStatus.Dock = System.Windows.Forms.DockStyle.None;
            this._mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._label,
            this._lableFenstring});
            this._mainStatus.Location = new System.Drawing.Point(0, 0);
            this._mainStatus.Name = "_mainStatus";
            this._mainStatus.Size = new System.Drawing.Size(632, 22);
            this._mainStatus.TabIndex = 4;
            this._mainStatus.Text = "statusStrip2";
            // 
            // _label
            // 
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(68, 17);
            this._label.Text = "_statusLabel";
            // 
            // _lableFenstring
            // 
            this._lableFenstring.Name = "_lableFenstring";
            this._lableFenstring.Size = new System.Drawing.Size(54, 17);
            this._lableFenstring.Text = "FENString";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this._mainStatus);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this._stripContainer);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(632, 398);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(632, 445);
            this.toolStripContainer1.TabIndex = 5;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // DemoMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MainMenuStrip = this._mainMenuStrip;
            this.Name = "DemoMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DemoMainForm";
            this._mainSpliter.Panel1.ResumeLayout(false);
            this._mainSpliter.Panel2.ResumeLayout(false);
            this._mainSpliter.ResumeLayout(false);
            this._splitContainer3.Panel2.ResumeLayout(false);
            this._splitContainer3.Panel2.PerformLayout();
            this._splitContainer3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._rightSplit.Panel1.ResumeLayout(false);
            this._rightSplit.Panel2.ResumeLayout(false);
            this._rightSplit.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this._mainMenuStrip.ResumeLayout(false);
            this._mainMenuStrip.PerformLayout();
            this._stripContainer.ContentPanel.ResumeLayout(false);
            this._stripContainer.ResumeLayout(false);
            this._stripContainer.PerformLayout();
            this._mainStatus.ResumeLayout(false);
            this._mainStatus.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gean.Gui.ChessControl.RecordListView _recordListView;
        private System.Windows.Forms.SplitContainer _mainSpliter;
        private System.Windows.Forms.MenuStrip _mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _openingsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer _rightSplit;
        private System.Windows.Forms.TreeView _recordTree;
        private System.Windows.Forms.ToolStripMenuItem _pgnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _pgnConventToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _newGameToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _stepsTabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripContainer _stripContainer;
        private System.Windows.Forms.StatusStrip _mainStatus;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripStatusLabel _label;
        private System.Windows.Forms.ToolStripStatusLabel _lableFenstring;
        private System.Windows.Forms.SplitContainer _splitContainer3;
        private System.Windows.Forms.TextBox _fenTextBox;
    }
}

