namespace NKnife.Kits.SocketKnife.StressTest
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServiceSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.protocolViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(684, 579);
            this.MainPanel.TabIndex = 4;
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(684, 25);
            this.MainMenuStrip.TabIndex = 3;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServiceSettingToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // ServiceSettingToolStripMenuItem
            // 
            this.ServiceSettingToolStripMenuItem.Name = "ServiceSettingToolStripMenuItem";
            this.ServiceSettingToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ServiceSettingToolStripMenuItem.Text = "服务设置";
            this.ServiceSettingToolStripMenuItem.Click += new System.EventHandler(this.ServiceSettingToolStripMenuItemClick);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logViewToolStripMenuItem,
            this.serverViewToolStripMenuItem,
            this.clientViewToolStripMenuItem,
            this.protocolViewToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.ViewToolStripMenuItem.Text = "视图";
            // 
            // logViewToolStripMenuItem
            // 
            this.logViewToolStripMenuItem.Name = "logViewToolStripMenuItem";
            this.logViewToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.logViewToolStripMenuItem.Text = "日志视图";
            this.logViewToolStripMenuItem.Click += new System.EventHandler(this.LogViewToolStripMenuItemClick);
            // 
            // serverViewToolStripMenuItem
            // 
            this.serverViewToolStripMenuItem.Name = "serverViewToolStripMenuItem";
            this.serverViewToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.serverViewToolStripMenuItem.Text = "服务端视图";
            this.serverViewToolStripMenuItem.Click += new System.EventHandler(this.ServerViewToolStripMenuItemClick);
            // 
            // clientViewToolStripMenuItem
            // 
            this.clientViewToolStripMenuItem.Name = "clientViewToolStripMenuItem";
            this.clientViewToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.clientViewToolStripMenuItem.Text = "客户端视图";
            this.clientViewToolStripMenuItem.Click += new System.EventHandler(this.ClientViewToolStripMenuItemClick);
            // 
            // protocolViewToolStripMenuItem
            // 
            this.protocolViewToolStripMenuItem.Name = "protocolViewToolStripMenuItem";
            this.protocolViewToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.protocolViewToolStripMenuItem.Text = "协议编辑视图";
            this.protocolViewToolStripMenuItem.Click += new System.EventHandler(this.ProtocolViewToolStripMenuItemClick);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.HelpToolStripMenuItem.Text = "帮助";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AboutToolStripMenuItem.Text = "关于";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 604);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MainMenuStrip);
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SocketKnife压力测试";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ServiceSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem protocolViewToolStripMenuItem;
    }
}

