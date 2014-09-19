using System.Windows.Forms;
namespace Jeelu.Win
{
    partial class BaseMediaCodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseMediaCodeForm));
            this.ClearButton = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.mediaInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.tabTextBox = new System.Windows.Forms.TextBox();
            this.accessKeyTextBox = new System.Windows.Forms.TextBox();
            this.medioPathBtn = new System.Windows.Forms.Button();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.audioTitle = new System.Windows.Forms.Label();
            this.audioTab = new System.Windows.Forms.Label();
            this.audioAccessKey = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.mediaPathTextBox = new System.Windows.Forms.TextBox();
            this.mediaIDTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.IdLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.alignComboBox = new System.Windows.Forms.ComboBox();
            this.heightUintComboBox = new System.Windows.Forms.ComboBox();
            this.widthUintComboBox = new System.Windows.Forms.ComboBox();
            this.vspaceNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.hspaceNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.heightNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.widthNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.resizeButton = new System.Windows.Forms.Button();
            this.limitScaleCheckBox = new System.Windows.Forms.CheckBox();
            this.widthCheckBox = new System.Windows.Forms.CheckBox();
            this.heightCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.scaleComboBox = new System.Windows.Forms.ComboBox();
            this.qualityComboBox = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.autoPlayCheckBox = new System.Windows.Forms.CheckBox();
            this.loopCheckBox = new System.Windows.Forms.CheckBox();
            this.mediaInfoGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vspaceNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hspaceNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClearButton
            // 
            this.ClearButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ClearButton.Location = new System.Drawing.Point(27, 348);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(80, 24);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "清除";
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.okBtn.Location = new System.Drawing.Point(310, 348);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(80, 24);
            this.okBtn.TabIndex = 6;
            this.okBtn.Text = "确定";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelBtn.Location = new System.Drawing.Point(396, 348);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(80, 24);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "取消";
            // 
            // mediaInfoGroupBox
            // 
            this.mediaInfoGroupBox.Controls.Add(this.titleTextBox);
            this.mediaInfoGroupBox.Controls.Add(this.tabTextBox);
            this.mediaInfoGroupBox.Controls.Add(this.accessKeyTextBox);
            this.mediaInfoGroupBox.Controls.Add(this.medioPathBtn);
            this.mediaInfoGroupBox.Controls.Add(this.idTextBox);
            this.mediaInfoGroupBox.Controls.Add(this.audioTitle);
            this.mediaInfoGroupBox.Controls.Add(this.audioTab);
            this.mediaInfoGroupBox.Controls.Add(this.audioAccessKey);
            this.mediaInfoGroupBox.Controls.Add(this.Label2);
            this.mediaInfoGroupBox.Controls.Add(this.mediaPathTextBox);
            this.mediaInfoGroupBox.Controls.Add(this.mediaIDTextBox);
            this.mediaInfoGroupBox.Controls.Add(this.label8);
            this.mediaInfoGroupBox.Controls.Add(this.IdLabel);
            this.mediaInfoGroupBox.Location = new System.Drawing.Point(18, 12);
            this.mediaInfoGroupBox.Name = "mediaInfoGroupBox";
            this.mediaInfoGroupBox.Size = new System.Drawing.Size(458, 132);
            this.mediaInfoGroupBox.TabIndex = 8;
            this.mediaInfoGroupBox.TabStop = false;
            this.mediaInfoGroupBox.Text = "媒体信息";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(293, 75);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(141, 21);
            this.titleTextBox.TabIndex = 27;
            // 
            // tabTextBox
            // 
            this.tabTextBox.Location = new System.Drawing.Point(293, 101);
            this.tabTextBox.Name = "tabTextBox";
            this.tabTextBox.Size = new System.Drawing.Size(141, 21);
            this.tabTextBox.TabIndex = 29;
            // 
            // accessKeyTextBox
            // 
            this.accessKeyTextBox.Location = new System.Drawing.Point(71, 99);
            this.accessKeyTextBox.Name = "accessKeyTextBox";
            this.accessKeyTextBox.Size = new System.Drawing.Size(141, 21);
            this.accessKeyTextBox.TabIndex = 28;
            // 
            // medioPathBtn
            // 
            this.medioPathBtn.Image = ((System.Drawing.Image)(resources.GetObject("medioPathBtn.Image")));
            this.medioPathBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.medioPathBtn.Location = new System.Drawing.Point(409, 19);
            this.medioPathBtn.Name = "medioPathBtn";
            this.medioPathBtn.Size = new System.Drawing.Size(24, 23);
            this.medioPathBtn.TabIndex = 16;
            this.medioPathBtn.Click += new System.EventHandler(this.medioPathBtn_Click);
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(71, 74);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(141, 21);
            this.idTextBox.TabIndex = 15;
            // 
            // audioTitle
            // 
            this.audioTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.audioTitle.Location = new System.Drawing.Point(254, 87);
            this.audioTitle.Name = "audioTitle";
            this.audioTitle.Size = new System.Drawing.Size(39, 13);
            this.audioTitle.TabIndex = 14;
            this.audioTitle.Text = "标题:";
            // 
            // audioTab
            // 
            this.audioTab.Location = new System.Drawing.Point(223, 104);
            this.audioTab.Name = "audioTab";
            this.audioTab.Size = new System.Drawing.Size(70, 13);
            this.audioTab.TabIndex = 14;
            this.audioTab.Text = "Tab键索引:";
            // 
            // audioAccessKey
            // 
            this.audioAccessKey.Location = new System.Drawing.Point(16, 103);
            this.audioAccessKey.Name = "audioAccessKey";
            this.audioAccessKey.Size = new System.Drawing.Size(59, 13);
            this.audioAccessKey.TabIndex = 14;
            this.audioAccessKey.Text = "访问键:";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(16, 74);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(59, 13);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "ClassID:";
            // 
            // mediaPathTextBox
            // 
            this.mediaPathTextBox.Location = new System.Drawing.Point(70, 48);
            this.mediaPathTextBox.Name = "mediaPathTextBox";
            this.mediaPathTextBox.Size = new System.Drawing.Size(363, 21);
            this.mediaPathTextBox.TabIndex = 9;
            // 
            // mediaIDTextBox
            // 
            this.mediaIDTextBox.Location = new System.Drawing.Point(70, 21);
            this.mediaIDTextBox.Name = "mediaIDTextBox";
            this.mediaIDTextBox.Size = new System.Drawing.Size(333, 21);
            this.mediaIDTextBox.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.Location = new System.Drawing.Point(26, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "地址:";
            // 
            // IdLabel
            // 
            this.IdLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.IdLabel.Location = new System.Drawing.Point(26, 25);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(38, 13);
            this.IdLabel.TabIndex = 8;
            this.IdLabel.Text = "地址:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.alignComboBox);
            this.groupBox2.Controls.Add(this.heightUintComboBox);
            this.groupBox2.Controls.Add(this.widthUintComboBox);
            this.groupBox2.Controls.Add(this.vspaceNumUpDown);
            this.groupBox2.Controls.Add(this.hspaceNumUpDown);
            this.groupBox2.Controls.Add(this.heightNumUpDown);
            this.groupBox2.Controls.Add(this.widthNumUpDown);
            this.groupBox2.Controls.Add(this.Label5);
            this.groupBox2.Controls.Add(this.Label4);
            this.groupBox2.Controls.Add(this.Label3);
            this.groupBox2.Controls.Add(this.resizeButton);
            this.groupBox2.Controls.Add(this.limitScaleCheckBox);
            this.groupBox2.Controls.Add(this.widthCheckBox);
            this.groupBox2.Controls.Add(this.heightCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(18, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 107);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " 大小与位置";
            // 
            // alignComboBox
            // 
            this.alignComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignComboBox.Location = new System.Drawing.Point(98, 71);
            this.alignComboBox.Name = "alignComboBox";
            this.alignComboBox.Size = new System.Drawing.Size(98, 21);
            this.alignComboBox.TabIndex = 26;
            // 
            // heightUintComboBox
            // 
            this.heightUintComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.heightUintComboBox.Items.AddRange(new object[] {
            "px",
            "%"});
            this.heightUintComboBox.Location = new System.Drawing.Point(195, 44);
            this.heightUintComboBox.Name = "heightUintComboBox";
            this.heightUintComboBox.Size = new System.Drawing.Size(54, 21);
            this.heightUintComboBox.TabIndex = 26;
            // 
            // widthUintComboBox
            // 
            this.widthUintComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widthUintComboBox.Items.AddRange(new object[] {
            "px",
            "%"});
            this.widthUintComboBox.Location = new System.Drawing.Point(194, 20);
            this.widthUintComboBox.Name = "widthUintComboBox";
            this.widthUintComboBox.Size = new System.Drawing.Size(54, 21);
            this.widthUintComboBox.TabIndex = 26;
            // 
            // vspaceNumUpDown
            // 
            this.vspaceNumUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vspaceNumUpDown.Location = new System.Drawing.Point(328, 43);
            this.vspaceNumUpDown.Name = "vspaceNumUpDown";
            this.vspaceNumUpDown.Size = new System.Drawing.Size(58, 21);
            this.vspaceNumUpDown.TabIndex = 26;
            // 
            // hspaceNumUpDown
            // 
            this.hspaceNumUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hspaceNumUpDown.Location = new System.Drawing.Point(328, 20);
            this.hspaceNumUpDown.Name = "hspaceNumUpDown";
            this.hspaceNumUpDown.Size = new System.Drawing.Size(58, 21);
            this.hspaceNumUpDown.TabIndex = 25;
            // 
            // heightNumUpDown
            // 
            this.heightNumUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.heightNumUpDown.Location = new System.Drawing.Point(133, 44);
            this.heightNumUpDown.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.heightNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightNumUpDown.Name = "heightNumUpDown";
            this.heightNumUpDown.Size = new System.Drawing.Size(55, 21);
            this.heightNumUpDown.TabIndex = 24;
            this.heightNumUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // widthNumUpDown
            // 
            this.widthNumUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.widthNumUpDown.Location = new System.Drawing.Point(133, 20);
            this.widthNumUpDown.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.widthNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthNumUpDown.Name = "widthNumUpDown";
            this.widthNumUpDown.Size = new System.Drawing.Size(56, 21);
            this.widthNumUpDown.TabIndex = 11;
            this.widthNumUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(54, 74);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(35, 13);
            this.Label5.TabIndex = 22;
            this.Label5.Text = "对齐:";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(263, 22);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(59, 13);
            this.Label4.TabIndex = 19;
            this.Label4.Text = "水平边距:";
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(263, 45);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(59, 13);
            this.Label3.TabIndex = 18;
            this.Label3.Text = "垂直边距:";
            // 
            // resizeButton
            // 
            this.resizeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.resizeButton.Location = new System.Drawing.Point(289, 71);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(97, 21);
            this.resizeButton.TabIndex = 17;
            this.resizeButton.Text = "恢复原始大小";
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            // 
            // limitScaleCheckBox
            // 
            this.limitScaleCheckBox.Enabled = false;
            this.limitScaleCheckBox.Location = new System.Drawing.Point(217, 74);
            this.limitScaleCheckBox.Name = "limitScaleCheckBox";
            this.limitScaleCheckBox.Size = new System.Drawing.Size(77, 18);
            this.limitScaleCheckBox.TabIndex = 16;
            this.limitScaleCheckBox.Text = "限制比例";
            // 
            // widthCheckBox
            // 
            this.widthCheckBox.Location = new System.Drawing.Point(57, 21);
            this.widthCheckBox.Name = "widthCheckBox";
            this.widthCheckBox.Size = new System.Drawing.Size(77, 18);
            this.widthCheckBox.TabIndex = 11;
            this.widthCheckBox.Text = "宽度(&W):";
            // 
            // heightCheckBox
            // 
            this.heightCheckBox.Location = new System.Drawing.Point(57, 44);
            this.heightCheckBox.Name = "heightCheckBox";
            this.heightCheckBox.Size = new System.Drawing.Size(77, 18);
            this.heightCheckBox.TabIndex = 10;
            this.heightCheckBox.Text = "高度(&H):";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.scaleComboBox);
            this.groupBox3.Controls.Add(this.qualityComboBox);
            this.groupBox3.Controls.Add(this.Label7);
            this.groupBox3.Controls.Add(this.Label6);
            this.groupBox3.Controls.Add(this.autoPlayCheckBox);
            this.groupBox3.Controls.Add(this.loopCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(18, 263);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(458, 68);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " 选项";
            // 
            // scaleComboBox
            // 
            this.scaleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scaleComboBox.Location = new System.Drawing.Point(199, 38);
            this.scaleComboBox.Name = "scaleComboBox";
            this.scaleComboBox.Size = new System.Drawing.Size(108, 21);
            this.scaleComboBox.TabIndex = 27;
            // 
            // qualityComboBox
            // 
            this.qualityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.qualityComboBox.Location = new System.Drawing.Point(199, 15);
            this.qualityComboBox.Name = "qualityComboBox";
            this.qualityComboBox.Size = new System.Drawing.Size(108, 21);
            this.qualityComboBox.TabIndex = 26;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(158, 41);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(35, 13);
            this.Label7.TabIndex = 21;
            this.Label7.Text = "比例:";
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(158, 18);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(35, 13);
            this.Label6.TabIndex = 20;
            this.Label6.Text = "品质:";
            // 
            // autoPlayCheckBox
            // 
            this.autoPlayCheckBox.Checked = true;
            this.autoPlayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoPlayCheckBox.Location = new System.Drawing.Point(57, 39);
            this.autoPlayCheckBox.Name = "autoPlayCheckBox";
            this.autoPlayCheckBox.Size = new System.Drawing.Size(77, 18);
            this.autoPlayCheckBox.TabIndex = 13;
            this.autoPlayCheckBox.Text = "自动播放";
            // 
            // loopCheckBox
            // 
            this.loopCheckBox.Checked = true;
            this.loopCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loopCheckBox.Location = new System.Drawing.Point(58, 16);
            this.loopCheckBox.Name = "loopCheckBox";
            this.loopCheckBox.Size = new System.Drawing.Size(77, 18);
            this.loopCheckBox.TabIndex = 12;
            this.loopCheckBox.Text = "循环";
            // 
            // BaseMediaCodeForm
            // 
            this.AcceptButton = this.okBtn;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(493, 384);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.mediaInfoGroupBox);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseMediaCodeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "新增媒体";
            this.Load += new System.EventHandler(this.BaseMediaCodeForm_Load);
            this.mediaInfoGroupBox.ResumeLayout(false);
            this.mediaInfoGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vspaceNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hspaceNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNumUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button ClearButton;
        private Button okBtn;
        private Button cancelBtn;
        private Label Label2;
        private TextBox mediaIDTextBox;
        private Label IdLabel;
        private TextBox idTextBox;
        private GroupBox groupBox2;
        private Label Label4;
        private Label Label3;
        private CheckBox widthCheckBox;
        private CheckBox heightCheckBox;
        private Label Label5;
        private GroupBox groupBox3;
        private CheckBox autoPlayCheckBox;
        private CheckBox loopCheckBox;
        private Label Label7;
        private Label Label6;
        private NumericUpDown vspaceNumUpDown;
        private NumericUpDown hspaceNumUpDown;
        private TextBox titleTextBox;
        private TextBox accessKeyTextBox;
        private TextBox tabTextBox;
        private ComboBox scaleComboBox;
        private Label audioTitle;
        private Label audioTab;
        private Label audioAccessKey;
        protected internal ComboBox qualityComboBox;
        protected internal ComboBox alignComboBox;
        private Button medioPathBtn;
        protected GroupBox mediaInfoGroupBox;
        protected Button resizeButton;
        protected CheckBox limitScaleCheckBox;
        protected NumericUpDown widthNumUpDown;
        protected NumericUpDown heightNumUpDown;
        protected ComboBox heightUintComboBox;
        protected ComboBox widthUintComboBox;
        private TextBox mediaPathTextBox;
        private Label label8;
    }
}