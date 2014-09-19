namespace Jeelu.SimplusD.Client.Win
{
    partial class SaveResourceImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveResourceImageForm));
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelListView = new System.Windows.Forms.Panel();
            this.listViewFodel = new Jeelu.Win.ListViewEx();
            this.resourceFilesToolStrip = new System.Windows.Forms.ToolStrip();
            this.parentResToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.viewsResToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.thumbnailViewResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconViewResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconViewResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsViewResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSaveAs = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelListView.SuspendLayout();
            this.resourceFilesToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEnter
            // 
            this.buttonEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnter.Location = new System.Drawing.Point(250, 298);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(62, 23);
            this.buttonEnter.TabIndex = 0;
            this.buttonEnter.Text = "保 存";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(318, 298);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(62, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelListView
            // 
            this.panelListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListView.Controls.Add(this.listViewFodel);
            this.panelListView.Controls.Add(this.resourceFilesToolStrip);
            this.panelListView.Location = new System.Drawing.Point(12, 25);
            this.panelListView.Name = "panelListView";
            this.panelListView.Size = new System.Drawing.Size(368, 178);
            this.panelListView.TabIndex = 2;
            // 
            // listViewFodel
            // 
            this.listViewFodel.CanLoad = false;
            this.listViewFodel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFodel.FolderName = "C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE";
            this.listViewFodel.Location = new System.Drawing.Point(0, 36);
            this.listViewFodel.Name = "listViewFodel";
            this.listViewFodel.Size = new System.Drawing.Size(368, 142);
            this.listViewFodel.TabIndex = 5;
            this.listViewFodel.ThumbBorderColor = System.Drawing.Color.Wheat;
            this.listViewFodel.ThumbNailSize = 95;
            this.listViewFodel.UseCompatibleStateImageBehavior = false;
            this.listViewFodel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewFodel_MouseDoubleClick);
            // 
            // resourceFilesToolStrip
            // 
            this.resourceFilesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.resourceFilesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parentResToolStripButton,
            this.toolStripSeparator1,
            this.viewsResToolStripDropDownButton});
            this.resourceFilesToolStrip.Location = new System.Drawing.Point(0, 0);
            this.resourceFilesToolStrip.Name = "resourceFilesToolStrip";
            this.resourceFilesToolStrip.Size = new System.Drawing.Size(368, 36);
            this.resourceFilesToolStrip.TabIndex = 4;
            this.resourceFilesToolStrip.Text = "toolStrip1";
            // 
            // parentResToolStripButton
            // 
            this.parentResToolStripButton.Enabled = false;
            this.parentResToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("parentResToolStripButton.Image")));
            this.parentResToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.parentResToolStripButton.Name = "parentResToolStripButton";
            this.parentResToolStripButton.Size = new System.Drawing.Size(82, 33);
            this.parentResToolStripButton.Text = "R:Parent Level";
            this.parentResToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.parentResToolStripButton.Click += new System.EventHandler(this.parentResToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // viewsResToolStripDropDownButton
            // 
            this.viewsResToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thumbnailViewResToolStripMenuItem,
            this.largeIconViewResToolStripMenuItem,
            this.smallIconViewResToolStripMenuItem,
            this.listViewResToolStripMenuItem,
            this.detailsViewResToolStripMenuItem});
            this.viewsResToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("viewsResToolStripDropDownButton.Image")));
            this.viewsResToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewsResToolStripDropDownButton.Name = "viewsResToolStripDropDownButton";
            this.viewsResToolStripDropDownButton.Size = new System.Drawing.Size(58, 33);
            this.viewsResToolStripDropDownButton.Text = "R:Views";
            this.viewsResToolStripDropDownButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // thumbnailViewResToolStripMenuItem
            // 
            this.thumbnailViewResToolStripMenuItem.Checked = true;
            this.thumbnailViewResToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.thumbnailViewResToolStripMenuItem.Name = "thumbnailViewResToolStripMenuItem";
            this.thumbnailViewResToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.thumbnailViewResToolStripMenuItem.Text = "Thumbnail View";
            this.thumbnailViewResToolStripMenuItem.Click += new System.EventHandler(this.thumbnailViewResToolStripMenuItem_Click);
            // 
            // largeIconViewResToolStripMenuItem
            // 
            this.largeIconViewResToolStripMenuItem.Name = "largeIconViewResToolStripMenuItem";
            this.largeIconViewResToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.largeIconViewResToolStripMenuItem.Text = "LargeIcon View";
            this.largeIconViewResToolStripMenuItem.Click += new System.EventHandler(this.thumbnailViewResToolStripMenuItem_Click);
            // 
            // smallIconViewResToolStripMenuItem
            // 
            this.smallIconViewResToolStripMenuItem.Name = "smallIconViewResToolStripMenuItem";
            this.smallIconViewResToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.smallIconViewResToolStripMenuItem.Text = "SmallIcon View";
            this.smallIconViewResToolStripMenuItem.Click += new System.EventHandler(this.thumbnailViewResToolStripMenuItem_Click);
            // 
            // listViewResToolStripMenuItem
            // 
            this.listViewResToolStripMenuItem.Name = "listViewResToolStripMenuItem";
            this.listViewResToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.listViewResToolStripMenuItem.Text = "List View";
            this.listViewResToolStripMenuItem.Click += new System.EventHandler(this.thumbnailViewResToolStripMenuItem_Click);
            // 
            // detailsViewResToolStripMenuItem
            // 
            this.detailsViewResToolStripMenuItem.Name = "detailsViewResToolStripMenuItem";
            this.detailsViewResToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.detailsViewResToolStripMenuItem.Text = "Details View";
            this.detailsViewResToolStripMenuItem.Click += new System.EventHandler(this.thumbnailViewResToolStripMenuItem_Click);
            // 
            // labelSaveAs
            // 
            this.labelSaveAs.AutoSize = true;
            this.labelSaveAs.Location = new System.Drawing.Point(12, 9);
            this.labelSaveAs.Name = "labelSaveAs";
            this.labelSaveAs.Size = new System.Drawing.Size(55, 13);
            this.labelSaveAs.TabIndex = 3;
            this.labelSaveAs.Text = "保存到：";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(250, 225);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(121, 21);
            this.textBoxName.TabIndex = 4;
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "JPG",
            "PNG",
            "GIF",
            "BMP"});
            this.comboBoxType.Location = new System.Drawing.Point(250, 265);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(122, 21);
            this.comboBoxType.TabIndex = 5;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(247, 209);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(67, 13);
            this.labelName.TabIndex = 6;
            this.labelName.Text = "文件名称：";
            // 
            // labelType
            // 
            this.labelType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(247, 249);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(67, 13);
            this.labelType.TabIndex = 7;
            this.labelType.Text = "文件类型：";
            // 
            // panelPreview
            // 
            this.panelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreview.BackColor = System.Drawing.Color.White;
            this.panelPreview.Location = new System.Drawing.Point(70, 11);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(86, 86);
            this.panelPreview.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panelPreview);
            this.panel1.Location = new System.Drawing.Point(12, 209);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 112);
            this.panel1.TabIndex = 9;
            // 
            // SaveResourceImageForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(384, 333);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelSaveAs);
            this.Controls.Add(this.panelListView);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonEnter);
            this.MaximizeBox = false;
            this.Name = "SaveResourceImageForm";
            this.Text = "SaveResourceImageForm";
            this.panelListView.ResumeLayout(false);
            this.panelListView.PerformLayout();
            this.resourceFilesToolStrip.ResumeLayout(false);
            this.resourceFilesToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelListView;
        private System.Windows.Forms.Label labelSaveAs;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.ToolStrip resourceFilesToolStrip;
        private System.Windows.Forms.ToolStripButton parentResToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton viewsResToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem thumbnailViewResToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeIconViewResToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallIconViewResToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listViewResToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsViewResToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private Jeelu.Win.ListViewEx listViewFodel;
    }
}