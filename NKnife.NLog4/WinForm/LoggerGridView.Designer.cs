﻿namespace NKnife.NLog.WinForm
{
    sealed partial class LoggerGridView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerGridView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._LevelToolButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._TraceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._DebugMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._InfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._WarnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ErrorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._FatalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ClearToolButton = new System.Windows.Forms.ToolStripButton();
            this._LogGridView = new System.Windows.Forms.DataGridView();
            this._ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LogGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._LevelToolButton,
            this._ClearToolButton});
            resources.ApplyResources(this._ToolStrip, "_ToolStrip");
            this._ToolStrip.Name = "_ToolStrip";
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
            this._LevelToolButton.Name = "_LevelToolButton";
            resources.ApplyResources(this._LevelToolButton, "_LevelToolButton");
            // 
            // _TraceMenuItem
            // 
            this._TraceMenuItem.Name = "_TraceMenuItem";
            resources.ApplyResources(this._TraceMenuItem, "_TraceMenuItem");
            this._TraceMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _DebugMenuItem
            // 
            this._DebugMenuItem.Name = "_DebugMenuItem";
            resources.ApplyResources(this._DebugMenuItem, "_DebugMenuItem");
            this._DebugMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _InfoMenuItem
            // 
            this._InfoMenuItem.Name = "_InfoMenuItem";
            resources.ApplyResources(this._InfoMenuItem, "_InfoMenuItem");
            this._InfoMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _WarnMenuItem
            // 
            this._WarnMenuItem.Name = "_WarnMenuItem";
            resources.ApplyResources(this._WarnMenuItem, "_WarnMenuItem");
            this._WarnMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _ErrorMenuItem
            // 
            this._ErrorMenuItem.Name = "_ErrorMenuItem";
            resources.ApplyResources(this._ErrorMenuItem, "_ErrorMenuItem");
            this._ErrorMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _FatalMenuItem
            // 
            this._FatalMenuItem.Name = "_FatalMenuItem";
            resources.ApplyResources(this._FatalMenuItem, "_FatalMenuItem");
            this._FatalMenuItem.Click += new System.EventHandler(this.SetLevelGroupToolButton_Click);
            // 
            // _ClearToolButton
            // 
            this._ClearToolButton.Name = "_ClearToolButton";
            resources.ApplyResources(this._ClearToolButton, "_ClearToolButton");
            this._ClearToolButton.Click += new System.EventHandler(this.ClearToolButton_Click);
            // 
            // _LogGridView
            // 
            this._LogGridView.AllowUserToAddRows = false;
            this._LogGridView.AllowUserToDeleteRows = false;
            this._LogGridView.AllowUserToResizeRows = false;
            this._LogGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this._LogGridView, "_LogGridView");
            this._LogGridView.MultiSelect = false;
            this._LogGridView.Name = "_LogGridView";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._LogGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._LogGridView.RowTemplate.Height = 23;
            this._LogGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._LogGridView.ShowCellErrors = false;
            this._LogGridView.ShowCellToolTips = false;
            this._LogGridView.ShowEditingIcon = false;
            this._LogGridView.ShowRowErrors = false;
            // 
            // LoggerGridView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._LogGridView);
            this.Controls.Add(this._ToolStrip);
            this.Name = "LoggerGridView";
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LogGridView)).EndInit();
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
        private System.Windows.Forms.DataGridView _LogGridView;
    }
}