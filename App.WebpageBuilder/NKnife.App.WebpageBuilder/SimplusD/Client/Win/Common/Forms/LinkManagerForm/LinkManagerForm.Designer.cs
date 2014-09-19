namespace Jeelu.SimplusD.Client.Win
{
    partial class LinkManagerForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.emailTabPage = new System.Windows.Forms.TabPage();
            this.emailGroupBox = new System.Windows.Forms.GroupBox();
            this.emailTextTextBox = new System.Windows.Forms.TextBox();
            this.emailSubjectTextBox = new System.Windows.Forms.TextBox();
            this.emailSubjectLabel = new System.Windows.Forms.Label();
            this.emailTextLabel = new System.Windows.Forms.Label();
            this.internelTabPage = new System.Windows.Forms.TabPage();
            this.linkClearButton = new System.Windows.Forms.Button();
            this.internetLinkOptionGroupBox = new System.Windows.Forms.GroupBox();
            this.tabKeyIndexTextBox = new System.Windows.Forms.TextBox();
            this.tabKeyIndexLabel = new System.Windows.Forms.Label();
            this.accesskeyTextBox = new System.Windows.Forms.TextBox();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.accesskeyLabel = new System.Windows.Forms.Label();
            this.linkTitleTextBox = new System.Windows.Forms.TextBox();
            this.linkTitleLabel = new System.Windows.Forms.Label();
            this.targetLabel = new System.Windows.Forms.Label();
            this.internetLinkGroupBox = new System.Windows.Forms.GroupBox();
            this.openLinkResButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkTextTextBox = new System.Windows.Forms.TextBox();
            this.linkTextLabel = new System.Windows.Forms.Label();
            this.bookmarkComboBox = new System.Windows.Forms.ComboBox();
            this.bookmarkLabel = new System.Windows.Forms.Label();
            this.openLinkPageButton = new System.Windows.Forms.Button();
            this.linkAddressTextBox = new System.Windows.Forms.TextBox();
            this.linkAddressLabel = new System.Windows.Forms.Label();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.emailTabPage.SuspendLayout();
            this.emailGroupBox.SuspendLayout();
            this.internelTabPage.SuspendLayout();
            this.internetLinkOptionGroupBox.SuspendLayout();
            this.internetLinkGroupBox.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(394, 383);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 28);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "R:okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(490, 383);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 28);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "R:cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // emailTabPage
            // 
            this.emailTabPage.Controls.Add(this.emailGroupBox);
            this.emailTabPage.Location = new System.Drawing.Point(4, 22);
            this.emailTabPage.Name = "emailTabPage";
            this.emailTabPage.Padding = new System.Windows.Forms.Padding(15, 20, 15, 3);
            this.emailTabPage.Size = new System.Drawing.Size(560, 339);
            this.emailTabPage.TabIndex = 3;
            this.emailTabPage.Text = "R:emailTabPage";
            this.emailTabPage.UseVisualStyleBackColor = true;
            // 
            // emailGroupBox
            // 
            this.emailGroupBox.Controls.Add(this.emailTextTextBox);
            this.emailGroupBox.Controls.Add(this.emailSubjectTextBox);
            this.emailGroupBox.Controls.Add(this.emailSubjectLabel);
            this.emailGroupBox.Controls.Add(this.emailTextLabel);
            this.emailGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.emailGroupBox.Location = new System.Drawing.Point(15, 20);
            this.emailGroupBox.Name = "emailGroupBox";
            this.emailGroupBox.Size = new System.Drawing.Size(530, 120);
            this.emailGroupBox.TabIndex = 1;
            this.emailGroupBox.TabStop = false;
            this.emailGroupBox.Text = "R:emailGroupBox";
            // 
            // emailTextTextBox
            // 
            this.emailTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.emailTextTextBox.Location = new System.Drawing.Point(71, 25);
            this.emailTextTextBox.Name = "emailTextTextBox";
            this.emailTextTextBox.Size = new System.Drawing.Size(431, 21);
            this.emailTextTextBox.TabIndex = 2;
            // 
            // emailSubjectTextBox
            // 
            this.emailSubjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.emailSubjectTextBox.Location = new System.Drawing.Point(71, 52);
            this.emailSubjectTextBox.Multiline = true;
            this.emailSubjectTextBox.Name = "emailSubjectTextBox";
            this.emailSubjectTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.emailSubjectTextBox.Size = new System.Drawing.Size(431, 51);
            this.emailSubjectTextBox.TabIndex = 3;
            // 
            // emailSubjectLabel
            // 
            this.emailSubjectLabel.AutoSize = true;
            this.emailSubjectLabel.Location = new System.Drawing.Point(18, 52);
            this.emailSubjectLabel.Name = "emailSubjectLabel";
            this.emailSubjectLabel.Size = new System.Drawing.Size(79, 13);
            this.emailSubjectLabel.TabIndex = 2;
            this.emailSubjectLabel.Text = "R:SubjectLabel";
            // 
            // emailTextLabel
            // 
            this.emailTextLabel.AutoSize = true;
            this.emailTextLabel.Location = new System.Drawing.Point(18, 28);
            this.emailTextLabel.Name = "emailTextLabel";
            this.emailTextLabel.Size = new System.Drawing.Size(78, 13);
            this.emailTextLabel.TabIndex = 0;
            this.emailTextLabel.Text = "R:textForEmail";
            // 
            // internelTabPage
            // 
            this.internelTabPage.Controls.Add(this.linkClearButton);
            this.internelTabPage.Controls.Add(this.internetLinkOptionGroupBox);
            this.internelTabPage.Controls.Add(this.internetLinkGroupBox);
            this.internelTabPage.Location = new System.Drawing.Point(4, 22);
            this.internelTabPage.Name = "internelTabPage";
            this.internelTabPage.Padding = new System.Windows.Forms.Padding(15, 20, 15, 3);
            this.internelTabPage.Size = new System.Drawing.Size(560, 339);
            this.internelTabPage.TabIndex = 2;
            this.internelTabPage.Text = "R:internelTabPage";
            this.internelTabPage.UseVisualStyleBackColor = true;
            // 
            // linkClearButton
            // 
            this.linkClearButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkClearButton.Location = new System.Drawing.Point(15, 287);
            this.linkClearButton.Name = "linkClearButton";
            this.linkClearButton.Size = new System.Drawing.Size(80, 24);
            this.linkClearButton.TabIndex = 0;
            this.linkClearButton.Text = "R:linkClear";
            // 
            // internetLinkOptionGroupBox
            // 
            this.internetLinkOptionGroupBox.Controls.Add(this.tabKeyIndexTextBox);
            this.internetLinkOptionGroupBox.Controls.Add(this.tabKeyIndexLabel);
            this.internetLinkOptionGroupBox.Controls.Add(this.accesskeyTextBox);
            this.internetLinkOptionGroupBox.Controls.Add(this.targetComboBox);
            this.internetLinkOptionGroupBox.Controls.Add(this.accesskeyLabel);
            this.internetLinkOptionGroupBox.Controls.Add(this.linkTitleTextBox);
            this.internetLinkOptionGroupBox.Controls.Add(this.linkTitleLabel);
            this.internetLinkOptionGroupBox.Controls.Add(this.targetLabel);
            this.internetLinkOptionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.internetLinkOptionGroupBox.Location = new System.Drawing.Point(15, 141);
            this.internetLinkOptionGroupBox.Name = "internetLinkOptionGroupBox";
            this.internetLinkOptionGroupBox.Size = new System.Drawing.Size(530, 140);
            this.internetLinkOptionGroupBox.TabIndex = 4;
            this.internetLinkOptionGroupBox.TabStop = false;
            this.internetLinkOptionGroupBox.Text = "R: internetLinkOptionGroupBox";
            // 
            // tabKeyIndexTextBox
            // 
            this.tabKeyIndexTextBox.Location = new System.Drawing.Point(258, 48);
            this.tabKeyIndexTextBox.Name = "tabKeyIndexTextBox";
            this.tabKeyIndexTextBox.Size = new System.Drawing.Size(100, 21);
            this.tabKeyIndexTextBox.TabIndex = 2;
            // 
            // tabKeyIndexLabel
            // 
            this.tabKeyIndexLabel.AutoSize = true;
            this.tabKeyIndexLabel.Location = new System.Drawing.Point(193, 51);
            this.tabKeyIndexLabel.Name = "tabKeyIndexLabel";
            this.tabKeyIndexLabel.Size = new System.Drawing.Size(105, 13);
            this.tabKeyIndexLabel.TabIndex = 22;
            this.tabKeyIndexLabel.Text = "R:tabKeyIndexLabel";
            // 
            // accesskeyTextBox
            // 
            this.accesskeyTextBox.Location = new System.Drawing.Point(76, 48);
            this.accesskeyTextBox.Name = "accesskeyTextBox";
            this.accesskeyTextBox.Size = new System.Drawing.Size(100, 21);
            this.accesskeyTextBox.TabIndex = 1;
            // 
            // targetComboBox
            // 
            this.targetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetComboBox.Items.AddRange(new object[] {
            "_blank",
            "_parent",
            "_self",
            "_top"});
            this.targetComboBox.Location = new System.Drawing.Point(76, 23);
            this.targetComboBox.Name = "targetComboBox";
            this.targetComboBox.Size = new System.Drawing.Size(198, 21);
            this.targetComboBox.TabIndex = 0;
            // 
            // accesskeyLabel
            // 
            this.accesskeyLabel.AutoSize = true;
            this.accesskeyLabel.Location = new System.Drawing.Point(11, 51);
            this.accesskeyLabel.Name = "accesskeyLabel";
            this.accesskeyLabel.Size = new System.Drawing.Size(92, 13);
            this.accesskeyLabel.TabIndex = 20;
            this.accesskeyLabel.Text = "R:accesskeyLabel";
            // 
            // linkTitleTextBox
            // 
            this.linkTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkTitleTextBox.Location = new System.Drawing.Point(76, 73);
            this.linkTitleTextBox.Multiline = true;
            this.linkTitleTextBox.Name = "linkTitleTextBox";
            this.linkTitleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.linkTitleTextBox.Size = new System.Drawing.Size(444, 51);
            this.linkTitleTextBox.TabIndex = 3;
            // 
            // linkTitleLabel
            // 
            this.linkTitleLabel.AutoSize = true;
            this.linkTitleLabel.Location = new System.Drawing.Point(11, 76);
            this.linkTitleLabel.Name = "linkTitleLabel";
            this.linkTitleLabel.Size = new System.Drawing.Size(78, 13);
            this.linkTitleLabel.TabIndex = 18;
            this.linkTitleLabel.Text = "R:linkTitleLabel";
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(11, 26);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(73, 13);
            this.targetLabel.TabIndex = 16;
            this.targetLabel.Text = "R:targetLabel";
            // 
            // internetLinkGroupBox
            // 
            this.internetLinkGroupBox.Controls.Add(this.openLinkResButton);
            this.internetLinkGroupBox.Controls.Add(this.textBox1);
            this.internetLinkGroupBox.Controls.Add(this.label1);
            this.internetLinkGroupBox.Controls.Add(this.linkTextTextBox);
            this.internetLinkGroupBox.Controls.Add(this.linkTextLabel);
            this.internetLinkGroupBox.Controls.Add(this.bookmarkComboBox);
            this.internetLinkGroupBox.Controls.Add(this.bookmarkLabel);
            this.internetLinkGroupBox.Controls.Add(this.openLinkPageButton);
            this.internetLinkGroupBox.Controls.Add(this.linkAddressTextBox);
            this.internetLinkGroupBox.Controls.Add(this.linkAddressLabel);
            this.internetLinkGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.internetLinkGroupBox.Location = new System.Drawing.Point(15, 20);
            this.internetLinkGroupBox.Name = "internetLinkGroupBox";
            this.internetLinkGroupBox.Size = new System.Drawing.Size(530, 121);
            this.internetLinkGroupBox.TabIndex = 5;
            this.internetLinkGroupBox.TabStop = false;
            this.internetLinkGroupBox.Text = "R: internetLinkGroupBox";
            // 
            // openLinkResButton
            // 
            this.openLinkResButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openLinkResButton.Location = new System.Drawing.Point(459, 17);
            this.openLinkResButton.Name = "openLinkResButton";
            this.openLinkResButton.Size = new System.Drawing.Size(28, 26);
            this.openLinkResButton.TabIndex = 18;
            this.openLinkResButton.Click += new System.EventHandler(this.openLinkResButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(76, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(441, 21);
            this.textBox1.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "R:linkAnalogView";
            // 
            // linkTextTextBox
            // 
            this.linkTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkTextTextBox.Location = new System.Drawing.Point(76, 83);
            this.linkTextTextBox.Name = "linkTextTextBox";
            this.linkTextTextBox.Size = new System.Drawing.Size(198, 21);
            this.linkTextTextBox.TabIndex = 0;
            // 
            // linkTextLabel
            // 
            this.linkTextLabel.AutoSize = true;
            this.linkTextLabel.Location = new System.Drawing.Point(11, 86);
            this.linkTextLabel.Name = "linkTextLabel";
            this.linkTextLabel.Size = new System.Drawing.Size(80, 13);
            this.linkTextLabel.TabIndex = 15;
            this.linkTextLabel.Text = "R:linkTextLabel";
            // 
            // bookmarkComboBox
            // 
            this.bookmarkComboBox.Location = new System.Drawing.Point(363, 83);
            this.bookmarkComboBox.Name = "bookmarkComboBox";
            this.bookmarkComboBox.Size = new System.Drawing.Size(154, 21);
            this.bookmarkComboBox.TabIndex = 3;
            // 
            // bookmarkLabel
            // 
            this.bookmarkLabel.AutoSize = true;
            this.bookmarkLabel.Location = new System.Drawing.Point(280, 86);
            this.bookmarkLabel.Name = "bookmarkLabel";
            this.bookmarkLabel.Size = new System.Drawing.Size(89, 13);
            this.bookmarkLabel.TabIndex = 14;
            this.bookmarkLabel.Text = "R:bookmarkLabel";
            // 
            // openLinkPageButton
            // 
            this.openLinkPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openLinkPageButton.Location = new System.Drawing.Point(489, 17);
            this.openLinkPageButton.Name = "openLinkPageButton";
            this.openLinkPageButton.Size = new System.Drawing.Size(28, 26);
            this.openLinkPageButton.TabIndex = 2;
            this.openLinkPageButton.Click += new System.EventHandler(this.openLinkResButton_Click);
            // 
            // linkAddressTextBox
            // 
            this.linkAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkAddressTextBox.Location = new System.Drawing.Point(76, 20);
            this.linkAddressTextBox.Name = "linkAddressTextBox";
            this.linkAddressTextBox.Size = new System.Drawing.Size(377, 21);
            this.linkAddressTextBox.TabIndex = 1;
            // 
            // linkAddressLabel
            // 
            this.linkAddressLabel.AutoSize = true;
            this.linkAddressLabel.Location = new System.Drawing.Point(11, 23);
            this.linkAddressLabel.Name = "linkAddressLabel";
            this.linkAddressLabel.Size = new System.Drawing.Size(72, 13);
            this.linkAddressLabel.TabIndex = 8;
            this.linkAddressLabel.Text = "R:linkAddress";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.internelTabPage);
            this.mainTabControl.Controls.Add(this.emailTabPage);
            this.mainTabControl.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(568, 365);
            this.mainTabControl.TabIndex = 5;
            // 
            // LinkManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 425);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.MinimumSize = new System.Drawing.Size(600, 460);
            this.Name = "LinkManagerForm";
            this.Text = "LinkManagerForm";
            this.emailTabPage.ResumeLayout(false);
            this.emailGroupBox.ResumeLayout(false);
            this.emailGroupBox.PerformLayout();
            this.internelTabPage.ResumeLayout(false);
            this.internetLinkOptionGroupBox.ResumeLayout(false);
            this.internetLinkOptionGroupBox.PerformLayout();
            this.internetLinkGroupBox.ResumeLayout(false);
            this.internetLinkGroupBox.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabPage emailTabPage;
        private System.Windows.Forms.GroupBox emailGroupBox;
        private System.Windows.Forms.TextBox emailTextTextBox;
        private System.Windows.Forms.TextBox emailSubjectTextBox;
        private System.Windows.Forms.Label emailSubjectLabel;
        private System.Windows.Forms.Label emailTextLabel;
        private System.Windows.Forms.TabPage internelTabPage;
        private System.Windows.Forms.Button linkClearButton;
        private System.Windows.Forms.GroupBox internetLinkOptionGroupBox;
        private System.Windows.Forms.TextBox tabKeyIndexTextBox;
        private System.Windows.Forms.Label tabKeyIndexLabel;
        private System.Windows.Forms.TextBox accesskeyTextBox;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.Label accesskeyLabel;
        private System.Windows.Forms.TextBox linkTitleTextBox;
        private System.Windows.Forms.Label linkTitleLabel;
        private System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.GroupBox internetLinkGroupBox;
        private System.Windows.Forms.TextBox linkTextTextBox;
        private System.Windows.Forms.Label linkTextLabel;
        private System.Windows.Forms.ComboBox bookmarkComboBox;
        private System.Windows.Forms.Label bookmarkLabel;
        private System.Windows.Forms.Button openLinkPageButton;
        private System.Windows.Forms.TextBox linkAddressTextBox;
        private System.Windows.Forms.Label linkAddressLabel;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openLinkResButton;
    }
}