using System.Windows.Forms;
using System.Drawing;
using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class PagePropertyForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblContentBrief = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.txtDesignSummary = new System.Windows.Forms.TextBox();
            this.lblDesignSummary = new System.Windows.Forms.Label();
            this.modifyByComboBox = new System.Windows.Forms.ComboBox();
            this.lblModifyBy = new System.Windows.Forms.Label();
            this.authorByNameCombo = new System.Windows.Forms.ComboBox();
            this.creatDTPicker = new System.Windows.Forms.DateTimePicker();
            this.lblCreatorAlias = new System.Windows.Forms.Label();
            this.lblCreateTime = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitleAlias = new System.Windows.Forms.Label();
            this.txtTitleAlias = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtContentTag = new System.Windows.Forms.TextBox();
            this.contentSourceComboBox = new System.Windows.Forms.ComboBox();
            this.btnTrackback = new System.Windows.Forms.Button();
            this.singleLine1 = new Jeelu.Win.SingleLine();
            this.txtBeginPubTime = new System.Windows.Forms.TextBox();
            this.lblEndPub = new System.Windows.Forms.Label();
            this.lblBeginPub = new System.Windows.Forms.Label();
            this.contPubEndDTPicker = new System.Windows.Forms.DateTimePicker();
            this.isAlwaysPubCheckBox = new System.Windows.Forms.CheckBox();
            this.lblContentSource = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(371, 442);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(285, 442);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblContentBrief);
            this.groupBox1.Controls.Add(this.txtSummary);
            this.groupBox1.Controls.Add(this.txtDesignSummary);
            this.groupBox1.Controls.Add(this.lblDesignSummary);
            this.groupBox1.Controls.Add(this.modifyByComboBox);
            this.groupBox1.Controls.Add(this.lblModifyBy);
            this.groupBox1.Controls.Add(this.authorByNameCombo);
            this.groupBox1.Controls.Add(this.creatDTPicker);
            this.groupBox1.Controls.Add(this.lblCreatorAlias);
            this.groupBox1.Controls.Add(this.lblCreateTime);
            this.groupBox1.Controls.Add(this.lblTitle);
            this.groupBox1.Controls.Add(this.lblTitleAlias);
            this.groupBox1.Controls.Add(this.txtTitleAlias);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Location = new System.Drawing.Point(18, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 297);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BaseInfo";
            // 
            // lblContentBrief
            // 
            this.lblContentBrief.AutoSize = true;
            this.lblContentBrief.Location = new System.Drawing.Point(24, 181);
            this.lblContentBrief.Name = "lblContentBrief";
            this.lblContentBrief.Size = new System.Drawing.Size(29, 13);
            this.lblContentBrief.TabIndex = 8;
            this.lblContentBrief.Text = "brief";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(27, 197);
            this.txtSummary.MaxLength = 512;
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSummary.Size = new System.Drawing.Size(384, 88);
            this.txtSummary.TabIndex = 0;
            // 
            // txtDesignSummary
            // 
            this.txtDesignSummary.Location = new System.Drawing.Point(27, 136);
            this.txtDesignSummary.Multiline = true;
            this.txtDesignSummary.Name = "txtDesignSummary";
            this.txtDesignSummary.Size = new System.Drawing.Size(384, 42);
            this.txtDesignSummary.TabIndex = 6;
            // 
            // lblDesignSummary
            // 
            this.lblDesignSummary.AutoSize = true;
            this.lblDesignSummary.Location = new System.Drawing.Point(24, 120);
            this.lblDesignSummary.Name = "lblDesignSummary";
            this.lblDesignSummary.Size = new System.Drawing.Size(93, 13);
            this.lblDesignSummary.TabIndex = 7;
            this.lblDesignSummary.Text = "lblDesignSummary";
            // 
            // modifyByComboBox
            // 
            this.modifyByComboBox.FormattingEnabled = true;
            this.modifyByComboBox.Location = new System.Drawing.Point(286, 93);
            this.modifyByComboBox.Name = "modifyByComboBox";
            this.modifyByComboBox.Size = new System.Drawing.Size(125, 21);
            this.modifyByComboBox.TabIndex = 4;
            // 
            // lblModifyBy
            // 
            this.lblModifyBy.AutoSize = true;
            this.lblModifyBy.Location = new System.Drawing.Point(213, 96);
            this.lblModifyBy.Name = "lblModifyBy";
            this.lblModifyBy.Size = new System.Drawing.Size(61, 13);
            this.lblModifyBy.TabIndex = 5;
            this.lblModifyBy.Text = "lblModifyBy";
            // 
            // authorByNameCombo
            // 
            this.authorByNameCombo.Location = new System.Drawing.Point(85, 93);
            this.authorByNameCombo.Name = "authorByNameCombo";
            this.authorByNameCombo.Size = new System.Drawing.Size(122, 21);
            this.authorByNameCombo.TabIndex = 3;
            // 
            // creatDTPicker
            // 
            this.creatDTPicker.CustomFormat = "yyyy\'-\'MM\'-\'dd HH\':\'mm\':\'ss";
            this.creatDTPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.creatDTPicker.Location = new System.Drawing.Point(85, 69);
            this.creatDTPicker.Name = "creatDTPicker";
            this.creatDTPicker.Size = new System.Drawing.Size(139, 21);
            this.creatDTPicker.TabIndex = 2;
            // 
            // lblCreatorAlias
            // 
            this.lblCreatorAlias.Location = new System.Drawing.Point(24, 96);
            this.lblCreatorAlias.Name = "lblCreatorAlias";
            this.lblCreatorAlias.Size = new System.Drawing.Size(59, 13);
            this.lblCreatorAlias.TabIndex = 1;
            this.lblCreatorAlias.Text = "lblCreatorAlias";
            // 
            // lblCreateTime
            // 
            this.lblCreateTime.Location = new System.Drawing.Point(24, 73);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new System.Drawing.Size(59, 13);
            this.lblCreateTime.TabIndex = 1;
            this.lblCreateTime.Text = "lblCreateTime";
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(24, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(59, 13);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "lblTitle";
            // 
            // lblTitleAlias
            // 
            this.lblTitleAlias.Location = new System.Drawing.Point(24, 48);
            this.lblTitleAlias.Name = "lblTitleAlias";
            this.lblTitleAlias.Size = new System.Drawing.Size(59, 13);
            this.lblTitleAlias.TabIndex = 1;
            this.lblTitleAlias.Text = "lblTitleAlias";
            // 
            // txtTitleAlias
            // 
            this.txtTitleAlias.Location = new System.Drawing.Point(85, 45);
            this.txtTitleAlias.MaxLength = 123;
            this.txtTitleAlias.Name = "txtTitleAlias";
            this.txtTitleAlias.Size = new System.Drawing.Size(326, 21);
            this.txtTitleAlias.TabIndex = 1;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(85, 21);
            this.txtTitle.MaxLength = 456;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(326, 21);
            this.txtTitle.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtContentTag);
            this.groupBox2.Location = new System.Drawing.Point(18, 316);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(438, 48);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "key";
            // 
            // txtContentTag
            // 
            this.txtContentTag.Location = new System.Drawing.Point(27, 17);
            this.txtContentTag.Name = "txtContentTag";
            this.txtContentTag.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContentTag.Size = new System.Drawing.Size(384, 21);
            this.txtContentTag.TabIndex = 24;
            // 
            // contentSourceComboBox
            // 
            this.contentSourceComboBox.FormattingEnabled = true;
            this.contentSourceComboBox.Location = new System.Drawing.Point(84, 399);
            this.contentSourceComboBox.Name = "contentSourceComboBox";
            this.contentSourceComboBox.Size = new System.Drawing.Size(263, 21);
            this.contentSourceComboBox.TabIndex = 21;
            // 
            // btnTrackback
            // 
            this.btnTrackback.Location = new System.Drawing.Point(353, 397);
            this.btnTrackback.Name = "btnTrackback";
            this.btnTrackback.Size = new System.Drawing.Size(76, 23);
            this.btnTrackback.TabIndex = 22;
            this.btnTrackback.Text = "btnTrackback";
            this.btnTrackback.UseVisualStyleBackColor = true;
            // 
            // singleLine1
            // 
            this.singleLine1.Location = new System.Drawing.Point(18, 389);
            this.singleLine1.Name = "singleLine1";
            this.singleLine1.Size = new System.Drawing.Size(438, 1);
            this.singleLine1.TabIndex = 24;
            this.singleLine1.TabStop = false;
            this.singleLine1.Text = "SingleLine1";
            // 
            // txtBeginPubTime
            // 
            this.txtBeginPubTime.Location = new System.Drawing.Point(84, 534);
            this.txtBeginPubTime.Name = "txtBeginPubTime";
            this.txtBeginPubTime.ReadOnly = true;
            this.txtBeginPubTime.Size = new System.Drawing.Size(171, 21);
            this.txtBeginPubTime.TabIndex = 32;
            this.txtBeginPubTime.Visible = false;
            // 
            // lblEndPub
            // 
            this.lblEndPub.Location = new System.Drawing.Point(19, 561);
            this.lblEndPub.Name = "lblEndPub";
            this.lblEndPub.Size = new System.Drawing.Size(59, 13);
            this.lblEndPub.TabIndex = 31;
            this.lblEndPub.Text = "lblEndPub";
            this.lblEndPub.Visible = false;
            // 
            // lblBeginPub
            // 
            this.lblBeginPub.Location = new System.Drawing.Point(19, 537);
            this.lblBeginPub.Name = "lblBeginPub";
            this.lblBeginPub.Size = new System.Drawing.Size(59, 13);
            this.lblBeginPub.TabIndex = 30;
            this.lblBeginPub.Text = "lblBeginPub";
            this.lblBeginPub.Visible = false;
            // 
            // contPubEndDTPicker
            // 
            this.contPubEndDTPicker.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.contPubEndDTPicker.CustomFormat = "yyyy\'-\'MM\'-\'dd HH\':\'mm\':\'ss";
            this.contPubEndDTPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.contPubEndDTPicker.Location = new System.Drawing.Point(84, 557);
            this.contPubEndDTPicker.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.contPubEndDTPicker.Name = "contPubEndDTPicker";
            this.contPubEndDTPicker.Size = new System.Drawing.Size(171, 21);
            this.contPubEndDTPicker.TabIndex = 29;
            this.contPubEndDTPicker.Value = new System.DateTime(2007, 9, 5, 0, 0, 0, 0);
            this.contPubEndDTPicker.Visible = false;
            // 
            // isAlwaysPubCheckBox
            // 
            this.isAlwaysPubCheckBox.Location = new System.Drawing.Point(22, 512);
            this.isAlwaysPubCheckBox.Name = "isAlwaysPubCheckBox";
            this.isAlwaysPubCheckBox.Size = new System.Drawing.Size(165, 18);
            this.isAlwaysPubCheckBox.TabIndex = 28;
            this.isAlwaysPubCheckBox.Text = "isAlwaysPubCheckBox";
            this.isAlwaysPubCheckBox.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.isAlwaysPubCheckBox.Visible = false;
            // 
            // lblContentSource
            // 
            this.lblContentSource.AutoSize = true;
            this.lblContentSource.Location = new System.Drawing.Point(18, 402);
            this.lblContentSource.Name = "lblContentSource";
            this.lblContentSource.Size = new System.Drawing.Size(89, 13);
            this.lblContentSource.TabIndex = 23;
            this.lblContentSource.Text = "lblContentSource";
            // 
            // PagePropertyForm
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(474, 475);
            this.Controls.Add(this.txtBeginPubTime);
            this.Controls.Add(this.lblEndPub);
            this.Controls.Add(this.lblBeginPub);
            this.Controls.Add(this.contPubEndDTPicker);
            this.Controls.Add(this.isAlwaysPubCheckBox);
            this.Controls.Add(this.contentSourceComboBox);
            this.Controls.Add(this.btnTrackback);
            this.Controls.Add(this.lblContentSource);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.singleLine1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "PagePropertyForm";
            this.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.Text = "ContentPropertyForm";
            this.Load += new System.EventHandler(this.ContentPropertyForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCancel;
        private Button btnOK;
        private GroupBox groupBox1;
        private TextBox txtDesignSummary;
        private Label lblDesignSummary;
        private ComboBox modifyByComboBox;
        private Label lblModifyBy;
        private ComboBox authorByNameCombo;
        private Label lblCreatorAlias;
        private Label lblCreateTime;
        private Label lblTitle;
        private Label lblTitleAlias;
        private TextBox txtTitleAlias;
        private TextBox txtTitle;
        private GroupBox groupBox2;
        private TextBox txtSummary;
        private ComboBox contentSourceComboBox;
        private Button btnTrackback;
        private TextBox txtContentTag;
        private DateTimePicker creatDTPicker;
        private SingleLine singleLine1;
        private TextBox txtBeginPubTime;
        private Label lblEndPub;
        private Label lblBeginPub;
        private DateTimePicker contPubEndDTPicker;
        private CheckBox isAlwaysPubCheckBox;
        private Label lblContentBrief;
        private Label lblContentSource;
    }
}