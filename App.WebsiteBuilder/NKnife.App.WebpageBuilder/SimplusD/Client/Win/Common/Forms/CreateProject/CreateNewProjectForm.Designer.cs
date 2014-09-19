using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class CreateNewProjectForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("SimplusD已安装的模板", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("我的模板", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewProjectForm));
            this.browserBtn = new System.Windows.Forms.Button();
            this.lblSitePath = new System.Windows.Forms.Label();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.pathTBox = new System.Windows.Forms.ComboBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.singleLine1 = new Jeelu.Win.SingleLine();
            this.selectListView = new System.Windows.Forms.ListView();
            this.isStartupSiteWiz = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.smallIconToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.largeIconToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lblSiteSubType = new System.Windows.Forms.Label();
            this.txtSiteTypeHelpTip = new System.Windows.Forms.TextBox();
            this.siteTypeTreeView = new System.Windows.Forms.TreeView();
            this.lblSiteType = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.txtProjectName = new Jeelu.Win.ValidateTextBox();
            this.toolStrip1.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // browserBtn
            // 
            this.browserBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browserBtn.Location = new System.Drawing.Point(560, 28);
            this.browserBtn.Name = "browserBtn";
            this.browserBtn.Size = new System.Drawing.Size(78, 21);
            this.browserBtn.TabIndex = 2;
            this.browserBtn.Text = "(B)";
            this.browserBtn.UseVisualStyleBackColor = true;
            this.browserBtn.Click += new System.EventHandler(this.browserBtn_Click);
            // 
            // lblSitePath
            // 
            this.lblSitePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSitePath.AutoSize = true;
            this.lblSitePath.Location = new System.Drawing.Point(3, 31);
            this.lblSitePath.Name = "lblSitePath";
            this.lblSitePath.Size = new System.Drawing.Size(57, 13);
            this.lblSitePath.TabIndex = 5;
            this.lblSitePath.Text = "lblSitePath";
            // 
            // lblSiteName
            // 
            this.lblSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(3, 7);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(62, 13);
            this.lblSiteName.TabIndex = 6;
            this.lblSiteName.Text = "lblSiteName";
            // 
            // pathTBox
            // 
            this.pathTBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTBox.FormattingEnabled = true;
            this.pathTBox.Location = new System.Drawing.Point(71, 28);
            this.pathTBox.Name = "pathTBox";
            this.pathTBox.Size = new System.Drawing.Size(483, 21);
            this.pathTBox.TabIndex = 1;
            this.pathTBox.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(558, 79);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(80, 24);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "cancelBtn";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.Enabled = false;
            this.okBtn.Location = new System.Drawing.Point(473, 79);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(80, 24);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "okBtn";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // singleLine1
            // 
            this.singleLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.singleLine1.Location = new System.Drawing.Point(0, 72);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Size = new System.Drawing.Size(641, 1);
            this.singleLine1.TabIndex = 13;
            this.singleLine1.TabStop = false;
            // 
            // selectListView
            // 
            this.selectListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            listViewGroup1.Header = "SimplusD已安装的模板";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "我的模板";
            listViewGroup2.Name = "listViewGroup2";
            this.selectListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.selectListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.selectListView.HideSelection = false;
            this.selectListView.Location = new System.Drawing.Point(164, 35);
            this.selectListView.MultiSelect = false;
            this.selectListView.Name = "selectListView";
            this.selectListView.Size = new System.Drawing.Size(488, 224);
            this.selectListView.TabIndex = 4;
            this.selectListView.UseCompatibleStateImageBehavior = false;
            this.selectListView.View = System.Windows.Forms.View.SmallIcon;
            this.selectListView.SelectedIndexChanged += new System.EventHandler(this.selectListView_SelectedIndexChanged);
            // 
            // isStartupSiteWiz
            // 
            this.isStartupSiteWiz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isStartupSiteWiz.AutoSize = true;
            this.isStartupSiteWiz.Location = new System.Drawing.Point(6, 52);
            this.isStartupSiteWiz.Name = "isStartupSiteWiz";
            this.isStartupSiteWiz.Size = new System.Drawing.Size(104, 17);
            this.isStartupSiteWiz.TabIndex = 2;
            this.isStartupSiteWiz.Text = "isStartupSiteWiz";
            this.isStartupSiteWiz.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallIconToolStripButton,
            this.largeIconToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(10, 10);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(641, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // smallIconToolStripButton
            // 
            this.smallIconToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.smallIconToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("smallIconToolStripButton.Image")));
            this.smallIconToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.smallIconToolStripButton.Name = "smallIconToolStripButton";
            this.smallIconToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.smallIconToolStripButton.Text = "toolStripButton1";
            this.smallIconToolStripButton.Click += new System.EventHandler(this.smallIconToolStripButton_Click);
            // 
            // largeIconToolStripButton
            // 
            this.largeIconToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.largeIconToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("largeIconToolStripButton.Image")));
            this.largeIconToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.largeIconToolStripButton.Name = "largeIconToolStripButton";
            this.largeIconToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.largeIconToolStripButton.Text = "toolStripButton2";
            this.largeIconToolStripButton.Click += new System.EventHandler(this.largeIconToolStripButton_Click);
            // 
            // lblSiteSubType
            // 
            this.lblSiteSubType.AutoSize = true;
            this.lblSiteSubType.Location = new System.Drawing.Point(161, 15);
            this.lblSiteSubType.Name = "lblSiteSubType";
            this.lblSiteSubType.Size = new System.Drawing.Size(77, 13);
            this.lblSiteSubType.TabIndex = 2;
            this.lblSiteSubType.Text = "lblSiteSubType";
            // 
            // txtSiteTypeHelpTip
            // 
            this.txtSiteTypeHelpTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteTypeHelpTip.Location = new System.Drawing.Point(10, 265);
            this.txtSiteTypeHelpTip.Name = "txtSiteTypeHelpTip";
            this.txtSiteTypeHelpTip.ReadOnly = true;
            this.txtSiteTypeHelpTip.Size = new System.Drawing.Size(641, 21);
            this.txtSiteTypeHelpTip.TabIndex = 5;
            // 
            // siteTypeTreeView
            // 
            this.siteTypeTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.siteTypeTreeView.HideSelection = false;
            this.siteTypeTreeView.Location = new System.Drawing.Point(10, 35);
            this.siteTypeTreeView.Name = "siteTypeTreeView";
            this.siteTypeTreeView.Size = new System.Drawing.Size(148, 224);
            this.siteTypeTreeView.TabIndex = 3;
            // 
            // lblSiteType
            // 
            this.lblSiteType.AutoSize = true;
            this.lblSiteType.Location = new System.Drawing.Point(13, 15);
            this.lblSiteType.Name = "lblSiteType";
            this.lblSiteType.Size = new System.Drawing.Size(59, 13);
            this.lblSiteType.TabIndex = 1;
            this.lblSiteType.Text = "lblSiteType";
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.txtProjectName);
            this.bottomPanel.Controls.Add(this.lblSitePath);
            this.bottomPanel.Controls.Add(this.okBtn);
            this.bottomPanel.Controls.Add(this.cancelBtn);
            this.bottomPanel.Controls.Add(this.pathTBox);
            this.bottomPanel.Controls.Add(this.lblSiteName);
            this.bottomPanel.Controls.Add(this.browserBtn);
            this.bottomPanel.Controls.Add(this.isStartupSiteWiz);
            this.bottomPanel.Controls.Add(this.singleLine1);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(10, 292);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(641, 120);
            this.bottomPanel.TabIndex = 0;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(71, 4);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.RegexText = "";
            this.txtProjectName.RegexTextRuntime = "^[_a-zA-Z0-9]*$";
            this.txtProjectName.Size = new System.Drawing.Size(482, 21);
            this.txtProjectName.TabIndex = 0;
            this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
            // 
            // CreateNewProjectForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(661, 412);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.selectListView);
            this.Controls.Add(this.lblSiteType);
            this.Controls.Add(this.siteTypeTreeView);
            this.Controls.Add(this.txtSiteTypeHelpTip);
            this.Controls.Add(this.lblSiteSubType);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CreateNewProjectForm";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.Text = "Create New Site Project";
            this.Load += new System.EventHandler(this.CreateNewProjectForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browserBtn;
        private System.Windows.Forms.Label lblSitePath;
        private System.Windows.Forms.Label lblSiteName;
        private System.Windows.Forms.ComboBox pathTBox;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private SingleLine singleLine1;
        private System.Windows.Forms.ListView selectListView;
        private System.Windows.Forms.CheckBox isStartupSiteWiz;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton smallIconToolStripButton;
        private System.Windows.Forms.ToolStripButton largeIconToolStripButton;
        private System.Windows.Forms.Label lblSiteSubType;
        private System.Windows.Forms.TextBox txtSiteTypeHelpTip;
        private System.Windows.Forms.TreeView siteTypeTreeView;
        private System.Windows.Forms.Label lblSiteType;
        private System.Windows.Forms.Panel bottomPanel;
        private ValidateTextBox txtProjectName;

    }
}

