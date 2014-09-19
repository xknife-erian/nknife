namespace Jeelu.SimplusD.Client.Win
{
    partial class AddListPartForm
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
            this.treeViewChannels = new System.Windows.Forms.TreeView();
            this.labelSelectChannels = new System.Windows.Forms.Label();
            this.checkBoxHomePage = new System.Windows.Forms.CheckBox();
            this.groupBoxSelectPageType = new System.Windows.Forms.GroupBox();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.comboBoxInviteBidding = new System.Windows.Forms.ComboBox();
            this.comboBoxHr = new System.Windows.Forms.ComboBox();
            this.comboBoxKnowledge = new System.Windows.Forms.ComboBox();
            this.comboBoxProduct = new System.Windows.Forms.ComboBox();
            this.comboBoxGenaral = new System.Windows.Forms.ComboBox();
            this.comboBoxHome = new System.Windows.Forms.ComboBox();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.labelProject = new System.Windows.Forms.Label();
            this.labelInviteBidding = new System.Windows.Forms.Label();
            this.labelHr = new System.Windows.Forms.Label();
            this.labelKnowledge = new System.Windows.Forms.Label();
            this.labelProduct = new System.Windows.Forms.Label();
            this.labelGeneral = new System.Windows.Forms.Label();
            this.labelHome = new System.Windows.Forms.Label();
            this.checkBoxGeneral = new System.Windows.Forms.CheckBox();
            this.checkBoxProject = new System.Windows.Forms.CheckBox();
            this.checkBoxHr = new System.Windows.Forms.CheckBox();
            this.checkBoxInviteBidding = new System.Windows.Forms.CheckBox();
            this.checkBoxProduct = new System.Windows.Forms.CheckBox();
            this.checkBoxKnowledge = new System.Windows.Forms.CheckBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelSort = new System.Windows.Forms.Label();
            this.cmbSortType = new System.Windows.Forms.ComboBox();
            this.textBoxKeyWords = new System.Windows.Forms.TextBox();
            this.numericUpDownAmount = new System.Windows.Forms.NumericUpDown();
            this.labelAmount = new System.Windows.Forms.Label();
            this.groupBoxSelectPageType.SuspendLayout();
            this.panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewChannels
            // 
            this.treeViewChannels.CheckBoxes = true;
            this.treeViewChannels.Location = new System.Drawing.Point(12, 25);
            this.treeViewChannels.Name = "treeViewChannels";
            this.treeViewChannels.Size = new System.Drawing.Size(179, 278);
            this.treeViewChannels.TabIndex = 0;
            this.treeViewChannels.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewChannels_AfterCheck);
            // 
            // labelSelectChannels
            // 
            this.labelSelectChannels.AutoSize = true;
            this.labelSelectChannels.Location = new System.Drawing.Point(12, 9);
            this.labelSelectChannels.Name = "labelSelectChannels";
            this.labelSelectChannels.Size = new System.Drawing.Size(107, 13);
            this.labelSelectChannels.TabIndex = 1;
            this.labelSelectChannels.Text = "选择目标频道/栏目";
            // 
            // checkBoxHomePage
            // 
            this.checkBoxHomePage.AutoSize = true;
            this.checkBoxHomePage.Location = new System.Drawing.Point(6, 28);
            this.checkBoxHomePage.Name = "checkBoxHomePage";
            this.checkBoxHomePage.Size = new System.Drawing.Size(86, 17);
            this.checkBoxHomePage.TabIndex = 2;
            this.checkBoxHomePage.Text = "主页型页面";
            this.checkBoxHomePage.UseVisualStyleBackColor = true;
            this.checkBoxHomePage.CheckedChanged += new System.EventHandler(this.checkBoxHomePage_CheckedChanged);
            // 
            // groupBoxSelectPageType
            // 
            this.groupBoxSelectPageType.Controls.Add(this.comboBoxProject);
            this.groupBoxSelectPageType.Controls.Add(this.comboBoxInviteBidding);
            this.groupBoxSelectPageType.Controls.Add(this.comboBoxHr);
            this.groupBoxSelectPageType.Controls.Add(this.comboBoxKnowledge);
            this.groupBoxSelectPageType.Controls.Add(this.comboBoxProduct);
            this.groupBoxSelectPageType.Controls.Add(this.comboBoxGenaral);
            this.groupBoxSelectPageType.Controls.Add(this.comboBoxHome);
            this.groupBoxSelectPageType.Controls.Add(this.panelPreview);
            this.groupBoxSelectPageType.Controls.Add(this.checkBoxGeneral);
            this.groupBoxSelectPageType.Controls.Add(this.checkBoxProject);
            this.groupBoxSelectPageType.Controls.Add(this.checkBoxHr);
            this.groupBoxSelectPageType.Controls.Add(this.checkBoxInviteBidding);
            this.groupBoxSelectPageType.Controls.Add(this.checkBoxProduct);
            this.groupBoxSelectPageType.Controls.Add(this.checkBoxKnowledge);
            this.groupBoxSelectPageType.Controls.Add(this.checkBoxHomePage);
            this.groupBoxSelectPageType.Location = new System.Drawing.Point(197, 9);
            this.groupBoxSelectPageType.Name = "groupBoxSelectPageType";
            this.groupBoxSelectPageType.Size = new System.Drawing.Size(373, 239);
            this.groupBoxSelectPageType.TabIndex = 3;
            this.groupBoxSelectPageType.TabStop = false;
            this.groupBoxSelectPageType.Text = "选择/编辑页面类型";
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProject.Enabled = false;
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(98, 200);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(75, 21);
            this.comboBoxProject.TabIndex = 26;
            this.comboBoxProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxProject_SelectedIndexChanged);
            // 
            // comboBoxInviteBidding
            // 
            this.comboBoxInviteBidding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInviteBidding.Enabled = false;
            this.comboBoxInviteBidding.FormattingEnabled = true;
            this.comboBoxInviteBidding.Location = new System.Drawing.Point(98, 171);
            this.comboBoxInviteBidding.Name = "comboBoxInviteBidding";
            this.comboBoxInviteBidding.Size = new System.Drawing.Size(75, 21);
            this.comboBoxInviteBidding.TabIndex = 25;
            this.comboBoxInviteBidding.SelectedIndexChanged += new System.EventHandler(this.comboBoxInviteBidding_SelectedIndexChanged);
            // 
            // comboBoxHr
            // 
            this.comboBoxHr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHr.Enabled = false;
            this.comboBoxHr.FormattingEnabled = true;
            this.comboBoxHr.Location = new System.Drawing.Point(98, 142);
            this.comboBoxHr.Name = "comboBoxHr";
            this.comboBoxHr.Size = new System.Drawing.Size(75, 21);
            this.comboBoxHr.TabIndex = 24;
            this.comboBoxHr.SelectedIndexChanged += new System.EventHandler(this.comboBoxHr_SelectedIndexChanged);
            // 
            // comboBoxKnowledge
            // 
            this.comboBoxKnowledge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKnowledge.Enabled = false;
            this.comboBoxKnowledge.FormattingEnabled = true;
            this.comboBoxKnowledge.Location = new System.Drawing.Point(98, 113);
            this.comboBoxKnowledge.Name = "comboBoxKnowledge";
            this.comboBoxKnowledge.Size = new System.Drawing.Size(75, 21);
            this.comboBoxKnowledge.TabIndex = 23;
            this.comboBoxKnowledge.SelectedIndexChanged += new System.EventHandler(this.comboBoxKnowledge_SelectedIndexChanged);
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProduct.Enabled = false;
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.Location = new System.Drawing.Point(98, 84);
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.Size = new System.Drawing.Size(75, 21);
            this.comboBoxProduct.TabIndex = 22;
            this.comboBoxProduct.SelectedIndexChanged += new System.EventHandler(this.comboBoxProduct_SelectedIndexChanged);
            // 
            // comboBoxGenaral
            // 
            this.comboBoxGenaral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGenaral.Enabled = false;
            this.comboBoxGenaral.FormattingEnabled = true;
            this.comboBoxGenaral.Location = new System.Drawing.Point(98, 55);
            this.comboBoxGenaral.Name = "comboBoxGenaral";
            this.comboBoxGenaral.Size = new System.Drawing.Size(75, 21);
            this.comboBoxGenaral.TabIndex = 21;
            this.comboBoxGenaral.SelectedIndexChanged += new System.EventHandler(this.comboBoxGenaral_SelectedIndexChanged);
            // 
            // comboBoxHome
            // 
            this.comboBoxHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHome.Enabled = false;
            this.comboBoxHome.FormattingEnabled = true;
            this.comboBoxHome.Location = new System.Drawing.Point(98, 26);
            this.comboBoxHome.Name = "comboBoxHome";
            this.comboBoxHome.Size = new System.Drawing.Size(75, 21);
            this.comboBoxHome.TabIndex = 20;
            this.comboBoxHome.SelectedIndexChanged += new System.EventHandler(this.comboBoxHome_SelectedIndexChanged);
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.Color.White;
            this.panelPreview.Controls.Add(this.labelProject);
            this.panelPreview.Controls.Add(this.labelInviteBidding);
            this.panelPreview.Controls.Add(this.labelHr);
            this.panelPreview.Controls.Add(this.labelKnowledge);
            this.panelPreview.Controls.Add(this.labelProduct);
            this.panelPreview.Controls.Add(this.labelGeneral);
            this.panelPreview.Controls.Add(this.labelHome);
            this.panelPreview.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelPreview.Location = new System.Drawing.Point(179, 16);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(186, 217);
            this.panelPreview.TabIndex = 19;
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Enabled = false;
            this.labelProject.ForeColor = System.Drawing.Color.Blue;
            this.labelProject.Location = new System.Drawing.Point(13, 187);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(103, 13);
            this.labelProject.TabIndex = 6;
            this.labelProject.Text = "频道   标题     摘要";
            // 
            // labelInviteBidding
            // 
            this.labelInviteBidding.AutoSize = true;
            this.labelInviteBidding.Enabled = false;
            this.labelInviteBidding.ForeColor = System.Drawing.Color.Blue;
            this.labelInviteBidding.Location = new System.Drawing.Point(13, 158);
            this.labelInviteBidding.Name = "labelInviteBidding";
            this.labelInviteBidding.Size = new System.Drawing.Size(103, 13);
            this.labelInviteBidding.TabIndex = 5;
            this.labelInviteBidding.Text = "频道   标题     摘要";
            // 
            // labelHr
            // 
            this.labelHr.AutoSize = true;
            this.labelHr.Enabled = false;
            this.labelHr.ForeColor = System.Drawing.Color.Blue;
            this.labelHr.Location = new System.Drawing.Point(13, 129);
            this.labelHr.Name = "labelHr";
            this.labelHr.Size = new System.Drawing.Size(103, 13);
            this.labelHr.TabIndex = 4;
            this.labelHr.Text = "频道   标题     摘要";
            // 
            // labelKnowledge
            // 
            this.labelKnowledge.AutoSize = true;
            this.labelKnowledge.Enabled = false;
            this.labelKnowledge.ForeColor = System.Drawing.Color.Blue;
            this.labelKnowledge.Location = new System.Drawing.Point(13, 99);
            this.labelKnowledge.Name = "labelKnowledge";
            this.labelKnowledge.Size = new System.Drawing.Size(103, 13);
            this.labelKnowledge.TabIndex = 3;
            this.labelKnowledge.Text = "频道   标题     摘要";
            // 
            // labelProduct
            // 
            this.labelProduct.AutoSize = true;
            this.labelProduct.Enabled = false;
            this.labelProduct.ForeColor = System.Drawing.Color.Blue;
            this.labelProduct.Location = new System.Drawing.Point(13, 71);
            this.labelProduct.Name = "labelProduct";
            this.labelProduct.Size = new System.Drawing.Size(103, 13);
            this.labelProduct.TabIndex = 2;
            this.labelProduct.Text = "频道   标题     摘要";
            // 
            // labelGeneral
            // 
            this.labelGeneral.AutoSize = true;
            this.labelGeneral.Enabled = false;
            this.labelGeneral.ForeColor = System.Drawing.Color.Blue;
            this.labelGeneral.Location = new System.Drawing.Point(13, 42);
            this.labelGeneral.Name = "labelGeneral";
            this.labelGeneral.Size = new System.Drawing.Size(103, 13);
            this.labelGeneral.TabIndex = 1;
            this.labelGeneral.Text = "频道   标题     摘要";
            // 
            // labelHome
            // 
            this.labelHome.AutoSize = true;
            this.labelHome.Enabled = false;
            this.labelHome.ForeColor = System.Drawing.Color.Blue;
            this.labelHome.Location = new System.Drawing.Point(13, 13);
            this.labelHome.Name = "labelHome";
            this.labelHome.Size = new System.Drawing.Size(103, 13);
            this.labelHome.TabIndex = 0;
            this.labelHome.Text = "频道   标题     摘要";
            // 
            // checkBoxGeneral
            // 
            this.checkBoxGeneral.AutoSize = true;
            this.checkBoxGeneral.Location = new System.Drawing.Point(6, 57);
            this.checkBoxGeneral.Name = "checkBoxGeneral";
            this.checkBoxGeneral.Size = new System.Drawing.Size(86, 17);
            this.checkBoxGeneral.TabIndex = 10;
            this.checkBoxGeneral.Text = "普通型页面";
            this.checkBoxGeneral.UseVisualStyleBackColor = true;
            this.checkBoxGeneral.CheckedChanged += new System.EventHandler(this.checkBoxGeneral_CheckedChanged);
            // 
            // checkBoxProject
            // 
            this.checkBoxProject.AutoSize = true;
            this.checkBoxProject.Location = new System.Drawing.Point(6, 202);
            this.checkBoxProject.Name = "checkBoxProject";
            this.checkBoxProject.Size = new System.Drawing.Size(86, 17);
            this.checkBoxProject.TabIndex = 5;
            this.checkBoxProject.Text = "项目型页面";
            this.checkBoxProject.UseVisualStyleBackColor = true;
            this.checkBoxProject.CheckedChanged += new System.EventHandler(this.checkBoxProject_CheckedChanged);
            // 
            // checkBoxHr
            // 
            this.checkBoxHr.AutoSize = true;
            this.checkBoxHr.Location = new System.Drawing.Point(6, 144);
            this.checkBoxHr.Name = "checkBoxHr";
            this.checkBoxHr.Size = new System.Drawing.Size(86, 17);
            this.checkBoxHr.TabIndex = 7;
            this.checkBoxHr.Text = "招聘型页面";
            this.checkBoxHr.UseVisualStyleBackColor = true;
            this.checkBoxHr.CheckedChanged += new System.EventHandler(this.checkBoxHr_CheckedChanged);
            // 
            // checkBoxInviteBidding
            // 
            this.checkBoxInviteBidding.AutoSize = true;
            this.checkBoxInviteBidding.Location = new System.Drawing.Point(6, 173);
            this.checkBoxInviteBidding.Name = "checkBoxInviteBidding";
            this.checkBoxInviteBidding.Size = new System.Drawing.Size(86, 17);
            this.checkBoxInviteBidding.TabIndex = 6;
            this.checkBoxInviteBidding.Text = "招标型页面";
            this.checkBoxInviteBidding.UseVisualStyleBackColor = true;
            this.checkBoxInviteBidding.CheckedChanged += new System.EventHandler(this.checkBoxInviteBidding_CheckedChanged);
            // 
            // checkBoxProduct
            // 
            this.checkBoxProduct.AutoSize = true;
            this.checkBoxProduct.Location = new System.Drawing.Point(6, 86);
            this.checkBoxProduct.Name = "checkBoxProduct";
            this.checkBoxProduct.Size = new System.Drawing.Size(86, 17);
            this.checkBoxProduct.TabIndex = 9;
            this.checkBoxProduct.Text = "产品型页面";
            this.checkBoxProduct.UseVisualStyleBackColor = true;
            this.checkBoxProduct.CheckedChanged += new System.EventHandler(this.checkBoxProduct_CheckedChanged);
            // 
            // checkBoxKnowledge
            // 
            this.checkBoxKnowledge.AutoSize = true;
            this.checkBoxKnowledge.Location = new System.Drawing.Point(6, 115);
            this.checkBoxKnowledge.Name = "checkBoxKnowledge";
            this.checkBoxKnowledge.Size = new System.Drawing.Size(86, 17);
            this.checkBoxKnowledge.TabIndex = 8;
            this.checkBoxKnowledge.Text = "知识型页面";
            this.checkBoxKnowledge.UseVisualStyleBackColor = true;
            this.checkBoxKnowledge.CheckedChanged += new System.EventHandler(this.checkBoxKnowledge_CheckedChanged);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnter.Location = new System.Drawing.Point(459, 308);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(52, 23);
            this.buttonEnter.TabIndex = 12;
            this.buttonEnter.Text = "确 定";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(517, 308);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(52, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelSort
            // 
            this.labelSort.AutoSize = true;
            this.labelSort.Location = new System.Drawing.Point(200, 257);
            this.labelSort.Name = "labelSort";
            this.labelSort.Size = new System.Drawing.Size(67, 13);
            this.labelSort.TabIndex = 14;
            this.labelSort.Text = "排序方式：";
            // 
            // cmbSortType
            // 
            this.cmbSortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortType.FormattingEnabled = true;
            this.cmbSortType.Location = new System.Drawing.Point(273, 254);
            this.cmbSortType.Name = "cmbSortType";
            this.cmbSortType.Size = new System.Drawing.Size(97, 21);
            this.cmbSortType.TabIndex = 15;
            this.cmbSortType.SelectedIndexChanged += new System.EventHandler(this.cmbSortType_SelectedIndexChanged);
            // 
            // textBoxKeyWords
            // 
            this.textBoxKeyWords.Enabled = false;
            this.textBoxKeyWords.Location = new System.Drawing.Point(400, 254);
            this.textBoxKeyWords.Name = "textBoxKeyWords";
            this.textBoxKeyWords.Size = new System.Drawing.Size(170, 21);
            this.textBoxKeyWords.TabIndex = 16;
            this.textBoxKeyWords.Leave += new System.EventHandler(this.textBoxKeyWords_Leave);
            // 
            // numericUpDownAmount
            // 
            this.numericUpDownAmount.Location = new System.Drawing.Point(273, 282);
            this.numericUpDownAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAmount.Name = "numericUpDownAmount";
            this.numericUpDownAmount.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownAmount.TabIndex = 17;
            this.numericUpDownAmount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Location = new System.Drawing.Point(200, 284);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(67, 13);
            this.labelAmount.TabIndex = 18;
            this.labelAmount.Text = "显示条数：";
            // 
            // AddListPartForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 337);
            this.Controls.Add(this.labelAmount);
            this.Controls.Add(this.numericUpDownAmount);
            this.Controls.Add(this.textBoxKeyWords);
            this.Controls.Add(this.cmbSortType);
            this.Controls.Add(this.labelSort);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.groupBoxSelectPageType);
            this.Controls.Add(this.labelSelectChannels);
            this.Controls.Add(this.treeViewChannels);
            this.Name = "AddListPartForm";
            this.Text = "AddListPartForm";
            this.groupBoxSelectPageType.ResumeLayout(false);
            this.groupBoxSelectPageType.PerformLayout();
            this.panelPreview.ResumeLayout(false);
            this.panelPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewChannels;
        private System.Windows.Forms.Label labelSelectChannels;
        private System.Windows.Forms.CheckBox checkBoxHomePage;
        private System.Windows.Forms.GroupBox groupBoxSelectPageType;
        private System.Windows.Forms.CheckBox checkBoxGeneral;
        private System.Windows.Forms.CheckBox checkBoxProduct;
        private System.Windows.Forms.CheckBox checkBoxKnowledge;
        private System.Windows.Forms.CheckBox checkBoxHr;
        private System.Windows.Forms.CheckBox checkBoxInviteBidding;
        private System.Windows.Forms.CheckBox checkBoxProject;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Label labelHome;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.Label labelInviteBidding;
        private System.Windows.Forms.Label labelHr;
        private System.Windows.Forms.Label labelKnowledge;
        private System.Windows.Forms.Label labelProduct;
        private System.Windows.Forms.Label labelGeneral;
        private System.Windows.Forms.Label labelSort;
        private System.Windows.Forms.ComboBox cmbSortType;
        private System.Windows.Forms.TextBox textBoxKeyWords;
        private System.Windows.Forms.ComboBox comboBoxHome;
        private System.Windows.Forms.ComboBox comboBoxGenaral;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.ComboBox comboBoxInviteBidding;
        private System.Windows.Forms.ComboBox comboBoxHr;
        private System.Windows.Forms.ComboBox comboBoxKnowledge;
        private System.Windows.Forms.ComboBox comboBoxProduct;
        private System.Windows.Forms.NumericUpDown numericUpDownAmount;
        private System.Windows.Forms.Label labelAmount;
    }
}