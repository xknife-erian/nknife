namespace NKnife.Upgrade4Github.App
{
    partial class UpdaterWorkbench
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterWorkbench));
            this._updateInfoTextBox = new System.Windows.Forms.TextBox();
            this._updateProgressBar = new System.Windows.Forms.ProgressBar();
            this._updateButton = new System.Windows.Forms.Button();
            this._refreshButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._infoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._springLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._versionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._closeButton = new System.Windows.Forms.Button();
            this._packageDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._packageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._unPackageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _updateInfoTextBox
            // 
            this._updateInfoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._updateInfoTextBox.Location = new System.Drawing.Point(14, 46);
            this._updateInfoTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._updateInfoTextBox.Multiline = true;
            this._updateInfoTextBox.Name = "_updateInfoTextBox";
            this._updateInfoTextBox.ReadOnly = true;
            this._updateInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._updateInfoTextBox.Size = new System.Drawing.Size(608, 204);
            this._updateInfoTextBox.TabIndex = 5;
            // 
            // _updateProgressBar
            // 
            this._updateProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._updateProgressBar.Location = new System.Drawing.Point(14, 13);
            this._updateProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._updateProgressBar.Name = "_updateProgressBar";
            this._updateProgressBar.Size = new System.Drawing.Size(728, 25);
            this._updateProgressBar.TabIndex = 4;
            // 
            // _updateButton
            // 
            this._updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._updateButton.Enabled = false;
            this._updateButton.Location = new System.Drawing.Point(628, 86);
            this._updateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._updateButton.Name = "_updateButton";
            this._updateButton.Size = new System.Drawing.Size(114, 37);
            this._updateButton.TabIndex = 1;
            this._updateButton.Text = "更新";
            this._updateButton.UseVisualStyleBackColor = true;
            this._updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // _refreshButton
            // 
            this._refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._refreshButton.Location = new System.Drawing.Point(628, 46);
            this._refreshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._refreshButton.Name = "_refreshButton";
            this._refreshButton.Size = new System.Drawing.Size(114, 37);
            this._refreshButton.TabIndex = 0;
            this._refreshButton.Text = "刷新";
            this._refreshButton.UseVisualStyleBackColor = true;
            this._refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._infoLabel,
            this._springLabel,
            this._versionLabel,
            this._packageDropDownButton});
            this.statusStrip1.Location = new System.Drawing.Point(0, 258);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(754, 23);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _infoLabel
            // 
            this._infoLabel.Name = "_infoLabel";
            this._infoLabel.Size = new System.Drawing.Size(28, 18);
            this._infoLabel.Text = "……";
            // 
            // _springLabel
            // 
            this._springLabel.Name = "_springLabel";
            this._springLabel.Size = new System.Drawing.Size(620, 18);
            this._springLabel.Spring = true;
            // 
            // _versionLabel
            // 
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(15, 18);
            this._versionLabel.Text = "0";
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.Enabled = false;
            this._closeButton.Location = new System.Drawing.Point(628, 213);
            this._closeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(114, 37);
            this._closeButton.TabIndex = 2;
            this._closeButton.Text = "关闭";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // _packageDropDownButton
            // 
            this._packageDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._packageDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._unPackageMenuItem,
            this._packageMenuItem});
            this._packageDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("_packageDropDownButton.Image")));
            this._packageDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._packageDropDownButton.Name = "_packageDropDownButton";
            this._packageDropDownButton.Size = new System.Drawing.Size(45, 21);
            this._packageDropDownButton.Text = "助手";
            // 
            // _packageMenuItem
            // 
            this._packageMenuItem.Name = "_packageMenuItem";
            this._packageMenuItem.Size = new System.Drawing.Size(180, 22);
            this._packageMenuItem.Text = "打包";
            this._packageMenuItem.Click += new System.EventHandler(this._packageMenuItem_Click);
            // 
            // _unPackageMenuItem
            // 
            this._unPackageMenuItem.Name = "_unPackageMenuItem";
            this._unPackageMenuItem.Size = new System.Drawing.Size(180, 22);
            this._unPackageMenuItem.Text = "解包";
            this._unPackageMenuItem.Click += new System.EventHandler(this._unPackageMenuItem_Click);
            // 
            // UpdaterWorkbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 281);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._refreshButton);
            this.Controls.Add(this._updateButton);
            this.Controls.Add(this._updateInfoTextBox);
            this.Controls.Add(this._updateProgressBar);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterWorkbench";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件升级工具";
            this.Load += new System.EventHandler(this.Form_OnLoad);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox _updateInfoTextBox;
        private System.Windows.Forms.ProgressBar _updateProgressBar;
        private System.Windows.Forms.Button _updateButton;
        private System.Windows.Forms.Button _refreshButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _infoLabel;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.ToolStripStatusLabel _springLabel;
        private System.Windows.Forms.ToolStripStatusLabel _versionLabel;
        private System.Windows.Forms.ToolStripDropDownButton _packageDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem _unPackageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _packageMenuItem;
    }
}

