namespace NKnife.App.PictureTextPicker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateThumbNailBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.MainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.HolderToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OperationToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.MainMenuStrip.SuspendLayout();
            this.MainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(616, 25);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileToolStripMenuItem,
            this.OpenDirectoryToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.FileToolStripMenuItem.Text = "文件(&F)";
            // 
            // OpenFileToolStripMenuItem
            // 
            this.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem";
            this.OpenFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenFileToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.OpenFileToolStripMenuItem.Text = "打开文件(&O)...";
            this.OpenFileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OpenDirectoryToolStripMenuItem
            // 
            this.OpenDirectoryToolStripMenuItem.Name = "OpenDirectoryToolStripMenuItem";
            this.OpenDirectoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.OpenDirectoryToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.OpenDirectoryToolStripMenuItem.Text = "打开目录(&D)...";
            this.OpenDirectoryToolStripMenuItem.Click += new System.EventHandler(this.OpenDirectoryToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.ExitToolStripMenuItem.Text = "退出(&X)";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.EditToolStripMenuItem.Text = "编辑(&E)";
            // 
            // SettingToolStripMenuItem
            // 
            this.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem";
            this.SettingToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.SettingToolStripMenuItem.Text = "选项";
            this.SettingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItemClick);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.HelpToolStripMenuItem.Text = "帮助(H)";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.AboutToolStripMenuItem.Text = "关于(&A)";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // CreateThumbNailBackgroundWorker
            // 
            this.CreateThumbNailBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CreateThumbNailBackgroundWorkerDoWork);
            this.CreateThumbNailBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CreateThumbNailBackgroundWorkerRunWorkerCompleted);
            // 
            // MainToolStripContainer
            // 
            // 
            // MainToolStripContainer.BottomToolStripPanel
            // 
            this.MainToolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // MainToolStripContainer.ContentPanel
            // 
            this.MainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(616, 388);
            this.MainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStripContainer.LeftToolStripPanelVisible = false;
            this.MainToolStripContainer.Location = new System.Drawing.Point(0, 25);
            this.MainToolStripContainer.Name = "MainToolStripContainer";
            this.MainToolStripContainer.RightToolStripPanelVisible = false;
            this.MainToolStripContainer.Size = new System.Drawing.Size(616, 435);
            this.MainToolStripContainer.TabIndex = 1;
            this.MainToolStripContainer.Text = "toolStripContainer1";
            // 
            // MainToolStripContainer.TopToolStripPanel
            // 
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusToolStripStatusLabel,
            this.HolderToolStripStatusLabel,
            this.OperationToolStripProgressBar});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(616, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // StatusToolStripStatusLabel
            // 
            this.StatusToolStripStatusLabel.AutoSize = false;
            this.StatusToolStripStatusLabel.Name = "StatusToolStripStatusLabel";
            this.StatusToolStripStatusLabel.Size = new System.Drawing.Size(80, 17);
            this.StatusToolStripStatusLabel.Text = "就绪";
            this.StatusToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HolderToolStripStatusLabel
            // 
            this.HolderToolStripStatusLabel.Name = "HolderToolStripStatusLabel";
            this.HolderToolStripStatusLabel.Size = new System.Drawing.Size(40, 17);
            this.HolderToolStripStatusLabel.Spring = true;
            this.HolderToolStripStatusLabel.Text = "        ";
            // 
            // OperationToolStripProgressBar
            // 
            this.OperationToolStripProgressBar.Name = "OperationToolStripProgressBar";
            this.OperationToolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "打开";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 460);
            this.Controls.Add(this.MainToolStripContainer);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "MainForm";
            this.Text = "图片文本采集器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.MainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ResumeLayout(false);
            this.MainToolStripContainer.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker CreateThumbNailBackgroundWorker;
        private System.Windows.Forms.ToolStripContainer MainToolStripContainer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusToolStripStatusLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripStatusLabel HolderToolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar OperationToolStripProgressBar;
    }
}

