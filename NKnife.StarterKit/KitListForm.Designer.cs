namespace NKnife.StarterKit
{
    partial class KitListForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._MultiLanguageLoPanleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._LogPanelTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iMEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._IMEPopwinToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.简易拼音输入法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.汉字使用频率ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingToolStripMenuItem,
            this.iMEToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._MultiLanguageLoPanleMenuItem,
            this._LogPanelTestMenuItem});
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.loggingToolStripMenuItem.Text = "Logging";
            // 
            // _MultiLanguageLoPanleMenuItem
            // 
            this._MultiLanguageLoPanleMenuItem.CheckOnClick = true;
            this._MultiLanguageLoPanleMenuItem.Name = "_MultiLanguageLoPanleMenuItem";
            this._MultiLanguageLoPanleMenuItem.Size = new System.Drawing.Size(201, 22);
            this._MultiLanguageLoPanleMenuItem.Text = "中文LogPanel";
            // 
            // _LogPanelTestMenuItem
            // 
            this._LogPanelTestMenuItem.Name = "_LogPanelTestMenuItem";
            this._LogPanelTestMenuItem.Size = new System.Drawing.Size(201, 22);
            this._LogPanelTestMenuItem.Text = "打开LogPanel承载窗体";
            this._LogPanelTestMenuItem.Click += new System.EventHandler(this._LogPanelTestMenuItem_Click);
            // 
            // iMEToolStripMenuItem
            // 
            this.iMEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.汉字使用频率ToolStripMenuItem,
            this.toolStripSeparator1,
            this._IMEPopwinToolItem,
            this.简易拼音输入法ToolStripMenuItem});
            this.iMEToolStripMenuItem.Name = "iMEToolStripMenuItem";
            this.iMEToolStripMenuItem.Size = new System.Drawing.Size(43, 21);
            this.iMEToolStripMenuItem.Text = "IME";
            // 
            // _IMEPopwinToolItem
            // 
            this._IMEPopwinToolItem.Name = "_IMEPopwinToolItem";
            this._IMEPopwinToolItem.Size = new System.Drawing.Size(160, 22);
            this._IMEPopwinToolItem.Text = "弹出输入法窗体";
            this._IMEPopwinToolItem.Click += new System.EventHandler(this._IMEPopwinToolItem_Click);
            // 
            // 简易拼音输入法ToolStripMenuItem
            // 
            this.简易拼音输入法ToolStripMenuItem.Name = "简易拼音输入法ToolStripMenuItem";
            this.简易拼音输入法ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.简易拼音输入法ToolStripMenuItem.Text = "简易拼音输入法";
            this.简易拼音输入法ToolStripMenuItem.Click += new System.EventHandler(this.简易拼音输入法ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // 汉字使用频率ToolStripMenuItem
            // 
            this.汉字使用频率ToolStripMenuItem.Name = "汉字使用频率ToolStripMenuItem";
            this.汉字使用频率ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.汉字使用频率ToolStripMenuItem.Text = "汉字使用频率";
            this.汉字使用频率ToolStripMenuItem.Click += new System.EventHandler(this.汉字使用频率ToolStripMenuItem_Click);
            // 
            // KitListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "KitListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NKnife.StarterKit";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _LogPanelTestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _MultiLanguageLoPanleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iMEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _IMEPopwinToolItem;
        private System.Windows.Forms.ToolStripMenuItem 简易拼音输入法ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 汉字使用频率ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

