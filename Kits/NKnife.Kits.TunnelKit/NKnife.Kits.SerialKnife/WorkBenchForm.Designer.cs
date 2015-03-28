namespace NKnife.Kits.SerialKnife
{
    partial class WorkBenchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkBenchForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模拟串口连接器面板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.数据连接器设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(559, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据连接器设置ToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作面板ToolStripMenuItem,
            this.日志面板ToolStripMenuItem,
            this.模拟串口连接器面板ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 操作面板ToolStripMenuItem
            // 
            this.操作面板ToolStripMenuItem.Name = "操作面板ToolStripMenuItem";
            this.操作面板ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.操作面板ToolStripMenuItem.Text = "操作面板";
            this.操作面板ToolStripMenuItem.Click += new System.EventHandler(this.操作面板ToolStripMenuItem_Click);
            // 
            // 日志面板ToolStripMenuItem
            // 
            this.日志面板ToolStripMenuItem.Name = "日志面板ToolStripMenuItem";
            this.日志面板ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.日志面板ToolStripMenuItem.Text = "日志面板";
            this.日志面板ToolStripMenuItem.Click += new System.EventHandler(this.日志面板ToolStripMenuItem_Click);
            // 
            // 模拟串口连接器面板ToolStripMenuItem
            // 
            this.模拟串口连接器面板ToolStripMenuItem.Name = "模拟串口连接器面板ToolStripMenuItem";
            this.模拟串口连接器面板ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.模拟串口连接器面板ToolStripMenuItem.Text = "模拟串口连接器面板";
            this.模拟串口连接器面板ToolStripMenuItem.Click += new System.EventHandler(this.模拟串口连接器面板ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(559, 449);
            this.MainPanel.TabIndex = 1;
            // 
            // 数据连接器设置ToolStripMenuItem
            // 
            this.数据连接器设置ToolStripMenuItem.Name = "数据连接器设置ToolStripMenuItem";
            this.数据连接器设置ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.数据连接器设置ToolStripMenuItem.Text = "数据连接器设置";
            this.数据连接器设置ToolStripMenuItem.Click += new System.EventHandler(this.数据连接器设置ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // WorkBenchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 474);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WorkBenchForm";
            this.Text = "SerialKnife-Kits";
            this.Load += new System.EventHandler(this.WorkBenchForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日志面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模拟串口连接器面板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据连接器设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

