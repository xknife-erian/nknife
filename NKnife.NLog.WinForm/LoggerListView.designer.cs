using NKnife.NLog.WinForm.Util;

namespace NKnife.NLog.WinForm
{
    sealed partial class LoggerListView
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._LevelToolButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._TraceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DebugMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._InfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._WarnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ErrorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._FatalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ClearToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._MaxViewCountTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this._ContentRegxTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this._SourceRegxTextBox = new System.Windows.Forms.ToolStripTextBox();
            this._ListView = new NKnife.NLog.WinForm.Util.NotFlickerListView();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._LevelToolButton,
            this.toolStripSeparator2,
            this._ClearToolButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this._MaxViewCountTextBox,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this._ContentRegxTextBox,
            this.toolStripSeparator4,
            this.toolStripLabel3,
            this._SourceRegxTextBox});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(685, 25);
            this._ToolStrip.TabIndex = 1;
            // 
            // _LevelToolButton
            // 
            this._LevelToolButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._TraceMenuItem,
            this._DebugMenuItem,
            this._InfoMenuItem,
            this._WarnMenuItem,
            this._ErrorMenuItem,
            this._FatalMenuItem});
            this._LevelToolButton.Image = global::NKnife.NLog.WinForm.Properties.Resources.level;
            this._LevelToolButton.Name = "_LevelToolButton";
            this._LevelToolButton.Size = new System.Drawing.Size(61, 22);
            this._LevelToolButton.Text = "等级";
            // 
            // _TraceMenuItem
            // 
            this._TraceMenuItem.Name = "_TraceMenuItem";
            this._TraceMenuItem.Size = new System.Drawing.Size(115, 22);
            this._TraceMenuItem.Text = "Trace";
            this._TraceMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _DebugMenuItem
            // 
            this._DebugMenuItem.Name = "_DebugMenuItem";
            this._DebugMenuItem.Size = new System.Drawing.Size(115, 22);
            this._DebugMenuItem.Text = "Debug";
            this._DebugMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _InfoMenuItem
            // 
            this._InfoMenuItem.Name = "_InfoMenuItem";
            this._InfoMenuItem.Size = new System.Drawing.Size(115, 22);
            this._InfoMenuItem.Text = "Info";
            this._InfoMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _WarnMenuItem
            // 
            this._WarnMenuItem.Name = "_WarnMenuItem";
            this._WarnMenuItem.Size = new System.Drawing.Size(115, 22);
            this._WarnMenuItem.Text = "Warn";
            this._WarnMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _ErrorMenuItem
            // 
            this._ErrorMenuItem.Name = "_ErrorMenuItem";
            this._ErrorMenuItem.Size = new System.Drawing.Size(115, 22);
            this._ErrorMenuItem.Text = "Error";
            this._ErrorMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _FatalMenuItem
            // 
            this._FatalMenuItem.Name = "_FatalMenuItem";
            this._FatalMenuItem.Size = new System.Drawing.Size(115, 22);
            this._FatalMenuItem.Text = "Fatal";
            this._FatalMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _ClearToolButton
            // 
            this._ClearToolButton.Image = global::NKnife.NLog.WinForm.Properties.Resources.clean;
            this._ClearToolButton.Name = "_ClearToolButton";
            this._ClearToolButton.Size = new System.Drawing.Size(23, 22);
            this._ClearToolButton.Click += new System.EventHandler(this.ClearToolButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel1.Text = "数量:";
            // 
            // _MaxViewCountTextBox
            // 
            this._MaxViewCountTextBox.Name = "_MaxViewCountTextBox";
            this._MaxViewCountTextBox.Size = new System.Drawing.Size(35, 25);
            this._MaxViewCountTextBox.Text = "300";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(83, 22);
            this.toolStripLabel2.Text = "内容正则过滤:";
            this.toolStripLabel2.Visible = false;
            // 
            // _ContentRegxTextBox
            // 
            this._ContentRegxTextBox.Name = "_ContentRegxTextBox";
            this._ContentRegxTextBox.Size = new System.Drawing.Size(150, 25);
            this._ContentRegxTextBox.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel3.Text = "日志源过滤:";
            this.toolStripLabel3.Visible = false;
            // 
            // _SourceRegxTextBox
            // 
            this._SourceRegxTextBox.Name = "_SourceRegxTextBox";
            this._SourceRegxTextBox.Size = new System.Drawing.Size(120, 25);
            this._SourceRegxTextBox.Visible = false;
            // 
            // _ListView
            // 
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.HideSelection = false;
            this._ListView.Location = new System.Drawing.Point(0, 25);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.ShowItemToolTips = true;
            this._ListView.Size = new System.Drawing.Size(685, 284);
            this._ListView.TabIndex = 0;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // LoggerListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListView);
            this.Controls.Add(this._ToolStrip);
            this.Name = "LoggerListView";
            this.Size = new System.Drawing.Size(685, 309);
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.ToolStripButton _ClearToolButton;
        private System.Windows.Forms.ToolStripDropDownButton _LevelToolButton;
        private System.Windows.Forms.ToolStripMenuItem _TraceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _DebugMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _InfoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _WarnMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ErrorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _FatalMenuItem;
        private NotFlickerListView _ListView;
        private System.Windows.Forms.ToolStripTextBox _MaxViewCountTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox _ContentRegxTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox _SourceRegxTextBox;
    }
}
