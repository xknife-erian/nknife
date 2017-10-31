namespace NKnife.Kits.StarterKit
{
    partial class LibraryDemoWorkbench
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraryDemoWorkbench));
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ControlTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ImagesPanelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._CustomStripControlTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._FunctionTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ChineseCharUseFrequencyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._ThreadTimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this._ControlTestToolStripMenuItem,
            this._FunctionTestMenuItem,
            this._HelpMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(1008, 25);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ExitToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // _ExitToolStripMenuItem
            // 
            this._ExitToolStripMenuItem.Name = "_ExitToolStripMenuItem";
            this._ExitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._ExitToolStripMenuItem.Text = "退出(&X)";
            // 
            // _ControlTestToolStripMenuItem
            // 
            this._ControlTestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ImagesPanelMenuItem,
            this.toolStripSeparator3,
            this._CustomStripControlTestMenuItem});
            this._ControlTestToolStripMenuItem.Name = "_ControlTestToolStripMenuItem";
            this._ControlTestToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this._ControlTestToolStripMenuItem.Text = "控件测试(&E)";
            // 
            // _ImagesPanelMenuItem
            // 
            this._ImagesPanelMenuItem.Name = "_ImagesPanelMenuItem";
            this._ImagesPanelMenuItem.Size = new System.Drawing.Size(172, 22);
            this._ImagesPanelMenuItem.Text = "图片容器控件";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
            // 
            // _CustomStripControlTestMenuItem
            // 
            this._CustomStripControlTestMenuItem.Name = "_CustomStripControlTestMenuItem";
            this._CustomStripControlTestMenuItem.Size = new System.Drawing.Size(172, 22);
            this._CustomStripControlTestMenuItem.Text = "自定义工具栏控件";
            // 
            // _FunctionTestMenuItem
            // 
            this._FunctionTestMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ChineseCharUseFrequencyMenuItem,
            this._ThreadTimerToolStripMenuItem});
            this._FunctionTestMenuItem.Name = "_FunctionTestMenuItem";
            this._FunctionTestMenuItem.Size = new System.Drawing.Size(83, 21);
            this._FunctionTestMenuItem.Text = "功能测试(&T)";
            // 
            // _ChineseCharUseFrequencyMenuItem
            // 
            this._ChineseCharUseFrequencyMenuItem.Name = "_ChineseCharUseFrequencyMenuItem";
            this._ChineseCharUseFrequencyMenuItem.Size = new System.Drawing.Size(177, 22);
            this._ChineseCharUseFrequencyMenuItem.Text = "汉字使用频率计算";
            // 
            // _HelpMenuItem
            // 
            this._HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AboutMenuItem});
            this._HelpMenuItem.Name = "_HelpMenuItem";
            this._HelpMenuItem.Size = new System.Drawing.Size(61, 21);
            this._HelpMenuItem.Text = "帮助(&H)";
            // 
            // _AboutMenuItem
            // 
            this._AboutMenuItem.Name = "_AboutMenuItem";
            this._AboutMenuItem.Size = new System.Drawing.Size(152, 22);
            this._AboutMenuItem.Text = "关于(&A)...";
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Location = new System.Drawing.Point(0, 707);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(1008, 22);
            this._StatusStrip.TabIndex = 0;
            // 
            // _ThreadTimerToolStripMenuItem
            // 
            this._ThreadTimerToolStripMenuItem.Name = "_ThreadTimerToolStripMenuItem";
            this._ThreadTimerToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this._ThreadTimerToolStripMenuItem.Text = "Thread.Timer测试";
            this._ThreadTimerToolStripMenuItem.Click += new System.EventHandler(this._ThreadTimerToolStripMenuItem_Click);
            // 
            // LibraryDemoWorkbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._MenuStrip);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this._MenuStrip;
            this.Name = "LibraryDemoWorkbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NKnife StarterKit";
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ControlTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ImagesPanelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _FunctionTestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ChineseCharUseFrequencyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AboutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem _CustomStripControlTestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ThreadTimerToolStripMenuItem;
    }
}

