using System.Windows.Forms;

namespace NKnife.ChannelKnife.Views
{
    partial class Workbench
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workbench));
            this._StripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._SpringStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._VersionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this._FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._NewPortMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._SaveDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._OptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._LoggerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._StripContainer.BottomToolStripPanel.SuspendLayout();
            this._StripContainer.TopToolStripPanel.SuspendLayout();
            this._StripContainer.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this._MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StripContainer
            // 
            // 
            // _StripContainer.BottomToolStripPanel
            // 
            this._StripContainer.BottomToolStripPanel.Controls.Add(this._StatusStrip);
            // 
            // _StripContainer.ContentPanel
            // 
            this._StripContainer.ContentPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this._StripContainer.ContentPanel.Size = new System.Drawing.Size(884, 615);
            this._StripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._StripContainer.Location = new System.Drawing.Point(0, 0);
            this._StripContainer.Name = "_StripContainer";
            this._StripContainer.Size = new System.Drawing.Size(884, 662);
            this._StripContainer.TabIndex = 0;
            this._StripContainer.Text = "toolStripContainer1";
            // 
            // _StripContainer.TopToolStripPanel
            // 
            this._StripContainer.TopToolStripPanel.Controls.Add(this._MenuStrip);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._SpringStatusLabel,
            this._VersionStatusLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 0);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(884, 22);
            this._StatusStrip.TabIndex = 0;
            // 
            // _SpringStatusLabel
            // 
            this._SpringStatusLabel.Name = "_SpringStatusLabel";
            this._SpringStatusLabel.Size = new System.Drawing.Size(793, 17);
            this._SpringStatusLabel.Spring = true;
            // 
            // _VersionStatusLabel
            // 
            this._VersionStatusLabel.Name = "_VersionStatusLabel";
            this._VersionStatusLabel.Size = new System.Drawing.Size(45, 17);
            this._VersionStatusLabel.Text = "0.0.0.0";
            this._VersionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FileToolStripMenuItem,
            this._ToolMenuItem,
            this._HelpMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(884, 25);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "menuStrip1";
            // 
            // _FileToolStripMenuItem
            // 
            this._FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewPortMenuItem,
            this.toolStripSeparator,
            this._SaveDataMenuItem,
            this.toolStripSeparator2,
            this._ExitMenuItem});
            this._FileToolStripMenuItem.Name = "_FileToolStripMenuItem";
            this._FileToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this._FileToolStripMenuItem.Text = "文件(&F)";
            // 
            // _NewPortMenuItem
            // 
            this._NewPortMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_NewPortMenuItem.Image")));
            this._NewPortMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewPortMenuItem.Name = "_NewPortMenuItem";
            this._NewPortMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this._NewPortMenuItem.Size = new System.Drawing.Size(189, 22);
            this._NewPortMenuItem.Text = "新建端口(&N)";
            this._NewPortMenuItem.Click += new System.EventHandler(this._NewPortToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(186, 6);
            // 
            // _SaveDataMenuItem
            // 
            this._SaveDataMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_SaveDataMenuItem.Image")));
            this._SaveDataMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SaveDataMenuItem.Name = "_SaveDataMenuItem";
            this._SaveDataMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._SaveDataMenuItem.Size = new System.Drawing.Size(189, 22);
            this._SaveDataMenuItem.Text = "保存数据(&S)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // _ExitMenuItem
            // 
            this._ExitMenuItem.Name = "_ExitMenuItem";
            this._ExitMenuItem.Size = new System.Drawing.Size(189, 22);
            this._ExitMenuItem.Text = "退出(&X)";
            // 
            // _ToolMenuItem
            // 
            this._ToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OptionMenuItem});
            this._ToolMenuItem.Enabled = false;
            this._ToolMenuItem.Name = "_ToolMenuItem";
            this._ToolMenuItem.Size = new System.Drawing.Size(59, 21);
            this._ToolMenuItem.Text = "工具(&T)";
            // 
            // _OptionMenuItem
            // 
            this._OptionMenuItem.Name = "_OptionMenuItem";
            this._OptionMenuItem.Size = new System.Drawing.Size(118, 22);
            this._OptionMenuItem.Text = "选项(&O)";
            // 
            // _HelpMenuItem
            // 
            this._HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._LoggerMenuItem,
            this._AboutMenuItem});
            this._HelpMenuItem.Name = "_HelpMenuItem";
            this._HelpMenuItem.Size = new System.Drawing.Size(61, 21);
            this._HelpMenuItem.Text = "帮助(&H)";
            // 
            // _LoggerMenuItem
            // 
            this._LoggerMenuItem.Name = "_LoggerMenuItem";
            this._LoggerMenuItem.Size = new System.Drawing.Size(138, 22);
            this._LoggerMenuItem.Text = "程序日志(&L)";
            // 
            // _AboutMenuItem
            // 
            this._AboutMenuItem.Name = "_AboutMenuItem";
            this._AboutMenuItem.Size = new System.Drawing.Size(138, 22);
            this._AboutMenuItem.Text = "关于(&A)...";
            this._AboutMenuItem.Click += new System.EventHandler(this._AboutMenuItem_Click);
            // 
            // Workbench
            // 
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this._StripContainer);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(20, 20);
            this.MainMenuStrip = this._MenuStrip;
            this.Name = "Workbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChannelKnife 2019";
            this._StripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._StripContainer.BottomToolStripPanel.PerformLayout();
            this._StripContainer.TopToolStripPanel.ResumeLayout(false);
            this._StripContainer.TopToolStripPanel.PerformLayout();
            this._StripContainer.ResumeLayout(false);
            this._StripContainer.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        private ToolStripContainer _StripContainer;
        private StatusStrip _StatusStrip;
        private MenuStrip _MenuStrip;
        private ToolStripMenuItem _FileToolStripMenuItem;
        private ToolStripMenuItem _NewPortMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem _SaveDataMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem _ExitMenuItem;
        private ToolStripMenuItem _ToolMenuItem;
        private ToolStripMenuItem _OptionMenuItem;
        private ToolStripMenuItem _HelpMenuItem;
        private ToolStripMenuItem _AboutMenuItem;


        #endregion
        private ToolStripStatusLabel _SpringStatusLabel;
        private ToolStripStatusLabel _VersionStatusLabel;
        private ToolStripMenuItem _LoggerMenuItem;
    }
}

